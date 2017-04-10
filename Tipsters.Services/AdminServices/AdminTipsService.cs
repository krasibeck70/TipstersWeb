using System.Collections.Generic;
using System.Linq;
using Tipsters.Data.Interfaces;
using Tipsters.Models.Models;

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
    }
}
