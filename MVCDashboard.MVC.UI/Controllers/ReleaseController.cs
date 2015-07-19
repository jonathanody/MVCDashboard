using System;
using System.Web.Mvc;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using MVCDashboard.UI.MVC.Models;
using MVCDashboard.UI.MVC.ActionFilters;
using MVCDashboard.UI.MVC.Helpers.ExtensionMethods;
using StructureMap;

namespace MVCDashboard.UI.MVC.Controllers
{
	public class ReleaseController : Controller
	{
		private IReleaseDataService _releaseDataService;
		private IReleaseQueryService _releaseQueryService;

		public ReleaseController(IReleaseDataService releaseDataService, IReleaseQueryService releaseQuerySerivce)
		{
			_releaseDataService = releaseDataService;
			_releaseQueryService = releaseQuerySerivce;
		}

		//
		// GET: /Release/		
		public ActionResult Index(string queryId)
		{			
			Releases releases = _releaseQueryService.GetAllReleaseQueries();

			ReleasesViewModel releasesViewModel = releases.ConvertToViewModel();

			return View(releasesViewModel);
		}

		//
		// GET: /Release/DisplayDashboard
		[EnableAutomaticRefresh(RefreshPeriodInSeconds = 5)]
		public ActionResult DisplayDashboard(string queryId)
		{			
			ReleaseDashboard releaseDashboard = _releaseDataService.GetReleaseDataFor(new Guid(queryId));

			ReleaseDashboardViewModel releaseDashboardViewModel = releaseDashboard.ConvertToViewModel();			

			return View(releaseDashboardViewModel);
		}

		//
		// GET: /Release/RefreshAndDisplayReleaseQueries
		public ActionResult RefreshAndDisplayReleaseQueries()
		{			
			_releaseQueryService.ClearReleaseQueriesFromCache();			

			return RedirectToAction("Index");
		}
	}
}
