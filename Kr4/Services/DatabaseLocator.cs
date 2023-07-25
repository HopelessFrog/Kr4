using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model;

namespace Kr4.Services
{
   public  static class DatabaseLocator
    {
        public static AstronomicalContext? Context { get; set; }
    }
}
