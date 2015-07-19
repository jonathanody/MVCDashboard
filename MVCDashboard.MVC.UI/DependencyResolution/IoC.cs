using StructureMap;
using MVCDashboard.Domain.Abstract;
using MVCDashboard.Repository.CachedStorage;
using MVCDashboard.Repository;
using MVCDashboard.Repository.Mock;
using MVCDashboard.Service;

namespace MVCDashboard.MVC.UI {
	public static class IoC {
		public static IContainer Initialize() {
			ObjectFactory.Initialize(x =>
						{
							x.Scan(scan =>
									{
										scan.TheCallingAssembly();
										scan.WithDefaultConventions();
									});
							x.For<ICacheStorage>().Use<HttpRuntimeCachedStorage>();
							x.For<ICachedReleaseQueryRepository>().Use<CachedReleaseQueryRepository>();
							x.For<IReleaseDataService>().Use<ReleaseDataService>();
							x.For<IReleaseQueryService>().Use<ReleaseQueryService>();
							x.For<ITestingService>().Use<TestingService>();
							x.For<IDevelopmentService>().Use<DevelopmentService>();

							/*Mock Repository*/
							x.For<IReleaseQueryRepository>().Use<MockReleaseQueryRepository>();
							x.For<IReleaseWorkItemRepository>().Use<MockReleaseWorkItemRepository>();
							x.For<IBlogRepository>().Use<MockBlogRepository>();
							x.For<IProjectRepository>().Use<MockProjectRepository>();
							x.For<IReleaseRepository>().Use<MockReleaseRepository>();
						});
			return ObjectFactory.Container;
		}
	}
}