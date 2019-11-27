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
    public class CustomersController : Controller
    {
        private TravelExpertsEntities db = new TravelExpertsEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Agent);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgtFirstName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CustFirstName,CustLastName,CustAddress,CustCity," +
                                    "CustProv,CustPostal,CustCountry,CustHomePhone,CustBusPhone,CustEmail,AgentId," +
                                    "UserId,Password,ConfirmPassword")] Customer customer)
        {
            string beforeEncrpt = customer.Password;
            string afterEncrpt = EncryptDecrypt.Encrypt(customer.Password);
            string confirmAfterEncrypt = EncryptDecrypt.Encrypt(customer.ConfirmPassword);
            string afterDecrypt = EncryptDecrypt.Decrypt(afterEncrpt);
            string confirmafterDecrypt = EncryptDecrypt.Decrypt(confirmAfterEncrypt);

            Console.WriteLine("*****" + beforeEncrpt + "*******" + afterEncrpt + "******" + afterDecrypt);
            try
            {              
                if (ModelState.IsValid)
                {
                    var pd = EncryptDecrypt.Encrypt(customer.Password);
                    var pdCnform = EncryptDecrypt.Encrypt(customer.ConfirmPassword);
                    customer.Password = pd;
                    customer.ConfirmPassword = pdCnform;
                    customer.CustEmail = customer.CustEmail.Trim();
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }catch(Exception e)
            {
                ViewBag.Errormessage = e.Message;
            }
            

            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgtFirstName", customer.AgentId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgtFirstName", customer.AgentId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustFirstName,CustLastName,CustAddress," +
                                "CustCity,CustProv,CustPostal,CustCountry,CustHomePhone," +
                                "CustBusPhone,CustEmail,AgentId,UserId,Password,ConfirmPassword")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pd = EncryptDecrypt.Encrypt(customer.Password);
                    var pdCnform = EncryptDecrypt.Encrypt(customer.ConfirmPassword);
                    customer.Password = pd;
                    customer.ConfirmPassword = pdCnform;
                    customer.CustEmail = customer.CustEmail.Trim();
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = (int)Session["CustomerId"] });
                }
            }
            catch (Exception e)
            {
                ViewBag.Errormessage = e.Message;
            }

            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgtFirstName", customer.AgentId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
