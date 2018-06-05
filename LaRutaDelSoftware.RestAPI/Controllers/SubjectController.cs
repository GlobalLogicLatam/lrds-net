using LaRutaDelSoftware.BussinessLogic.Services;
using LaRutaDelSoftware.DomainEntities;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace LaRutaDelSoftware.RestAPI.Controllers
{
    [RoutePrefix("user")]
    public class SubjectController : ApiController
    {
        private StudentSubjectService studentSubjectService;
        private StudentService studentService;
        public SubjectController(StudentSubjectService studentSubjectService, StudentService studentService)
        {
            this.studentSubjectService = studentSubjectService;
            this.studentService = studentService;
        }

        [HttpGet]
        [Route("{user_id}/materias")]
        [ResponseType(typeof(IEnumerable<Subject>))]
        public IHttpActionResult GetMaterias(int user_id)
        {
            Student student = this.studentService.GetStudent(user_id);
            List<Subject> result = studentSubjectService.GetAllOfUser(student);
            return Ok(result);
        }

        /// <summary>
        /// Obtener el estado de la materia para un alumno en particular
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="materia_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{user_id}/materia/{materia_id}")]
        [ResponseType(typeof(StudentSubject))]
        public IHttpActionResult GetMateria(int user_id, int materia_id)
        {
            Student student = this.studentService.GetStudent(user_id);
            StudentSubject subjectStatus = this.studentSubjectService.GetSubjectStatus(student, materia_id);

            return Ok(subjectStatus);
        }


        /// <summary>
        /// Inscribir o desincribir a un alumno en una materia
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="materia_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{user_id}/materia/{materia_id}")]
        public IHttpActionResult PostMateria(int user_id, int materia_id)
        {
            Student student = this.studentService.GetStudent(user_id);

            var statusSubject = this.studentSubjectService.GetSubjectStatus(student, materia_id);
            if (statusSubject == null)
                this.studentSubjectService.RegisterStudentToSubject(student, materia_id);
            else
                this.studentSubjectService.UnregisterStudentToSubject(student, materia_id);

            return Ok();
        }

    }
}
