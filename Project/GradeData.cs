using System;

namespace SchoolJournal
{
    /// <summary>
    /// Оцінка учня по предмету.
    /// </summary>
    public class GradeData
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public ChildData? Student { get; set; }

        public int TeacherId { get; set; }
        public TeacherData? Teacher { get; set; }

        public string Subject { get; set; } = string.Empty;
        public int Value { get; set; }
        public DateTime DateAssigned { get; set; } = DateTime.Today;

        public string Comment { get; set; } = string.Empty;
    }
}
