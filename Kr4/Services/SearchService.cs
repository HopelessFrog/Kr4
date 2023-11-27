using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;
using Kr4.Services.Interface;

namespace Kr4.Services
{
    public class SearchService : ISearchService
    {
        public List<Planet> SearchPlanet(string? searchBar, int minAge, int maxAge)
        {
            var planets = DatabaseLocator.Context!.Planets.AsQueryable();
            List<Expression<Func<Planet, bool>>> conditionsPlanets = new List<Expression<Func<Planet, bool>>>();
            if (!string.IsNullOrEmpty(searchBar))
                conditionsPlanets.Add(p => p.Name!.Contains(searchBar));
            conditionsPlanets.Add(p => p.Age >= minAge);
            conditionsPlanets.Add(p => p.Age <= maxAge);
            foreach (var item in conditionsPlanets)
            {
                planets = planets.Where(item);
            }

            return planets.ToList();
        }

        public List<Star> SearchStar(string? searchBar, SpectralClass? spectralClass, int minAge, int maxAge)
        {
            var stars = DatabaseLocator.Context!.Stars.AsQueryable();
            List<Expression<Func<Star, bool>>> conditionsStars = new List<Expression<Func<Star, bool>>>();
            if (spectralClass != null && spectralClass.Name != "none")
                conditionsStars.Add(s => s.Class!.Name == spectralClass.Name);
            if (searchBar != "" && searchBar != null)
                conditionsStars.Add(p => p.Name!.Contains(searchBar));
            conditionsStars.Add(p => p.Age >= minAge);
            conditionsStars.Add(p => p.Age <= maxAge);
            foreach (var item in conditionsStars)
            {
                stars = stars.Where(item);
            }
            return stars.ToList();
        }

        public List<Galaxy> SearchGalaxy(string? searchBar, GalaxyType? galaxyType, int minAge, int maxAge)
        {
            var galaxies = DatabaseLocator.Context!.Galaxies.AsQueryable();
            List<Expression<Func<Galaxy, bool>>> conditionsGalaxies = new List<Expression<Func<Galaxy, bool>>>();
            if (galaxyType != null && galaxyType.Name != "none")
                conditionsGalaxies.Add(g => g.Type!.Name == galaxyType.Name);
            if (searchBar != "" && searchBar != null)
                conditionsGalaxies.Add(p => p.Name!.Contains(searchBar));
            conditionsGalaxies.Add(p => p.Age >= minAge);
            conditionsGalaxies.Add(p => p.Age <= maxAge);
            foreach (var item in conditionsGalaxies)
            {
                galaxies = galaxies.Where(item);
            }
            return galaxies.ToList();
        }
    }
}
