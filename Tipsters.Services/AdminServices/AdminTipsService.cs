using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.Models;
using Tipsters.Models.ViewModels.PronosticsViewModel;

namespace Tipsters.Services.AdminServices
{
    public class AdminTipsService : Service
    {
        public AdminTipsService(ITipstersData data) : base(data)
        {
        }

        public List<Pronostic> GetAllTips()
        {
            var tips = data.Pronostics.GetAll().ToList();
            return tips;
        }

        public void RemoveTips(string id)
        {
            var tip = data.Pronostics.Find(x => x.Id == id).First();
            var commentsTip = tip.OwnerComments.ToList();
            tip.UsersLikes.Clear();
            foreach (Comment comment in commentsTip)
            {
                data.Comments.Delete(comment);
            }
            data.Pronostics.Delete(tip);
            data.SaveChanges();
        }

        public Pronostic TipById(string id)
        {
            return data.Pronostics.Find(x => x.Id == id).First();
        }
        public PronosticViewModel TipByIdViewModel(string id)
        {
            var tip = this.TipById(id);
            PronosticViewModel model = Mapper.Map<PronosticViewModel>(tip);
            return model;
        }
        public void EditTip(string id, EditTipBindingModel etbm)
        {
            var tip = this.TipById(id);
            tip.LocalTeam = etbm.HomeTeam;
            tip.AwayTeam = etbm.AwayTeam;
            tip.Chance = etbm.Chance;
            tip.Koeficent = etbm.Coefficient;
            tip.TypeOfPrognise = etbm.TypeOfTip;
            data.Pronostics.InsertOrUpdate(tip);
            data.SaveChanges();
        }
    }
}
