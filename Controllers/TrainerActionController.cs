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
    [Authorize(Roles = "TrainingStaff,Trainer")]
    public class TrainerActionController : Controller
    {
        TrainingDBEntities1 db = new TrainingDBEntities1();
        // GET: TrainerAction
        public ActionResult Profile()
        {
            var UserName = User.Identity.Name;
            var userId = UserName.Substring(4);

            var trainer = (from a in db.Trainers where a.TrainerID.ToString().Equals(userId) select a).FirstOrDefault();
            return View(trainer);
        }
        public ActionResult TopicAssigned()
        {
            var UserName = User.Identity.Name;
            var userId = UserName.Substring(4);
            var assigned = from b in db.TrainerAssigns where b.Trainer.TrainerID.ToString().Equals(userId) select b;

            return View(assigned.ToList());
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerID,TrainerName,TrainerWorkingPlace,TrainerPhone,TrainerEmail")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(trainer);
        }
    }
}