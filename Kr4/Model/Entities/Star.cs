using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Model.Entities
{
    public class Star : ViewModelBase, IAstronomicalObject
    {
        public int Id { get; set; }
        public SpectralClass? Class { get; set; }
        public double? Luminosity { get; set; }
        public string Name { get; set; }
        public double DistanceFromEarth { get; set; }
        public double Age { get; set; }

      
    }
}
