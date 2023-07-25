using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using AutoMapper.Execution;
using DevExpress.Mvvm.UI.Native;
using Kr4.Model.Entities;
using Kr4.Services.Interface;
using Kr4.View;
using Kr4.View.Interface;
using Kr4.ViewModel.EditViewModels;
using Kr4.ViewModel.EditViewModels.Interface;
using Parameter = Autofac.Core.Parameter;

namespace Kr4.Services
{
    public  class EditWindowFactory : IEditWindowsFactory
    {
       

        public IEditWindow CreateEditWindow(IAstronomicalObject item)
        {
           
            if (item is Planet)
            {
                var window = new EditPlanetView();

                var parameters = new Parameter[]
                {
                    new TypedParameter(typeof(Planet), item),
                    new TypedParameter(typeof(Action), (Action)window.Close),
                };
                var viewModel = Bootstrapper.Bootstrapper.Resolve<IEditPlanetViewModel>(parameters);
                    //new EditPlanetViewModel(item as Planet, window.Close);
                window.DataContext = viewModel;
                return window;

            }
            else if (item is Star)
            {
                var window = new EditStarView();
                var parameters = new Parameter[]
                {
                    new TypedParameter(typeof(Star), item),
                    new TypedParameter(typeof(Action), (Action)window.Close),
                };
                var viewModel = Bootstrapper.Bootstrapper.Resolve<IEditStarViewModel>(parameters);
               
                window.DataContext = viewModel;
                return window;
            }
            else  
            {
                var window = new EditGalaxyView();
                var parameters = new Parameter[]
                {
                    new TypedParameter(typeof(Galaxy), item),
                    new TypedParameter(typeof(Action), (Action)window.Close),
                };
                var viewModel = Bootstrapper.Bootstrapper.Resolve<IEditGalaxyViewModel>(parameters);
                window.DataContext = viewModel;
                return window;
            }
            
        }

       
    }
}
