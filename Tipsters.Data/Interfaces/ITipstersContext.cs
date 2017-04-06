using System.Data.Entity;
using Tipsters.Models.Models;

namespace Tipsters.Data.Interfaces
{
    public interface ITipstersContext
    {
        IDbSet<ApplicationUser> Users { get; }
        IDbSet<Team> Teams { get; }
        IDbSet<County> Countries { get; }
        IDbSet<League> Leagues { get; }
        IDbSet<Pronostic> Pronostics { get; }
        IDbSet<Tip> Tips { get; }
        IDbSet<Comment> Comments { get; }
        DbContext DbContext { get; }

        int SaveChanges();
        IDbSet<T> Set<T>()
           where T : class;

    }
}
