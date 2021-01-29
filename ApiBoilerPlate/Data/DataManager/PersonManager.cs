using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Data.DataManager
{
    public class PersonnelManager : DbFactoryBase, IPersonnelManager
    {
        private readonly ILogger<PersonnelManager> _logger;
        public PersonnelManager(IConfiguration config, ILogger<PersonnelManager> logger) : base(config)
        {
            _logger = logger;
        }

        public async Task<(IEnumerable<Personnel> Personnel, Pagination Pagination)> GetPersonnelAsync(UrlQueryParameters urlQueryParameters)
        {
            IEnumerable<Personnel> Personnel;
            int recordCount = default;

             ////For SqlServer
            var query = @"SELECT ID, FirstName, LastName, DateOfBirth, Type FROM Personnel
                            ORDER BY ID DESC
                            OFFSET @Limit * (@Offset -1) ROWS
                            FETCH NEXT @Limit ROWS ONLY";

            var param = new DynamicParameters();
            param.Add("Limit", urlQueryParameters.PageSize);
            param.Add("Offset", urlQueryParameters.PageNumber);

            if (urlQueryParameters.IncludeCount)
            {
                query += " SELECT COUNT(ID) FROM Personnel";
                var pagedRows = await DbQueryMultipleAsync<Personnel, int>(query, param);

                Personnel = pagedRows.Data;
                recordCount = pagedRows.RecordCount;
            }
            else
            {
                Personnel = await DbQueryAsync<Personnel>(query, param);
            }

            var metadata = new Pagination
            {
                PageNumber = urlQueryParameters.PageNumber,
                PageSize = urlQueryParameters.PageSize,
                TotalRecords = recordCount

            };

            return (Personnel, metadata);

        }
        public async Task<IEnumerable<Personnel>> GetAllAsync()
        {
            return await DbQueryAsync<Personnel>("SELECT * FROM Personnel");
        }

        public async Task<long> CreateAsync(Personnel Personnel)
        {
            string sqlQuery = $@"INSERT INTO Personnel (FirstName, LastName, DateOfBirth, Type) 
                                     VALUES (@FirstName, @LastName, @DateOfBirth, @Type)
                                     SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return await DbQuerySingleAsync<long>(sqlQuery, Personnel);
        }




    }
}
