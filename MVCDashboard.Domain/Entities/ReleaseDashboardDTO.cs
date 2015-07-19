using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;

namespace MVCDashboard.Domain.Entities
{
	public class ReleaseDashboardDTO
	{
		public string ReleaseVersion { get; set; }
		public IEnumerable<ReleaseWorkItem> ReleaseWorkItems { get; set; }
		public IEnumerable<DefectWorkItem> DefectWorkItems { get; set; }
	}
}
