using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Messages;
using MVCDashboard.Service.Helpers;

namespace MVCDashboard.Service
{
	public class DevelopmentService : IDevelopmentService
	{
		private readonly IReleaseRepository _releaseRepository;
		private readonly IProjectRepository _projectRepositry;
		private readonly IBlogRepository _blogRepository;
		private readonly DataAggregator _dataAggregator;

		public DevelopmentService(IBlogRepository blogRepository, IReleaseRepository releaseRepository, 
			IProjectRepository projectRepository)
		{
			_blogRepository = blogRepository;
			_releaseRepository = releaseRepository;
			_projectRepositry = projectRepository;
			
			_dataAggregator = new DataAggregator();
		}

		public DevelopmentDashboard GetDevelopmentDashboardData(DevelopmentDashboardRequest request)
		{
			DevelopmentDashboard dashboard = new DevelopmentDashboard();

			dashboard.LatestBlogs = _blogRepository.RetrieveLatestBlogs(request.NumberOfBlogs);
			dashboard.Projects = _projectRepositry.RetrieveProjects(request.NumberOfProjects);
			dashboard.ProjectStatusSummaries = _dataAggregator.CalculateStatusSummaries(_projectRepositry.RetrieveAllProjects());
			dashboard.ReleaseStatusSummaries = _dataAggregator.CalculateStatusSummaries(_releaseRepository.RetrieveAllReleases());

			return dashboard;
		}

		public IEnumerable<Project> GetRandomProjects(int numberOfProjects)
		{
			IEnumerable<Project> projects = _projectRepositry.RetrieveRandomProjects(numberOfProjects);

			return projects;
		}
	}
}
