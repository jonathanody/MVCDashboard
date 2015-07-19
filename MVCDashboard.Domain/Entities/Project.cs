using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Entities
{
	public class Project
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Stage { get; set; }
		public string Status { get; set; }
	}
}
