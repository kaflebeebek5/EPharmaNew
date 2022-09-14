using EPharma.Application.Interfaces.Services;
using EPharma.Application.Models.Chat;
using EPharma.Infrastructure.Models.Identity;
using EPharma.Domain.Contracts;
using EPharma.Domain.Entities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EPharma.Domain.Entities.ExtendedAttributes;
using EPharma.Domain.Entities.Misc;

namespace EPharma.Infrastructure.Contexts
{
    public class EPharmaContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public EPharmaContext(DbContextOptions<EPharmaContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<ChatHistory<HrUser>> ChatHistories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentExtendedAttribute> DocumentExtendedAttributes { get; set; }
        public DbSet<MasterTableSetup> MasterTableSetup { get; set; }
        public DbSet<StaticVariable> StaticVariable { get; set; }
        public DbSet<MenuList> MenuList { get; set; }
        public DbSet<MenuRole> MenuRole { get; set; }
        public DbSet<MaritalStatus> MaritalStatus { get; set; }
        public DbSet<BloodGroup> BloodGroup { get; set; }
        public DbSet<Nationality> Nationality { get; set; }
        public DbSet<Level> Level { get; set; }
        public DbSet<SubDepartment> SubDepartment { get; set; }
        public DbSet<SubUnit> SubUnit { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<LocalBodiesType> LocalBodiesType { get; set; }
        public DbSet<LocalBodies> LocalBodies { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<BranchType> BranchType { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<BankSetup> BankSetup { get; set;}
    
        public DbSet<ShiftType> ShiftType { get; set; }
        public DbSet<FileType> FileType { get; set; }
        public DbSet<TblMedicine> tblMedicine { get; set; }
        public DbSet<BillSetup> tblBillSetup { get; set; }
        public DbSet<DoctorSetUp> tblDoctor { get; set; }
        public DbSet<tblCategory> tblCategory { get; set; }
        public DbSet<tblSubCategory> tblSubCategory { get; set; }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
            builder.Entity<ChatHistory<HrUser>>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<HrUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<HrRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<HrRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });

            builder.Entity<BankSetup>(bank =>
            {
                bank.HasMany(b => b.Children)
                    .WithOne(p => p.ParentItem)
                    .HasForeignKey(u => u.BankParentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<MenuList>(item =>
            {
                item.ToTable("MenuList", "Identity");
                item.HasMany(y => y.Children)
                    .WithOne(r => r.ParentItem)
                    .HasForeignKey(u => u.ParentId);

                item.HasMany(t => t.RoleMenus)
                    .WithOne(u => u.MenuList)
                    .HasForeignKey(r => r.MenuId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<MenuRole>(roleMenu =>
            {
                roleMenu.ToTable("MenuRole", "Identity");

                roleMenu.HasOne(o => o.Role)
                    .WithMany(u => u.menuRoles)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);

                roleMenu.HasOne(o => o.MenuList)
                    .WithMany(u => u.RoleMenus)
                    .HasForeignKey(e => e.MenuId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<LocalBodies>(localBodies =>
            {
                localBodies.HasOne(d => d.Province)
                    .WithMany()
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.NoAction);

                localBodies.HasOne(d => d.District)
                     .WithMany()
                     .HasForeignKey(d => d.DistrictId)
                     .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Branch>(branch =>
            {
                branch.HasOne(d => d.BranchType)
                    .WithMany()
                    .HasForeignKey(d => d.BranchTypeId)
                    .OnDelete(DeleteBehavior.NoAction);

                branch.HasOne(d => d.Province)
                    .WithMany()
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.NoAction);

                branch.HasOne(d => d.District)
                     .WithMany()
                     .HasForeignKey(d => d.DistrictId)
                     .OnDelete(DeleteBehavior.NoAction);

                branch.HasOne(d => d.LocalBodies)
                    .WithMany()
                    .HasForeignKey(d => d.LocalBodiesId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}