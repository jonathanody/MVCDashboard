using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCDashboard.UI.MVC.Models;
using MVCDashboard.UI.MVC.Helpers.ExtensionMethods;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Messages;

namespace MVCDashboard.UI.MVC.Controllers
{
	public class DevelopmentController : Controller
	{
		private readonly IDevelopmentService _developmentService;

		public DevelopmentController(IDevelopmentService developmentService)
		{
			_developmentService = developmentService;
		}

		//
		// GET: /Development/
		public ActionResult Index()
		{			
			DevelopmentDashboard dashboard = _developmentService.GetDevelopmentDashboardData(
				new DevelopmentDashboardRequest
				{
					NumberOfBlogs = 4,
					NumberOfProjects = 6
				});

			DevelopmentSummaryViewModel developmentSummaryViewModel = dashboard.ConvertToViewModel();

			return View(developmentSummaryViewModel);
		}

		public PartialViewResult ProjectStatusUpdate()
		{
			int numberOfProjects = 6;
			IEnumerable<ProjectTileViewModel> projectTiles = _developmentService.GetRandomProjects(numberOfProjects).ConvertToViewModel();

			return PartialView(projectTiles);
		}
	}
}
