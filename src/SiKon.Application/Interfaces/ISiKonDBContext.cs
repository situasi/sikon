using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiKon.Domain.Entities;

namespace SiKon.Application.Interfaces
{
    // codingannya akan tergantung dari ORM yang kita gunakan
    public interface ISiKonDBContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<TCPEndpoint> TCPEndpoints { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}