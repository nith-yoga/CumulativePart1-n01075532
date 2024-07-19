using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativePart1_n01075532.Models;

namespace CumulativePart1_n01075532.Controllers
{
    public class TeacherController : Controller
    {
        //GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        
        //GET: Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teachers> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        //GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teachers NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
    }
}