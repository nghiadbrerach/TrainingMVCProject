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
{    [Authorize(Roles ="TrainingStaff")]
    public class TopicAssignsController : Controller
    {
        private TrainingDBEntities1 db = new TrainingDBEntities1();

        // GET: TopicAssigns
        public ActionResult Index()
        {
            var topicAssigns = db.TopicAssigns.Include(t => t.Course).Include(t => t.Topic);
            return View(topicAssigns.ToList());
        }

        // GET: TopicAssigns/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicAssign topicAssign = db.TopicAssigns.Find(id);
            if (topicAssign == null)
            {
                return HttpNotFound();
            }
            return View(topicAssign);
        }

        // GET: TopicAssigns/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName");
            return View();
        }

        // POST: TopicAssigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicAssignID,TopicID,CourseID")] TopicAssign topicAssign)
        {
            if (ModelState.IsValid)
            {
                db.TopicAssigns.Add(topicAssign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", topicAssign.CourseID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", topicAssign.TopicID);
            return View(topicAssign);
        }

        // GET: TopicAssigns/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicAssign topicAssign = db.TopicAssigns.Find(id);
            if (topicAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", topicAssign.CourseID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", topicAssign.TopicID);
            return View(topicAssign);
        }

        // POST: TopicAssigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicAssignID,TopicID,CourseID")] TopicAssign topicAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", topicAssign.CourseID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", topicAssign.TopicID);
            return View(topicAssign);
        }

        // GET: TopicAssigns/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicAssign topicAssign = db.TopicAssigns.Find(id);
            if (topicAssign == null)
            {
                return HttpNotFound();
            }
            return View(topicAssign);
        }

        // POST: TopicAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TopicAssign topicAssign = db.TopicAssigns.Find(id);
            db.TopicAssigns.Remove(topicAssign);
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
