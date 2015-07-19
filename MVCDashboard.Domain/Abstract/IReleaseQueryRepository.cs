using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;

namespace MVCDashboard.Domain.Abstract
{
	public interface IReleaseQueryRepository
	{
		IEnumerable<ReleaseQuery> GetAllReleaseQueries();
	}
}
