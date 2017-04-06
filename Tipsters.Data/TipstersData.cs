using System;
using System.Data.Entity.Validation;
using Tipsters.Data.Interfaces;
using Tipsters.Data.Repositories;
using Tipsters.Models.Models;

namespace Tipsters.Data
{
    public class TipstersData : ITipstersData
    {
        private readonly ITipstersContext context;

        public TipstersData()
            : this(new TipstersContext())
        {

        }
        public TipstersData(ITipstersContext context)
        {
            this.context = context;
        }

        public Repository<ApplicationUser> Users => new Repository<ApplicationUser>(this.context);
        public Repository<County> Countries => new Repository<County>(this.context);
        public Repository<League> Leagues => new Repository<League>(this.context);
        public Repository<Team> Teams => new Repository<Team>(this.context);
        public Repository<Tip> Tips => new Repository<Tip>(this.context);
        public Repository<Pronostic> Pronostics => new Repository<Pronostic>(this.context);
        public Repository<Comment> Comments => new Repository<Comment>(this.context);
        public ITipstersContext Context => this.context;
        public int SaveChanges()
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                return this.Context.DbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }


    }
}
