using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;

namespace MVCDashboard.Service
{
	public class ReleaseQueryService : IReleaseQueryService
	{
		private readonly ICachedReleaseQueryRepository _cachedReleaseQueryRepository;

		public ReleaseQueryService(ICachedReleaseQueryRepository cachedReleaseQueryRepository)
		{
			_cachedReleaseQueryRepository = cachedReleaseQueryRepository;
		}

		public Releases GetAllReleaseQueries()
		{
			Releases releaseQueries = _cachedReleaseQueryRepository.RetrieveCachedReleaseQueries();

			return releaseQueries;
		}

		public void ClearReleaseQueriesFromCache()
		{
			_cachedReleaseQueryRepository.RemoveCachedReleaseQueries();
		}
	}
}
