using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Entities
{
	public class ReleaseDashboard
	{
		public string DashboardTitle { get; set; }
		public IEnumerable<ChartValue> StateChartValues { get; set; }
		public IEnumerable<ChartValue> PassedTestingChartValues { get; set; }
		public IEnumerable<ChartValue> TriagedStatusChartValues { get; set; }
		public IEnumerable<DefectWorkItem> NonTriagedWorkItems { get; set; }
	}
}
