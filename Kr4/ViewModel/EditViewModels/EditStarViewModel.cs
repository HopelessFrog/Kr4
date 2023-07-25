using DevExpress.Mvvm;
using Kr4.Model.Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Kr4.Services;
using Kr4.ViewModel.EditViewModels.Interface;
using Kr4.Services.Interface;

namespace Kr4.ViewModel.EditViewModels
{
    public class EditStarViewModel : ViewModelBase, IEditStarViewModel
    {
        private Star star;
        private Star defaultStar;
        private Action close;

        private IMessageService messageService;

        public List<SpectralClass> SpectralClasses
        {
            get
            {
                var spectralClasses = DatabaseLocator.Context.SpectralClasses.ToList();
                return spectralClasses;
            }
        }
        public string Name
        {
            get { return star.Name; }
            set
            {
                if (star.Name != value)
                {
                    star.Name = value;
                    RaisePropertiesChanged(nameof(Name));
                }
            }
        }

        public double? Luminosity
        {
            get { return star.Luminosity; }
            set
            {
                if (star.Luminosity != value)
                {
                    star.Luminosity = value;
                    RaisePropertiesChanged(nameof(Luminosity));
                }
            }
        }

        public SpectralClass SpectralClass
        {
            get { return star!.Class; }
            set
            {
                if( star.Class!= value)
                {
                    star.Class = value;
                    RaisePropertiesChanged(nameof(SpectralClass));
                }
            }
        }

        public double DistanceFromEarth
        {
            get { return star.DistanceFromEarth; }
            set
            {
                if (star.DistanceFromEarth != value)
                {
                    star.DistanceFromEarth = value;
                    RaisePropertiesChanged(nameof(DistanceFromEarth));
                }
            }
        }

        public double Age
        {
            get { return star.Age; }
            set
            {
                if (star.Age != value)
                {
                    star.Age = value;
                    RaisePropertiesChanged(nameof(Age));
                }
            }
        }

        public EditStarViewModel(Star star, Action close, IMessageService messageService)
        {
            this.messageService = messageService;
            TinyMapper.Bind<Star, Star>(config => config.Ignore(x => x.Class));
            TinyMapper.Bind<SpectralClass,SpectralClass>();
            this.star = star;
            defaultStar = TinyMapper.Map<Star>(star);
            defaultStar.Class =  star.Class;
            this.close = close;
        }
        public ICommand CancelChanges
        {
            get
            {
                SpectralClass tempClass;
                return new DelegateCommand(() =>
                {
                    TinyMapper.Map<Star, Star>(defaultStar, star);
                    star.Class = defaultStar.Class;

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
            if(Name != "" && SpectralClass != null)
                return true;
            else
            {
                messageService.SendMessageError("Fill in required fields");
                return false;
            }
        }
    }
}

