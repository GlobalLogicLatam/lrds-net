using CommonServiceLocator;
using LaRutaDelSoftware.BussinessLogic.Services;
using LaRutaDelSoftware.DataAccess.Interfaces;
using LaRutaDelSoftware.DataAccess.Nhibernate;
using LaRutaDelSoftware.DataAccess.EntityFramework;
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

            ////NHIBERNATE
            //container.RegisterType<DataAccess.Nhibernate.UnitOfWork>(new PerRequestLifetimeManager());
            //var factoryNH = new InjectionFactory(x => x.Resolve<DataAccess.Nhibernate.UnitOfWork>());
            //container.RegisterType<IUnitOfWork>(factoryNH);
            //container.RegisterType<IRepository<User>, DataAccess.Nhibernate.Repository<User>>();
            //container.RegisterType<IRepository<Student>, DataAccess.Nhibernate.Repository<Student>>();
            //container.RegisterType<IRepository<StudentSubject>, DataAccess.Nhibernate.Repository<StudentSubject>>();
            //container.RegisterType<IRepository<Subject>, DataAccess.Nhibernate.Repository<Subject>>();

            //ENTITY FRAMEWORK
            container.RegisterType<DataAccess.EntityFramework.UnitOfWork>(new PerRequestLifetimeManager());
            var factoryEF = new InjectionFactory(x => x.Resolve<DataAccess.EntityFramework.UnitOfWork>());
            container.RegisterType<IUnitOfWork>(factoryEF);
            container.RegisterType<IRepository<User>, DataAccess.EntityFramework.Repository<User>>();
            container.RegisterType<IRepository<Student>, DataAccess.EntityFramework.Repository<Student>>();
            container.RegisterType<IRepository<StudentSubject>, DataAccess.EntityFramework.Repository<StudentSubject>>();
            container.RegisterType<IRepository<Subject>, DataAccess.EntityFramework.Repository<Subject>>();
        }
    }
}