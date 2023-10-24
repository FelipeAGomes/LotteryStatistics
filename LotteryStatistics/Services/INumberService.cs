using LotteryStatistics.Models;
using LotteryStatistics.Models.ViewModels;

namespace LotteryStatistics.Services
{
	public interface INumberService
	{
        Task<Dictionary<double, int>> GetNumberStatisticsAsync();
        List<double> GenerateRandomNumbersFromTop20(int count);
        public Task<List<Numbers>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
        public Task InsertAsync(NumbersViewModel obj);
    }
}

