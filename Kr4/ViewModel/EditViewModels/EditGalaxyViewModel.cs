using DevExpress.Mvvm;
using Kr4.Model.Entities;
using Kr4.Services;
using Kr4.ViewModel.EditViewModels.Interface;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Kr4.Services.Interface;

namespace Kr4.ViewModel.EditViewModels
{
     public class EditGalaxyViewModel : ViewModelBase, IEditGalaxyViewModel
    {
        private Galaxy galaxy;
        private Galaxy defaultGalaxy;
        private Action close;

        private IMessageService messageService;


        public List<GalaxyType> GalaxyTypes
        {
            get
            {
                var galaxyType = DatabaseLocator.Context!.GalaxysTypes.ToList();
                return galaxyType;
            }
        }

        public string Name
        {
            get { return galaxy.Name; }
            set
            {
                if (galaxy.Name != value)
                {
                    galaxy.Name = value;
                    RaisePropertiesChanged(nameof(Name));
                }
            }
        }

        public GalaxyType GalaxyType
        {
            get { return galaxy.Type; }
            set
            {
                if (galaxy.Type != value)
                {
                    galaxy.Type = value;
                    RaisePropertiesChanged(nameof(GalaxyType));
                }
            }
        }

       

        public double DistanceFromEarth
        {
            get { return galaxy.DistanceFromEarth; }
            set
            {
                if (galaxy.DistanceFromEarth != value)
                {
                    galaxy.DistanceFromEarth = value;
                    RaisePropertiesChanged(nameof(DistanceFromEarth));
                }
            }
        }

        public double Age
        {
            get { return galaxy.Age; }
            set
            {
                if (galaxy.Age != value)
                {
                    galaxy.Age = value;
                    RaisePropertiesChanged(nameof(Age));
                }
            }
        }

        public EditGalaxyViewModel(Galaxy galaxy, Action close, IMessageService messageService)
        {
            this.messageService = messageService;
            TinyMapper.Bind<Galaxy, Galaxy>(config => config.Ignore(x => x.Type));
            this.galaxy = galaxy;
            defaultGalaxy = TinyMapper.Map<Galaxy>(galaxy);
            this.close = close;
        }
        public ICommand CancelChanges
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    TinyMapper.Map<Galaxy, Galaxy>(defaultGalaxy, galaxy);

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
            if (Name != "" && GalaxyType != null)
                return true;
            else
            {
                messageService.SendMessageError("Fill in required fields");
                return false;
            }
        }
    }
}

