using System.Collections.Generic;

namespace Tipsters.Models.Models
{
    public class County
    {
        public County()
        {
            this.Teams = new HashSet<Team>();
            this.Leagues = new HashSet<League>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<League> Leagues { get; set; }
    }
}
