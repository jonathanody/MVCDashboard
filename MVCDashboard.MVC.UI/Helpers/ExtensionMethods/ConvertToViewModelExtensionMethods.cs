using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCDashboard.UI.MVC.Models;
using MVCDashboard.UI.MVC.Helpers.Enums;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Enums;

namespace MVCDashboard.UI.MVC.Helpers.ExtensionMethods
{
	public static class ConvertToViewModelExtensionMethods
	{
		public static IEnumerable<DefectWorkItemViewModel> ConvertToViewModels(
			this IEnumerable<DefectWorkItem> defectWorkItems)
		{
			IEnumerable<DefectWorkItemViewModel> defectWorkItemViewModels =
				from workItem in defectWorkItems
				select new DefectWorkItemViewModel
				{
					Id = workItem.Id.ToString(),
					Title = workItem.Title,
					CreateDate = workItem.CreatedDate.ToShortDateString(),
					RagStatus = CalculateRAGStatus(workItem.CreatedDate)
				};

			return defectWorkItemViewModels;
		}
		
		private static string CalculateRAGStatus(DateTime createdDate)
		{
			//TODO Use enums here
			DateTime now = DateTime.Now;
			string ragStatus = "success";

			if (now.AddDays(-3) > createdDate)
			{
				ragStatus = "error";
			}
			else if (now.AddDays(-1) > createdDate)
			{
				ragStatus = "warning";
			}

			return ragStatus;
		}

		public static ReleasesViewModel ConvertToViewModel(
			this Releases releases)
		{
			ReleasesViewModel releasesViewModel = new ReleasesViewModel();

			releasesViewModel.ReleaseQueries = releases.ReleaseQueries.ConvertToViewModels();
			releasesViewModel.TimeCached = releases.TimeCached.ToString("dd/MM/yy@hh:mm:ss");

			return releasesViewModel;
		}

		private static IEnumerable<ReleaseQueryViewModel> ConvertToViewModels(
			this IEnumerable<ReleaseQuery> releaseQueries)
		{
			IEnumerable<ReleaseQueryViewModel> releaseQueryViewModels =
				releaseQueries.OrderByDescending(r => r.Name)
							  .Select(r => new ReleaseQueryViewModel
							  {
								  Id = r.Id.ToString(),
								  Name = r.Name
							  });

			return releaseQueryViewModels;
		}
		
		public static ReleaseDashboardViewModel ConvertToViewModel(
			this ReleaseDashboard releaseDashboard)
		{
			HighchartsChartBuilder highchartsChartBuilder = new HighchartsChartBuilder();

			ReleaseDashboardViewModel releaseDashboardViewModel = new ReleaseDashboardViewModel();

			releaseDashboardViewModel.Title = releaseDashboard.DashboardTitle;
			releaseDashboardViewModel.StatePieChart =
				highchartsChartBuilder.GenerateStateChart(releaseDashboard.StateChartValues);
			releaseDashboardViewModel.PassedTestingChart =
				highchartsChartBuilder.GeneratePassedTestingChart(releaseDashboard.PassedTestingChartValues);
			releaseDashboardViewModel.TriageStatusChart =
				highchartsChartBuilder.GenerateTriageStatusChart(releaseDashboard.TriagedStatusChartValues);
			releaseDashboardViewModel.NonTriagedWorkItems =
				releaseDashboard.NonTriagedWorkItems.ConvertToViewModels();

			return releaseDashboardViewModel;
		}

		public static IEnumerable<BlogTileViewModel> ConvertToViewModel(
			this IEnumerable<Blog> blogs)
		{
			IEnumerable<BlogTileViewModel> blogTiles =
				from blog in blogs
				select new BlogTileViewModel
				{
					ProjectName = blog.Project.Name,
					DateCreated = blog.DateCreated.ToString(),
					Body = blog.Body
				};

			return blogTiles;
		}

		public static IEnumerable<ProjectTileViewModel> ConvertToViewModel(
			this IEnumerable<Project> projects)
		{
			int count = projects.Count();
			IList<ProjectTileViewModel> projectTiles = new List<ProjectTileViewModel>();

			for (int i = 0; i < count; i++)
			{
				Project project = projects.ElementAt(i);
				projectTiles.Add(new ProjectTileViewModel
				{
					ProjectName = project.Name,
					Stage = project.Stage,
					TileName = string.Format("projectTile{0}", i),
					StatusCSSClass = ConvertStatusToCSSBackgroundColour(project.Status),
					QueryId = project.Id
				});
			}

			return projectTiles;
		}

		public static ProjectSummary ConvertToViewModel(this Project project)
		{
			ProjectSummary projectSummary = new ProjectSummary
			{
				Name = project.Name,
				Stage = project.Stage,
				Status = project.Status
			};

			return projectSummary;
		}

		public static IEnumerable<SummaryTileViewModel> ConvertToViewModel(
			this IEnumerable<StatusSummary> summaries)
		{
			IEnumerable<SummaryTileViewModel> summaryTiles =
				from summary in summaries
				select new SummaryTileViewModel
				{
					Name = summary.Name,
					Count = summary.Count,
					BackgroundCSSClass = ConvertStatusToCSSBackgroundColour(summary.Name)
				};

			return summaryTiles;
		}

		private static string ConvertStatusToCSSBackgroundColour(string status)
		{
			string statusCSSClass = StatusCSSBackgroundColor.Alert;

			if (status == Status.OK)
			{
				statusCSSClass = StatusCSSBackgroundColor.OK;
			}
			else if (status == Status.Warning)
			{
				statusCSSClass = StatusCSSBackgroundColor.Warning;
			}

			return statusCSSClass;
		}

		public static TestingSummaryViewModel ConvertToViewModel(this TestingDashboard dashboard)
		{
			TestingSummaryViewModel testingSummaryViewModel = new TestingSummaryViewModel
			{
				ProjectTiles = dashboard.Projects.ConvertToViewModel(),
				BlogTiles = dashboard.LatestBlogs.ConvertToViewModel(),
				ReleaseSummaryTiles = dashboard.ReleaseStatusSummaries.ConvertToViewModel(),
				ReleaseCount = dashboard.ReleaseStatusSummaries.Sum(releaseStatus => releaseStatus.Count),
				ProjectSummaryTiles = dashboard.ProjectStatusSummaries.ConvertToViewModel(),
				ProjectCount = dashboard.ProjectStatusSummaries.Sum(statusSummary => statusSummary.Count)
			};

			return testingSummaryViewModel;
		}

		public static DevelopmentSummaryViewModel ConvertToViewModel(this DevelopmentDashboard dashboard)
		{
			DevelopmentSummaryViewModel developmentSummaryViewModel = new DevelopmentSummaryViewModel
			{
				BlogTiles = dashboard.LatestBlogs.ConvertToViewModel(),
				ProjectSummaryTiles = dashboard.ProjectStatusSummaries.ConvertToViewModel(),
				ProjectCount = dashboard.ProjectStatusSummaries.Sum(projectSummary => projectSummary.Count),
				ReleaseSummaryTiles = dashboard.ReleaseStatusSummaries.ConvertToViewModel(),
				ReleaseCount = dashboard.ReleaseStatusSummaries.Sum(releaseSummary => releaseSummary.Count)
			};

			return developmentSummaryViewModel;
		}
	}
}