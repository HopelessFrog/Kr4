using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using DevExpress.Mvvm;
using Kr4.Model.Entities;
using Kr4.Services;
using Kr4.Services.Interface;
using Kr4.ViewModel.EditViewModels.Interface;
using Nelibur.ObjectMapper;


namespace Kr4.ViewModel.EditViewModels
{
    public class EditPlanetViewModel : ViewModelBase , IEditPlanetViewModel
    {
        private Planet planet;
        private Planet defaultPlanet;
        private Action close;

        private IMessageService messageService;


        public string Name
        {
            get { return planet.Name; }
            set
            {
                if (planet.Name != value)
                {
                    planet.Name = value;
                   RaisePropertiesChanged(nameof(Name));
                }
            }
        }

        public double Size
        {
            get { return planet.Size; }
            set
            {
                if (planet.Size != value)
                {
                    planet.Size = value;
                    RaisePropertiesChanged(nameof(Size));
                }
            }
        }

        public double? OrbitalPeriod
        {
            get { return planet.OrbitalPeriod; }
            set
            {
                if (planet.OrbitalPeriod != value)
                {
                    planet.OrbitalPeriod = value;
                    RaisePropertiesChanged(nameof(OrbitalPeriod));
                }
            }
        }

        public double DistanceFromEarth
        {
            get { return planet.DistanceFromEarth; }
            set
            {
                if (planet.DistanceFromEarth != value)
                {
                    planet.DistanceFromEarth = value;
                    RaisePropertiesChanged(nameof(DistanceFromEarth));
                }
            }
        }

        public double Age
        {
            get { return planet.Age; }
            set
            {
                if (planet.Age != value)
                {
                    planet.Age = value;
                    RaisePropertiesChanged(nameof(Age));
                }
            }
        }

        public EditPlanetViewModel(Planet planet, Action close, IMessageService messageService)
        {
            this.messageService = messageService;
            TinyMapper.Bind<Planet, Planet>();
            this.planet = planet;
            defaultPlanet = TinyMapper.Map<Planet>(planet);
            this.close = close;
        }
        public ICommand CancelChanges
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    TinyMapper.Map<Planet, Planet>(defaultPlanet, planet);
                    
                    CloseWindow();
                });

            }
        }
        public ICommand SaveChanges
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    CloseWindow();
                });

            }
        }


        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public Action Close { get; set; }

        public bool CanClose()
        {
            if(Name != "")
                return true;
            else
            {
                messageService.SendMessageError("Fill in required fields");
                return false;
            }
        }
    }
}
