namespace SchoolJournal
{
    public class TeacherData
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Посада (наприклад: "Вчитель", "Асистент вчителя")
        /// </summary>
        public string Position { get; set; } = "Вчитель";

        /// <summary>
        /// Класний керівник класу (для відображення у списку класів)
        /// </summary>
        public bool IsPrimary { get; set; }

        public int GroupId { get; set; }
        public GroupData? Group { get; set; }

        public System.Collections.Generic.List<GradeData> Grades { get; set; } = new();
    }
}
