namespace LotteryStatistics.Services
{
	public interface INumberService
	{
        Task<Dictionary<double, int>> GetNumberStatisticsAsync();
        List<double> GenerateRandomNumbersFromTop20(int count);
    }
}

