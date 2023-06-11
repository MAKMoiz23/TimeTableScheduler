using TimetableScheduler;

// Define the input data
List<Course> courses = new List<Course>() {
	new Course { Code = "CS101", Semester = 1, Year = 2023, Session = 1, Dept = "CS" },
	new Course { Code = "SE102", Semester = 1, Year = 2023, Session = 1, Dept = "SE" },
	new Course { Code = "EE201", Semester = 1, Year = 2023, Session = 1, Dept = "EE" },
	new Course { Code = "EL202", Semester = 1, Year = 2023, Session = 1, Dept = "EL" },
	new Course { Code = "CS203", Semester = 1, Year = 2023, Session = 1, Dept = "CS" },
	new Course { Code = "SE204", Semester = 1, Year = 2023, Session = 1, Dept = "SE" },
	new Course { Code = "EL205", Semester = 1, Year = 2023, Session = 1, Dept = "EL" },
	new Course { Code = "EE206", Semester = 1, Year = 2023, Session = 1, Dept = "EE" },
	new Course { Code = "CS207", Semester = 3, Year = 2023, Session = 1, Dept = "CS" },
	new Course { Code = "SE208", Semester = 3, Year = 2023, Session = 1, Dept = "SE" },
	new Course { Code = "EL209", Semester = 3, Year = 2023, Session = 1, Dept = "EL" },
	new Course { Code = "EE301", Semester = 3, Year = 2023, Session = 1, Dept = "EE" },
	new Course { Code = "SE302", Semester = 5, Year = 2023, Session = 1, Dept = "SE" },
	new Course { Code = "EL303", Semester = 5, Year = 2023, Session = 1, Dept = "EL" },
	new Course { Code = "EE304", Semester = 5, Year = 2023, Session = 1, Dept = "EE" },
	new Course { Code = "CS305", Semester = 5, Year = 2023, Session = 1, Dept = "CS" },
	new Course { Code = "SE306", Semester = 7, Year = 2023, Session = 1, Dept = "SE" },
	new Course { Code = "EL307", Semester = 7, Year = 2023, Session = 1, Dept = "EL" },
	new Course { Code = "EE308", Semester = 7, Year = 2023, Session = 1, Dept = "EE" },
	new Course { Code = "CS309", Semester = 7, Year = 2023, Session = 1, Dept = "CS" },
	new Course { Code = "SE401", Semester = 7, Year = 2023, Session = 1, Dept = "SE" },
	new Course { Code = "EL402", Semester = 7, Year = 2023, Session = 1, Dept = "EL" },
	new Course { Code = "EE403", Semester = 7, Year = 2023, Session = 1, Dept = "EE" },
};

//List<Course> coursescopy = courses.GetRange(0, courses.Count);

DateTime examStartDate = new DateTime(2023, 5, 1);
DateTime examEndDate = new DateTime(2023, 5, 10);
int examPeriodInMinutes = 120;
TimeSpan classStartTime = new TimeSpan(8, 0, 0);
TimeSpan classEndTime = new TimeSpan(17, 0, 0);

// Calculate the number of days between the exam start and end dates
int examDays = (int)(examEndDate - examStartDate).TotalDays + 1;

// Calculate the number of slots per day based on the class start and end times and the exam period
int slotsPerDay = (int)((classEndTime - classStartTime).TotalMinutes / examPeriodInMinutes);


List<TimeSpan> timeSlots = new List<TimeSpan>();

// Create time slots based on the number of slots per day
TimeSpan currentTime = classStartTime;
for (int i = 0; i < slotsPerDay; i++)
{
	timeSlots.Add(currentTime);
	currentTime = currentTime.Add(TimeSpan.FromMinutes(examPeriodInMinutes));
}
// Initialize the 2D array for the time table
//string[,] timeTable = new string[examDays, slotsPerDay];

Scheduler sc = new Scheduler();

List<List<List<Course>>> TimeTable = sc.GenerateTimeTable(courses, examStartDate, examEndDate, examPeriodInMinutes, classStartTime, classEndTime, slotsPerDay, examDays);

//Print the time table
//for (int i = 0; i < TimeTable.GetLength(0); i++)
for (int i = 0; i < TimeTable.Count; i++)
{
	DateTime currentDate = examStartDate.AddDays(i);
	Console.Write(currentDate.ToShortDateString()+ "	");
	//for (int j = 0; j < TimeTable.GetLength(1); j++)
	for (int j = 0; j < TimeTable[i]?.Count; j++)
	{
		Console.Write(timeSlots[j]);
		if (TimeTable[i][j]?.Count > 0)
		{
			List<string> temp = new List<string>();
			for (int k = 0; k < TimeTable[i][j]?.Count; k++)
			{
				temp.Add(TimeTable[i][j][k].Code);
			}
			Console.Write("[" + string.Join(", ", temp) + "] ");
		}
		else
		{
			Console.Write("[] ");
		}
	}
	Console.WriteLine();
}


