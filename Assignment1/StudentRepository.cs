
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class StudentRepository
    {
        string con;
        SqlConnection sqlConnection;
        CourseRepository courseRepository = new CourseRepository();
        public StudentRepository()
        {
            con = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            sqlConnection = new SqlConnection(con);
        }
        public List<Student> ViewAllStudents()
        {
            List<Student> students = new List<Student>();
            sqlConnection.Open();
            string querystring = "select * from Student";
            SqlCommand command = new SqlCommand(querystring, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student(
                    Convert.ToInt32(reader["ID"]), 
                    reader["FirstName"].ToString(), 
                    reader["lastName"].ToString(), 
                    reader["Email"].ToString(), 
                    Convert.ToInt64(reader["Mobile"]), 
                    reader["CourseName"].ToString()));
            }
            sqlConnection.Close();
            return students;
        }

        public bool AddStudent(Student student)
        {
            //check course availability
            //check student already enrolled or not

            bool course_avilable = courseRepository.isCourseAvailable(student.CourseName);
            bool seats_available = courseRepository.SeatsAvailable(student.CourseName);
            if (course_avilable && seats_available)
            {
                if(!isEnrolled(student.Email, student.CourseName))
                {
                    sqlConnection.Open();
                    string query = "insert into Student values(@fname,@lname,@email,@mobile,@coursename)";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.Parameters.AddWithValue("@fname", student.FirstName);
                    command.Parameters.AddWithValue("@lname", student.LastName);
                    command.Parameters.AddWithValue("email", student.Email);
                    command.Parameters.AddWithValue("@mobile", Convert.ToString(student.Mobile));
                    command.Parameters.AddWithValue("@coursename", student.CourseName);
                    int n = command.ExecuteNonQuery();

                    string querystring = "UPDATE Courses SET Totalseats=Totalseats-1 where Name like @cname ";
                    SqlCommand command1 = new SqlCommand(querystring, sqlConnection);
                    command1.Parameters.AddWithValue("@cname", student.CourseName);
                    int updatedrecords = command1.ExecuteNonQuery();


                    sqlConnection.Close();
                    return n == 1;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }
        public bool isEnrolled(string mail,string cname)
        {
            sqlConnection.Open();
            string query = "select count(*) from Student where Email like @mail and CourseName like @course";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@mail", mail);
            command.Parameters.AddWithValue("@course", cname);
            int count = (int)command.ExecuteScalar();
            sqlConnection.Close();
            return count > 0;
        }
    }
}
