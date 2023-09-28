using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Kr4.Model.Entities;
using Kr4.Services;
using System.Windows.Input;
using Kr4.Services.Interface;
using Kr4.ViewModel.EditViewModels.Interface;

namespace Kr4.ViewModel
{
    public class AddViewModel : ViewModelBase , IAddViewModel
    {
        
        private const int AddPlanet = 0;
        private const int AddStar = 1;
        private const int AddSpectralClass = 2;
        private const int AddGalaxy = 3;
        private const int AddGalaxyType = 4;


        private string ChangedList = "";

        private IMessageService messageService;

        public AddViewModel(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public int SelectedTab { get; set; }
        public string Name { get; set; } = null!;
        public double Size { get; set; }

        public double OrbitalPeriod { get; set; }

        public double DistanceFromEarth { get; set; }

        public double Age { get; set; }

        public double Luminosity { get; set; }

        public SpectralClass SpectralClass { get; set; } = null!;

        public GalaxyType GalaxyType { get; set; } = null!;


        private void clearFields()
        {
            Name = string.Empty;
            Size = 0;
            OrbitalPeriod = 0;
            DistanceFromEarth = 0;
            Age = 0;
            Luminosity = 0;
            
        }
        public List<GalaxyType> GalaxyTypes
        {
            get
            {
                var galaxyType = DatabaseLocator.Context!.GalaxysTypes.ToList();
                return galaxyType;
            }
            
        }

        public List<SpectralClass> SpectralClasses
        {
            get
            {
                var spectralClasses = DatabaseLocator.Context!.SpectralClasses.ToList();
                return spectralClasses;
            }
        }

        public void AddOnePlanet(string name, double distance, double age, double orbitalPeriod, double size)
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

        public void AddOneStar(string name, double distance, double age, SpectralClass spectralClass, double luminosity)
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

        public void AddOneSpectralClass(string name)
        {
            if (name != "")
            {
                DatabaseLocator.Context!.SpectralClasses.Add(new SpectralClass() { Name = name });
                ChangedList = nameof(SpectralClasses);
                messageService.SendMessage("Object created successfully");
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name");
                return;

            }
        }

        public void  AddOneGalaxy(string name, double distance, double age, GalaxyType galaxyType)
        {
            if (name != "" && galaxyType != null)
            {
                DatabaseLocator.Context!.Galaxies.Add(new Galaxy()
                {
                    Name = name,
                    Type =galaxyType,
                    DistanceFromEarth = distance,
                    Age = age
                });
                messageService.SendMessage("Object created successfully");
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name and Type");
                return;
            }
        }

        public void AddOneGakaxyType(string name)
        {
            if (name != "")
            {
                ChangedList = nameof(GalaxyTypes);
                DatabaseLocator.Context!.GalaxysTypes.Add(new GalaxyType() { Name = name });
                messageService.SendMessage("Object created successfully");
            }
            else
            {
                messageService.SendMessageError("Fill out the required field Name");
                return;
            }
        }

        public ICommand Add
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    switch (SelectedTab)
                    {

                        case AddPlanet:
                            AddOnePlanet(this.Name,this.DistanceFromEarth,this.Age,this.OrbitalPeriod,this.Size);
                            
                            break;
                        case AddStar:
                            AddOneStar(this.Name, this.DistanceFromEarth, this.Age, this.SpectralClass, this.Luminosity);

                            break;
                        case AddSpectralClass:
                            AddOneSpectralClass(this.Name);
                            

                            break;
                        case AddGalaxy:
                            AddOneGalaxy(this.Name,this.DistanceFromEarth, this.Age,this.GalaxyType);
                           

                            break;
                        case AddGalaxyType:
                            AddOneGakaxyType(this.Name);

                            break;
                    }
                   
                    DatabaseLocator.Context!.SaveChanges();
                    AddOne?.Invoke(this,EventArgs.Empty);
                 if(ChangedList != "")
                    RaisePropertiesChanged(ChangedList);
                 clearFields();
                });
            }

        }


        public event EventHandler? AddOne;
    }
}
