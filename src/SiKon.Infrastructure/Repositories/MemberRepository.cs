using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SiKon.Application.Interfaces;
using SiKon.Domain.Entities;
using SiKon.Infrastructure.Common;

namespace SiKon.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeOffsetService _dateTimeOffset;
        private readonly ICurrentUserService _currentUser;

        public MemberRepository(
            IConfiguration configuration,
            IDateTimeOffsetService dateTimeOffset,
            ICurrentUserService currentUser)
        {
            _configuration = configuration;
            _dateTimeOffset = dateTimeOffset;
            _currentUser = currentUser;
        }

        public async Task<int> Add(Member entity)
        {
            entity.Created = _dateTimeOffset.Now;
            entity.CreatedBy = _currentUser.Username;
            entity.Modified = _dateTimeOffset.Now;
            entity.ModifiedBy = _currentUser.Username;
             
            var sql = "INSERT INTO Members (MemberUsername, FullName, Created, CreatedBy, Modified, ModifiedBy) VALUES (@MemberUsername, @FullName, @Status, @Created, @CreatedBy, @Modified, @ModifiedBy);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Members WHERE MemberID = @MemberID;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { MemberID = id });
                return affectedRows;
            }
        }

        public async Task<Member> Get(int id)
        {
            var sql = "SELECT * FROM Members WHERE MemberID = @MemberID;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var result = await connection.QueryAsync<Member>(sql, new { MemberID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            var sql = "SELECT * FROM Members;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var result = await connection.QueryAsync<Member>(sql);
                return result;
            }
        }

        public async Task<int> Update(Member entity)
        {
            entity.Modified = _dateTimeOffset.Now;
            entity.ModifiedBy = _currentUser.Username;

            var sql = "UPDATE Members SET Username = @Username, FullName = @FullName, Modified = @Modified, ModifiedBy = @ModifiedBy WHERE MemberID = @MemberID;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStringName.SiKonDatabase)))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}