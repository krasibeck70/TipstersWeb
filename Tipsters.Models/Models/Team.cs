namespace Tipsters.Models.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual County County { get; set; }

        public virtual League League { get; set; }
    }
}
