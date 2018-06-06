using LaRutaDelSoftware.BussinessLogic.Services;
using LaRutaDelSoftware.DomainEntities;
using LaRutaDelSoftware.RestAPI.Filters;
using LaRutaDelSoftware.RestAPI.Models;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace LaRutaDelSoftware.RestAPI.Controllers
{
    [RoutePrefix("user")]
    public class LoginController : ApiController
    {
        private UserService userService;
        private StudentService studentService;
        private static ConcurrentDictionary<string, int> userFailedAttempts = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<string, DateTime> userFailedAttemptDate = new ConcurrentDictionary<string, DateTime>();

        public LoginController(UserService userService, StudentService studentService)
        {
            this.userService = userService;
            this.studentService = studentService;
        }

        /// <summary>
        /// Ingresar al sistema con usuario ya creado. 
        /// En caso de exito la aplicacion devolvera un header con un token asosiado a la conexion del usuario.
        /// </summary>
        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(LoginReply))]
        public IHttpActionResult Login(LoginRequest login)
        {
            //control de intentos
            int counter = 0;
            userFailedAttempts.TryGetValue(login.UserName, out counter);
            if (counter == 3)
            {
                DateTime dateUntilSuspend;
                userFailedAttemptDate.TryGetValue(login.UserName, out dateUntilSuspend);
                if (dateUntilSuspend > DateTime.Now)
                {
                    return Content(HttpStatusCode.Unauthorized, "Suspendido temporalmente");
                }
                else
                {
                    userFailedAttemptDate.TryRemove(login.UserName, out dateUntilSuspend);
                }

            }

            User user = this.userService.GetUser(login.UserName, login.Password);

            if (user == null)//"Credenciales inválidas"
            {
                userFailedAttempts.AddOrUpdate(login.UserName, 1, (key, oldValue) => oldValue + 1);
                userFailedAttempts.TryGetValue(login.UserName, out counter);
                //acaba de llegar a 3
                if (counter == 3)
                {
                    var dateUntilSuspend = DateTime.Now.AddMinutes(1);
                    userFailedAttemptDate.AddOrUpdate(login.UserName, dateUntilSuspend, (key, oldvalue) => dateUntilSuspend);
                }
                return Content(HttpStatusCode.Forbidden, "Usuario o contraseña incorrectos");
            }

            string sessionToken = Guid.NewGuid().ToString();
            this.userService.Login(user, sessionToken);

            var reply = new LoginReply
            {
                Token = sessionToken
            };

            return Ok(reply);
        }

        /// <summary>
        /// Permite enviar la peticion de terminar la sesion actualmente activa.
        /// </summary>
        [HttpPost]
        [Route("logout")]
        [AuthorizeActionFilter]
        public IHttpActionResult Logout()
        {
            User currentUser = this.userService.GetCurrentUser();
            this.userService.Logout(currentUser);
            return Ok("Deslogueado");
        }


        /// <summary>
        /// Obtener los datos del usuario a partir del UserName
        /// </summary>
        [HttpGet]
        [Route("{userName}")]
        [AuthorizeActionFilter]
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetUser(string userName)
        {
            Student student = this.studentService.GetStudent(userName);
            if (student == null)
                return Content(HttpStatusCode.NotFound, "Alumno no encontrado");

            return Ok(student);
        }

    }
}
