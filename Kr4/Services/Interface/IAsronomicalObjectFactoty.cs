using Kr4.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Services.Interface
{
    public interface IAsronomicalObjectFactoty
    {
        void AddPlanet(string name, double distance, double age, double orbitalPeriod, double size);
        void AddStar(string name, double distance, double age, SpectralClass spectralClass, double luminosity);

        string AddSpectralClass(string name, List<SpectralClass> spectralClasses);
        void AddGalaxy(string name, double distance, double age, GalaxyType galaxyType);
        string AddGakaxyType(string name, List<GalaxyType> galaxyTypes);

    }
}
