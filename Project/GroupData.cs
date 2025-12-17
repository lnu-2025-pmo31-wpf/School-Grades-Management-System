using System.Collections.Generic;

namespace SchoolJournal
{
    public class GroupData
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string AgeCategory { get; set; } = string.Empty;

        public int MaxChildren { get; set; }
        public int CurrentChildren { get; set; }

        // Залишено для сумісності з поточним UI (поля форми "Групи").
        // Коли все перейде на таблицю Teachers — це поле можна буде прибрати.
        public string Teacher { get; set; } = string.Empty;

        public string Room { get; set; } = string.Empty;

        // Навігаційні властивості (зв'язки)
        public List<TeacherData> Teachers { get; set; } = new();
        public List<ChildData> Children { get; set; } = new();
    }
}
