using Kr4.Model;
using Kr4.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Kr4.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Testqwe
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //C:\poe\Kr4.db
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\poe\Kr4.db");
        }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Galaxy> Galaxies { get; set; }

        public DbSet<SpectralClass> SpectralClasses { get; set; }

        public DbSet<GalaxyType> GalaxysTypes { get; set; }

    

    [TestMethod]
        public void AddGalaxy_ShouldAddGalaxyToDatabase()
        {
            using (var context = new AstronomicalContext())
            {
                // Arrange
                var galaxy = new Galaxy
                {
                    Type = new GalaxyType { Name = "Spiral" },
                    Name = "Andromeda",
                    DistanceFromEarth = 2.537e6,
                    Age = 1.4e10
                };

                // Act
                context.Galaxies.Add(galaxy);
                context.SaveChanges();

                // Assert
                Assert.AreEqual(1, context.Galaxies.Count());
                Assert.AreEqual("Andromeda", context.Galaxies.Single().Name);
            }
        }

        [TestMethod]
        public void DeleteGalaxy_ShouldRemoveGalaxyFromDatabase()
        {
            using (var context = new AstronomicalContext())
            {
                // Arrange
                var galaxy = new Galaxy
                {
                    Type = new GalaxyType { Name = "Elliptical" },
                    Name = "Messier 87",
                    DistanceFromEarth = 5.321e7,
                    Age = 1.2e10
                };
                context.Galaxies.Add(galaxy);
                context.SaveChanges();

                // Act
                context.Galaxies.Remove(galaxy);
                context.SaveChanges();

                // Assert
                Assert.AreEqual(0, context.Galaxies.Count());
            }
        }

        [TestMethod]
        public void AddPlanet_ShouldAddPlanetToDatabase()
        {
            using (var context = new AstronomicalContext())
            {
                // Arrange
                var planet = new Planet()
                {
                    Name = "Andromeda",
                    DistanceFromEarth = 2.537e6,
                    Age = 1.4e10
                };

                // Act
                context.Planets.Add(planet);
                context.SaveChanges();

                // Assert
                Assert.AreEqual(1, context.Planets.Count());
                Assert.AreEqual("Andromeda", context.Planets.Single().Name);
                Assert.AreEqual(2.537e6, context.Planets.Single().DistanceFromEarth);
                Assert.AreEqual(1.4e10, context.Planets.Single().Age);
            }
        }

        [TestMethod]
        public void DeletePlanet_ShouldRemovePlanetFromDatabase()
        {
            using (var context = new AstronomicalContext())
            {
                // Arrange
                var planet = new Planet()
                {
                    Name = "Messier 87",
                    DistanceFromEarth = 5.321e7,
                    Age = 1.2e10
                };
                context.Planets.Add(planet);
                context.SaveChanges();

                // Act
                context.Planets.Remove(planet);
                context.SaveChanges();

                // Assert
                Assert.AreEqual(0, context.Planets.Count());
            }
        }
    }
}