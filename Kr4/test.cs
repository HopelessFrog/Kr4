using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;

namespace Kr4
{
    public static class test
    {
        public static IEnumerable<Planet> GetPlanets()
        {
            return new List<Planet>()
            {
                new Planet() {Name = "Mars" ,Id = 0 , Size = 12 , OrbitalPeriod = 23, DistanceFromEarth = 123, Age = 10},
                new Planet() {Name = "NeMars" ,Id = 1 , Size = 112 , OrbitalPeriod = 11, DistanceFromEarth = 123, Age = 13},
                new Planet() {Name = "Sasha" ,Id = 2 , Size = 142 , OrbitalPeriod = 253, DistanceFromEarth = 14, Age = 50}

            };
        }
    }
}
