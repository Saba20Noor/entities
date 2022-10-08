using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;
using eproj.Models;


namespace eproj.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        shopEntities ac = new shopEntities();
        public ActionResult Index(string search)
        {
            var result = ac.categories.Where(p => p.cate_name == search).ToList();
            return View(result);
        }

        public ActionResult shop()
        {

            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid = DateTime.Today.Minute + digit + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Millisecond.ToString();

            ViewBag.proid = proid;
            var a = ac.products.ToList().Where(x => x.category.cate_name == "cloths");
            return View(a);
        }
        public ActionResult about()
        {
            return View();
        }
   
        public ActionResult page404()
            {
            return View();
            }

        public ActionResult contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult contact(contact i)
        {
            ac.contacts.Add(i);
            try
            {
                ac.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.err = "error";
            }
        
            return View();
        }
        public ActionResult contactdetail()
        {
            var a = ac.contacts.ToList();
            return View(a);
        }

        public ActionResult address()
        {
            return View();
        }

        public ActionResult typography()
        {
            return View();
        }
        public ActionResult pricing()
        {
            return View();
        }
        public ActionResult faq()
        {
            return View(ac.faqs.ToList());
        }
       
        public ActionResult confirmation()
        {
            return View(ac.order_detail.ToList());
        }
        public ActionResult orders()
        {

         

          
            return View(ac.order_detail.ToList());
        }
        [HttpPost]
        public ActionResult orders(admin_detail a,string txtsearch)
        {
            
            
            
       
            var result=ac.order_detail.Where(model =>model.full_name.Contains(txtsearch)).ToList();
            return View(result);
        }

        public ActionResult profile(employee i)
        {
        
            return View(ac.employees.ToList());
        }

        public ActionResult productdetails()
        {
            return View();
        }

        public ActionResult sidebar(product i)
        {
            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid = DateTime.Today.Minute + digit + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Hour.ToString() + digit;

            ViewBag.proid = proid;


            var a = ac.products.ToList().Where(x => x.category.cate_name == "sidebar");
            return View(a);
           
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(customer i,admin_detail b)
        {
          
            var t = ac.customers.Where(a => a.cust_username == i.cust_username).SingleOrDefault();
            if(t!=null)
            {
                Session["custid"] = t.cust_id;
                if (Crypto.VerifyHashedPassword(t.cust_pass, i.cust_pass))
                {
                    Session["username"] = t.cust_username;
                    return RedirectToAction("index");
                }
                else
                {
                  ViewBag.err = "invalid";
                }
                //if (Session["username"] == "admin")
                //{
                //    Session["username"] = t.cust_username;
                //}
                
                
                    
                

            }
            //if (Crypto.VerifyHashedPassword(t.cust_pass, i.cust_pass))
            //{
            //    Session["username"] = t.cust_username;
            //    return RedirectToAction("index");
            //}
            //else
            //{
            //    ViewBag.err = "invalid";
            //}
            //if (Session["username"] == "admin")
            //{
            //    Session["username"] = t.cust_username;
            //}
            //else
            //{
            //    return RedirectToA
            //}



            return View();
        }
        public ActionResult logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("login");
        }

        public ActionResult addcart( cart c, int id)

        {
            product p1 = ac.products.Find(id);

            c.cust_id =4;          //Convert.ToInt32(Session["cust_id"]);
            c.pro_id = id;
            c.total = p1.price;
            c.cqty = 1;

            ac.carts.Add(c);
            try
            {
                ac.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.err = ex.ToString();
            }

            return RedirectToAction("addtocart");
        
    }

        public ActionResult addtocart()
        {

            int id = Convert.ToInt32(Session["custid"]);
            return View(ac.carts.ToList().Where(x=>x.cust_id==id));
        }

       [HttpPost]
        public ActionResult addcart(cart c, int? id)
        {
            
            product p1 = ac.products.Find(id);

            c.cust_id = 4;          //Convert.ToInt32(Session["cust_id"]);
            c.pro_id = id;
            c.total = p1.price;
            
            ac.carts.Add(c);
            try {
                ac.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.err = ex.ToString();
            }
        
            return RedirectToAction("addtocart");
        }
        public ActionResult removecart(int id)
        {
            var t = ac.carts.Where(kb => kb.cart_id == id).FirstOrDefault();
          
            ac.carts.Remove(t);
            ac.SaveChanges();

            return RedirectToAction("addtocart");
           
        }
        public ActionResult signin()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult signin(customer i)
        {

            string encrypted_pass = Crypto.HashPassword(i.cust_pass);
            i.cust_pass = encrypted_pass;

            ac.customers.Add(i);
           
               ac.SaveChanges();
         

            return RedirectToAction("index");
        }


        public ActionResult order_reg()
        {
            return View();
        }

        public ActionResult checkout(payment_method i)
        {
            ViewBag.pay_id = new SelectList(ac.payment_method.ToList(), "pay_id", "pay_type");
            return View();
        }
        [HttpPost]
        public ActionResult checkout(order_detail i)
        {
            ViewBag.pay_id = new SelectList(ac.payment_method.ToList(), "pay_id", "pay_type");
            ac.order_detail.Add(i);
            ac.SaveChanges();
            return View();
        }
        public ActionResult list()
        {
            var a = ac.customers.ToList();
            return View(a);
        }

     
        public ActionResult shoes(product i)
        {
            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid = DateTime.Today.Minute + digit + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Millisecond.ToString()+digit+DateTime.Today.Hour.ToString()+digit;
            
                ViewBag.proid = proid;


            var a = ac.products.ToList().Where(x=>x.category.cate_name=="shoes"); 
            return View(a);
         
        }
        public ActionResult toys()
        {
            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid = DateTime.Today.Millisecond.ToString() + DateTime.Today.Minute + digit + DateTime.Today.Millisecond.ToString() + digit +/* DateTime.Today.Millisecond.ToString() +*/ digit + DateTime.Today.Hour.ToString() + digit;

            ViewBag.proid = proid;


            var a = ac.products.ToList().Where(x => x.category.cate_name == "toys");
            return View(a);
        }
        public ActionResult jewellery()
        {
            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid = DateTime.Today.Millisecond.ToString() + DateTime.Today.Minute + digit +/* DateTime.Today.Millisecond.ToString() +*/ digit + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Hour.ToString() + digit;

            ViewBag.proid = proid;

            var a = ac.products.ToList().Where(x => x.category.cate_name == "jewellery");
            return View(a);
        }
        public ActionResult makeup()
        {
            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid = /*DateTime.Today.Minute*/  digit + DateTime.Today.Minute + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Millisecond.ToString() + digit + DateTime.Today.Hour.ToString() + digit;

            ViewBag.proid = proid;
            var a = ac.products.ToList().Where(x => x.category.cate_name == "makeup");
            return View(a);
        }


        public ActionResult perfume()
        {
            Random r1 = new Random();
            int digit = r1.Next(7);
            string proid =   digit + DateTime.Today.Hour + DateTime.Today.Second.ToString() + digit + digit+ digit + digit;

            ViewBag.proid = proid;
            var a = ac.products.ToList().Where(x => x.category.cate_name == "perfume");
            return View(a);
        }
        public ActionResult blog()
        {
            return View();
        }
        public ActionResult empsignup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult empsignup(employee i)
        {
            Random r1 = new Random();
            string code = r1.Next(10, 9999).ToString() + r1.Next(10, 9999).ToString();
            string encrypted_pass = Crypto.HashPassword(i.emp_pass);
            i.emp_pass = encrypted_pass;
            ac.employees.Add(i);
            ac.SaveChanges();

            return RedirectToAction("index");
        }
        public ActionResult empdetail()
        {
            var a = ac.employees.ToList();
            return View(a);
        }

        public ActionResult emplogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult emplogin(employee i)
        {
            var t = ac.employees.Where(a => a.emp_username == i.emp_username).SingleOrDefault();
            if (Crypto.VerifyHashedPassword(t.emp_pass, i.emp_pass))
            {
                Session["username"] = t.emp_username;
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.err = "invalid";
            }
            return View();
        }

        public ActionResult feedback()
        {
            return View();

        }
        [HttpPost]
        public ActionResult feedback(feedback i)
        {
            ac.feedbacks.Add(i);
            ac.SaveChanges();
            return View();

        }

       
    
        public ActionResult check()
        {
            return View();
        }
        [HttpPost]
        public ActionResult check(order_detail i)
        {
            ac.order_detail.Add(i);
            ac.SaveChanges();
            return View();
        }

       
    }

    

}