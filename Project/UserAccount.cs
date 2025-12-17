using System;

namespace SchoolJournal
{
    /// <summary>
    /// Обліковий запис для входу в систему.
    /// Зв'язується з TeacherData (для вчителя) або з ChildData (для батьків).
    /// </summary>
    public class UserAccount
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public UserRole Role { get; set; }

        public int? TeacherId { get; set; }
        public TeacherData? Teacher { get; set; }

        public int? ChildId { get; set; }
        public ChildData? Child { get; set; }
    }
}
