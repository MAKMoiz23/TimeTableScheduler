namespace TimetableScheduler
{
    public class Room
    {
        public int RoomID { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int RoomCode { get; set; }
        public int? ExamID{ get; set; }
        public List<List<Student?>> RowColStudents { get; set; } = new();
    }

}
