using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Abstract;

namespace MVCDashboard.Repository.Mock
{
	public class MockBlogRepository : IBlogRepository
	{
		public IEnumerable<Blog> RetrieveLatestBlogs(int numberOfBlogs)
		{
			IEnumerable<Blog> blogs = MockDataGenerator.GenerateBlogs(numberOfBlogs);

			return blogs;
		}
	}
}
