﻿using Autofac;
using System;
using Xamarin.EmguCV.Services.Navigation;

namespace Xamarin.EmguCV.ViewModels.Base
{
    public class ViewModelLocator
    {
        IContainer container;
        ContainerBuilder containerBuilder;

        public ViewModelLocator()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();

            containerBuilder.RegisterType<FeatureDetectionViewModel>();
            containerBuilder.RegisterType<HomeViewModel>();
            containerBuilder.RegisterType<MainViewModel>();
            containerBuilder.RegisterType<MenuViewModel>();
        }

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Build() => container = containerBuilder.Build();
    }
}