using System.Collections.Generic;

namespace FreeParkingSystem.Parking.Infastructure.Queries
{
    public abstract class BaseQuery<TQueryClass> where TQueryClass : class
    {
        public abstract IEnumerable<TQueryClass> Apply(IEnumerable<TQueryClass> query);
    }
}