using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiKon.Application.Interfaces;
using SiKon.Domain.Common;
using SiKon.Domain.Entities;

namespace SiKon.Infrastructure.Persistence
{
    public class SiKonDBContext : DbContext, ISiKonDBContext
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTimeOffsetService _dateTimeOffset;

        public DbSet<Member> Members { get; set; }
        public DbSet<TCPEndpoint> TCPEndpoints { get; set; }

        public SiKonDBContext(
            DbContextOptions<SiKonDBContext> options,
            ICurrentUserService currentUser,
            IDateTimeOffsetService dateTimeOffset)
            : base(options)
        {
            _currentUser = currentUser;
            _dateTimeOffset = dateTimeOffset;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeOffset.Now;
                        entry.Entity.CreatedBy = _currentUser.Username;
                        entry.Entity.Modified = _dateTimeOffset.Now;
                        entry.Entity.ModifiedBy = _currentUser.Username;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = _dateTimeOffset.Now;
                        entry.Entity.ModifiedBy = _currentUser.Username;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeOffset.Now;
                        entry.Entity.CreatedBy = _currentUser.Username;
                        entry.Entity.Modified = _dateTimeOffset.Now;
                        entry.Entity.ModifiedBy = _currentUser.Username;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = _dateTimeOffset.Now;
                        entry.Entity.ModifiedBy = _currentUser.Username;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}