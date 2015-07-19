using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Messages;

namespace MVCDashboard.Domain.Abstract
{
	public interface IDevelopmentService
	{
		DevelopmentDashboard GetDevelopmentDashboardData(DevelopmentDashboardRequest request);
		IEnumerable<Project> GetRandomProjects(int numberOfProjects);
	}
}
