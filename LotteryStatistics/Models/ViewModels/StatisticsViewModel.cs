using System;
namespace LotteryStatistics.Models.ViewModels
{
	public class StatisticsViewModel
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Dictionary<double, int> MostFrequentNumbers { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

