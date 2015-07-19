using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;
using MVCDashboard.Domain.Enums;
using MVCDashboard.Repository.Mock.Helpers;

namespace MVCDashboard.Repository.Mock
{
	public class MockDataGenerator
	{
		private static Random _random = new Random();
		private static readonly string _loremIpsum =
			@"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
			Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
			Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, 
			sunt in culpa qui officia deserunt mollit anim id est laborum.";

		public static IEnumerable<Blog> GenerateBlogs(int numberOfBlogs)
		{
			IList<Blog> blogs = new List<Blog>();

			for (int i = 0; i < numberOfBlogs; i++)
			{
				blogs.Add(new Blog
				{
					Project = new Project { Name = GenerateProjectName() },
					DateCreated = DateTime.Now,
					Body = _loremIpsum
				});
			}

			return blogs;
		}

		public static IEnumerable<Project> GenerateProjects(int numberOfProjects, ReleaseType type)
		{
			IList<Project> projects = new List<Project>();

			for (int i = 0; i < numberOfProjects; i++)
			{
				projects.Add(new Project
				{
					Id = Guid.NewGuid(),
					Name = type == ReleaseType.Project ? GenerateProjectName() : GenerateReleaseName(),
					Stage = GenerateTestingStage(),
					Status = GenerateStatus()
				});
			}

			return projects;
		}

		private static string GenerateProjectName()
		{
			return string.Format("Project {0}", _random.Next(1, 99));
		}

		private static string GenerateReleaseName()
		{
			return string.Format("Release {0}", _random.Next(1, 99));
		}

		private static string GenerateStatus()
		{
			int selector = _random.Next() % 3;

			switch (selector)
			{
				case 0:
					return Status.Alert;
				case 1:
					return Status.Warning;
				case 2:
					return Status.OK;
				default:
					return Status.Alert;
			}
		}

		private static string GenerateTestingStage()
		{
			int selector = _random.Next() % 5;

			switch (selector)
			{
				case 0:
					return TestingStage.ToBeResourced;
				case 1:
					return TestingStage.TestDesign;
				case 2:
					return TestingStage.SystemTesting;
				case 3:
					return TestingStage.IntegrationTesting;
				case 4:
					return TestingStage.Released;
				default:
					return TestingStage.Unknown;
			}
		}
	}
}
