using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EPharma.Infrastructure.Repositories.Dapper
{
    public abstract class RepositoryBase
    {
        private readonly IConfiguration configuration;
        //private readonly ILogger logger;

        /// <summary>
        /// Initializes the new instance of <see cref="BaseService"/>.
        /// </summary>
        /// <param name="configuration">The instance of <see cref="IConfiguration"/>.</param>
        /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
        protected RepositoryBase(IConfiguration configuration)
        {
            this.configuration = configuration;
            //this.logger = logger;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        protected IDbConnection Connection => new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));

        /// <summary>
        /// Executes the query to get the results.
        /// </summary>
        /// <typeparam name="T">The type of result data.</typeparam>
        /// <param name="sQuery">The sql query.</param>
        /// <param name="parameters">The paramters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed,
        /// otherwise an instance is created per row,
        /// and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        protected async Task<List<T>> GetQueryResultAsync<T>(string sQuery, object parameters = null, IDbTransaction transaction = null)
        {
            //this.logger.LogDebug($"QUERY GetQueryResultAsync COMMAND | { sQuery }");
            //this.logger.LogDebug($"QUERY GetQueryResultAsync PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await this.Connection.QueryAsync<T>(command);

            //this.logger.LogDebug($"QUERY GetQueryResultAsync EXECUTED");

            return result.ToList();

        }

        /// <summary>
        /// Executes the query to get the results.
        /// </summary>
        /// <typeparam name="T">The type of result data.</typeparam>
        /// <param name="sQuery">The sql query.</param>
        /// <param name="parameters">The paramters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed,
        /// otherwise an instance is created per row,
        /// and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        protected async Task<T> GetQueryFirstOrDefaultResultAsync<T>(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            //this.logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync COMMAND | { sQuery }");
            //this.logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await this.Connection.QueryFirstOrDefaultAsync<T>(command);

            //this.logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync EXECUTED");

            return result;
        }

        /// <summary>
        /// Executes the query
        /// </summary>
        /// <param name="sQuery">The sql query.</param>
        /// <param name="parameters">The query parameters</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>The affected number of rows.</returns>
        protected async Task<int> ExecuteAsync(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            //this.logger.LogDebug($"QUERY ExecuteAsync COMMAND | { sQuery }");
            //this.logger.LogDebug($"QUERY ExecuteAsync PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var rowsAffected = await this.Connection.ExecuteAsync(command);

            //this.logger.LogDebug($"QUERY ExecuteAsync EXECUTED");

            return rowsAffected;
        }
        /// <summary>
        /// Reads from multiple query.
        /// </summary>
        /// <param name="sQuery">The sql query</param>
        /// <param name="parameters">THe parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>The grid reader.</returns>
        protected async Task<GridReader> GetFromMultipleQuery(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            //this.logger.LogDebug($"QUERY InsertAndGetId COMMAND | { sQuery }");
            //this.logger.LogDebug($"QUERY InsertAndGetId PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await this.Connection.QueryMultipleAsync(command);

            //this.logger.LogDebug($"QUERY GetQueryResultCount EXECUTED");

            return result;
        }
    }
}
