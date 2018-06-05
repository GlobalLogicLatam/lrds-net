using LaRutaDelSoftware.BussinessLogic.Services;
using LaRutaDelSoftware.DomainEntities;
using LaRutaDelSoftware.RestAPI.Filters;
using LaRutaDelSoftware.RestAPI.Models;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace LaRutaDelSoftware.RestAPI.Controllers
{
    [RoutePrefix("user")]
    public class LoginController : ApiController
    {
        private UserService userService;
        private StudentService studentService;

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
            string sessionToken = Guid.NewGuid().ToString();
            User user = this.userService.GetUser(login.UserName, login.Password);
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
            return Ok();
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
            
            return Ok(student);
        }

    }
}
