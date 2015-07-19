using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Entities
{
	public class Releases
	{
		public IEnumerable<ReleaseQuery> ReleaseQueries { get; set; }
		public DateTime TimeCached { get; set; }
	}
}
