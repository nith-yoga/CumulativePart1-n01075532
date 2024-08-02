using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativePart1_n01075532.Models;

using System.Diagnostics;

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
        public ActionResult List(string SearchKey)
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

        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teachers NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Ajax_New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherNumber, string HireDate, string Salary)
        {
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teachers NewTeacher = new Teachers();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherNumber = TeacherNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

    }
}