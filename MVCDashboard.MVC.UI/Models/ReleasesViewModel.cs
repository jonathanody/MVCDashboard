using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDashboard.UI.MVC.Models
{
	public class ReleasesViewModel
	{
		public IEnumerable<ReleaseQueryViewModel> ReleaseQueries { get; set; }
		public string TimeCached { get; set; }
	}
}