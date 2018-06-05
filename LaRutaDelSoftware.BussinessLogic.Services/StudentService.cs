using LaRutaDelSoftware.DataAccess.Interfaces;
using LaRutaDelSoftware.DomainEntities;
using System.Linq;

namespace LaRutaDelSoftware.BussinessLogic.Services
{
    public class StudentService
    {
        private UserService userService;
        private IRepository<Student> repositoryStudents;

        public StudentService(UserService userService, IRepository<Student> repositoryStudents)
        {
            this.userService = userService;
            this.repositoryStudents = repositoryStudents;
        }

        public void CreateStudent(Student newStudent)
        {
            this.userService.CreateUser(newStudent.User);
            this.repositoryStudents.Create(newStudent);
        }

        public Student GetStudent(string userName)
        {
            Student student = repositoryStudents.GetAll().SingleOrDefault(s => s.User.UserName == userName);

            return student;
        }
        public Student GetStudent(int userId)
        {
            Student student = repositoryStudents.GetById(userId);

            return student;
        }

    }
}
