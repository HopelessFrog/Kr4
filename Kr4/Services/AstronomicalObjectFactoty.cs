using Kr4.Model.Entities;
using Kr4.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Services
{
    public class AstronomicalObjectFactoty : IAsronomicalObjectFactoty
    {
        private IMessageService messageService;

        public AstronomicalObjectFactoty(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public void AddPlanet(string name, double distance, double age, double orbitalPeriod, double size)
        {
            if (name != "")
            {
                DatabaseLocator.Context!.Planets.Add(new Planet()
                {
                    Name = name,
                    DistanceFromEarth = distance,
                    Age = age,
                    OrbitalPeriod = orbitalPeriod,
                    Size = size
                });
                messageService.SendMessage("Object created successfully");
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name");
                return;
            }
        }

        public void AddStar(string name, double distance, double age, SpectralClass spectralClass, double luminosity)
        {
            if (name != "" && spectralClass != null)
            {
                DatabaseLocator.Context!.Stars.Add(new Star()
                {
                    Name = name,
                    Age = age,
                    DistanceFromEarth = distance,
                    Class = spectralClass,
                    Luminosity = luminosity
                });
                messageService.SendMessage("Object created successfully");
                
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name and Spectral Class");
                return;
            }
        }

        public string AddSpectralClass(string name, List<SpectralClass> spectralClasses)
        {
            if (name != "")
            {
                DatabaseLocator.Context!.SpectralClasses.Add(new SpectralClass() { Name = name });
              
                messageService.SendMessage("Object created successfully");

                return nameof(spectralClasses);
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name");

                return "";

            }
        }

        public void AddGalaxy(string name, double distance, double age, GalaxyType galaxyType)
        {
            if (name != "" && galaxyType != null)
            {
                DatabaseLocator.Context!.Galaxies.Add(new Galaxy()
                {
                    Name = name,
                    Type = galaxyType,
                    DistanceFromEarth = distance,
                    Age = age
                });
                DatabaseLocator.Context!.SaveChanges();
                messageService.SendMessage("Object created successfully");
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name and Type");
                return;
            }
        }

        public string AddGakaxyType(string name, List<GalaxyType> galaxyTypes)
        {
            if (name != "")
            {
                return nameof(galaxyTypes);
                DatabaseLocator.Context!.GalaxysTypes.Add(new GalaxyType() { Name = name });
                messageService.SendMessage("Object created successfully");
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name");
                return "";
            }
        }
    }
}
