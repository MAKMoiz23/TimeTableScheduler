﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableScheduler
{
	public class Dept
	{
		public int DeptId { get; set; }
		public string? DeptName { get; set; }
		public List<Course> Courses { get; set; }
	}
}
