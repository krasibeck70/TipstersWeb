using Tipsters.Data.Interfaces;

namespace Tipsters.Services
{
    public class TipsService : Service
    {
        public TipsService(ITipstersData data) : base(data)
        {
        }
        
    
    }
}
