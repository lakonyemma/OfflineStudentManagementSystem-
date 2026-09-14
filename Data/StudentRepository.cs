using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OfflineStudentManagementSystem.Models;

namespace OfflineStudentManagementSystem.Data
{
    public static class StudentRepository
    {
        private static readonly List<Student> Students = new List<Student>
        {
            CreateStudent("ST001", "Lakony Emmanuel", "Software Engineering"),
            CreateStudent("ST002", "Amina Nansubuga", "Information Technology"),
            CreateStudent("ST003", "Daniel Okello", "Computer Science")
        };

        public static Student Authenticate(string studentId, string password, string department)
        {
            return Students.FirstOrDefault(student =>
                student.StudentId == studentId.Trim().ToUpper() &&
                student.Password == password &&
                student.Department == department);
        }

        private static Student CreateStudent(string studentId, string studentName, string department)
        {
            return new Student
            {
                StudentId = studentId,
                Password = "1234",
                StudentName = studentName,
                Department = department,
                YearOfStudy = "Year 2",
                RegistrationStatus = "Registered",
                TotalFees = 4000000m,
                AmountAlreadyPaid = 2500000m,
                Modules = new ObservableCollection<ModuleResult>
                {
                    new ModuleResult { ModuleCode = "DSE2101", ModuleName = "Database Management Systems", CreditUnits = 4, Lecturer = "Mr. Kato", CourseworkMark = 28, ExaminationMark = 61 },
                    new ModuleResult { ModuleCode = "DSE2102", ModuleName = "Object-Oriented Programming", CreditUnits = 4, Lecturer = "Ms. Auma", CourseworkMark = 25, ExaminationMark = 54 },
                    new ModuleResult { ModuleCode = "DSE2103", ModuleName = "Computer Networks", CreditUnits = 3, Lecturer = "Mr. Ochieng", CourseworkMark = 21, ExaminationMark = 48 },
                    new ModuleResult { ModuleCode = "DSE2104", ModuleName = "Web Application Development", CreditUnits = 4, Lecturer = "Ms. Nakato", CourseworkMark = 17, ExaminationMark = 42 },
                    new ModuleResult { ModuleCode = "DSE2105", ModuleName = "Systems Analysis and Design", CreditUnits = 3, Lecturer = "Dr. Mugisha", CourseworkMark = 15, ExaminationMark = 34 },
                    new ModuleResult { ModuleCode = "DSE2106", ModuleName = "Mobile Application Development", CreditUnits = 4, Lecturer = "Mr. Ssenyonga", CourseworkMark = 24, ExaminationMark = 51 }
                }
            };
        }
    }
}

