﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
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

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
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

        public async Task<IEnumerable<TCPEndpoint>> GetAllIncludeMember()
        {
            var sql = "SELECT * FROM TCPEndpoints endpoint INNER JOIN Members member on endpoint.MemberID = member.MemberID";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var result = await connection.QueryAsync<TCPEndpoint, Member, TCPEndpoint>(sql, (endpoint, member) => { endpoint.Member = member; return endpoint; }, splitOn: "MemberID");
                return result;
            }
        }
    }
}