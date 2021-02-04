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
{   [Authorize(Roles ="Admin,TrainingStaff")]
    public class TrainingStaffsController : Controller
    {
        private TrainingDBEntities1 db = new TrainingDBEntities1();

        // GET: TrainingStaffs
        public ActionResult Index(string searchString)
        {
            var training = from m in db.TrainingStaffs select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                training = training.Where(s => s.TStaffName.Contains(searchString));
            }
            return View(training);
        }

        // GET: TrainingStaffs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            return View(trainingStaff);
        }

        // GET: TrainingStaffs/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TStaffID,TStaffName,TStaffAddress,TStaffPhone,TStaffEmail")] TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            { 
                try
                { 
                   db.TrainingStaffs.Add(trainingStaff);
                   db.SaveChanges();

                   AuthenController.CreateAccount("TningStaff" + trainingStaff.TStaffID, "trainingstaff", "TrainingStaff");
                   return RedirectToAction("Index");

                }catch (Exception ex)
                {
                    ModelState.AddModelError("", "ID is already existed! ");
                    return View(trainingStaff);
                }
            }

            return View(trainingStaff);
        }

        // GET: TrainingStaffs/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            return View(trainingStaff);
        }

        // POST: TrainingStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "TStaffID,TStaffName,TStaffAddress,TStaffPhone,TStaffEmail")] TrainingStaff trainingStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingStaff);
        }

        // GET: TrainingStaffs/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            if (trainingStaff == null)
            {
                return HttpNotFound();
            }
            return View(trainingStaff);
        }

        // POST: TrainingStaffs/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TrainingStaff trainingStaff = db.TrainingStaffs.Find(id);
            db.TrainingStaffs.Remove(trainingStaff);
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
