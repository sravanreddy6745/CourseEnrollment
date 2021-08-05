using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class CourseRepository
    {
        string con;
        SqlConnection sqlConnection;
        public CourseRepository()
        {
            con= ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            sqlConnection = new SqlConnection(con);
        }
        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            sqlConnection.Open();
            string querystring = "select Id,Name,Description,Totalseats from Courses";
            SqlCommand command = new SqlCommand(querystring, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                courses.Add(new Course(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3])));
            }
            sqlConnection.Close();
            return courses;
        }
        public List<Course> GetAvailableCourses()
        {
            List<Course> courses = new List<Course>();
            sqlConnection.Open();
            string querystring = "select Id,Name,Description,Totalseats from Courses where TotalSeats>0";
            SqlCommand command = new SqlCommand(querystring, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                courses.Add(new Course(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3])));
            }
            sqlConnection.Close();
            return courses;
        }
        public bool CreateNewCourse(Course course)
        {
            if (!isCourseAvailable(course.Name))
            {
                sqlConnection.Open();
                string query = "insert into Courses values(@name,@desc,@totalseats)";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@name", course.Name);
                command.Parameters.AddWithValue("@desc", course.Description);
                command.Parameters.AddWithValue("@totalseats", course.TotalSeats);
                int n=command.ExecuteNonQuery();
                sqlConnection.Close();
                return n == 1;
            }
            else
            {
                return false;
            }
        }

        public  bool isCourseAvailable(string courseName)
        {

            sqlConnection.Open();
            string querystring = "select count(*) from Courses where Name like @Name";
            SqlCommand command = new SqlCommand(querystring, sqlConnection);
            command.Parameters.AddWithValue("@Name", courseName);
            int count = (int)command.ExecuteScalar();
            sqlConnection.Close();
            return count > 0;
        }


        public bool SeatsAvailable(string courseName)
        {

            sqlConnection.Open();
            string querystring = "select Count(*) from Courses where Name like @Name and TotalSeats>0";
            SqlCommand command = new SqlCommand(querystring, sqlConnection);
            command.Parameters.AddWithValue("@Name", courseName);
            int count = (int)command.ExecuteScalar();
            sqlConnection.Close();
            return count > 0;
        }



    }
}