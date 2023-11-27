using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;
using Kr4.Services.Interface;

namespace Kr4.Services
{
    public class RemoveFromDbService : IRemoveFromDbService
    {
        public void Remove(IAstronomicalObject? obj)
        {
            if (obj == null) 
                return;
            if (obj is Planet)
            {
                DatabaseLocator.Context?.Planets.Remove((obj! as Planet)!);
            }
            else if (obj is Galaxy)
            {
                DatabaseLocator.Context?.Galaxies.Remove((obj! as Galaxy)!);
            }
            else if (obj is Star)
            {
                DatabaseLocator.Context?.Stars.Remove((obj! as Star)!);
            }

            DatabaseLocator.Context?.SaveChanges();
        }
    }
}
