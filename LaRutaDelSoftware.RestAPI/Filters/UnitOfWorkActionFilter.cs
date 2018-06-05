using LaRutaDelSoftware.DataAccess.Interfaces;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LaRutaDelSoftware.RestAPI.Filters
{
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            UnitOfWork = actionContext.Request.GetDependencyScope().GetService(typeof(IUnitOfWork)) as IUnitOfWork;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            UnitOfWork = actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            try
            {
                if (actionExecutedContext.Exception == null)
                {
                    // commit if no exceptions
                    UnitOfWork.Save();
                }
                else
                {
                    // rollback if exception
                    UnitOfWork.Rollback();
                }
            }
            catch (Exception ex)
            {
                //nothing for now
                throw;
            }
        }
    }
}