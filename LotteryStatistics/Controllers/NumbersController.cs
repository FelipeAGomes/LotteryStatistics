using System;
using LotteryStatistics.Data;
using LotteryStatistics.Models;
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

        public async Task<IActionResult> SearchByDate(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("dd-MM-yyyy");
            ViewData["maxDate"] = maxDate.Value.ToString("dd-MM-yyyy");
            var result = await _numberService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var numbersViewModel = new NumbersViewModel
            {
                numbers = new Numbers()
            };
            return View(numbersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NumbersViewModel numbersViewModel)
        {
            try
            {
                // Certifique-se de que a propriedade numbers esteja inicializada
                if (numbersViewModel.numbers == null)
                {
                    numbersViewModel.numbers = new Numbers();
                }

                await _numberService.InsertAsync(numbersViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex);
                return RedirectToAction("Error"); // Redirecionar para uma página de erro
            }
        }
    }
}

