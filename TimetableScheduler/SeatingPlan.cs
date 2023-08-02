using System;
using System.Collections.Generic;
using System.Linq;

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

            int rowStep = 2; // Skip every other row
            int courseIndex = 0;

            // Dictionary to keep track of exams already placed in each room
            Dictionary<Room, List<Exam>> roomExamsDict = new Dictionary<Room, List<Exam>>();

            var examsGroupedByDateAndCourse = exams.GroupBy(exam => new { exam.Date, exam.Timeslot });
            foreach (var examsGroup in examsGroupedByDateAndCourse)
            {
                List<Room> availableRoomsCopy = rooms.GetRange(0, rooms.Count);

                var examToProcess = examsGroup.ToList();
                foreach (Exam exam in examToProcess)
                {
                    int index = random.Next(availableRoomsCopy.Count);
                    availableRoomsCopy[index].Exam?.Add(exam);
                    if (availableRoomsCopy[index].RowColStudents.Count == 0)
                    {
                        availableRoomsCopy[index].RowColStudents = Enumerable.Range(0, availableRoomsCopy[index].Rows)
                            .Select(_ => Enumerable.Repeat(default(Student), availableRoomsCopy[index].Columns).ToList())
                            .ToList();
                    }

                    // Populating students in the seating plan
                    int startRow = 0;

                    List<Exam> examsInRoom;
                    if (roomExamsDict.TryGetValue(availableRoomsCopy[index], out examsInRoom))
                    {
                        // Check if any exam in the room matches the current exam's date, time, and room
                        bool hasMatchingExam = examsInRoom.Any(e =>
                            e.Date == exam.Date &&
                            e.Timeslot == exam.Timeslot &&
                            e.Room?.RoomID == exam.Room?.RoomID);

                        if (!hasMatchingExam)
                        {
                            for (int col = courseIndex; col < availableRoomsCopy[index].Columns; col += 2)
                            {
                                for (int row = startRow; row < availableRoomsCopy[index].Rows; row += rowStep)
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
                        }
                        else
                        {
                            // Use the same columns as the matching exam
                            foreach (Exam matchingExam in examsInRoom)
                            {
                                for (int col = 0; col < availableRoomsCopy[index].Columns; col++)
                                {
                                    for (int row = startRow; row < availableRoomsCopy[index].Rows; row += rowStep)
                                    {
                                        if (availableRoomsCopy[index].RowColStudents[row][col] == null)
                                        {
                                            if (matchingExam.Students != null && matchingExam.Students.Count > 0)
                                            {
                                                availableRoomsCopy[index].RowColStudents[row][col] = matchingExam.Students[0];
                                                matchingExam.Students.RemoveAt(0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Update the dictionary with the current exam
                    if (examsInRoom == null)
                    {
                        examsInRoom = new List<Exam>();
                        roomExamsDict[availableRoomsCopy[index]] = examsInRoom;
                    }
                    examsInRoom.Add(exam);

                    courseIndex = (courseIndex + 1) % 2; // Switch to the other course for the next iteration
                }
                result.AddRange(availableRoomsCopy);
            }
            return result;
        }
    }
}
