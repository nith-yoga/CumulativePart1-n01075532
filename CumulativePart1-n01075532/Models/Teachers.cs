using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace CumulativePart1_n01075532.Models
{
    public class Teachers
    {
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string TeacherNumber;
        public string HireDate;
        public string Salary;

        public bool isValid()
        {
            bool valid = true;

            if (TeacherFname == null || TeacherLname == null || TeacherNumber == null)
            {
                valid = false;
            }
            else
            {
                if (TeacherFname.Length < 2 || TeacherFname.Length > 255) valid = false;
                if (TeacherLname.Length < 2 || TeacherLname.Length > 255) valid = false;
            }
            return valid;
        }

        public Teachers() { }
    }
}