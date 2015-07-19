using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCDashboard.Domain.Abstract
{
	public interface ICacheStorage
	{
		void Add(string key, object value, DateTime absoluteExpiration);
		void AddUsingDependency(string key, object value, string dependency);

		void Remove(string key);

		object Retrieve(string key);
		T Retrieve<T>(string key);
	}
}
