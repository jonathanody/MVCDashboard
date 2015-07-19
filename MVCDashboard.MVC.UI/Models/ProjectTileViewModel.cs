using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCDashboard.UI.MVC.Helpers.Enums;

namespace MVCDashboard.UI.MVC.Models
{
	public class ProjectTileViewModel
	{
		public string TileName { get; set; }
		public string ProjectName { get; set; }
		public string Stage { get; set; }
		public string StatusCSSClass { get; set; }
		public Guid QueryId { get; set; }
	}
}