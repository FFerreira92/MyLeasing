using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Helpers
{
    public class NotFoundViewResult : ViewResult
{

        public NotFoundViewResult(string viewName)
        {
                ViewName = viewName;
                StatusCode = (int)HttpStatusCode.NotFound;
        }



    }
}
