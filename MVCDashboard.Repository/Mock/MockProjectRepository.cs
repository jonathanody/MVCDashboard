using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Repository.Mock.Helpers;

namespace MVCDashboard.Repository.Mock
{
	public class MockProjectRepository : IProjectRepository
	{
		public IEnumerable<Project> RetrieveProjects(int numberOfProjects)
		{
			IEnumerable<Project> projects = MockDataGenerator.GenerateProjects(numberOfProjects, 
				ReleaseType.Project);

			return projects;
		}

		public IEnumerable<Project> RetrieveAllProjects()
		{
			IEnumerable<Project> projects = MockDataGenerator.GenerateProjects(20, ReleaseType.Project);

			return projects;
		}
	

		public IEnumerable<Project> RetrieveRandomProjects(int numberOfProjects)
		{
			IEnumerable<Project> project = MockDataGenerator.GenerateProjects(numberOfProjects, ReleaseType.Project);

			return project;
		}
	}
}
