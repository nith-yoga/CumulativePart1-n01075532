using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativePart1_n01075532.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;
using ZstdSharp.Unsafe;
using System.Web;

namespace CumulativePart1_n01075532.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Teachers> ListTeachers()
        {
            MySqlConnection conn = School.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teachers> Teachers = new List<Teachers>{};
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherNumber = ResultSet["employeenumber"].ToString();
                Teachers NewTeacher = new Teachers();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;

                Teachers.Add(NewTeacher);
            }
            conn.Close();
            return Teachers;
        }

        [HttpGet]

        public Teachers FindTeacher(int TeacherId)
        {
            Teachers NewTeacher = new Teachers();
            
            MySqlConnection Conn = School.AccessDatabase();
            
            Conn.Open();
            
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT * from teachers where teacherid=" + TeacherId;
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Prepare();
            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int teacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherNumber = TeacherNumber;
                NewTeacher.HireDate = HireDate;
            }

            Conn.Close();

            return NewTeacher;
        }

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            MySqlConnection conn = School.AccessDatabase();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody]Teachers NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);
            Conn.Close();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "INSERT into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherNumber", NewTeacher.TeacherNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public void UpdateTeacher(int id, [FromBody]Teachers TeacherInfo)
        {
            MySqlConnection Conn = School.AccessDatabase();
            try
            {
                
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();

                cmd.CommandText = "UPDATE teachers SET teacherfname=@TeacherFname, teacherlname=@TeacherLname, teachernumber=@TeacherNumber, salary=@TeacherSalary WHERE teacherid=@TeacherId";
                cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
                cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
                cmd.Parameters.AddWithValue("@TeacherNumber", TeacherInfo.TeacherNumber);
                cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.Salary);
                cmd.Parameters.AddWithValue("@TeacherId", TeacherInfo.TeacherId);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex);
                throw new ApplicationException("There was an issue in the database", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new ApplicationException("There was an issue with the server", ex);
            }
            finally
            {
                Conn.Close();
            }
        }
    }
}
