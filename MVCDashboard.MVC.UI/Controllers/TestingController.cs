using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCDashboard.UI.MVC.Models;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.UI.MVC.Helpers.ExtensionMethods;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Messages;

namespace MVCDashboard.UI.MVC.Controllers
{
	public class TestingController : Controller
	{
		private readonly ITestingService _testingService;

		public TestingController(ITestingService blogService)
		{
			_testingService = blogService;
		}

		//
		// GET: /Testing/
		public ActionResult Index()
		{
			TestingDashboard dashboard = _testingService.GetTestingDashboardData(
				new TestingDashboardRequest
				{
					NumberOfBlogs = 4,
					NumberOfProjects = 6
				});						

			TestingSummaryViewModel testingSummaryViewModel = dashboard.ConvertToViewModel();

			return View(testingSummaryViewModel);
		}

		public JsonResult GetProjectStatus()
		{
			ProjectSummary projectSummary = _testingService.GetRandomProject().ConvertToViewModel();

			return Json(projectSummary);
		}
	}
}
