using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Entities
{
	public class ReleaseWorkItem
	{
		public string State { get; set; }		
		public string Severity { get; set; }
		public DateTime PassedIntegrationTestingDate { get; set; }
	}
}
