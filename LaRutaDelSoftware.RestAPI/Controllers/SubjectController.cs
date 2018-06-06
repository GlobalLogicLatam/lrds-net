using LaRutaDelSoftware.BussinessLogic.Services;
using LaRutaDelSoftware.DomainEntities;
using LaRutaDelSoftware.RestAPI.Filters;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace LaRutaDelSoftware.RestAPI.Controllers
{
    [RoutePrefix("user")]
    [AuthorizeActionFilter]
    public class SubjectController : ApiController
    {
        private StudentSubjectService studentSubjectService;
        private StudentService studentService;
        private SubjectService subjectService;
        public SubjectController(StudentSubjectService studentSubjectService, StudentService studentService, SubjectService subjectService)
        {
            this.studentSubjectService = studentSubjectService;
            this.studentService = studentService;
            this.subjectService = subjectService;
        }

        [HttpGet]
        [Route("{user_id}/materias")]
        [ResponseType(typeof(IEnumerable<Subject>))]
        public IHttpActionResult GetMaterias(int user_id)
        {
            Student student = this.studentService.GetStudent(user_id);
            if (student == null)
                return Content(HttpStatusCode.NotFound, "Alumno no encontrado");
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
            if (student == null)
                return Content(HttpStatusCode.NotFound, "Alumno no encontrado");

            StudentSubject subjectStatus = this.studentSubjectService.GetSubjectStatus(student, materia_id);
            if (subjectStatus == null)
                return Content(HttpStatusCode.NotFound, "Par Alumno-Materia no encontrado");

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
            if (student == null)
                return Content(HttpStatusCode.NotFound, "Alumno no encontrado");

            Subject subject = this.subjectService.GetSubject(materia_id);
            if (subject == null)
                return Content(HttpStatusCode.NotFound, "Materia no encontrada");

            string resultMessage = "";
            var statusSubject = this.studentSubjectService.GetSubjectStatus(student, materia_id);
            if (statusSubject == null || statusSubject.Registered == false)
            {
                this.studentSubjectService.RegisterStudentToSubject(student, materia_id);
                resultMessage = "Inscripto ok!";
            }
            else
            {
                this.studentSubjectService.UnregisterStudentToSubject(student, materia_id);
                resultMessage = "Desinscripto ok!";
            }

            return Ok(resultMessage);
        }

    }
}
