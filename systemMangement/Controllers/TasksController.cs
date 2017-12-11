using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace systemMangement.Controllers
{
    public class TasksController : Controller
    {
        private SystemManagementDBEntities db = new SystemManagementDBEntities();

        [AllowAnonymous]
        // GET: Tasks
        public ActionResult Index(Task tt)
        {
          
            var userInfo = Session["usserInfo"];
            var tasks = db.Tasks.Include(t => t.User).Include(t => t.State).ToList().OrderBy(c=>c.priority);
            var x = tasks.Where(c => c.StateId == 1);
            if(x!=null)
                foreach(var item in x)
                    item.priority = 2000;

            return View(tasks.ToList());
        }
      
        int UserId()
        {
            var userName = Session["usserInfo"];
            var userInfo= db.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = userInfo.ID;
            return userId;
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }
        [Authorize]
        // GET: Tasks/Create
        public ActionResult Create()
        {

            ViewBag.UserId = new SelectList(db.Users, "ID", "UserName");
            ViewBag.StateId = new SelectList(db.States, "ID", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,StateId,Date,priority,TaskTitle,Description")] Task task)
        {
            var userName = Session["usserInfo"];
            var userInfo = db.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = userInfo.ID;
            task.Date = DateTime.Now;
            task.UserId = userId;
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "ID", "UserName", task.UserId);
            ViewBag.StateId = new SelectList(db.States, "ID", "Name", task.StateId);
            return View(task);
        }
        [Authorize]
        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "ID", "UserName", task.UserId);
            ViewBag.StateId = new SelectList(db.States, "ID", "Name", task.StateId);
            return View(task);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,StateId,Date,priority,TaskTitle,Description")] Task task)
        {
            var userName = Session["usserInfo"];
            var userInfo = db.Users.FirstOrDefault(u => u.UserName == userName);
            var userId = userInfo.ID;
            task.UserId = userId;
            task.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (userName != null)
                {
                    
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.UserId = new SelectList(db.Users, "ID", "UserName", task.UserId);
            ViewBag.StateId = new SelectList(db.States, "ID", "Name", task.StateId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Test()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
