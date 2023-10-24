using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryStatistics.Models
{
    [Table("MegaSena")]
	public class Numbers
	{
        public int Id { get; set; }

        [Display(Name = "Contest Number")]
        public double ContestNumber { get; set; }

        [Display(Name ="Contest Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ContestDate { get; set; }

        [Display(Name ="Ball 1")]
		public double Number1 { get; set; }

        [Display(Name = "Ball 2")]
        public double Number2 { get; set; }

        [Display(Name = "Ball 3")]
        public double Number3 { get; set; }

        [Display(Name = "Ball 4")]
        public double Number4 { get; set; }

        [Display(Name = "Ball 5")]
        public double Number5 { get; set; }

        [Display(Name = "Ball 6")]
        public double Number6 { get; set; }

        public Numbers(int id, double number1, double number2, double number3, double number4, double number5, double number6, double contestNumber, DateTime contestDate)
        {
            Id = id;
            Number1 = number1;
            Number2 = number2;
            Number3 = number3;
            Number4 = number4;
            Number5 = number5;
            Number6 = number6;
            ContestNumber = contestNumber;
            ContestDate = contestDate;
        }

        public Numbers()
        {

        }
    }
}

