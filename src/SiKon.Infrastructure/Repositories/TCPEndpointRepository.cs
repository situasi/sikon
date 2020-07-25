using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SiKon.Application.Interfaces;
using SiKon.Domain.Entities;
using SiKon.Infrastructure.Common;

namespace SiKon.Infrastructure.Repositories
{
    public class TCPEndpointRepository : ITCPEndpointRepository
    {
        private readonly IConfiguration _configuration;

        public TCPEndpointRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<int> Add(TCPEndpoint entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TCPEndpoint> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TCPEndpoint>> GetAll()
        {
            var sql = "SELECT * FROM TCPEndpoints;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var result = await connection.QueryAsync<TCPEndpoint>(sql);
                return result;
            }
        }

        public Task<int> Update(TCPEndpoint entity)
        {
            throw new NotImplementedException();
        }
    }
}