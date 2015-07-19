using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDashboard.UI.MVC.Models
{
	public class DevelopmentSummaryViewModel
	{
		public IEnumerable<BlogTileViewModel> BlogTiles { get; set; }
		public IEnumerable<SummaryTileViewModel> ProjectSummaryTiles { get; set; }
		public IEnumerable<SummaryTileViewModel> ReleaseSummaryTiles { get; set; }
		public int ProjectCount { get; set; }
		public int ReleaseCount { get; set; }
	}
}