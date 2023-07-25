using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Core;
using Kr4.Model;
using Kr4.Services;
using Kr4.Services.Interface;
using Kr4.ViewModel;
using Kr4.ViewModel.EditViewModels.Interface;

namespace Kr4.Bootstrapper
{
    public static class Bootstrapper
    {
        private static ILifetimeScope? rootScope;

        public static IMainViewModel RootVisual
        {
            get
            {
                if (rootScope == null)
                   Run();
                return rootScope!.Resolve<IMainViewModel>();
            }
        }
        public static void  Run()
        {
          if(rootScope != null)
              return;
          rootScope = ContainerPreparer.PrepareContainer();


        }

        public static void Stop()
        {
            rootScope?.Dispose();
        }

        public static T Resolve<T>(Parameter[] parameters) where T : notnull
        {
            if (rootScope == null) throw new Exception("Bootstrapper hasn't been started!");

            return rootScope.Resolve<T>(parameters);
        }

        public static T Resolve<T>() where T : notnull
        {
            if (rootScope == null) throw new Exception("Bootstrapper hasn't been started!");

            return rootScope.Resolve<T>();
        }
    }
}
