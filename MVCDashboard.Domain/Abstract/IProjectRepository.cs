using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;

namespace MVCDashboard.Domain.Abstract
{
	public interface IProjectRepository
	{
		IEnumerable<Project> RetrieveProjects(int numberOfProjects);
		IEnumerable<Project> RetrieveAllProjects();
		IEnumerable<Project> RetrieveRandomProjects(int numberOfProjects);
	}
}
