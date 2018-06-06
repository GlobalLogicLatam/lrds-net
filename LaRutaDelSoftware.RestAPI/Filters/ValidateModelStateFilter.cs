using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LaRutaDelSoftware.RestAPI.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                IEnumerable<string> listOfErrors = actionContext.ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
                string errorMessage = string.Join(" - ", listOfErrors);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}