using System;
using System.Diagnostics.Contracts;
using System.Net.WebSockets;

namespace LINQMethodSyntax
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };



            var groupedGPA = studentGPAList.GroupBy(s => s.GPA);
          
            foreach (var GPAGroup in groupedGPA)
            {
                Console.WriteLine("Student GPA: " + GPAGroup.Key);
                foreach (StudentGPA s in GPAGroup)
                {
                    Console.WriteLine("Student ID: " + s.StudentID);
                    Console.WriteLine("----------------------------------");
                }
                Console.WriteLine();
            }

            var Clubs = studentClubList.OrderBy(c => c.ClubName).GroupBy(s => s.ClubName);
                foreach (var clubGroup in Clubs)
                {
                Console.WriteLine("Club Name: " + clubGroup.Key);
                foreach (StudentClubs s in clubGroup)
                {
                    Console.WriteLine(" Student ID: " + s.StudentID);
                    Console.WriteLine("----------------------------------");
                }
              
                    Console.WriteLine();
                }                    
           
           
            var countGPA = studentGPAList.Count();
            Console.WriteLine("----All students with a GPA between 2.5 and 4.0---");
            countGPA = studentGPAList.Count(s => s.GPA > 2.5);
            Console.WriteLine("The number of students with a GPA of 2.5 to a 4.o is " + countGPA);
            var totalTuition = studentList.Average(s => s.Tuition);
            Console.WriteLine("The average tuition for all students is " + string.Format("{0:c}", totalTuition));
           // totalTuition = studentList.Sum(s => s.Tuition);
           //Console.WriteLine("Tuition is " + string.Format("{0:c}", totalTuition));
            var maxTuition = studentList.Max(s => s.Tuition);
            Console.WriteLine("The max tuition for students is " + String.Format("{0:C}", maxTuition));
            
            Console.WriteLine("Students paying the max tuition per year are...");
            foreach (Student s in studentList)
            {
                for (int i = 0; i >= maxTuition; i++)
                {
                    maxTuition = studentList.Count(s => s.Tuition >= 5500);
                    Console.WriteLine("Name:" + s.StudentName);
                    Console.WriteLine("Major:" + s.Major);
                    Console.WriteLine("Tuition:" + s.Tuition);

                }

                
                Console.WriteLine($"Name: {s.StudentName} \tMajor:{s.Major} \tTuition:{s.Tuition}");
                Console.WriteLine();
             }
                var innerJoin = studentList.Join(studentGPAList,
                    student => student.StudentID,
                    SG => SG.StudentID,
                    (student, SG) => new
                    {
                        StudentName = student.StudentName,
                        StudentMajor = student.Major,
                        GPA = SG.GPA 
                    });
                Console.WriteLine("Student who belong to the GPA club");
                Console.WriteLine();
                foreach (var t in innerJoin)
                {
                    Console.WriteLine($"Name: {t.StudentName} \tMajor: {t.StudentMajor} \tGPA: {t.GPA}");
                    Console.WriteLine();
                }
                
                var innerJoin2 = studentList.Join(studentClubList,
                    student => student.StudentID,
                    studentName => studentName.StudentID,
                    (student, studentName) => new

                    {
                        studentName = student.StudentName,
                        ClubName = studentName.ClubName
                    });
            
                Console.WriteLine("student who belong in the  Game club");
                Console.WriteLine();
                foreach(var e in innerJoin2)
                {
                    Console.WriteLine($"---Name: {e.studentName}---");
                    Console.WriteLine();
                }
        
            
        }
    }
}