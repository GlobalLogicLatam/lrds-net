using LaRutaDelSoftware.DataAccess.Interfaces;
using LaRutaDelSoftware.DomainEntities;
using System.Collections.Generic;
using System.Linq;

namespace LaRutaDelSoftware.BussinessLogic.Services
{
    public class StudentSubjectService
    {
        private IRepository<StudentSubject> repositorySubjectStudents;
        private IRepository<Subject> repositorySubjects;
        public StudentSubjectService(IRepository<StudentSubject> repositorySubjectStudents, IRepository<Subject> repositorySubjects)
        {
            this.repositorySubjectStudents = repositorySubjectStudents;
            this.repositorySubjects = repositorySubjects;
        }

        public List<Subject> GetAllOfUser(Student student)
        {
            List<Subject> result = this.repositorySubjectStudents.GetAll().Where(s =>
                s.Student.Id == student.Id).
                Select(x => x.Subject).ToList();
            return result;
        }

        public StudentSubject GetSubjectStatus(Student student, int subjectId)
        {
            StudentSubject result = this.repositorySubjectStudents.GetAll().SingleOrDefault(x => x.Subject.Id == subjectId && x.Student.Id == student.Id);
            return result;
        }

        public void RegisterStudentToSubject(Student student, int subjectId)
        {
            Subject subject = this.repositorySubjects.GetById(subjectId);
            this.repositorySubjectStudents.Create(new StudentSubject
            {
                Student = student,
                Subject = subject,
                Registered = true,
            });
        }

        public void UnregisterStudentToSubject(Student student, int subjectId)
        {
            StudentSubject subjectRecord = this.repositorySubjectStudents.GetAll().SingleOrDefault(
                x => x.Student.Id == student.Id && x.Subject.Id == subjectId);
            subjectRecord.Registered = false;
            this.repositorySubjectStudents.Update(subjectRecord);
        }
    }
}
