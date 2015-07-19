using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Repository.Mock.Helpers;

namespace MVCDashboard.Repository.Mock
{
	public class MockReleaseRepository : IReleaseRepository
	{
		public IEnumerable<Project> RetrieveReleases(int numberOfReleases)
		{
			IEnumerable<Project> releases = MockDataGenerator.GenerateProjects(numberOfReleases, 
				ReleaseType.Release);

			return releases;
		}

		public IEnumerable<Project> RetrieveAllReleases()
		{
			IEnumerable<Project> releases = MockDataGenerator.GenerateProjects(20, ReleaseType.Release);

			return releases;
		}
	}
}
