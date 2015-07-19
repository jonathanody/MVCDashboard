using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;
using System.Xml.Linq;
using System.Web;

namespace MVCDashboard.Repository.Mock
{
	public class MockReleaseQueryRepository : IReleaseQueryRepository
	{
		public IEnumerable<ReleaseQuery> GetAllReleaseQueries()
		{
			XDocument data = XDocument.Load(HttpContext.Current.Server.MapPath(MockDataXmlFileLocationStore.ReleaseQueries));

			IEnumerable<ReleaseQuery> releaseQueries =
				from releaseQuery in data.Descendants("ReleaseQuery")
				select new ReleaseQuery
				{
					Id = Guid.Parse(releaseQuery.Element("Id").Value),
					Name = releaseQuery.Element("Name").Value
				};			

			return releaseQueries;
		}
	}
}
