using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativePart1_n01075532.Models;
using MySql.Data.MySqlClient;

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
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"].ToString();
                Teachers NewTeacher = new Teachers();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherName = TeacherName;

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
            
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int teacherId = (int)ResultSet["teacherid"];
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"].ToString();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherName = TeacherName;
            }

            return NewTeacher;
        }
    }
}
