using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone6.Models; //added model to home controller cc

namespace Capstone6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AllUserTasks(Task getTasks)
        {

            //1. get database
            Capstone6Entities ormList = new Capstone6Entities();

            //2.load items into viewbag matching userid
            ViewBag.theUsersTasks = ormList.Tasks.Where(u => u.userID == getTasks.userID).ToString();


            //3.return view
            return View();
        }
        public ActionResult AllUserTasksView()
        {

            return View();
        }
    }
}