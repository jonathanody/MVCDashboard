using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Entities
{
	public class DevelopmentDashboard
	{
		public IEnumerable<Blog> LatestBlogs { get; set; }
		public IEnumerable<Project> Projects { get; set; }
		public IEnumerable<StatusSummary> ProjectStatusSummaries { get; set; }
		public IEnumerable<StatusSummary> ReleaseStatusSummaries { get; set; }
	}
}
