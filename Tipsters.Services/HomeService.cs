using Tipsters.Data.Interfaces;

namespace Tipsters.Services
{
    public class HomeService : Service
    {
        public HomeService(ITipstersData data) : base(data)
        {

        }
    }
}
