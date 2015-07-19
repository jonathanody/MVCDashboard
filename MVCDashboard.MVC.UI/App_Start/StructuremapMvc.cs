using System.Web.Mvc;
using StructureMap;
using MVCDashboard.MVC.UI;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MVCDashboard.UI.MVC.App_Start.StructuremapMvc), "Start")]

namespace MVCDashboard.UI.MVC.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}