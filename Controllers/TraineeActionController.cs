using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingMVCProject.Models;

namespace TrainingMVCProject.Controllers
{
    [Authorize(Roles = "Trainee")]
    public class TraineeActionController : Controller
    {
        TrainingDBEntities1 db = new TrainingDBEntities1();
        // GET: TrainerAction
        public ActionResult Profile()
        {
            var UserName = User.Identity.Name;
            var userId = UserName.Substring(4);

            var trainee = (from a in db.Trainees where a.TraineeID.ToString().Equals(userId) select a).FirstOrDefault();
            return View(trainee);
        }
        public ActionResult CourseAssigned()
        {
            var UserName = User.Identity.Name;
            var userId = UserName.Substring(4);
            var assigned = from b in db.TraineeAssigns where b.Trainee.TraineeID.ToString().Equals(userId) select b;

            return View(assigned.ToList());
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeID,TraineeName,TraineeAddress,TraineeAge,DOB,MProgramL,TOEIC,Experience")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(trainee);
        }
    }
}