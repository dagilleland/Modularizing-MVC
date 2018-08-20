using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvcApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Gleaned from <see cref="https://www.itprotoday.com/software-development/mvc-routing-iis-seo-and-custom-errors-oh-my"/>
    /// </remarks>
    public class ErrorController : SiteController
    {
        public ActionResult Index()
        {
            if (Response.StatusCode == 200)
                Response.StatusCode = 500;

            return View("Crash500");
        }

        public ActionResult Forbidden()
        {
            if (Response.StatusCode == 200)
                Response.StatusCode = 403;

            return View();
        }

        public ActionResult Crash()
        {
            if (Response.StatusCode == 200)
                Response.StatusCode = 500;

            return View("Crash500");
        }

        public ActionResult NotFound(string url)
        {
            return this.View("NotFound");
            // Or, the following...
            //this.View("NotFound").ExecuteResult(this.ControllerContext);
            //return new EmptyResult();

            // Note: the following can be used onced the rest of the "Inferred" is created to fulfill the "Gleaned"
            return base.ExecuteNotFound(url);
        }
    }

    #region Gleaned & Inferred
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Partially gleaned from <see cref="https://www.itprotoday.com/software-development/mvc-routing-iis-seo-and-custom-errors-oh-my"/>
    /// and adapted/inferred as seemed appropriate at the time.
    /// </remarks>
    public abstract class SiteController : Controller
    {
        private NotFoundDescriptorManager NotFoundDescriptorManager { get; set; } =
            new NotFoundDescriptorManager();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <remarks>
        /// Gleaned from <see cref="https://www.itprotoday.com/software-development/mvc-routing-iis-seo-and-custom-errors-oh-my"/>
        /// </remarks>
        internal ActionResult ExecuteNotFound(string url)
        {
            string path = url;

            NotFoundDescriptor descriptor = this.NotFoundDescriptorManager.NotFoundDescriptorByPath(path);
            if (descriptor != null)
            {
                switch (descriptor.Type)
                {
                    case NotFoundDescriptorType.Redirect:
                        return new RedirectResult(descriptor.RedirectedPath, false);
                    case NotFoundDescriptorType.RedirectPermanent:
                        return new RedirectResult(descriptor.RedirectedPath, true);
                    case NotFoundDescriptorType.Removed:
                        return View("../Error/Gone410");  // sets 410 in the view itself. 
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // deal with idiotic issues from IIS: 
            Response.TrySkipIisCustomErrors = true;

            return View("../Error/NotFound404"); // sets 404 in the view itself.
        }
        protected override void HandleUnknownAction(string actionName)
        {
            string requestedUrl = HttpContext.Request.Path;

            this.ExecuteNotFound(requestedUrl).ExecuteResult(this.ControllerContext);
        }
    }
    public enum NotFoundDescriptorType
    { Redirect, RedirectPermanent, Removed, Unknown }
    public class NotFoundDescriptorManager
    {
        internal NotFoundDescriptor NotFoundDescriptorByPath(string path)
        {
            return new NotFoundDescriptor(path, NotFoundDescriptorType.Unknown);
        }
    }
    public class NotFoundDescriptor
    {
        public string RedirectedPath { get; private set; }
        public NotFoundDescriptorType Type { get; private set; }

        public NotFoundDescriptor(string redirectedPath, NotFoundDescriptorType type)
        {
            RedirectedPath = redirectedPath;
            Type = type;
        }
    }
    #endregion

}