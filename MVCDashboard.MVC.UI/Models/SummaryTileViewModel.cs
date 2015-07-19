using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDashboard.UI.MVC.Models
{
	public class SummaryTileViewModel
	{
		public string Name { get; set; }
		public int Count { get; set; }
		public string BackgroundCSSClass { get; set; }
	}
}