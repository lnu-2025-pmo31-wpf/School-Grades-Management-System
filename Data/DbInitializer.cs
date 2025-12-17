using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolJournal.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            using var db = new SchoolDbContext();

            // Якщо раніше була стара БД без потрібних таблиць,
            // то при спробі доступу буде "no such table".
            // Для навчального проєкту найпростіше — пересоздати БД.
            bool needsRecreate = false;
            try
            {
                _ = db.Teachers.Any();
                _ = db.Children.Any();
                _ = db.Children.Select(c => c.NotesForParents).Any();
                _ = db.Grades.Any();
                _ = db.Users.Any();
            }
            catch
            {
                needsRecreate = true;
            }

            if (needsRecreate)
            {
                db.Database.EnsureDeleted();
            }

            db.Database.EnsureCreated();

            // ===== КЛАСИ =====
            if (!db.Groups.Any())
            {
                for (int grade = 1; grade <= 11; grade++)
                {
                    db.Groups.Add(new GroupData
                    {
                        Name = $"{grade}-А",
                        AgeCategory = $"{grade} клас",
                        MaxChildren = 30,
                        CurrentChildren = 0,
                        Teacher = "",
                        Room = $"Каб. {100 + grade}"
                    });
                }

                db.SaveChanges();
            }

            // ===== ВЧИТЕЛІ =====
            if (!db.Teachers.Any())
            {
                var groups = db.Groups
                    .OrderBy(g => g.Name)
                    .ToList();

                string[] names =
                {
                    "Коваленко О.М.", "Петренко І.В.", "Шевчук Л.О.", "Мельник А.А.", "Бойко Н.М.",
                    "Ткаченко С.П.", "Кравчук І.П.", "Олійник В.М.", "Романюк Т.С.", "Демченко А.В.",
                    "Іваненко Н.О."
                };

                for (int i = 0; i < groups.Count && i < names.Length; i++)
                {
                    db.Teachers.Add(new TeacherData
                    {
                        FullName = names[i],
                        Phone = $"+38067{(1000000 + i * 12345) % 10000000:0000000}",
                        Email = $"teacher{i + 1}@school.ua",
                        Position = "Вчитель",
                        IsPrimary = true,
                        GroupId = groups[i].Id
                    });
                }

                db.SaveChanges();
            }

            // ===== УЧНІ =====
            if (!db.Children.Any())
            {
                var groups = db.Groups
                    .OrderBy(g => g.Name)
                    .ToList();

                string[] surnames =
                {
                    "Шевченко", "Коваль", "Мельник", "Бондар", "Бойко", "Ткачук", "Кравчук", "Олійник", "Романюк", "Демченко",
                    "Савченко", "Мороз", "Козак", "Сидоренко", "Литвин", "Паламарчук", "Поліщук", "Гнатюк", "Костенко", "Левченко"
                };

                string[] firstNames =
                {
                    "Марія", "Андрій", "Софія", "Денис", "Олена", "Максим", "Ірина", "Артем", "Вікторія", "Дмитро",
                    "Назар", "Аліна", "Богдан", "Катерина", "Михайло", "Юлія", "Олександр", "Поліна", "Тимур", "Вероніка"
                };

                string[] parentNames =
                {
                    "Оксана", "Ігор", "Наталія", "Сергій", "Тетяна", "Олег", "Світлана", "Володимир", "Ірина", "Андрій",
                    "Олена", "Микола", "Марина", "Роман", "Оксана", "Іван", "Наталія", "Сергій", "Тетяна", "Олег"
                };

                int idx = 0;
                foreach (var group in groups)
                {
                    int grade = 1;
                    var parts = group.Name.Split('-');
                    if (parts.Length > 0 && int.TryParse(parts[0], out var g)) grade = g;

                    int birthYear = 2019 - (grade - 1); // 1 клас ~ 2019, 11 клас ~ 2009

                    for (int k = 0; k < 2; k++)
                    {
                        var s = surnames[idx % surnames.Length];
                        var n = firstNames[(idx + 3) % firstNames.Length];
                        var p = parentNames[idx % parentNames.Length];

                        db.Children.Add(new ChildData
                        {
                            FullName = $"{s} {n}",
                            BirthDate = new DateTime(birthYear, (idx % 12) + 1, ((idx * 3) % 27) + 1),
                            ParentFullName = $"{s} {p}",
                            ParentPhone = $"+38050{(2000000 + idx * 24680) % 10000000:0000000}",
                            Address = "Львів",
                            MedicalNotes = "",
                            NotesForParents = grade <= 4 ? "Нагадування: змінне взуття та форма на фізкультуру." : "",
                            GroupId = group.Id
                        });

                        idx++;
                    }
                }

                db.SaveChanges();
            }

            // ===== ОЦІНКИ =====
            if (!db.Grades.Any())
            {
                var groups = db.Groups
                    .Include(g => g.Children)
                    .Include(g => g.Teachers)
                    .OrderBy(g => g.Name)
                    .ToList();

                foreach (var group in groups.Take(6))
                {
                    var student = group.Children.OrderBy(c => c.Id).FirstOrDefault();
                    var teacher = group.Teachers.OrderByDescending(t => t.IsPrimary).FirstOrDefault();
                    if (student == null || teacher == null) continue;

                    db.Grades.AddRange(
                        new GradeData { StudentId = student.Id, TeacherId = teacher.Id, Subject = "Математика", Value = 10, DateAssigned = DateTime.Today.AddDays(-7), Comment = "Добре" },
                        new GradeData { StudentId = student.Id, TeacherId = teacher.Id, Subject = "Українська мова", Value = 11, DateAssigned = DateTime.Today.AddDays(-5), Comment = "Чудово" },
                        new GradeData { StudentId = student.Id, TeacherId = teacher.Id, Subject = "Англійська", Value = 9, DateAssigned = DateTime.Today.AddDays(-3), Comment = "Потрібно повторити" }
                    );
                }

                db.SaveChanges();
            }

            // ===== КОРИСТУВАЧІ (логін/пароль) =====
            SeedUsers(db);

            // Оновлюємо кеш-поля в Groups: Teacher (імена вчителів) і CurrentChildren
            RefreshGroupCache(db);
            db.SaveChanges();
        }

        private static void SeedUsers(SchoolDbContext db)
        {
            // Щоб не плодити дублікати
            if (db.Users.Any()) return;

            // DIRECTOR
            var (directorHash, directorSalt) = PasswordHasher.HashPassword("12345");
            db.Users.Add(new UserAccount
            {
                Username = "director",
                Role = UserRole.Director,
                PasswordHash = directorHash,
                PasswordSalt = directorSalt
            });

            // TEACHERS → teacher1/12345, teacher2/12345...
            var teachers = db.Teachers.OrderBy(t => t.Id).ToList();
            int tIndex = 1;
            foreach (var t in teachers)
            {
                var (h, s) = PasswordHasher.HashPassword("12345");
                db.Users.Add(new UserAccount
                {
                    Username = $"teacher{tIndex}",
                    Role = UserRole.Teacher,
                    TeacherId = t.Id,
                    PasswordHash = h,
                    PasswordSalt = s
                });
                tIndex++;
            }

            // PARENTS → parent1/12345, parent2/12345... (прив'язка до конкретної дитини)
            var kids = db.Children.OrderBy(c => c.Id).ToList();
            int pIndex = 1;
            foreach (var c in kids)
            {
                var (h, s) = PasswordHasher.HashPassword("12345");
                db.Users.Add(new UserAccount
                {
                    Username = $"parent{pIndex}",
                    Role = UserRole.Parent,
                    ChildId = c.Id,
                    PasswordHash = h,
                    PasswordSalt = s
                });
                pIndex++;
            }

            // STUDENTS → student1/12345, student2/12345...
            int sIndex = 1;
            foreach (var c in kids)
            {
                var (h, s) = PasswordHasher.HashPassword("12345");
                db.Users.Add(new UserAccount
                {
                    Username = $"student{sIndex}",
                    Role = UserRole.Student,
                    ChildId = c.Id,
                    PasswordHash = h,
                    PasswordSalt = s
                });
                sIndex++;
            }

            db.SaveChanges();
        }

        public static void RefreshGroupCache(SchoolDbContext db)
        {
            var groups = db.Groups
                .Include(x => x.Teachers)
                .Include(x => x.Children)
                .ToList();

            foreach (var group in groups)
            {
                // Якщо Children таблиця реально використовується — синхронізуємо.
                // Якщо записів дітей нема — не затираємо вручну введене значення.
                if (group.Children.Count > 0)
                    group.CurrentChildren = group.Children.Count;

                // Якщо Teachers таблиця реально використовується — синхронізуємо.
                // Якщо вчителів в таблиці нема — залишаємо те, що введено у формі "Класи".
                if (group.Teachers.Count > 0)
                {
                    var orderedTeachers = group.Teachers
                        .OrderByDescending(t => t.IsPrimary)
                        .ThenBy(t => t.FullName)
                        .Select(t => t.FullName)
                        .ToList();

                    group.Teacher = string.Join(", ", orderedTeachers);
                }
                else if (string.IsNullOrWhiteSpace(group.Teacher))
                {
                    group.Teacher = "—";
                }
            }
        }
    }
}
