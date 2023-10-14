using System;
using LotteryStatistics.Models;
using Microsoft.EntityFrameworkCore;

namespace LotteryStatistics.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Numbers> Numbers { get; set; }
	}
}

