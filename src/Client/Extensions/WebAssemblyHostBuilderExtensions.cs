using Blazored.LocalStorage;
using EPharma.Client.Infrastructure.Authentication;
using EPharma.Client.Infrastructure.Managers;
using EPharma.Client.Infrastructure.Managers.ExtendedAttribute;
using EPharma.Client.Infrastructure.Managers.Preferences;
using EPharma.Client.Infrastructure.Managers.Settings.StaticVariable;
using EPharma.Domain.Entities.ExtendedAttributes;
using EPharma.Domain.Entities.Misc;
using EPharma.Shared.Constants.Permission;
using EPharma.Shared.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace EPharma.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "BlazorHero.API";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder
                .Services
                .AddLocalization(options =>
                {
                    options.ResourcesPath = "Resources";
                })
                .AddAuthorizationCore(options =>
                {
                    RegisterPermissionClaims(options);
                })
                .AddBlazoredLocalStorage()
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                    configuration.SnackbarConfiguration.ShowCloseIcon = false;
                })
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ClientPreferenceManager>()
                .AddScoped<HrStateProvider>()
                .AddScoped<AuthenticationStateProvider, HrStateProvider>()
                .AddManagers()
                .AddGlobalVariable()
                .AddExtendedAttributeManagers()
                .AddTransient<AuthenticationHeaderHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
            builder.Services.AddHttpClientInterceptor();
            return builder;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

        internal static IServiceCollection AddGlobalVariable(this IServiceCollection services)
        {
            Action<GlobalVariable> staticVariable = (async opt =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var staticVariableManager = serviceProvider.GetService<IStaticVariableManager>();
                var allStaticVariable = await staticVariableManager.GetAllAsync();

                var optProp = opt.GetType().GetProperties();
                foreach (var prop in optProp)
                {
                    prop.SetValue(opt, allStaticVariable.Data.Where(d => d.Name == prop.Name).Select(d => d.Value).FirstOrDefault());
                }
            });
            services.Configure(staticVariable);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<GlobalVariable>>().Value);
            return services;
        }

        public static IServiceCollection AddExtendedAttributeManagers(this IServiceCollection services)
        {
            //TODO - add managers with reflection!

            return services
                .AddTransient(typeof(IExtendedAttributeManager<int, int, Document, DocumentExtendedAttribute>), typeof(ExtendedAttributeManager<int, int, Document, DocumentExtendedAttribute>));
        }

        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
                }
            }
        }
    }
}