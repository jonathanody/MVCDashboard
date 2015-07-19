using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Domain.Entities;

namespace MVCDashboard.Repository
{
	public class CachedReleaseQueryRepository : ICachedReleaseQueryRepository
	{
		private readonly string _cachedReleaseQueriesKey = "ReleaseCachedQueries";
		private readonly double _hoursToCacheReleaseQueriesFor = 6;
		private readonly ICacheStorage _cacheStorage;
		private readonly IReleaseQueryRepository _releaseQueryRepository;

		public CachedReleaseQueryRepository(ICacheStorage cacheStorage, 
			IReleaseQueryRepository releaseQueryRepository)
		{
			_cacheStorage = cacheStorage;
			_releaseQueryRepository = releaseQueryRepository;
		}

		public Releases RetrieveCachedReleaseQueries()
		{
			Releases cachedReleaseQueries = _cacheStorage.Retrieve<Releases>(_cachedReleaseQueriesKey);

			if (cachedReleaseQueries == null)
			{
				cachedReleaseQueries = new Releases();

				cachedReleaseQueries.ReleaseQueries = _releaseQueryRepository.GetAllReleaseQueries();
				cachedReleaseQueries.TimeCached = DateTime.Now;

				InsertReleaseQueriesIntoCache(cachedReleaseQueries);
			}

			return cachedReleaseQueries;
		}

		public void RemoveCachedReleaseQueries()
		{
			_cacheStorage.Remove(_cachedReleaseQueriesKey);
		}

		private void InsertReleaseQueriesIntoCache(Releases releaseQueriesToCache)
		{
			_cacheStorage.Add(_cachedReleaseQueriesKey, releaseQueriesToCache,
				DateTime.Now.AddHours(_hoursToCacheReleaseQueriesFor));
		}
	}
}
