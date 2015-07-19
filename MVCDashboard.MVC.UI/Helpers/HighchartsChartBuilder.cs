using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using MVCDashboard.Domain.Entities;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using System.Drawing;
using DotNet.Highcharts.Helpers;
using MVCDashboard.UI.MVC.Helpers.Containers;

namespace MVCDashboard.UI.MVC.Helpers
{
	public class HighchartsChartBuilder
	{
		public Highcharts GenerateStateChart(IEnumerable<ChartValue> statePieChartValues)
		{
			Highcharts statePieChart = BuildPieChartWithoutLegend(
				ConvertToObjectArray(statePieChartValues), "Work Item States");

			return statePieChart;
		}

		public Highcharts GeneratePassedTestingChart(IEnumerable<ChartValue> passedTestingChartValues)
		{
			Highcharts passedTestingChart = BuildAreaSplineChart(
				ConvertToXAndYAxisArrays(passedTestingChartValues), 
				"Work Items Passed Integration Testing Burndown",
				"Number of Work Items");

			return passedTestingChart;
		}

		public Highcharts GenerateTriageStatusChart(IEnumerable<ChartValue> triageStatusChartValues)
		{
			Highcharts triageStatusChart = BuildColumnChart(
				ConvertToXAndYAxisArrays(triageStatusChartValues),
				"Release Defects Triage Status",
				"Number of Work Items");

			return triageStatusChart;
		}

		private Highcharts BuildPieChartWithoutLegend(object[] chartData, string chartTitle)
		{
			Highcharts chart = new Highcharts(GenerateChartName(chartTitle))
				.InitChart(new Chart
				{
					PlotShadow = false
				})				
				.SetTitle(new Title { Text = string.Empty })
				.SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(1) +' %'; }" })
				.SetPlotOptions(new PlotOptions
				{
					Pie = new PlotOptionsPie
					{
						AllowPointSelect = true,
						Cursor = Cursors.Pointer,
						DataLabels = new PlotOptionsPieDataLabels
						{
							Color = ColorTranslator.FromHtml("#000000"),
							ConnectorColor = ColorTranslator.FromHtml("#000000"),
							Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(1) +' %'; }"
						}
					}
				})
				.SetSeries(new Series
				{
					Type = ChartTypes.Pie,
					Name = chartTitle,
					Data = new Data(chartData)
				});

			return chart;
		}

		private Highcharts BuildAreaSplineChart(XAndYAxisArrayContainer chartData, string chartTitle, string yAxisTitle)
		{
			Highcharts chart = new Highcharts(GenerateChartName(chartTitle))
				.InitChart(new Chart { DefaultSeriesType = ChartTypes.Areaspline })
				.SetTitle(new Title { Text = string.Empty })
				.SetLegend(new Legend { Enabled = false })
				.SetXAxis(new XAxis { Categories = chartData.XAxis })
				.SetYAxis(new YAxis { Title = new YAxisTitle { Text = yAxisTitle } })
				.SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.x +': '+ this.y + ' WIs Remaining'; }" })
				.SetCredits(new Credits { Enabled = false })
				.SetPlotOptions(new PlotOptions { Areaspline = new PlotOptionsAreaspline { FillOpacity = 0.5 } })
				.SetSeries(new[] { new Series { Data = new Data(chartData.YAxis) } });

			return chart;
		}

		private Highcharts BuildColumnChart(XAndYAxisArrayContainer chartData, string chartTitle, string yAxisTitle)
		{
			Highcharts chart = new Highcharts(GenerateChartName(chartTitle))
				.InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
				.SetTitle(new Title { Text = string.Empty })
				.SetXAxis(new XAxis { Categories = chartData.XAxis })
				.SetYAxis(new YAxis
				{
					Min = 0,
					Title = new YAxisTitle { Text = yAxisTitle }
				})
				.SetLegend(new Legend { Enabled = false })
				.SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' WIs'; }" })
				.SetPlotOptions(new PlotOptions
				{
					Column = new PlotOptionsColumn
					{
						PointPadding = 0.1,
						BorderWidth = 0
					}
				})
				.SetSeries(new[] { new Series { Data = new Data(chartData.YAxis) } });

			return chart;
		}

		private static string GenerateChartName(string chartTitle)
		{
			return chartTitle.Replace(" ", string.Empty);
		}

		private object[] ConvertToObjectArray(IEnumerable<ChartValue> chartValues)
		{
			int numberOfChartValues = chartValues.Count();

			object[] objectArray = new object[numberOfChartValues];

			for (int i = 0; i < numberOfChartValues; i++)
			{
				objectArray[i] = new object[]
				{
					chartValues.ElementAt(i).Name,
					chartValues.ElementAt(i).Value
				};
			}

			return objectArray;
		}

		private XAndYAxisArrayContainer ConvertToXAndYAxisArrays(IEnumerable<ChartValue> chartValues)
		{
			XAndYAxisArrayContainer xAndYAxisArrayContainer = new XAndYAxisArrayContainer();

			xAndYAxisArrayContainer.XAxis = BuildNameStringArray(chartValues);
			xAndYAxisArrayContainer.YAxis = BuildValueObjectArray(chartValues);

			return xAndYAxisArrayContainer;
		}

		private string[] BuildNameStringArray(IEnumerable<ChartValue> chartValues)
		{
			IList<string> valueNames = new List<string>();

			foreach (ChartValue chartValue in chartValues)
			{
				valueNames.Add(chartValue.Name);
			}

			return valueNames.ToArray();
		}

		private object[] BuildValueObjectArray(IEnumerable<ChartValue> chartValues)
		{
			IList<object> values = new List<object>();

			foreach (ChartValue chartValue in chartValues)
			{
				values.Add(chartValue.Value);
			}

			return values.ToArray();
		}

		private BackColorOrGradient TransparentBackground()
		{
			return new BackColorOrGradient(new Gradient
					{
						LinearGradient = new[] { 0, 0, 0, 400 },
						Stops = new object[,]
						{
							{ 0, Color.FromArgb(13, 255, 255, 255) },
							{ 1, Color.FromArgb(13, 255, 255, 255) }
						}
					});
		}
	}
}