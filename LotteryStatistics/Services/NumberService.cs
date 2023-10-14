using LotteryStatistics.Data;
using LotteryStatistics.Models;
using Microsoft.EntityFrameworkCore;

namespace LotteryStatistics.Services
{
    public class NumberService : INumberService
    {
        private readonly ApplicationDbContext _context;

        public NumberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Numbers>> FindAllAsync()
        {
            return await _context.Numbers.OrderByDescending(x => x.ContestNumber).ToListAsync();
        }

        public async Task<Dictionary<double, int>> GetNumberStatisticsAsync()
        {
            var frequencyMap = new Dictionary<double, int>();
            var numbers = await _context.Numbers.ToListAsync();

            foreach (var n in numbers)
            {
                UpdateFrequency(frequencyMap, n.Number1);
                UpdateFrequency(frequencyMap, n.Number2);
                UpdateFrequency(frequencyMap, n.Number3);
                UpdateFrequency(frequencyMap, n.Number4);
                UpdateFrequency(frequencyMap, n.Number5);
                UpdateFrequency(frequencyMap, n.Number6);
            }

            var sortedResult = frequencyMap.OrderByDescending(x => x.Value)
                .Take(20)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return sortedResult;
        }

        private void UpdateFrequency(Dictionary<double, int> frequencyMap, double number)
        {
            if (frequencyMap.ContainsKey(number))
            {
                frequencyMap[number]++;
            }
            else
            {
                frequencyMap[number] = 1;
            }
        }

        public List<double> GenerateRandomNumbersFromTop20(int count)
        {
            var mostFrequentNumbers = GetNumberStatisticsAsync().Result.Keys.ToList();
            var randomNumbers = mostFrequentNumbers.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
            return randomNumbers;
        }

    }
}
