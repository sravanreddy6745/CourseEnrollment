using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Exist = true;
            do
            {
                Console.WriteLine("Please Select the operation You Want to Perform:");
                Console.WriteLine("1:View All Courses" + "\n" + "2: View Only Available Courses \n" + "3: Add New Course \n" + "4: View All Students\n" + "5: Student New Registration\n"+"6: Exit");
                int operation = Convert.ToInt32(Console.ReadLine());
                CourseRepository courseRepository = new CourseRepository();
                StudentRepository studentRepository = new StudentRepository();

                switch (operation)
                {
                    case 1:
                        Console.WriteLine("All Courses Are");
                        List<Course> courses = courseRepository.GetAllCourses();
                        foreach (Course course in courses)
                        {
                            Console.WriteLine(course.Id + " " + course.Name + " " + course.Description + " " + course.TotalSeats);
                        }
                        Console.WriteLine("\n");
                        break;
                    case 2:
                        Console.WriteLine("Available Courses to Register");
                        List<Course> AvailableCourses = courseRepository.GetAvailableCourses();
                        foreach (Course course in AvailableCourses)
                        {
                            Console.WriteLine(course.Id + " " + course.Name + " " + course.Description + " " + course.TotalSeats);
                        }
                        Console.WriteLine("\n");
                        break;
                    case 3:
                        Console.WriteLine("Enter Course Details");
                        Course c = new Course();
                        Console.WriteLine("Enter Course name");
                         c.Name=Console.ReadLine();
                        Console.WriteLine("Enter Course Description");
                        c.Description = Console.ReadLine();
                        Console.WriteLine("Enter Seats Available");
                        c.TotalSeats = Convert.ToInt32(Console.ReadLine());
                        bool availble = courseRepository.CreateNewCourse(c);
                        if (!availble)
                        {
                            Console.WriteLine("Course Already Exist");
                        }
                        else
                        {
                            Console.WriteLine("Course Added successfully");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 4:
                        List<Student> students = studentRepository.ViewAllStudents();
                        foreach(Student s in students)
                        {
                            Console.WriteLine(s.FirstName + "  " + s.LastName + "  " + s.Email + "  " + s.CourseName);
                        }
                        Console.WriteLine("\n");
                        break;
                    case 5:
                        Student student = new Student();

                        Console.WriteLine("Enter first name");
                        student.FirstName = Console.ReadLine();

                        Console.WriteLine("Enter Last Name");
                        student.LastName = Console.ReadLine();

                        Console.WriteLine("Enter Mobile Number of the student");
                        student.Mobile = Convert.ToInt64(Console.ReadLine());

                        Console.WriteLine("Enter Email");
                        student.Email = Console.ReadLine();

                        Console.WriteLine("Enter Course Name");
                        student.CourseName = Console.ReadLine();

                        if (studentRepository.AddStudent(student))
                        {
                            Console.WriteLine("Students Details Added");
                        }
                        else
                        {
                            Console.WriteLine("Failed..... Check the Course and Student Details");
                        }
                        Console.WriteLine("\n");

                        break;
                    case 6:
                        Exist = false;
                        break;

                }
            } while (Exist);
            
        }
    }
}
