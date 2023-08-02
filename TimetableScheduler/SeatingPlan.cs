namespace TimetableScheduler
{
    public class SeatingPlan
    {
        private static Random random = new Random();

        public List<Room> GenerateSeatingPlan(List<Room> rooms, List<Exam> exams, int studentsPerColumn)
        {
            List<Room> result = new List<Room>();

            int n = rooms.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (rooms[n], rooms[k]) = (rooms[k], rooms[n]);
            }
            int courseIndex = 0;

            var examsGroupedByDateAndCourse = exams.GroupBy(exam => new { exam.Date, exam.Timeslot });
            foreach (var examsGroup in examsGroupedByDateAndCourse)
            {
                List<Room> availableRoomsCopyMain = rooms.GetRange(0, rooms.Count);

                var examToProcess = examsGroup.ToList();
                foreach (Exam exam in examToProcess)
                {
                    List<Room> availableRoomsCopy = rooms.GetRange(0, rooms.Count);
                    while (!exam.Flag)
                    {
                        if(exam.Students?.Count <= 0 || availableRoomsCopy.Count <= 0)
                        {
                            exam.Flag = true;
                            break;
                        }

                        int index = random.Next(availableRoomsCopy.Count);
                        availableRoomsCopy[index].Exam?.Add(exam);
                        if (availableRoomsCopy[index].RowColStudents.Count == 0)
                        {
                            availableRoomsCopy[index].RowColStudents = Enumerable.Range(0, availableRoomsCopy[index].Rows)
                                .Select(_ => Enumerable.Repeat(default(Student), availableRoomsCopy[index].Columns).ToList())
                                .ToList();
                        }
                        for (int col = courseIndex; col < availableRoomsCopy[index].Columns; col += 2)
                        {
                            for (int row = 0; row < availableRoomsCopy[index].Rows; row++)
                            {
                                if (availableRoomsCopy[index].RowColStudents[row][col] == null)
                                {
                                    if (exam.Students != null && exam.Students.Count > 0)
                                    {
                                        availableRoomsCopy[index].RowColStudents[row][col] = exam.Students[0];
                                        exam.Students.RemoveAt(0);
                                    }
                                }
                            }
                        }
                        availableRoomsCopyMain[index] = availableRoomsCopy[index];
                        availableRoomsCopy.RemoveAt(index);
                    }
                    courseIndex++;
                }
                result.AddRange(availableRoomsCopyMain);
            }
            return result;
        }
    }
}
