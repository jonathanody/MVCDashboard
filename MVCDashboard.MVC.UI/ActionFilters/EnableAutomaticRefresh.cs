using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDashboard.UI.MVC.ActionFilters
{
	public class EnableAutomaticRefresh : ActionFilterAttribute
	{
		public int RefreshPeriodInSeconds { get; set; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			//filterContext.Controller.ViewBag.EnableRefresh = new HtmlString("http-equiv=\"refresh\" content=\"5\"");
			filterContext.Controller.ViewBag.EnableRefresh = new HtmlString(string.Format("http-equiv=\"refresh\" content=\"{0}\"", RefreshPeriodInSeconds));
		}
	}
}