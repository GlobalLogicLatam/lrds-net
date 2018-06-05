using System;

namespace LaRutaDelSoftware.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        void Rollback();
    }
}
