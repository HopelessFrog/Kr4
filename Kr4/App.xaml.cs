using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Kr4.Bootstrapper;
using Kr4.Model;
using Kr4.Services;
using NLog;

namespace Kr4
{
   
    public partial class App 
    {
        //private Bootstrapper.Bootstrapper? _bootstrapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected override void OnStartup(StartupEventArgs e)
        {
          

            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("NLog.config");


           
            AppDomain.CurrentDomain.UnhandledException += App_DispatcherUnhandledException;
            Current.DispatcherUnhandledException += DispatcherOnUnhandledException;

            
            LogManager.Setup().LoadConfigurationFromFile("NLog.config");
            base.OnStartup(e);
            var context = new AstronomicalContext();
            context.Database.EnsureCreated();
            DatabaseLocator.Context = context;

            var window = new MainWindow();
            window.DataContext = Bootstrapper.Bootstrapper.RootVisual;

            window.Closed += Window_Closed;
            Current.Exit += Current_Exit;


         
            logger.Debug("Start\n");

            window.Show();


        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            logger.Debug("Exit");
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            Bootstrapper.Bootstrapper.Stop();
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Error(e.Exception as Exception, "Unhandled dispatcher thread exception");
        }

        private  void App_DispatcherUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
           
            logger.Error(e.ExceptionObject as Exception, "Unhandled app domain exception");

        }
    }
}
