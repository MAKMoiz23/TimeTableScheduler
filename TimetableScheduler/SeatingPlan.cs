namespace TimetableScheduler
{
    public class SeatingPlan
    {
        public List<Room> GenerateSeatingPlan(List<Room> rooms, List<Exam> exams, int studentsPerColumn)
        {
            // Step 1: Group exams by the same date and course.
            var examsGroupedByDateAndCourse = exams.GroupBy(exam => new { exam.Date, exam.CourseCode });

            foreach (var examsGroup in examsGroupedByDateAndCourse)
            {
                var examsToProcess = examsGroup.ToList();
                var roomGroups = new List<List<Room>>();

                // Create separate room groups for exams with the same date but different courses
                foreach (var room in rooms)
                {
                    room.RowColStudents =  Enumerable.Range(0, room.Rows)
                                                     .Select(_ => Enumerable.Repeat(default(Student), room.Columns).ToList())
                                                     .ToList();
                    if (examsToProcess.Count == 0)
                        break;

                    var currentRoomGroup = new List<Room>();
                    currentRoomGroup.Add(room);

                    // Add more rooms to the current group until no more exams can be assigned
                    for (int i = 1; i < rooms.Count && examsToProcess.Count > 0; i++)
                    {
                        var nextRoom = rooms[(rooms.IndexOf(room) + i) % rooms.Count];
                        if (examsToProcess.Any(exam => exam.Students?.Count <= nextRoom.Rows * nextRoom.Columns - nextRoom.RowColStudents.Sum(row => row.Count)))
                            currentRoomGroup.Add(nextRoom);
                    }

                    roomGroups.Add(currentRoomGroup);
                }

                // Distribute exams in the current group among the rooms
                foreach (var roomGroup in roomGroups)
                {
                    int examIndex = 0;
                    var roomsToProcess = roomGroup.ToList();

                    while (examsToProcess.Count > 0 && roomsToProcess.Count > 0)
                    {
                        var currentRoom = roomsToProcess[examIndex % roomsToProcess.Count];

                        // Find the first exam in examsToProcess that can fit into the room.
                        var examToAssign = examsToProcess.FirstOrDefault(exam => exam.Students?.Count <= currentRoom.Rows * currentRoom.Columns - currentRoom.RowColStudents.Sum(row => row.Count));

                        if (examToAssign != null)
                        {
                            // Assign students from the selected exam to this room.
                            int studentsAssigned = 0;
                            while (studentsAssigned < examToAssign.Students.Count)
                            {
                                int availableSeats = currentRoom.Rows * currentRoom.Columns - currentRoom.RowColStudents.Sum(row => row.Count);
                                int studentsToAssign = Math.Min(availableSeats, studentsPerColumn);

                                // Calculate the number of students to be assigned to each column.
                                int studentsPerRow = (int)Math.Ceiling((double)studentsToAssign / currentRoom.Columns);

                                // Assign students to columns.
                                int studentIndex = studentsAssigned;
                                for (int col = 0; col < currentRoom.Columns; col++)
                                {
                                    var columnStudents = new List<Student>();
                                    for (int row = 0; row < currentRoom.Rows && studentIndex < examToAssign.Students.Count; row++)
                                    {
                                        columnStudents.Add(examToAssign.Students[studentIndex]);
                                        studentIndex++;
                                    }
                                    currentRoom.RowColStudents[col].AddRange(columnStudents);
                                }

                                // Update the number of students assigned for this exam.
                                studentsAssigned += studentsToAssign;
                            }

                            // Remove the assigned exam from the examsToProcess list.
                            examsToProcess.Remove(examToAssign);
                        }

                        examIndex++;
                    }
                }
            }

            return rooms;
        }

    }
}