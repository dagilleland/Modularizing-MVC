using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RouteArea("Hub", AreaPrefix = "App")]
    [Route("{action=Proxy}")]
    public class AppController : Controller
    {
        // GET: App
        [Route("{nameOfArea}/{nameOfController}/{nameOfAction}/{*payload}")]
        public ActionResult Proxy(string nameOfArea, string nameOfController, string nameOfAction, string payload)
        {
            var obj = new SampleRouteInfo
            {
                NameOfArea = nameOfArea,
                NameOfController = nameOfController,
                NameOfAction = nameOfAction
            };
            if (!string.IsNullOrWhiteSpace(payload))
            {
                if (payload.Split('/').Length > 1)
                {
                    var pairs = payload.Split('/');
                    for (int index = 1; index < pairs.Length; index++)
                        obj.OtherValues.Add(pairs[index - 1], pairs[index]);
                }
                else
                {
                    obj.Id = payload;
                }
            }

            bool found = true;

            if (!found)
            {
                // send off to the ErrorController???
            }
            return View(obj);
        }

        public ActionResult ViewRegistrations()
        {
            var model = MvcApplication.RegisteredMvcModules;

            return View(model);
        }
    }
}