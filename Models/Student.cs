using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPIprueba.Models
{
    public class Student
    {
        //[Display(Name = "Id")]
        public int StudentId { get; set; }
        //[Display(Name = "Nombre")]
        public string StudentName { get; set; }
        //[Display(Name = "Edad")]
        public int StudentAge { get; set; }
        //[Display(Name = "Sala de clases")]
        // public ClassRoom StudentClroom { get; set; }
    }
}