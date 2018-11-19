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

        public ActionResult AddTask()
        {

            return View();
        }

        public ActionResult AddNew(Task newTask)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Capstone6Entities ORMtask = new Capstone6Entities();

                    var searchDup = ORMtask.Tasks.Where(x => x.taskID == newTask.taskID).SingleOrDefault();

                    if (searchDup == null)
                    {
                        ORMtask.Entry(newTask).State = System.Data.Entity.EntityState.Added;
                        ORMtask.SaveChanges();

                        ViewBag.AllItems = ORMtask.Tasks.ToList<Task>();
                        return RedirectToAction("UserTasksView");
                    }
                    else
                    {
                        ViewBag.WhereError = "Task ID Already in database";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.WhereError = "Model State Not Vaild";
                    return View("Error");

                }
            }
            catch (Exception)
            {
                ViewBag.WhereError = "Exception Error in Add Item";
                return View("Error");
            }
        }
    
        public ActionResult AddTaskActions()
        {
            //creating the ORM
            Capstone6Entities ORMAddtask = new Capstone6Entities();

            ViewBag.AddTask = ORMAddtask.Tasks.ToList(); // use edit item name to find old item


            ORMAddtask.SaveChanges();


            return RedirectToAction("UserTasksView");
        }

        public ActionResult UserTasksView()
        {
            //1. get database
            Capstone6Entities ormList = new Capstone6Entities();

            //2. Get user id
            User selectUser = ormList.Users.Find(1);

            //3.load items into viewbag matching userid

            ViewBag.theUserTasks = ormList.Tasks.Where(x => x.userID == selectUser.userID).ToList();

            //3.return view
            return View();
        }

        public ActionResult UserTaskComplete(int whichTasks)
        {
            //1. create ORM
            Capstone6Entities ORMtask = new Capstone6Entities();

            //2. find item
            Task getItem = ORMtask.Tasks.Find(whichTasks); // use edit item name to find old item

            //3. change item
            getItem.isComplete = true;
            //4. push to DB

            ORMtask.Entry(getItem).State = System.Data.Entity.EntityState.Modified;
            ORMtask.SaveChanges();

            //5. return to the list of item
            return RedirectToAction("UserTasksView"); // action to action instead of action to view

        }

        public ActionResult UserTaskNotComplete(int whichTasks)
        {
            //1. create ORM
            Capstone6Entities ORMtask = new Capstone6Entities();

            //2. find item
            Task getItem = ORMtask.Tasks.Find(whichTasks); // use edit item name to find old item

            //3. change item
            getItem.isComplete = false;
            //4. push to DB

            ORMtask.Entry(getItem).State = System.Data.Entity.EntityState.Modified;
            ORMtask.SaveChanges();

            //5. return to the list of item
            return RedirectToAction("UserTasksView"); // action to action instead of action to view

        }
        public ActionResult UserTaskDelete(int deleteItem)
        {

            //1. create ORM
            Capstone6Entities ORMtask = new Capstone6Entities();

            //2. get old item
            Task getItem = ORMtask.Tasks.Find(deleteItem); // use edit item name to find old item

            //4. push to DB

            ORMtask.Entry(getItem).State = System.Data.Entity.EntityState.Deleted;
            ORMtask.SaveChanges();

            //5. return to the list of item
            return RedirectToAction("UserTasksView"); // action to action instead of action to view

        }

        //for testing purposes
        public ActionResult AllUserTasksView()
        {
            //1. get database
            Capstone6Entities ormListTask = new Capstone6Entities();

            //3.load items into viewbag matching userid

            ViewBag.AllTasks = ormListTask.Tasks.ToList();

            //ViewBag.theUsersTasks = ormList.Tasks.Where(u => u.userID == getTasks.userID).ToString();

            //3.return view
            return View();
        }
    }
}