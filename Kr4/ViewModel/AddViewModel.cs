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
        private IAsronomicalObjectFactoty asronomicalObjectFactoty;

        public AddViewModel(IMessageService messageService, IAsronomicalObjectFactoty asronomicalObjectFactoty)
        {
            this.messageService = messageService;
            this.asronomicalObjectFactoty = asronomicalObjectFactoty;
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

       

        public ICommand Add
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    switch (SelectedTab)
                    {

                        case AddPlanet:
                            asronomicalObjectFactoty.AddPlanet(this.Name,this.DistanceFromEarth,this.Age,this.OrbitalPeriod,this.Size) ;
                            
                            break;
                        case AddStar:
                            asronomicalObjectFactoty.AddStar(this.Name, this.DistanceFromEarth, this.Age, this.SpectralClass, this.Luminosity);

                            break;
                        case AddSpectralClass:
                           ChangedList = asronomicalObjectFactoty.AddSpectralClass(this.Name, SpectralClasses);
                            

                            break;
                        case AddGalaxy:
                            asronomicalObjectFactoty.AddGalaxy(this.Name,this.DistanceFromEarth, this.Age,this.GalaxyType);
                           

                            break;
                        case AddGalaxyType:
                            ChangedList = asronomicalObjectFactoty.AddGakaxyType(this.Name, GalaxyTypes);

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
