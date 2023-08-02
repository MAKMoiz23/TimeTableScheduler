namespace TimetableScheduler
{
    public class Exam
    {
        public int ExamID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Timeslot { get; set; }
        public string? CourseCode { get; set; }
        public int NumberOfStudents { get; set; }
        public bool Flag { get; set; } = false;
        public List<Student>? Students { get; set; }
        public Room? Room { get; set; }
    }

}
