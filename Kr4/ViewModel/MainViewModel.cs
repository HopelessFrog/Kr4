using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Kr4.Model;
using Kr4.Model.Entities;
using Kr4.Services;
using Kr4.Services.Interface;
using Kr4.View;
using Kr4.ViewModel.EditViewModels;
using Kr4.ViewModel.EditViewModels.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kr4.ViewModel
{
    public class MainViewModel : ViewModelBase , IMainViewModel
    {

        private const int PlanetsTab = 0;
        private const int StarsTab = 1;
        private const int GalaxiesTab = 2;

       

        private IEditWindowsFactory editWindowsFactory;
        private IMessageService messageService;
        private ISettingService settingService;

        public ObservableCollection<Planet> Planets { get; set; }
        public ObservableCollection<Star> Stars { get; set; }
        public ObservableCollection<Galaxy> Galaxies { get; set; }

        //private ICommand openAddWindowCommand;
        //public ICommand OpenAddWindowCommand
        //{
        //    get => openAddWindowCommand;
        //    set => openAddWindowCommand = value;
        //}


        public MainViewModel(IEditWindowsFactory editWindowsFactory, IMessageService messageService, ISettingService settingService)
        {


            DatabaseLocator.Context!.Planets.Add(new Planet()
                { Age = 12, DistanceFromEarth = 34, Name = "qweqwe", OrbitalPeriod = 33, Size = 77 });
            DatabaseLocator.Context!.Planets.Add(new Planet()
                { Age = 99, DistanceFromEarth = 34, Name = "eee", OrbitalPeriod = 33, Size = 77 });
            DatabaseLocator.Context.Planets.Add(new Planet()
                { Age = 23, DistanceFromEarth = 34, Name = "rttt", OrbitalPeriod = 33, Size = 77 });
            DatabaseLocator.Context.GalaxysTypes.Add(new GalaxyType() { Name = "iiii" });
            DatabaseLocator.Context.GalaxysTypes.Add(new GalaxyType() { Name = "mmmm" });
            DatabaseLocator.Context.Stars.Add(new Star()
            {
                Name = "qwe", Age = 123, Class = new SpectralClass() { Name = "111" }, DistanceFromEarth = 33,
                Luminosity = 90
            });
            DatabaseLocator.Context.Stars.Add(new Star()
            {
                Name = "qwe",
                Age = 123,
                Class = new SpectralClass() { Name = "122211" },
                DistanceFromEarth = 33,
                Luminosity = 90
            });
            DatabaseLocator.Context.SaveChanges();
            
            Planets = new ObservableCollection<Planet>(DatabaseLocator.Context.Planets.ToList());
            Stars = new ObservableCollection<Star>(DatabaseLocator.Context.Stars.ToList());
            Galaxies = new ObservableCollection<Galaxy>(DatabaseLocator.Context.Galaxys.ToList());

            this.editWindowsFactory = editWindowsFactory;
            this.messageService = messageService;
            this.settingService = settingService;

            Planets.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Planets.AddRange(e.NewItems.Cast<Planet>());
                else if  (e.OldItems != null)
                    DatabaseLocator.Context.Planets.RemoveRange(e.OldItems.Cast<Planet>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Galaxy item in e!.NewItems)
                    {
                        DatabaseLocator.Context.Galaxys.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();

            };
            Stars.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Stars.AddRange(e.NewItems.Cast<Star>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Stars.RemoveRange(e.OldItems.Cast<Star>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Star item in e.NewItems)
                    {
                        DatabaseLocator.Context.Stars.Entry(item).State = EntityState.Modified;
                    }
                }
                DatabaseLocator.Context.SaveChanges();

            };
            Galaxies.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                    DatabaseLocator.Context.Galaxys.AddRange(e.NewItems.Cast<Galaxy>());
                else if (e.OldItems != null)
                    DatabaseLocator.Context.Galaxys.RemoveRange(e.OldItems.Cast<Galaxy>());
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (Galaxy item in e!.NewItems)
                    {
                        DatabaseLocator.Context.Galaxys.Entry(item).State = EntityState.Modified;
                    }
                }
               
                DatabaseLocator.Context.SaveChanges();

              


            };
            greetingsSwitch = settingService.Greetings;
            if (greetingsSwitch)
              messageService.SendMessage("Hey, have a good day");

        }

        public IAstronomicalObject SelectedObject { get; set; }

        private bool greetingsSwitch;
        public bool GreetingsSwitch
        {
            get
            {
                return settingService.Greetings;
            }
            set
            {
                if (value != greetingsSwitch)
                {
                    greetingsSwitch = value;
                    settingService.Greetings = greetingsSwitch;
                    RaisePropertiesChanged(nameof(GreetingsSwitch));
                }
            }
        }

        public int MinAge { get; set; } = 0;
        public int MaxAge { get; set; } = 99999;
        public int SelectedTab { get; set; }

        public string SearchBar { get; set; }

        public List<GalaxyType> GalaxyTypes
        {
            get
            {
                var galaxyType = DatabaseLocator.Context.GalaxysTypes.ToList();
                galaxyType.Insert(0, new GalaxyType() { Name = "none" });
                return galaxyType;
            }
        }

        public List<SpectralClass> SpectralClasses
        {
            get
            {
                var spectralClasses = DatabaseLocator.Context.SpectralClasses.ToList();
                spectralClasses.Insert(0, new SpectralClass() { Name = "none" });
                return spectralClasses;
               
            }
        }

        public GalaxyType GalaxyTypeEnter { get; set; }
        public SpectralClass SpectralClassEntered { get; set; }

        public ICommand LoadSpectralClasses
        {
            get { return new DelegateCommand(() =>
            {
                RaisePropertiesChanged(nameof(SpectralClasses));
            }); }
        }

        public ICommand LoadGalaxyTypes
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    RaisePropertiesChanged(nameof(GalaxyTypes));
                });
                
            }
        }

        public ICommand Change
        {
            get
            {
             
                return new DelegateCommand(() =>
                {
                    editWindowsFactory.CreateEditWindow(SelectedObject).Show();

                });

            }
        }

        public ICommand Delete
        {
            get
            {

                return new DelegateCommand(() =>
                {
                    if (SelectedObject is Planet)
                        Planets.Remove(SelectedObject as Planet);
                    else if (SelectedObject is Galaxy)
                       Galaxies.Remove(SelectedObject as Galaxy);
                    else if (SelectedObject is Star)
                        Stars.Remove(SelectedObject as Star);
                    else
                       messageService.SendMessage("Select an object to delete");
                   
                   


                });

            }
        }


        public ICommand Search
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    switch (SelectedTab)
                    {
                        case PlanetsTab:
                            var planets = DatabaseLocator.Context.Planets.AsQueryable();
                            List<Expression<Func<Planet, bool>>> conditionsPlanets = new List<Expression<Func<Planet, bool>>>();
                            if(SearchBar != "" && SearchBar != null)
                                conditionsPlanets.Add(p => p.Name.Contains(SearchBar));
                            if(MinAge != 0)
                                conditionsPlanets.Add(p => p.Age > MinAge);
                            if(MaxAge != 99999)
                                conditionsPlanets.Add(p => p.Age < MaxAge);
                            foreach (var item in conditionsPlanets)
                            {
                                planets = planets.Where(item);
                            }
                            Planets = new ObservableCollection<Planet>(planets.ToList());
                            break;
                        case StarsTab:
                            var stars = DatabaseLocator.Context.Stars.AsQueryable();
                            List<Expression<Func<Star, bool>>> conditionsStars = new List<Expression<Func<Star, bool>>>();
                            if(SpectralClassEntered != null && SpectralClassEntered.Name != "none"  )
                                conditionsStars.Add(s => s.Class.Name == SpectralClassEntered.Name);
                            if (SearchBar != "" && SearchBar != null)
                                conditionsStars.Add(p => p.Name.Contains(SearchBar));
                            if (MinAge != 0)
                                conditionsStars.Add(p => p.Age > MinAge);
                            if (MaxAge != 99999)
                                conditionsStars.Add(p => p.Age < MaxAge);
                            foreach (var item in conditionsStars)
                            {
                                stars = stars.Where(item);
                            }
                            Stars = new ObservableCollection<Star>(stars.ToList());
                            break;
                        case GalaxiesTab:
                            var galaxies = DatabaseLocator.Context!.Galaxys.AsQueryable();
                            List<Expression<Func<Galaxy, bool>>> conditionsGalaxies = new List<Expression<Func<Galaxy, bool>>>();
                            if (GalaxyTypeEnter != null && GalaxyTypeEnter.Name != "none" )
                                conditionsGalaxies.Add(g => g.Type.Name == GalaxyTypeEnter.Name);
                            if (SearchBar != "" && SearchBar != null)
                                conditionsGalaxies.Add(p => p.Name.Contains(SearchBar));
                            if (MinAge != 0)
                                conditionsGalaxies.Add(p => p.Age > MinAge);
                            if (MaxAge != 99999)
                                conditionsGalaxies.Add(p => p.Age < MaxAge);
                            foreach (var item in conditionsGalaxies)
                            {
                                galaxies = galaxies.Where(item);
                            }
                            Galaxies = new ObservableCollection<Galaxy>(galaxies.ToList());
                            break;

                    }
                       
                   
                });
            }
        }

        public ICommand OpenAdd
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var window = new AddWindow();
                    window.DataContext = Bootstrapper.Bootstrapper.Resolve<IAddViewModel>();
                    window.ShowDialog();
                });
            }
        }

    }
}
