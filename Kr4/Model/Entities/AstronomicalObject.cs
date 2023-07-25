using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Model.Entities
{
    public interface  IAstronomicalObject
    {
        int Id { get; set; }
        string Name { get; set; }
        double DistanceFromEarth { get; set; }
        double Age { get; set; }
    }
}
