using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using System.Xml.Linq;

namespace MVCDashboard.Repository.Mock
{
	public class MockReleaseWorkItemRepository : IReleaseWorkItemRepository
	{
		public ReleaseDashboardDTO RetrieveReleaseDataFor(Guid queryGuid)
		{
			ReleaseDashboardDTO releaseDashboardDTO = new ReleaseDashboardDTO();

			releaseDashboardDTO.ReleaseVersion = "Mock";
			releaseDashboardDTO.ReleaseWorkItems = LoadMockReleaseWorkItems(MockDataXmlFileLocationStore.ReleaseWorkItems);
			releaseDashboardDTO.DefectWorkItems = LoadMockDefectWorkItems(MockDataXmlFileLocationStore.DefectWorkItems);

			return releaseDashboardDTO;
		}

		private IEnumerable<ReleaseWorkItem> LoadMockReleaseWorkItems(string fileLocation)
		{
			XDocument data = XDocument.Load(HttpContext.Current.Server.MapPath(fileLocation));

			IEnumerable<ReleaseWorkItem> dashboardWorkItems =
				from workItem in data.Descendants("ReleaseWorkItem")
				select new ReleaseWorkItem
				{
					State = workItem.Element("State").Value,				
					Severity = workItem.Element("Severity").Value,
					PassedIntegrationTestingDate = DateTime.Parse(workItem.Element("PassedIntegrationTestingDate").Value)
				};

			return dashboardWorkItems;
		}

		private IEnumerable<DefectWorkItem> LoadMockDefectWorkItems(string fileLocation)
		{
			XDocument defectWorkItemsXmlData = XDocument.Load(HttpContext.Current.Server.MapPath(fileLocation));

			IEnumerable<DefectWorkItem> defectWorkItems =
				from workItem in defectWorkItemsXmlData.Descendants("DefectWorkItem")
				select new DefectWorkItem
				{
					Id = int.Parse(workItem.Element("Id").Value),
					Title = workItem.Element("Title").Value,
					Triaged = workItem.Element("Triaged").Value,
					CreatedDate = DateTime.Parse(workItem.Element("CreatedDate").Value)
				};

			return defectWorkItems;
		}
	}
}
