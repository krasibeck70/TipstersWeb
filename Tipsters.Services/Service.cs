using Tipsters.Data.Interfaces;

namespace Tipsters.Services
{
    public class Service
    {
        protected ITipstersData data;

        public Service(ITipstersData data)
        {
            this.data = data;
        }
    }
}
