
using System.Web.Mvc;
using DataLayer;
using ViewModels;

namespace systemMangement.Controllers
{

    public class AjaxController : Controller
    {
        private SystemManagementDBEntities db = new SystemManagementDBEntities();
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ajaxCall()
        {
            string message = "Hello i am from ajax call";
            System.Threading.Thread.Sleep(1000);
            return Json(new { message = message });
        }



        public ActionResult TaskList()
        {
            //
            //return View();
            return View();
        }

        [HttpPost]
        public ActionResult TaskListpost()
        {
            var result = db.Tasks;
            System.Threading.Thread.Sleep(1000);
            return Json(result);
        }

        public ActionResult AddUsser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUsser(string UserName, string Password, AddUsser model)
        {
            model.UserName = UserName;
            model.Password = Password;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}