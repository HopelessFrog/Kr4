using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Model.Entities
{
    public class Planet : ViewModelBase, IAstronomicalObject
    {
        public int Id { get; set; }
        public double Size { get; set; }
        public double? OrbitalPeriod { get; set; }
        public string Name { get; set; }
        public double DistanceFromEarth { get; set; }
        public double Age { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
