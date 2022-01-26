using Caliburn.Micro;
using FRMDesktopUI.Helpers;
using FRMDesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FRMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        protected override void Configure() // the container holds an instance of itself to pass out when people ask for simple container. Test
        {
            _container.Instance(_container);
            _container // Singleton is roughly same as static class
                .Singleton<IWindowManager, WindowManager>()  // Bringing windows in and out
                .Singleton<IEventAggregator, EventAggregator>() // Pass event messaging throughout our application. Clearinghouse of all events.
                .Singleton<IAPIHelper, APIHelper>(); // It will have that HTTP Client active and ready for us to use.

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType)); //Used Reflection. It takes models that end with "ViewModel" such as ShellViewModel
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
