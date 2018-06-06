using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace LaRutaDelSoftware.RestAPI.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// A global exception handler that will be used to catch any error
        /// </summary>
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, context.Exception);
        }
    }
}