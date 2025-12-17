using SchoolJournal.Data;

namespace SchoolJournal
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Ініціалізація БД (створення файлу + тестові дані при першому запуску)
            DbInitializer.Initialize();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new role_selection_form());
        }
    }
}
