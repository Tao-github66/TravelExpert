using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop_5_Group_3.Models;

namespace Workshop_5_Group_3.Controllers
{
    public class AccountController : Controller
    {
        // database context
        TravelExpertsEntities db = new TravelExpertsEntities();

        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        // GET
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            TravelExpertsEntities db = new TravelExpertsEntities();

            var user = db.Customers.Where(m => m.UserId == cust.UserId).FirstOrDefault();

            if (user != null)
            {
                string encrptedPassword = EncryptDecrypt.Encrypt(cust.Password);

                var result = db.Customers.Where(m => m.UserId == cust.UserId &&
                                                    m.Password == encrptedPassword).FirstOrDefault();

                //var pwd = db.Customers.Where(m => m.UserId == userId).FirstOrDefault().ToString();
                //pwd = EncryptDecrypt.Decrypt(pwd);
                //cust.Password = EncryptDecrypt.Encrypt(cust.Password);
                if (result != null)
                {
                    Session["CustomerId"] = user.CustomerId;
                    return RedirectToAction("Edit", "Customers", new { id = user.CustomerId });
                }
                else
                {
                    ViewBag.Message = string.Format("Password is incorrect");
                    return View();
                }
            }
            else
            {
                ViewBag.Message = string.Format("User id or Password is incorrect");
                return View();
            }
            //var result = db.Customers.Where(m => m.UserId == cust.UserId &&
            //                                    m.Password == cust.Password).FirstOrDefault();
            //if (result != null)
            //{
            //    Session["CustomerId"] = result.CustomerId;
            //    //return RedirectToAction("Index", "Bookings");
            //    return RedirectToAction("Edit", "Customers", new { id = result.CustomerId });
            //}
            //else
            //{
            //    ViewBag.Message = string.Format("UserName and Password is incorrect");
            //    return View();
            //}
        }

    }
}