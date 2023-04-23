using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableScheduler
{
	public class Course
	{
		public string Code { get; set; }
		public int Semester { get; set; }
		public int Year { get; set; }
		public int Session { get; set; }
		public string? Dept { get; set; }
	}
}
