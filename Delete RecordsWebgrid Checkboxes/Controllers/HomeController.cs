using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delete_RecordsWebgrid_Checkboxes.Models;

namespace Delete_RecordsWebgrid_Checkboxes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<UserTable> data = new List<Models.UserTable>();

            using (DeleteDBEntities db = new Models.DeleteDBEntities())
            {
                data = db.UserTables.ToList();
            }
                                

            return View(data);
        }

        [HttpPost]
        public ActionResult DeleteRecords(string[] UserIDs)
        {
            int[] id = null;

            if (UserIDs != null)
            {
                id = new int[UserIDs.Length];
                int j = 0;

                //assign the values in UserIDs array to int[] array by converting
                foreach (var i in UserIDs)
                {
                    int.TryParse(i, out id[j++]);
                }
            }

            //find and delete those selected records
            if (id != null && id.Length > 0)
            {
                List<UserTable> seletedList = new List<Models.UserTable>();

                using (DeleteDBEntities db = new Models.DeleteDBEntities ())
                {
                    seletedList = db.UserTables.Where(u => id.Contains(u.UserID)).ToList();

                    foreach (var i in seletedList)
                    {
                        db.UserTables.Remove(i);
                    }

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}