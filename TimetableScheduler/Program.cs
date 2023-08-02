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
//for (int i = 0; i < TimeTable.Count; i++)
//{
//	DateTime currentDate = examStartDate.AddDays(i);
//	Console.Write(currentDate.ToShortDateString() + "	");
//	//for (int j = 0; j < TimeTable.GetLength(1); j++)
//	for (int j = 0; j < TimeTable[i]?.Count; j++)
//	{
//		Console.Write(timeSlots[j]);
//		if (TimeTable[i][j]?.Count > 0)
//		{
//			List<string> temp = new List<string>();
//			for (int k = 0; k < TimeTable[i][j]?.Count; k++)
//			{
//				temp.Add(TimeTable[i][j][k].Code);
//			}
//			Console.Write("[" + string.Join(", ", temp) + "] ");
//		}
//		else
//		{
//			Console.Write("[] ");
//		}
//	}
//	Console.WriteLine();
//}

List<Room> rooms = new List<Room>
{
    new Room { RoomID = 1, Rows = 5, Columns = 5, RoomCode = 101 },
    //new Room { RoomID = 2, Rows = 6, Columns = 6, RoomCode = 102 }
    //new Room { RoomID = 2, Rows = 6, Columns = 6, RoomCode = 103 }
    // Add more rooms as needed
};

// Sample students for Exam 1
var studentsExam1 = new List<Student>
{
    new Student { StudentID = 1, StudentName = "Alice1", DSYID = 101 },
    new Student { StudentID = 2, StudentName = "Alice2", DSYID = 101 },
    new Student { StudentID = 3, StudentName = "Alice3", DSYID = 101 },
    new Student { StudentID = 4, StudentName = "Alice4", DSYID = 101 },
    new Student { StudentID = 5, StudentName = "Alice5", DSYID = 101 },
    new Student { StudentID = 6, StudentName = "Alice6", DSYID = 101 },
    new Student { StudentID = 7, StudentName = "Alice7", DSYID = 101 },
    new Student { StudentID = 8, StudentName = "Alice8", DSYID = 101 },
    new Student { StudentID = 9, StudentName = "Alice9", DSYID = 101 },
    new Student { StudentID = 10, StudentName = "Alice10", DSYID = 101 },
    new Student { StudentID = 11, StudentName = "Bob1", DSYID = 102 },
    new Student { StudentID = 12, StudentName = "Bob2", DSYID = 102 },
    new Student { StudentID = 13, StudentName = "Bob3", DSYID = 102 },
    new Student { StudentID = 14, StudentName = "Bob4", DSYID = 102 },
    new Student { StudentID = 15, StudentName = "Bob5", DSYID = 102 },
    new Student { StudentID = 16, StudentName = "Bob6", DSYID = 102 },
    new Student { StudentID = 17, StudentName = "Bob7", DSYID = 102 },
    new Student { StudentID = 18, StudentName = "Bob8", DSYID = 102 },
    new Student { StudentID = 19, StudentName = "Charlie1", DSYID = 103 },
    new Student { StudentID = 20, StudentName = "Charlie2", DSYID = 103 },
    new Student { StudentID = 21, StudentName = "Charlie3", DSYID = 103 },
    new Student { StudentID = 22, StudentName = "Charlie4", DSYID = 103 },
    new Student { StudentID = 23, StudentName = "Charlie5", DSYID = 103 },
    new Student { StudentID = 24, StudentName = "Charlie6", DSYID = 103 },
    new Student { StudentID = 25, StudentName = "Charlie7", DSYID = 103 },
    new Student { StudentID = 26, StudentName = "Charlie8", DSYID = 103 },
    new Student { StudentID = 27, StudentName = "Charlie9", DSYID = 103 },
    // Add more students as needed
};

// Sample students for Exam 2
var studentsExam2 = new List<Student>
{
    new Student { StudentID = 28, StudentName = "David1", DSYID = 201 },
    new Student { StudentID = 29, StudentName = "David2", DSYID = 201 },
    new Student { StudentID = 30, StudentName = "David3", DSYID = 201 },
    new Student { StudentID = 31, StudentName = "David4", DSYID = 201 },
    new Student { StudentID = 32, StudentName = "David5", DSYID = 201 },
    new Student { StudentID = 33, StudentName = "David6", DSYID = 201 },
    new Student { StudentID = 34, StudentName = "Eve1", DSYID = 202 },
    new Student { StudentID = 35, StudentName = "Eve2", DSYID = 202 },
    new Student { StudentID = 36, StudentName = "Eve3", DSYID = 202 },
    new Student { StudentID = 37, StudentName = "Eve4", DSYID = 202 },
    new Student { StudentID = 38, StudentName = "Eve5", DSYID = 202 },
    new Student { StudentID = 39, StudentName = "Frank1", DSYID = 203 },
    new Student { StudentID = 40, StudentName = "Frank2", DSYID = 203 },
    new Student { StudentID = 41, StudentName = "Frank3", DSYID = 203 },
    new Student { StudentID = 42, StudentName = "Frank4", DSYID = 203 },
    new Student { StudentID = 43, StudentName = "Frank5", DSYID = 203 },
    new Student { StudentID = 44, StudentName = "Frank6", DSYID = 203 },
    // Add more students as needed
};

// Sample students for Exam 3
var studentsExam3 = new List<Student>
{
    new Student { StudentID = 45, StudentName = "George1", DSYID = 301 },
    new Student { StudentID = 46, StudentName = "George2", DSYID = 301 },
    new Student { StudentID = 47, StudentName = "George3", DSYID = 301 },
    new Student { StudentID = 48, StudentName = "George4", DSYID = 301 },
    new Student { StudentID = 49, StudentName = "George5", DSYID = 301 },
    new Student { StudentID = 50, StudentName = "George6", DSYID = 301 },
    new Student { StudentID = 51, StudentName = "George7", DSYID = 301 },
    new Student { StudentID = 52, StudentName = "George8", DSYID = 301 },
    new Student { StudentID = 53, StudentName = "George9", DSYID = 301 },
    new Student { StudentID = 54, StudentName = "George10", DSYID = 301 },
    new Student { StudentID = 55, StudentName = "Hannah1", DSYID = 302 },
    new Student { StudentID = 56, StudentName = "Hannah2", DSYID = 302 },
    new Student { StudentID = 57, StudentName = "Hannah3", DSYID = 302 },
    new Student { StudentID = 58, StudentName = "Hannah4", DSYID = 302 },
    new Student { StudentID = 59, StudentName = "Hannah5", DSYID = 302 },
    new Student { StudentID = 60, StudentName = "Hannah6", DSYID = 302 },
    new Student { StudentID = 61, StudentName = "Hannah7", DSYID = 302 },
    new Student { StudentID = 62, StudentName = "Hannah8", DSYID = 302 },
    new Student { StudentID = 63, StudentName = "Hannah9", DSYID = 302 },
    // Add more students as needed
};

// Sample students for Exam 4
var studentsExam4 = new List<Student>
{
    new Student { StudentID = 64, StudentName = "Ivy1", DSYID = 401 },
    new Student { StudentID = 65, StudentName = "Ivy2", DSYID = 401 },
    new Student { StudentID = 66, StudentName = "Ivy3", DSYID = 401 },
    new Student { StudentID = 67, StudentName = "Ivy4", DSYID = 401 },
    new Student { StudentID = 68, StudentName = "Ivy5", DSYID = 401 },
    new Student { StudentID = 69, StudentName = "Ivy6", DSYID = 401 },
    new Student { StudentID = 70, StudentName = "Ivy7", DSYID = 401 },
    new Student { StudentID = 71, StudentName = "Ivy8", DSYID = 401 },
    new Student { StudentID = 72, StudentName = "Ivy9", DSYID = 401 },
    new Student { StudentID = 73, StudentName = "Ivy10", DSYID = 401 },
    new Student { StudentID = 74, StudentName = "Jack1", DSYID = 402 },
    new Student { StudentID = 75, StudentName = "Jack2", DSYID = 402 },
    new Student { StudentID = 76, StudentName = "Jack3", DSYID = 402 },
    new Student { StudentID = 77, StudentName = "Jack4", DSYID = 402 },
    new Student { StudentID = 78, StudentName = "Jack5", DSYID = 402 },
    new Student { StudentID = 79, StudentName = "Jack6", DSYID = 402 },
    new Student { StudentID = 80, StudentName = "Jack7", DSYID = 402 },
    new Student { StudentID = 81, StudentName = "Jack8", DSYID = 402 },
    new Student { StudentID = 82, StudentName = "Jack9", DSYID = 402 },
    // Add more students as needed
};


// Create the exams list and fill it with Exam objects including the students
var exams = new List<Exam>
{
    new Exam { ExamID = 1, Date = new DateTime(2023, 07, 30), Timeslot = new TimeSpan(9, 0, 0), CourseCode = "C001", Students = studentsExam1 },
    new Exam { ExamID = 2, Date = new DateTime(2023, 07, 30), Timeslot = new TimeSpan(9, 0, 0), CourseCode = "C002", Students = studentsExam2 },
    new Exam { ExamID = 3, Date = new DateTime(2023, 07, 30), Timeslot = new TimeSpan(9, 0, 0), CourseCode = "C003", Students = studentsExam3 },
    new Exam { ExamID = 4, Date = new DateTime(2023, 08, 01), Timeslot = new TimeSpan(15, 30, 0), CourseCode = "C004", Students = studentsExam4 },
    new Exam { ExamID = 5, Date = new DateTime(2023, 08, 01), Timeslot = new TimeSpan(15, 30, 0), CourseCode = "C005", Students = studentsExam4 }
    // Add more exams as needed
};


int studentsPerColumn = 20;

List<Room> seatingPlan = new SeatingPlan().GenerateSeatingPlan(rooms, exams, studentsPerColumn);
// Print the seating plan to the console
foreach (var room in seatingPlan)
{
    Console.WriteLine($"Room: {room.RoomCode}");

    for (int row = 0; row < room.Rows; row++)
    {
        for (int col = 0; col < room.Columns; col++)
        {
            var columnStudents = room.RowColStudents.Count > col ? room.RowColStudents[col] : null;
            var student = columnStudents != null && columnStudents.Count > row ? columnStudents[row] : null;
            if (student != null)
            {
                Console.Write($"| {student.StudentName,-10} ");
            }
            else
            {
                Console.Write($"|{' ',-12}");
            }
        }
        Console.WriteLine("|");
    }

    Console.WriteLine(new string('-', (room.Columns * 13) + 1));
}
