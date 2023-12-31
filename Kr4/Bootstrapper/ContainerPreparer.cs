﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Kr4.Services;
using Kr4.Services.Interface;
using Kr4.ViewModel;
using Kr4.ViewModel.EditViewModels;
using Kr4.ViewModel.EditViewModels.Interface;

namespace Kr4.Bootstrapper
{
    public static class ContainerPreparer
    {
        private static void Prepare (ContainerBuilder builder )
        {
            RegisterViewModels(builder);
            RegisterServices(builder);
            RegisterFactorys(builder);

        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<SettingService>().As<ISettingService>();
            builder.RegisterType<ExcelExportService>().As<IExcelExportService>();
            builder.RegisterType<FileDialogService>().As<IFileDialogService>();
            builder.RegisterType<RemoveFromDbService>().As<IRemoveFromDbService>();
            builder.RegisterType<SearchService>().As<ISearchService>();
        }

        private static void RegisterFactorys(ContainerBuilder builder)
        {
            builder.RegisterType<EditWindowFactory>().As<IEditWindowsFactory>();
            builder.RegisterType<AstronomicalObjectFactoty>().As<IAsronomicalObjectFactoty>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<AddViewModel>().As<IAddViewModel>();
            builder.RegisterType<EditGalaxyViewModel>().As<IEditGalaxyViewModel>();
            builder.RegisterType<EditPlanetViewModel>().As<IEditPlanetViewModel>();
            builder.RegisterType<EditStarViewModel>().As<IEditStarViewModel>();
        }

        public static IContainer PrepareContainer()
        {
            var builder = new ContainerBuilder();
            Prepare(builder);
            return builder.Build();
        }

    }
}

