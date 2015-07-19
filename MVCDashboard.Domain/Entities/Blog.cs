using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Entities
{
	public class Blog
	{
		public Project Project { get; set; }
		public DateTime DateCreated { get; set; }
		public string Body { get; set; }
	}
}
