using LaRutaDelSoftware.DataAccess.Interfaces;
using LaRutaDelSoftware.DomainEntities;

namespace LaRutaDelSoftware.BussinessLogic.Services
{
    public class SubjectService
    {
        private IRepository<Subject> repositorySubjects;
        public SubjectService(IRepository<Subject> repositorySubjects)
        {
            this.repositorySubjects = repositorySubjects;
        }

        public Subject GetSubject(int id)
        {
            return this.repositorySubjects.GetById(id);
        }
    }
}
