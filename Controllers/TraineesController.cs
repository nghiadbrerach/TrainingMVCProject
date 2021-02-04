using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrainingMVCProject.Models;

namespace TrainingMVCProject.Controllers
{

    [Authorize(Roles = "TrainingStaff,Admin")]
    public class TraineesController : Controller
    {   
        private TrainingDBEntities1 db = new TrainingDBEntities1();

        // GET: Trainees
        public ActionResult Index(string searchString)
        {

            var trainee = from m in db.Trainees select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainee = trainee.Where(s => s.TraineeName.Contains(searchString));
            }
            return View(trainee);
        }

        // GET: Trainees/Details/5
        public ActionResult Details(string id)
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

        // GET: Trainees/Create 
       
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create([Bind(Include = "TraineeID,TraineeName,TraineeAddress,TraineeAge,DOB,MProgramL,TOEIC,Experience")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Trainees.Add(trainee);
                    db.SaveChanges();

                    AuthenController.CreateAccount("Tnee" + trainee.TraineeID, "trainee", "Trainee");
                    return RedirectToAction("Index");
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", "ID is already existed! ");
                    return View(trainee);
                }

            }

            return View(trainee);
        }

        // GET: Trainees/Edit/5
        
       
        
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

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeID,TraineeName,TraineeAddress,TraineeAge,DOB,MProgramL,TOEIC,Experience")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainee);
        }

        // GET: Trainees/Delete/5
        [Authorize(Roles = "Admin,TrainingStaff")]
      
        public ActionResult Delete(string id)
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

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Trainee trainee = db.Trainees.Find(id);
            db.Trainees.Remove(trainee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
