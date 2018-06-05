using LaRutaDelSoftware.DataAccess.Interfaces;
using System.Linq;

namespace LaRutaDelSoftware.DataAccess.EntityFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private UnitOfWork _unitOfWork;

        public Repository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       

        public IQueryable<T> GetAll()
        {
            return _unitOfWork.GetSetOf<T>().AsQueryable();
        }

        public T GetById(object id)
        {
            return _unitOfWork.GetSetOf<T>().Find(id);
        }

        public void Create(T entity)
        {
            _unitOfWork.GetSetOf<T>().Add(entity);
        }

        public void Update(T entity)
        {
            //nothing, because context works with attached entities
            return;
        }

        public void Delete(object id)
        {
            T entity = this.GetById(id);
            _unitOfWork.GetSetOf<T>().Remove(entity);
        }
    }
}
