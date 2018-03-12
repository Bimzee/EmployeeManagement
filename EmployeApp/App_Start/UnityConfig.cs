using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using DAL.Abstract;
using DAL.Concrete;

namespace EmployeApp
{
    public static class UnityConfig
    {
        public static IUnityContainer container;
        public static void RegisterComponents()
        {
			 container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEmployeeRepository, EFEmployeeRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}