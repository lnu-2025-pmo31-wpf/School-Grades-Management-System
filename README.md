# School Grades Management System

PROTS KHRYSTYNA AND LAPCHUK SOLOMIA

The School Grades Management System is a desktop application for managing students, subjects, teachers, and grades in a school. The program is developed on the .NET platform using WPF for the graphical user interface. It enables tracking student performance, generating reports, and viewing educational achievement statistics without using a database, storing all data locally in files.
 
The goal of this project is to facilitate the work of school staff with student data, automate the calculation of average grades, generate class rankings, and create student report cards in a convenient format. The program also allows for quick searching of specific students or subjects through its search functionality.

## Main Features

1. **Student Management**
   - Adding new students with specified first name, last name, and class.
   - Editing and deleting student data.
   - Searching for students by various criteria such as first name, last name, or class.
   - Local information storage, allowing operation without a database connection.

2. **Subjects and Teachers**
   - Creating and editing lists of school subjects.
   - Assigning teachers to specific subjects.
   - Ability to add new teachers and edit their information.
   - Organization of the "teacher → subject → class" logic, simplifying grade book management.

3. **Grade Book**
   - Adding grades for each student in specific subjects.
   - Editing and deleting grades as needed.
   - Automatic calculation of a student's average grade per subject and the overall class average.
   - Visualization of grades in convenient table formats within WPF.

4. **Reports and Statistics**
   - Generating student report cards with all grades and average scores.
   - Student rankings within classes based on performance.
   - Ability to export reports to popular formats such as CSV or text files for further use or printing.
   - Performance statistics by subjects and classes.

5. **Local Data Storage**
   - All data is stored in local files or in the program's memory.
   - This allows the program to run without additional server or database configuration.
   - Easy backup and data recovery.

## Technologies Used

- **WPF (.NET)** – for creating a modern and user-friendly graphical interface, including tables, forms, and visual elements.
- **File System / Serialization** – for local storage of student data, grades, and reports without using a database.
- **Entity Framework (EF Core)** – for working with objects and managing data in memory.
- **ADO.NET** – for demonstrating work with SQL queries and local data.
- **C# (.NET)** – program logic and data
