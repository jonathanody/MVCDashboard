using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Messages;
using MVCDashboard.Service.Helpers;

namespace MVCDashboard.Service
{
	public class TestingService : ITestingService
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IProjectRepository _projectRepository;
		private readonly IReleaseRepository _releaseRepository;
		private readonly DataAggregator _dataAggregator;

		public TestingService(IBlogRepository blogRepository, IProjectRepository projectRepository, 
			IReleaseRepository releaseRepository)
		{
			_blogRepository = blogRepository;
			_projectRepository = projectRepository;
			_releaseRepository = releaseRepository;
			_dataAggregator = new DataAggregator();
		}				

		public TestingDashboard GetTestingDashboardData(TestingDashboardRequest request)
		{
			TestingDashboard dashboard = new TestingDashboard();

			dashboard.LatestBlogs = _blogRepository.RetrieveLatestBlogs(request.NumberOfBlogs);
			dashboard.Projects = _projectRepository.RetrieveProjects(request.NumberOfProjects);
			dashboard.ProjectStatusSummaries = _dataAggregator.CalculateStatusSummaries(_projectRepository.RetrieveAllProjects());
			dashboard.ReleaseStatusSummaries = _dataAggregator.CalculateStatusSummaries(_releaseRepository.RetrieveAllReleases());

			return dashboard;
		}

		public Project GetRandomProject()
		{
			int numberOfProjects = 1;
			Project project = _projectRepository.RetrieveRandomProjects(numberOfProjects).First();

			return project;
		}
	}
}
