using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace Kr4.Model.Entities
{
    public class Galaxy : ViewModelBase,IAstronomicalObject
    {
        public GalaxyType? Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double DistanceFromEarth { get; set; }
        public double Age { get; set; }
        
    }
}
