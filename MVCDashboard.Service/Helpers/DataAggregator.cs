using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCDashboard.Domain.Entities;

namespace MVCDashboard.Service.Helpers
{
	public class DataAggregator
	{
		public IEnumerable<ChartValue> CalculateStatePieChartValues(
			IEnumerable<ReleaseWorkItem> dashboardWorkItems)
		{
			double totalNumberOfWorkItems = dashboardWorkItems.Count();

			IEnumerable<ChartValue> statePieChartValues = from workItem in dashboardWorkItems
														  group workItem by workItem.State into g
														  select new ChartValue
														  {
															  Name = g.Key,
															  Value = CalculatePercentage(g.Count(), totalNumberOfWorkItems)
														  };
															

			return statePieChartValues;
		}

		public IEnumerable<ChartValue> CalculatePassedTestingLineChartValues(
			IEnumerable<ReleaseWorkItem> dashboardWorkItems)
		{
			IEnumerable<ChartValue> passedTestingLineChartValues = from workItem in dashboardWorkItems
																   where workItem.State == "Passed Integration Testing" || workItem.State == "Released"
																   group workItem by workItem.PassedIntegrationTestingDate.Date into g
																   orderby g.Key
																   select new ChartValue
																   {																	   
																	   Name = g.Key.ToString("dd/MM"),
																	   Value = g.Count()
																   };

			IEnumerable<ChartValue> passedTestingLineChartBurndownValues =
					ConvertToBurndownChartValues(passedTestingLineChartValues, dashboardWorkItems.Count());

			return passedTestingLineChartBurndownValues;
		}

		public IEnumerable<ChartValue> CalculateTraiageStateBarChartValues(
			IEnumerable<DefectWorkItem> dashboardWorkItems)
		{
			IEnumerable<ChartValue> triageStatusBarChartValues = from workItem in dashboardWorkItems
																 group workItem by workItem.Triaged into g
																 select new ChartValue
																 {
																	 Name = g.Key,
																	 Value = g.Count()
																 };

			return triageStatusBarChartValues;
		}

		public IEnumerable<DefectWorkItem> GetNonTriagedDefectWorkItems(
			IEnumerable<DefectWorkItem> defectWorkItems)
		{
			IEnumerable<DefectWorkItem> nonTriagedDefectWorkItems = from workItem in defectWorkItems
																	where workItem.Triaged.ToLower() == "no"
																	select workItem;

			return nonTriagedDefectWorkItems;
		}

		public IEnumerable<StatusSummary> CalculateStatusSummaries(IEnumerable<Project> projects)
		{
			IEnumerable<StatusSummary> statusSummaries = from project in projects
														 group project by project.Status into g
														 orderby g.Key
														 select new StatusSummary
														 {
															 Name = g.Key,
															 Count = g.Count()
														 };

			return statusSummaries;
		}

		private IEnumerable<ChartValue> ConvertToBurndownChartValues(IEnumerable<ChartValue> passedTestingLineChartValues,
			int totalNumberOfWorkItemsInRelease)
		{
			IList<ChartValue> burndownValues = new List<ChartValue>();

			if (passedTestingLineChartValues.Count() > 0)
			{
				burndownValues.Add(new ChartValue 
				{ 
					Name = CalculateDayBeforeEarliestBurndownDate(passedTestingLineChartValues),
					Value = totalNumberOfWorkItemsInRelease
				});

				double numberOfWorkItems = totalNumberOfWorkItemsInRelease;

				foreach (ChartValue chartValue in passedTestingLineChartValues)
				{
					numberOfWorkItems -= chartValue.Value;

					burndownValues.Add(new ChartValue
					{
						Name = chartValue.Name,
						Value = numberOfWorkItems 
					});
				}
			}
			else
			{
				burndownValues.Add(new ChartValue
				{ 
					Name = DateTime.Now.ToShortDateString(), 
					Value = totalNumberOfWorkItemsInRelease
				});
			}

			return burndownValues;
		}

		private double CalculatePercentage(double count, double totalNumberOfWorkItems)
		{
			double percentage = Math.Round((count / totalNumberOfWorkItems) * 100, 1);

			return percentage;
		}

		private string CalculateDayBeforeEarliestBurndownDate(IEnumerable<ChartValue> chartValues)
		{
			string dayBefore = DateTime.Parse(chartValues.ElementAt(0).Name).AddDays(-1).ToString("dd/MM");

			return dayBefore;
		}
	}
}
