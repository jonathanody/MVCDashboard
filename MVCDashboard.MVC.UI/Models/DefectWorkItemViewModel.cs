using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDashboard.UI.MVC.Models
{
	public class DefectWorkItemViewModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string CreateDate { get; set; }
		public string RagStatus { get; set; }
	}
}