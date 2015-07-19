using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Messages
{
	public class DevelopmentDashboardRequest
	{
		public int NumberOfBlogs { get; set; }
		public int NumberOfProjects { get; set; }
	}
}
