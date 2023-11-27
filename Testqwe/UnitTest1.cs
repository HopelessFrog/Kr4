using System.Drawing.Text;
using System.Reflection;
using Kr4.Bootstrapper;
using Kr4.Model;
using Kr4.Model.Entities;
using Kr4.Services;
using Kr4.Services.Interface;
using Kr4.ViewModel;
using Kr4.ViewModel.EditViewModels.Interface;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Testqwe
{
    public class UnitTest1
    {
        private AstronomicalContext? dbContext;
        private AstronomicalObjectFactoty? asronomicalObjectFactoty;
        private IRemoveFromDbService? removeFromDbService;
        private ISearchService? searchService;

      
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Run();
            asronomicalObjectFactoty = (AstronomicalObjectFactoty) Bootstrapper.Resolve<IAsronomicalObjectFactoty>();
            removeFromDbService = Bootstrapper.Resolve<IRemoveFromDbService>();
            searchService = Bootstrapper.Resolve<ISearchService>();
            var options = new DbContextOptionsBuilder<AstronomicalContext>()
                .UseSqlite("Data Source = test")
                .Options;

            dbContext = new AstronomicalContext(options);
            DatabaseLocator.Context = dbContext;

            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext!.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public void AddGalaxy_ShouldAddGalaxyToDatabase()
        {
                
           
             
                asronomicalObjectFactoty!.AddGalaxyLogic("QWe", 123, 123, new GalaxyType { Name = "Spiral" });

                var tmp = dbContext!.Galaxies.Single().Name;

            
                Assert.AreEqual("QWe", dbContext.Galaxies.Single().Name);
                Assert.AreEqual(123, dbContext.Galaxies.Single().DistanceFromEarth);
                Assert.AreEqual("Spiral", dbContext.Galaxies.Include(galaxy => galaxy.Type!).Single().Type!.Name!);
                Assert.AreEqual(123, dbContext.Galaxies.Single().Age);


        }

        [Test]
        public void DeleteGalaxy_ShouldRemoveGalaxyFromDatabase()
        {
            
                var galaxy = new Galaxy
                {
                    Type = new GalaxyType { Name = "Elliptical" },
                    Name = "Messier 87",
                    DistanceFromEarth = 5.321e7,
                    Age = 1.2e10
                };
                dbContext!.Galaxies.Add(galaxy);
                dbContext.SaveChanges();

                removeFromDbService?.Remove(galaxy);
            

                Assert.AreEqual(0, dbContext.Galaxies.Count());
            
        }

        [Test]
        public void AddPlanet_ShouldAddPlanetToDatabase()
        {
                

                asronomicalObjectFactoty!.AddPlanteLogic("QWe", 123, 123, 123,123);

                var tmp = dbContext!.Planets.Single();


                Assert.AreEqual("QWe", dbContext.Planets.Single().Name);
                Assert.AreEqual(123, dbContext.Planets.Single().Age);
                Assert.AreEqual(123, dbContext.Planets.Single().DistanceFromEarth);
                Assert.AreEqual(123, dbContext.Planets.Single().OrbitalPeriod);
                Assert.AreEqual(123, dbContext.Planets.Single().Size);
            
        }

        [Test]
        public void DeletePlanet_ShouldRemovePlanetFromDatabase()
        {
           
               
                var planet = new Planet()
                {
                    Name = "Messier 87",
                    DistanceFromEarth = 5.321e7,
                    Age = 1.2e10
                };
                dbContext!.Planets.Add(planet);
                dbContext.SaveChanges();


            removeFromDbService?.Remove(planet);

               
                Assert.AreEqual(0, dbContext.Planets.Count());
            
        }

        [Test]
        public void AddStar_ShouldAddStarToDatabase()
        {


            asronomicalObjectFactoty!.AddStarLogic("QWe", 123, 123, new SpectralClass() { Name = "kkk"}, 123);



            Assert.AreEqual("QWe", dbContext!.Stars.Single().Name);
            Assert.AreEqual(123, dbContext.Stars.Single().Age);
            Assert.AreEqual(123, dbContext.Stars.Single().DistanceFromEarth);
            Assert.AreEqual(123, dbContext.Stars.Single().Luminosity);
            Assert.AreEqual("kkk", dbContext.Stars.Include(star => star.Class).Single().Class?.Name);

        }

        [Test]
        public void DeleteStar_ShouldRemoveStarFromDatabase()
        {


            var star = new Star()
            {
                Name = "Star",
                DistanceFromEarth = 5.321e7,
                Age = 1.2e10,
                Class = new SpectralClass() {Name = "kkk"},
                Luminosity = 14
                
            };
            dbContext!.Stars.Add(star);
            dbContext.SaveChanges();


            removeFromDbService?.Remove(star);


            Assert.AreEqual(0, dbContext.Stars.Count());

        }

        [Test]
        public void SearchPlanetTest()
        {
            var planet1 = new Planet()
            {
                Name = "Planet1",
                DistanceFromEarth = 1,
                Age = 15
            };
            var planet2 = new Planet()
            {
                Name = "Planet2",
                DistanceFromEarth = 29,
                Age = 112
            };
            dbContext!.Planets.Add(planet1);
            dbContext.Planets.Add(planet2);
            dbContext.SaveChanges();

            var tempPlanet = searchService!.SearchPlanet("2", 16, 190);
            var planet = tempPlanet[0];

            Assert.IsTrue(tempPlanet.Count == 1);
            Assert.AreEqual(planet.Name, planet2.Name);
            Assert.AreEqual(planet.Age, planet2.Age);
            Assert.AreEqual(planet.DistanceFromEarth, planet2.DistanceFromEarth);
           

        }

        [Test]
        public void SearchStarTest()
        {
            var star1 = new Star()
            {
                Name = "Star1",
                DistanceFromEarth = 1,
                Age = 15,
                Luminosity = 22,
                Class = new SpectralClass() { Name = "qqq" }
            };
            var star2 = new Star()
            {
                Name = "Star2",
                DistanceFromEarth = 1,
                Age = 85,
                Luminosity = 22,
                Class = new SpectralClass() { Name = "www" }
            };
            dbContext!.Stars.Add(star1);
            dbContext.Stars.Add(star2);
            dbContext.SaveChanges();

            var tempStar = searchService!.SearchStar("1", new SpectralClass() { Name = "qqq" }, 1, 20);
            var star = tempStar[0];
            Assert.IsTrue(tempStar.Count == 1);
            Assert.AreEqual(star.Name, star1.Name);
            Assert.AreEqual(star.Luminosity, star1.Luminosity);
            Assert.AreEqual(star.Age, star1.Age);
            Assert.AreEqual(star.Class!.Name, star1.Class.Name);
            Assert.AreEqual(star.DistanceFromEarth, star1.DistanceFromEarth);

        }

        [Test]
        public void SearchGalaxyTest()
        {
            var galaxy1 = new Galaxy()
            {
                Name = "galaxy1",
                Age = 14,
                DistanceFromEarth = 9,
                Type = new GalaxyType() { Name = "qqq" }
            };
            var galaxy2 = new Galaxy()
            {
                Name = "galaxy2",
                Age = 14,
                DistanceFromEarth = 9,
                Type = new GalaxyType() { Name = "www" }
            };

            dbContext!.Galaxies.Add(galaxy1);
            dbContext.Galaxies.Add(galaxy2);
            dbContext.SaveChanges();

            var tempGalaxy = searchService!.SearchGalaxy("1", new GalaxyType() { Name = "qqq" }, 3, 15);
            var galaxy = tempGalaxy[0];

            Assert.AreEqual(galaxy1.Name, galaxy.Name);
            Assert.AreEqual(galaxy1.Age, galaxy.Age);
            Assert.AreEqual(galaxy1.DistanceFromEarth, galaxy.DistanceFromEarth);
            Assert.AreEqual(galaxy1.Type.Name, galaxy.Type!.Name);

        }
    }
}