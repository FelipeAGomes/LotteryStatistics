using System;
using LotteryStatistics.Data;
using LotteryStatistics.Models.ViewModels;
using LotteryStatistics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotteryStatistics.Controllers
{
	public class NumbersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly INumberService _numberService;

		public NumbersController(ApplicationDbContext context, INumberService numberService)
		{
			_context = context;
			_numberService = numberService;
		}

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {
            var totalItems = await _context.Numbers.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var lotteryNumbers = await _context.Numbers
                .OrderByDescending(x => x.ContestNumber)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(lotteryNumbers);
        }


        public async Task<IActionResult> Statistics()
        {
            var mostFrequentNumbers = await _numberService.GetNumberStatisticsAsync();
            var model = new StatisticsViewModel
            {
                MostFrequentNumbers = mostFrequentNumbers
            };
            return View(model);
        }

        public IActionResult GenerateRandomNumbers(int count = 6)
        {
            var randomNumbers = _numberService.GenerateRandomNumbersFromTop20(count);
            return Json(randomNumbers);
        }
    }
}

