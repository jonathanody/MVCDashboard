using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;

namespace MVCDashboard.UI.MVC.Models
{
	public class ReleaseDashboardViewModel
	{
		public string Title { get; set; }
		public Highcharts StatePieChart { get; set; }
		public Highcharts PassedTestingChart { get; set; }
		public Highcharts TriageStatusChart { get; set; }
		public IEnumerable<DefectWorkItemViewModel> NonTriagedWorkItems { get; set; }
	}
}