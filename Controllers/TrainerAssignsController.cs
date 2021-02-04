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
    public class TrainerAssignsController : Controller
    {
        private TrainingDBEntities1 db = new TrainingDBEntities1();

        // GET: TrainerAssigns
        public ActionResult Index(string searchString)
        {

            var trainerassign = from m in db.TrainerAssigns select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainerassign = trainerassign.Where(s => s.Trainer.TrainerName.Contains(searchString));
            }
            return View(trainerassign);


            //var trainerAssigns = db.TrainerAssigns.Include(t => t.Topic).Include(t => t.Trainer);
            //return View(trainerAssigns.ToList());
        }

        // GET: TrainerAssigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerAssign trainerAssign = db.TrainerAssigns.Find(id);
            if (trainerAssign == null)
            {
                return HttpNotFound();
            }
            return View(trainerAssign);
        }

        // GET: TrainerAssigns/Create
        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName");
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName");
            return View();
        }

        // POST: TrainerAssigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerAssignID,TopicID,TrainerID,Year")] TrainerAssign trainerAssign)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.TrainerAssigns.Add(trainerAssign);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "ID is already existed! ");
                    return View(trainerAssign);
                }
            }
        

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerAssign.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", trainerAssign.TrainerID);
            return View(trainerAssign);
        }

        // GET: TrainerAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerAssign trainerAssign = db.TrainerAssigns.Find(id);
            if (trainerAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerAssign.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", trainerAssign.TrainerID);
            return View(trainerAssign);
        }

        // POST: TrainerAssigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerAssignID,TopicID,TrainerID,Year")] TrainerAssign trainerAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainerAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerAssign.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", trainerAssign.TrainerID);
            return View(trainerAssign);
        }

        // GET: TrainerAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerAssign trainerAssign = db.TrainerAssigns.Find(id);
            if (trainerAssign == null)
            {
                return HttpNotFound();
            }
            return View(trainerAssign);
        }

        // POST: TrainerAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerAssign trainerAssign = db.TrainerAssigns.Find(id);
            db.TrainerAssigns.Remove(trainerAssign);
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
