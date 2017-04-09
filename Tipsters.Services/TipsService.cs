using System;
using System.Linq;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.Models;

namespace Tipsters.Services
{
    public class TipsService : Service
    {
        public TipsService(ITipstersData data) : base(data)
        {
        }

        public void PostTips(PostTipsBindingModel ptbm, string userId)
        {
            ApplicationUser user = this.data.Users.Find(x => x.Id == userId).First();
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            Pronostic pronostic = new Pronostic()
            {
                Id = g.ToString(),
                User = user,
                Chance = ptbm.Chance,
                AwayTeam = ptbm.SelectAwayTeam,
                LocalTeam = ptbm.SelectHomeTeam,
                Koeficent = ptbm.Koeficent,
                StartMatch = ptbm.StartMatch,
                TimeElpased = DateTime.Now,
                TypeOfPrognise = ptbm.SelectTips
            };
            data.Pronostics.InsertOrUpdate(pronostic);
            data.SaveChanges();
        }
    }
}
