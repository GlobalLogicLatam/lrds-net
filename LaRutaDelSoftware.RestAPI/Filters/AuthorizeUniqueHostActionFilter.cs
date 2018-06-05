using LaRutaDelSoftware.BussinessLogic.Services;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LaRutaDelSoftware.RestAPI.Filters
{
    public class AuthorizeUniqueHostActionFilter : AuthorizationFilterAttribute
    {
        private UserService userService { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string sessionToken = actionContext.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrWhiteSpace(sessionToken))
            {
                userService = (UserService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(UserService));
                DomainEntities.User user = userService.GetUser(sessionToken);

                if (user == null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
                else
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(sessionToken), new string[] { });
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnAuthorization(actionContext);
        }
    }
}