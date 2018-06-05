using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LaRutaDelSoftware.DataAccess.Interfaces;
using NHibernate;
using System.Configuration;

namespace LaRutaDelSoftware.DataAccess.Nhibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private

        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        private ISession _session { get; set; }

        static UnitOfWork()
        {
            string schema = ConfigurationManager.AppSettings["DataBase"];

            _sessionFactory = Fluently.Configure()
                            .Database(OracleClientConfiguration.Oracle10.ConnectionString(x =>
                                x.FromConnectionStringWithKey(schema)).Driver<LoggerClientDriver>())
                            .Mappings(map => map.FluentMappings.AddFromAssemblyOf<UnitOfWork>())
                            .BuildSessionFactory();
        }

        #endregion Private

        public ISession Session { get { return _session; } }
        public UnitOfWork()
        {
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        #region IUnitOfWork

        public void Save()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
        }

        public void Rollback()
        {
            if (_transaction != null && _transaction.IsActive)
                _transaction.Rollback();
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        #endregion IUnitOfWork
    }
}
