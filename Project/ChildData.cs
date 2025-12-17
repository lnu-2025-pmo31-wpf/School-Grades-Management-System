using System;
using System.Collections.Generic;

namespace SchoolJournal
{
    public class ChildData
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Дата народження (для зручності можна показувати вік у UI)
        /// </summary>
        public DateTime BirthDate { get; set; } = DateTime.Today;

        public string ParentFullName { get; set; } = string.Empty;
        public string ParentPhone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Медичні примітки / алергії (коротко)
        /// </summary>
        public string MedicalNotes { get; set; } = string.Empty;

        /// <summary>
        /// Примітки для батьків (повідомлення від вчителя/адміністрації)
        /// </summary>
        public string NotesForParents { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public GroupData? Group { get; set; }

        public List<GradeData> Grades { get; set; } = new();
    }
}
