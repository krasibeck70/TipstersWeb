using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tipsters.Models.BindingModels
{
    public class PostTipsBindingModel
    {
        public string SelectHomeTeam { get; set; }

        public string SelectAwayTeam { get; set; }

        public string SelectTips { get; set; }

        public DateTime StartMatch { get; set; }

        public decimal Koeficent { get; set; }

        public int Chance { get; set; }
    }
}
