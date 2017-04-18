using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tipsters.Models.BindingModels
{
    public class EditTipBindingModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int Chance { get; set; }
        public decimal Coefficient { get; set; }
        public string TypeOfTip { get; set; }
        public DateTime StartMatch { get; set; }
    }
}
