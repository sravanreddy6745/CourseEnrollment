using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public long Mobile { get; set; }

        public string CourseName { get; set; }

        public Student()
        {

        }
        public Student(int id, string fname, string lname, string email, long mobile, string coursename)
        {
            this.Id = id;
            this.FirstName = fname;
            this.LastName = lname;
            this.Email = email;
            this.Mobile = mobile;
            this.CourseName = coursename;

        }

    }
}
