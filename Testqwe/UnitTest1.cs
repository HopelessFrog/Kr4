using System.Drawing.Text;
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
        private AstronomicalContext dbContext;
        private IAsronomicalObjectFactoty asronomicalObjectFactoty;

        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Run();
            asronomicalObjectFactoty = Bootstrapper.Resolve<IAsronomicalObjectFactoty>();
            // Настроим тестовую базу данных SQLite в памяти
            var options = new DbContextOptionsBuilder<AstronomicalContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            dbContext = new AstronomicalContext(options);
            DatabaseLocator.Context = dbContext;

            // Создаем схему базы данных и добавляем начальные данные для тестов
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            // Очищаем тестовую базу данных и освобождаем ресурсы
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test] [STAThread]
        public void AddGalaxy_ShouldAddGalaxyToDatabase()
        {
                
           
                // Arrange
                var galaxy = new Galaxy
                {
                    Type = new GalaxyType { Name = "Spiral" },
                    Name = "Andromeda",
                    DistanceFromEarth = 2.537e6,
                    Age = 1.4e10
                };
                asronomicalObjectFactoty.AddGalaxy("QWe", 123, 123, new GalaxyType { Name = "Spiral" });
               
               
                //Assert.AreEqual(1, dbContext.Galaxies.Count());
                Assert.AreEqual("QWe", dbContext.Galaxies.Single().Name);
            
        }

        [Test]
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

        [Test]
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

        [Test]
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