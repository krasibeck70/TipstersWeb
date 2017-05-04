using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.Models;
using Tipsters.Models.ViewModels.PronosticsViewModel;

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

        public List<PronosticViewModel> GetAllPronosticViewModels()
        {
            var pronosticsViewModel = new List<PronosticViewModel>();
            var pronostics = data.Pronostics.GetAll().ToList();
            foreach (Pronostic pronostic in pronostics)
            {
                pronosticsViewModel.Add(Mapper.Map<PronosticViewModel>(pronostic));
            }
            return pronosticsViewModel;
        }

        public List<PronosticViewModel> Get10PronosticsSorted(string parameter)
        {
            List<PronosticViewModel> sortedPronostics = new List<PronosticViewModel>();
            var pronostics = data.Pronostics.GetAll().ToList();
            foreach (var pronostic in pronostics)
            {
                PronosticViewModel model = Mapper.Map<PronosticViewModel>(pronostic);
                sortedPronostics.Add(model);

            }
            if (parameter == "Percentage")
            {
                return sortedPronostics.OrderByDescending(x => x.CalculatePercentage()).Take(10).ToList();
            }
            if (parameter == "StartMatch")
            {
                return sortedPronostics.OrderByDescending(x => x.StartMatch).Take(10).ToList();
            }
            if (parameter == "Coefficient")
            {
                return sortedPronostics.OrderByDescending(x => x.Koeficent).Take(10).ToList();
            }
            return null;
        }

        public List<string> Errors(PostTipsBindingModel ptbm)
        {
            var countries = data.Countries.GetAll().Select(x=>x.Name);
            var errors = new List<string>();
            if (ptbm.SelectHomeTeam.Equals(ptbm.SelectAwayTeam))
            {
                errors.Add("A team can't play against itself! Please change team!");
            }
            if (ptbm.SelectHomeTeam.Equals("Home Team") || ptbm.SelectAwayTeam.Equals("Away Team") || 
                countries.Contains(ptbm.SelectHomeTeam) || countries.Contains(ptbm.SelectAwayTeam))
            {
                errors.Add("Invalid Team");
            }
            if (ptbm.SelectTips.Equals("Type Of Tip"))
            {
                errors.Add("Invalid type of tip");
            }
            if (ptbm.StartMatch < DateTime.Now)
            {
                errors.Add("Invalid Date");
            }
            if (ptbm.Koeficent is decimal)
            {
                
            }
            else
            {
                errors.Add("Is Not a decimal");
            }

            return errors;
        }

        public ICollection<Tip> GetAllTypeOfTips()
        {
            return this.data.Tips.GetAll().ToList();
        }
        public ICollection<County> GetAllCountries()
        {
            return this.data.Countries.GetAll().ToList();
        }

        public Pronostic FindPronosticById(string id)
        {
            return this.data.Pronostics.FindByPredicate(x => x.Id == id);
        }

        public void SaveChanges()
        {
            this.data.SaveChanges();
        }

        public void InsertOrUpdate(Comment comment)
        {
            this.data.Comments.InsertOrUpdate(comment);
        }
    }
}
