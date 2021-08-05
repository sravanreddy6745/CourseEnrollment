using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalSeats { get; set; }
        public Course()
        {

        }
        public Course(int id, string name, string desc, int totalseats)
        {
            this.Id = id;
            this.Name = name;
            this.Description = desc;
            this.TotalSeats = totalseats;

        }

    }
}
