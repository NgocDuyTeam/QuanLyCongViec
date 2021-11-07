using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace QL.API.Http
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class QLApiControlle : ApiController
    {
        protected HttpResponseMessage HttpOk(object result)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }
        protected HttpResponseMessage HttpBadRequest(object data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, data, Configuration.Formatters.JsonFormatter);
        }

        protected HttpResponseMessage HttpNotFound(object data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound, data, Configuration.Formatters.JsonFormatter);
        }

        protected HttpResponseMessage HttpForbidden(object data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden, data, Configuration.Formatters.JsonFormatter);
        }

        protected HttpResponseMessage HttpNotImplemented(object data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotImplemented, data, Configuration.Formatters.JsonFormatter);
        }
        protected HttpResponseMessage HttpInternalServerError(object data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError, data, Configuration.Formatters.JsonFormatter);
        }
    }
}