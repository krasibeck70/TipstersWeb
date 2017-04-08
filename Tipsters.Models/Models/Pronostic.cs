using System;
using System.Collections.Generic;
using System.Linq;

namespace Tipsters.Models.Models
{
    public class Pronostic
    {
        public string Id { get; set; }
        public string LocalTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime TimeElpased { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double PercentageWin
        {
            get
            {
                return CalculatePercentage();
            }
        }
        public int VotesUp { get; set; }
        public int VotesDown { get; set; }
        public int Chance { get; set; }
        public int TotalVotes { get; set; }
        public string TypeOfPrognise { get; set; }
        
        public DateTime StartMatch { get; set; }
        public decimal Koeficent { get; set; }
        public string TimeAgoPronostic {
            get
            {
                return TimeAgo(this.TimeElpased);
            }
            
        }

        public virtual ICollection<Comment> OwnerComments { get; set; }
        public virtual ICollection<ApplicationUser> UsersLikes { get; set; }


        public static string TimeAgo(DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    string.Format("about {0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    string.Format("about {0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    string.Format("about {0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    string.Format("about {0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    string.Format("about {0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }

            return result;
        }

        public double CalculatePercentage()
        {
            double percentage = 0;
            if (this.VotesDown == 0 && this.VotesUp == 0)
            {
                percentage = 50;
            }
            else if (this.VotesUp == 0 && this.VotesDown != 0)
            {
                percentage = 0;
            }
            else if (this.VotesUp != 0 && this.VotesDown == 0)
            {
                percentage = 100;
            }
            else
            {
                this.TotalVotes = this.VotesDown + this.VotesUp;

                percentage = (int)Math.Round((double)(this.VotesUp * 100) / this.TotalVotes);
            }

            return percentage;
        }

        public bool ContainsUserId(ICollection<ApplicationUser> users, string id)
        {
            var usersId = users.Select(x => x.Id);
            if (usersId.Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
