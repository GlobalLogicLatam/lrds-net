using CommonServiceLocator;
using LaRutaDelSoftware.BussinessLogic.Services;
using LaRutaDelSoftware.DataAccess.Interfaces;
using LaRutaDelSoftware.DataAccess.Nhibernate;
using LaRutaDelSoftware.DomainEntities;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.ServiceLocation;
using Unity.WebApi;

namespace LaRutaDelSoftware.RestAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            container.RegisterType<UserService>();
            container.RegisterType<StudentService>();
            container.RegisterType<StudentSubjectService>();

            //NHIBERNATE
            container.RegisterType<UnitOfWork>(new PerRequestLifetimeManager());
            var factory = new InjectionFactory(x => x.Resolve<UnitOfWork>());
            container.RegisterType<IUnitOfWork>(factory);
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<Student>, Repository<Student>>();
            container.RegisterType<IRepository<StudentSubject>, Repository<StudentSubject>>();
            container.RegisterType<IRepository<Subject>, Repository<Subject>>();
        }
    }
}