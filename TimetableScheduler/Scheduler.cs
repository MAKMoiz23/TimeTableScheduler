namespace TimetableScheduler
{
	public class Scheduler
	{
		public List<List<List<Course>>> GenerateTimeTable(List<Course> courses, DateTime examStartDate, DateTime examEndDate, int examPeriod,
									  TimeSpan classStartDay, TimeSpan classEndDay, int slotsPerDay, int examDays)
		{
			List<string> skipDays = new() { "Saturday", "Sunday" };
			// 2D array to store the time table
			//dynamic[,] timeTable = new List<Course>[examDays, slotsPerDay];
			List<List<List<Course>>> timeTable = new List<List<List<Course>>>();

			// check print 2d ----depreciated---- only use for output purpose because of complexity
			for (int i = 0; i < examDays; i++)
			{
				DateTime currentDate = examStartDate.AddDays(i);
				string dayString = currentDate.DayOfWeek.ToString();
				List<List<Course>> row = new List<List<Course>>();
				if (skipDays.Contains(dayString))
				{
					examDays--;
					//timeTable.RemoveAt(i);
					continue;
				}
				for (int j = 0; j < slotsPerDay; j++)
				{
					row.Add(new List<Course>());
				}
				timeTable.Add(row);
			}

			// Dictionary to keep track of courses already scheduled for each department on a particular day
			//Dictionary<(int day, string dept), Course> assignedCourses = new Dictionary<(int day, string dept), Course>();

			// list of available slots
			List<(int day, int slot)> availableSlots = new List<(int day, int slot)>();
			for (int day = 0; day < timeTable.Count; day++)
			{
				for (int slot = 0; slot < timeTable[day]?.Count; slot++)
				{
					//if (timeTable[day, slot] == "-")
					//{
						availableSlots.Add((day, slot));
					//}
				}
			}
			//List<(int day, int slot)> availableSlots = new List<(int day, int slot)>();
			//for (int day = 0; day < examDays; day++)
			//{
			//	for (int slot = 0; slot < slotsPerDay; slot++)
			//	{
			//		//if (timeTable[day, slot] == "-")
			//		//{
			//			availableSlots.Add((day, slot));
			//		//}
			//	}
			//}

			// Fisher-Yates shuffle algorithm, which randomizes the order of the elements in the list. ---Not my logic---
			Random rng = new Random();
			int n = availableSlots.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				(int day, int slot) temp = availableSlots[k];
				availableSlots[k] = availableSlots[n];
				availableSlots[n] = temp;
			}

			// Assigned courses indexes stored 
			Dictionary<(int day, int slot), Course> assignedSlots = new Dictionary<(int day, int slot), Course>();

			// Course based scheduling ---constraints satisfaction rehte and if all exams are scheduled in one go or seperate for each semester---
			foreach (var course in courses)
			{
				bool slotAssigned = false;
				List<(int day, int slot)> availableSlotsCopy = availableSlots.GetRange(0, availableSlots.Count);

				// Check until a slot is assigned
				while (!slotAssigned)
				{
					// Select a random available slot from the list
					int index = rng.Next(availableSlotsCopy.Count);
					//int day = 3;
					//int slot = 3;
					int day = availableSlotsCopy[index].day;
					int slot = availableSlotsCopy[index].slot;

					// Check if the slot is available and no course from the same department and different semester is already scheduled on the day
					if (
						//!assignedCourses.ContainsKey((day, course.Dept)) &&
						(!assignedSlots.ContainsKey((day, slot))
						|| (assignedSlots[(day, slot)].Semester != course.Semester && assignedSlots[(day, slot)].Dept == course.Dept) 
						|| (assignedSlots[(day, slot)].Semester == course.Semester && assignedSlots[(day, slot)].Dept != course.Dept))
						//|| (!assignedCourses.ContainsKey((day, course.Dept)) || assignedCourses[(day, course.Dept)].Code == course.Code)
						//&& (!sameDeptCourses.Any(s => assignedCourses.ContainsKey((day, course.Dept)) && assignedCourses[(day, course.Dept)].Semester != s))
						)
					{
						// Assign the slot to the course
						timeTable[day][slot].Add(course);
						//assignedCourses[(day, course.Dept)] = course;
						assignedSlots[(day, slot)] = course;
						availableSlotsCopy.RemoveAt(index);
						slotAssigned = true;
					}
					else
					{
						availableSlotsCopy.RemoveAt(index);
					}
				}
			}
			return timeTable;
		}
	}
}
