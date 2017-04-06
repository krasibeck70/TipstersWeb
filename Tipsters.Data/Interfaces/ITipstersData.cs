using Tipsters.Data.Repositories;
using Tipsters.Models.Models;

namespace Tipsters.Data.Interfaces
{
    public interface ITipstersData
    {
        Repository<ApplicationUser> Users { get; }
        Repository<County> Countries { get; }
        Repository<League> Leagues { get; }
        Repository<Team> Teams { get; }
        Repository<Tip> Tips { get; }
        Repository<Pronostic> Pronostics { get; }
        Repository<Comment> Comments { get; }

        ITipstersContext Context { get; }

        int SaveChanges();
    }
}
