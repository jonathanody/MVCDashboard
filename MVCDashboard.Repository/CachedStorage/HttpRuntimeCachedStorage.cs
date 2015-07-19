using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Abstract;
using System.Web.Caching;
using System.Web;

namespace MVCDashboard.Repository.CachedStorage
{
	public class HttpRuntimeCachedStorage : ICacheStorage
	{
		private readonly Cache _cache;

		public HttpRuntimeCachedStorage()
		{
			_cache = HttpRuntime.Cache;
		}

		public void Add(string key, object value, DateTime absoluteExpiration)
		{
			AddToCache(key, value, absoluteExpiration);
		}

		public void AddUsingDependency(string key, object value, string dependency)
		{
			throw new NotImplementedException();
		}

		public void Remove(string key)
		{
			if (Retrieve(key) != null)
				_cache.Remove(key);			
		}

		public object Retrieve(string key)
		{
			return _cache.Get(key);
		}

		public T Retrieve<T>(string key)
		{
			T value = default(T);
			object tempoaryCachedObject = _cache.Get(key);

			if (tempoaryCachedObject != null)
			{
				value = (T)tempoaryCachedObject;
			}

			return value;
		}

		private void AddToCache(string key, object value, DateTime absoluteExpiration)
		{
			_cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
		}
	}
}
