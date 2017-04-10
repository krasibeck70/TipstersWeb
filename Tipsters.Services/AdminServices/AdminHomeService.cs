using System.Linq;
using Tipsters.Data.Interfaces;

namespace Tipsters.Services.AdminServices
{
    public class AdminHomeService : Service
    {
        public AdminHomeService(ITipstersData data) : base(data)
        {
        }

        public int GetNumbersOfUsers()
        {
            int numbersOfUsers = 0;
            numbersOfUsers = data.Users.GetAll().Count();
            return numbersOfUsers;
        }
        public int GetNumbersOfTips()
        {
            int numbersOfTips = 0;
            numbersOfTips = data.Pronostics.GetAll().Count();
            return numbersOfTips;
        }
        public int GetNumbersOfComments()
        {
            int numbersOfComments = 0;
            numbersOfComments = data.Comments.GetAll().Count();
            return numbersOfComments;
        }
    }
}
