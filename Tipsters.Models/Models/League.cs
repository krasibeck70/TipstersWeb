using System.Collections.Generic;

namespace Tipsters.Models.Models
{
    public class League
    {
        public League()
        {
            this.Teams = new HashSet<Team>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual County County { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
