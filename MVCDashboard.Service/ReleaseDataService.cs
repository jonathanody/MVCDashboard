using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Service.Helpers;

namespace MVCDashboard.Service
{
	public class ReleaseDataService : IReleaseDataService
	{
		private readonly IReleaseWorkItemRepository _releaseWorkItemRepository;
		private readonly DataAggregator _releaseDataAggregator;

		public ReleaseDataService(IReleaseWorkItemRepository releaseWorkItemRepository)
		{
			_releaseWorkItemRepository = releaseWorkItemRepository;
			_releaseDataAggregator = new DataAggregator();
		}

		public ReleaseDashboard GetReleaseDataFor(Guid queryId)
		{
			ReleaseDashboardDTO releaseDashboardDTO = _releaseWorkItemRepository.RetrieveReleaseDataFor(queryId);

			ReleaseDashboard releaseDashboard = new ReleaseDashboard();

			releaseDashboard.DashboardTitle = string.Format("Release: {0}", releaseDashboardDTO.ReleaseVersion);
			releaseDashboard.StateChartValues =
				_releaseDataAggregator.CalculateStatePieChartValues(releaseDashboardDTO.ReleaseWorkItems);
			releaseDashboard.PassedTestingChartValues =
				_releaseDataAggregator.CalculatePassedTestingLineChartValues(releaseDashboardDTO.ReleaseWorkItems);
			releaseDashboard.TriagedStatusChartValues =
				_releaseDataAggregator.CalculateTraiageStateBarChartValues(releaseDashboardDTO.DefectWorkItems);
			releaseDashboard.NonTriagedWorkItems =
				_releaseDataAggregator.GetNonTriagedDefectWorkItems(releaseDashboardDTO.DefectWorkItems);

			return releaseDashboard;
		}
	}
}
