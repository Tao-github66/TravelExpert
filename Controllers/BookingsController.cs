using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Workshop_5_Group_3.Models;

namespace Workshop_5_Group_3.Controllers
{
    public class BookingsController : Controller
    {
        private TravelExpertsEntities db = new TravelExpertsEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int id = (int)Session["CustomerId"];
            var bookings = db.Bookings.Include(b => b.Customer)
                                      .Include(b => b.Package)
                                      .Include(b => b.TripType)
                                      .Where(b => b.CustomerId == id);

  
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustFirstName");
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PkgName");
            ViewBag.TripTypeId = new SelectList(db.TripTypes, "TripTypeId", "TTName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,BookingDate,BookingNo,TravelerCount,CustomerId,TripTypeId,PackageId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustFirstName", booking.CustomerId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PkgName", booking.PackageId);
            ViewBag.TripTypeId = new SelectList(db.TripTypes, "TripTypeId", "TTName", booking.TripTypeId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustFirstName", booking.CustomerId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PkgName", booking.PackageId);
            ViewBag.TripTypeId = new SelectList(db.TripTypes, "TripTypeId", "TTName", booking.TripTypeId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,BookingDate,BookingNo,TravelerCount,CustomerId,TripTypeId,PackageId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustFirstName", booking.CustomerId);
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "PkgName", booking.PackageId);
            ViewBag.TripTypeId = new SelectList(db.TripTypes, "TripTypeId", "TTName", booking.TripTypeId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
