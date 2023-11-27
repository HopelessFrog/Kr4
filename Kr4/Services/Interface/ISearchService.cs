using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;

namespace Kr4.Services.Interface
{
    public interface ISearchService
    {
        List<Planet> SearchPlanet(string? searchBar, int minAge, int maxAge);
        List<Star> SearchStar(string? searchBar, SpectralClass? spectralClass, int minAge, int maxAge);
        List<Galaxy> SearchGalaxy(string? searchBar, GalaxyType? galaxyType, int minAge, int maxAge);

    }
}
