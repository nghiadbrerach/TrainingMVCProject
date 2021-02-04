using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingMVCProject.Models;

namespace TrainingMVCProject.Controllers
{
    [Authorize(Roles = "TrainingStaff")]
    public class TraineeAssignsController : Controller
    {
        private TrainingDBEntities1 db = new TrainingDBEntities1();

        // GET: TraineeAssigns
        public ActionResult Index(String searchString)
        {
           
            var traineeassign = from m in db.TraineeAssigns select m;
            if (!String.IsNullOrEmpty(searchString))
            {   
                traineeassign = traineeassign.Where(s => s.Course.CourseName.Contains(searchString));
                
            }
         return View(traineeassign);
            
        //var traineeAssigns = db.TraineeAssigns.Include(t => t.Course).Include(t => t.Trainee);
        //    return View(traineeAssigns.ToList());
        }

        // GET: TraineeAssigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeAssign traineeAssign = db.TraineeAssigns.Find(id);
            if (traineeAssign == null)
            {
                return HttpNotFound();
            }
            return View(traineeAssign);
        }

        // GET: TraineeAssigns/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName");
            return View();
        }

        // POST: TraineeAssigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeAssignID,CourseID,TraineeID")] TraineeAssign traineeAssign)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.TraineeAssigns.Add(traineeAssign);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "ID is already existed! ");
                    return View(traineeAssign);
                }
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", traineeAssign.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", traineeAssign.TraineeID);
            return View(traineeAssign);
        }

        // GET: TraineeAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeAssign traineeAssign = db.TraineeAssigns.Find(id);
            if (traineeAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", traineeAssign.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", traineeAssign.TraineeID);
            return View(traineeAssign);
        }

        // POST: TraineeAssigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeAssignID,CourseID,TraineeID")] TraineeAssign traineeAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traineeAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", traineeAssign.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", traineeAssign.TraineeID);
            return View(traineeAssign);
        }

        // GET: TraineeAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeAssign traineeAssign = db.TraineeAssigns.Find(id);
            if (traineeAssign == null)
            {
                return HttpNotFound();
            }
            return View(traineeAssign);
        }

        // POST: TraineeAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TraineeAssign traineeAssign = db.TraineeAssigns.Find(id);
            db.TraineeAssigns.Remove(traineeAssign);
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
