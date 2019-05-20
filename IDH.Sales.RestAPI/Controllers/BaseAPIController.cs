using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace IDH.Sales.RestAPI.Controllers
{
    public class BaseAPIController : ApiController
    {
        public Guid UserID
        {
            get
            {
                return Guid.Parse(Request.Headers.GetValues("UserID").First());
            }
        }
    }
}
