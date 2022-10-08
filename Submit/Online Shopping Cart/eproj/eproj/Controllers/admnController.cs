using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;
using eproj.Models;
using System.Data.Entity;

namespace admntemp.Controllers
{
    public class admnController : Controller
    {
        shopEntities ac = new shopEntities();

        // GET: admn
       
        
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(admin_detail a)
        {
            var k = ac.admin_detail.Where(x => x.admin_username == a.admin_username && x.admin_pass == a.admin_pass).FirstOrDefault();
            if (k != null)
            {
                if (k.admin_username == "admin")
                {
                    Session["username"] = k.admin_username;
                    Session["pass"] = k.admin_pass;
                    return RedirectToAction("Index");
                }


            }
            else
            {
                ViewBag.error = "Invalid login";

            }

            return RedirectToAction("index");

}

        public ActionResult logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("login");
        }
        public ActionResult Index()
        {
            return View();
        }
        //------------------------------------------start add_categories--------------------------------------------------------
        public ActionResult add_categories()
        {
            ViewBag.cate_name = new SelectList(ac.categories.ToList(), "cate_id", "cate_name");
            return View();
 }
        [HttpPost]
        public ActionResult add_categories(category c)
        {
            ViewBag.cate_name = new SelectList(ac.categories.ToList(), "cate_id", "cate_name");
            ac.categories.Add(c);
            try
            {
                ac.SaveChanges();

            }
            catch (Exception ex)
            {
                ViewBag.error = "error";
            }


            return RedirectToAction("listcategory");
        }
        //-----------------------list caegory------------------------------------------
        public ActionResult listcategory()
        {
            return View(ac.categories.OrderByDescending(b => b.cate_id).ToList());
        }
        //---------------------------------end category-------------------------------------

        //------------------------------------------delete category--------------------------
        public ActionResult deletecat(int id)
        {
            var sun = ac.categories.Where(kb => kb.cate_id == id).FirstOrDefault();
            ac.SaveChanges();
            ac.categories.Remove(sun);
           try
            {
                ac.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.err = ex.InnerException;
            }
            return RedirectToAction("listcategory");
        }


        //--------------------------------------------delete category-------------------------------

        //-------------------------------------edit category----------------------------------------
        //public ActionResult editcat(int id)
        //{
        //    var yes = ac.categories.Where(kb => kb.cate_id == id).FirstOrDefault();

        //    return View(yes);
        //}
        //[HttpPost]
        //public ActionResult editcat(category c)
        //{
        //    var yes = ac.categories.Where(kb => kb.cate_id == c.cate_id).FirstOrDefault();
        //    yes.cate_name = c.cate_name;

        //    ac.SaveChanges();
        //    return RedirectToAction("listcategory");
        //}
        //---------------------------------------------edit category------------------------------------

        //---------------------------------------------------end categories---------------------------

        //-------------------------start add_product----------------------------------


        public ActionResult add_products()
        {
            ViewBag.cate_id = new SelectList(ac.categories.ToList(), "cate_id", "cate_name");
            return View();
        }
        [HttpPost]
        public ActionResult add_products(product p, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
              
                string FileName = img.FileName;
                if(img.ContentType=="image/jpeg" || img.ContentType == "image/png")
                {
                    string path = Server.MapPath("~/pimage/" + FileName);

                    p.productimg = FileName;
                    img.SaveAs(path);
                    ac.products.Add(p);
                }
              
               
                try
                {
                    ac.SaveChanges();
                }
                catch(Exception ex)
                {
                  ViewBag.err = ex.ToString();
                }
                
                return RedirectToAction("index","home");
                

                
            }

            ViewBag.cate_id = new SelectList(ac.categories.ToList(), "cate_id", "cate_name");


            ac.products.Add(p);
            ac.SaveChanges();

            return RedirectToAction("listproduct");


        }
        //--------------------------end add_products------------------------------------------------

        //----------------------------------start list product-----------------------------------------
        public ActionResult listproduct()
        {
            return View(ac.products.OrderByDescending(b => b.pro_id).ToList());

        }
        //--------------------------------------end list product---------------------------------------
        //--------------------------------------start delete product-----------------------------------------
        public ActionResult deleteproduct(int id)
        {
            var sun = ac.products.Where(ab=> ab.pro_id == id).FirstOrDefault();
            ac.SaveChanges();
            ac.products.Remove(sun);
            ac.SaveChanges();
            return RedirectToAction("listproduct","admn");
        }
        //---------------------------------------end delete product--------------------------------------

        //------------------------------------start edit product------------------------------------
        public ActionResult editproducts(int id)
        {
        //    ViewBag.cate_id = new SelectList(ac.categories.ToList(), "cate_id", "cate_name");

            product pr = ac.products.Find(id);
            return View(pr);
            //product pr = ac.products.Find(id);
            //return View(pr);

            //var edit = ac.products.Where(a => a.pro_id == id).FirstOrDefault();

            //return View(edit);
        }
        [HttpPost]
        public ActionResult editproducts(product pr)
        {
            //if (ModelState.IsValid)
            //{

            //    string FileName = image.FileName;
            //    string path = Server.MapPath("~/pimage/" + FileName);
            //    pr.productimg = FileName;
            //    image.SaveAs(path);
            //    ac.products.Add(pr);
            //    try
            //    {
            //        ac.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        ViewBag.err = ex.ToString();
            //    }
            //}

            ac.Entry(pr).State = System.Data.Entity.EntityState.Modified;
            ac.SaveChanges();

            return RedirectToAction("listproduct", "admn");
        }
                //var edit = ac.products.Where(a => a.pro_id == p.pro_id).FirstOrDefault();
                //edit.product_name = p.product_name;
                //ac.SaveChanges();
                // return RedirectToAction("listproduct");
            
        //--------------------------------------end edit product----------------------------------
        //-----------------------------------------------------end product--------------------------------------------------------
        
       
        //------------------------------------------employees------------------------------------

        //---------------------------------------add_employee----------------------------------
        public ActionResult add_employee()
        {

            return View();
        }
        [HttpPost]
        public ActionResult add_employee(employee ad,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string FileName = image.FileName;
                string path = Server.MapPath("~/pimage/" + FileName);
                ad.eimg = FileName;
                image.SaveAs(path);
                ac.employees.Add(ad);

                try
                {
                    ac.SaveChanges();

                }
                catch (Exception ex)
                {
                    ViewBag.error = ex.ToString();
                }
            }

            
            ac.employees.Add(ad);
            try
            {
                ac.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.ToString();
            }

            return RedirectToAction("profile", "home");

        }
        //--------------------------------------------end add_employee--------------------------------

        //--------------------------------------start litemployee----------------------------------
        public ActionResult listemployee()
        {
            return View(ac.employees.OrderByDescending(kb => kb.emp_id).ToList());
        }
        //----------------------------end litemployee-----------------------------------------

        //------------------------------------start deleteemployee-----------------------

        public ActionResult deleteemployee(int id)
        {
            var sun = ac.employees.Where(kb => kb.emp_id == id).FirstOrDefault();
            ac.SaveChanges();
            ac.employees.Remove(sun);
            ac.SaveChanges();
            return RedirectToAction("listemployee");
        }
        //-----------------------------------end deleteemployee---------------------

        //-----------------------------start editemployee----------------------------------
        public ActionResult editemploye(int id)
        {
            employee ad = ac.employees.Find(id);
            return View(ad);

            //var s = ac.employees.Where(a => a.emp_id == id).FirstOrDefault();

           
        }

        [HttpPost]
        public ActionResult editemploye(employee ad)
        {
            ac.Entry(ad).State = System.Data.Entity.EntityState.Modified;
            ac.SaveChanges();

            return RedirectToAction("listemployee", "admn");

            //var no = ac.employees.Where(a => a.emp_id == ad.emp_id).FirstOrDefault();
            //no.emp_username=ad.emp_username;
            //no.emp_email = ad.emp_email;
            //no.emp_pass = ad.emp_pass;

            //ac.Entry(ad).State = System.Data.Entity.EntityState.Modified;
            //try
            //{
            //    ac.SaveChanges();
            //}
            //catch(Exception ex)
            //{
            //    ViewBag.err = "error";
            //}
          
          //  return RedirectToAction("listemployee");
        }
        //-------------------------------------end edit employee------------------------------------

        //------------------------------------------end employee----------------------------------------
        public ActionResult profile()
        {
            return View();
        }
        public ActionResult compose(string mail, string pass, string to, string subj, string msg)
        {
            try
            {
                WebMail.From = "asmaaris20@gmail.com";
                WebMail.UserName = "asmaaris20@gmail.com";
                WebMail.Password = "Today123";
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.SmtpUseDefaultCredentials = true;





                WebMail.Send(to: to, subject:subj, body: msg, isBodyHtml: true);
            }
            catch (Exception ex)
            {

                ViewBag.ex = ex.ToString();
            }
            return View();
        }
        public ActionResult inbox()
        {
            return View();
        }
        public ActionResult read()
        {
            return View();
        }
        public ActionResult lockscreen()
        {

            return View();
        }
        [HttpPost]
        public ActionResult lockscreen(admin_detail t)
        {
            var r = ac.admin_detail.Where(x => x.admin_pass == t.admin_pass).SingleOrDefault();
            if (r != null)
            {

                Session["pass"] = r.admin_pass;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid user";
            }
            return View();
        }

        public ActionResult faq()
        {
            return View();
        }

        [HttpPost]
        public ActionResult faq(faq i)
        {
            ac.faqs.Add(i);
            ac.SaveChanges();
            return View();
        }
        public ActionResult faqlist()
        {

            return View(ac.faqs.ToList());
        }

        public ActionResult customercontact()
        {
            return View(ac.contacts.ToList());
        }

        public ActionResult feedbackresult()
        {
            return View(ac.feedbacks.ToList());
        }

        public ActionResult editrem(int id)
        {
            category i = ac.categories.Find(id);
            return View(i);
        }
        [HttpPost]
        public ActionResult editrem(category i)
        {
            ac.Entry(i).State = EntityState.Modified;
            ac.SaveChanges();
           
            return RedirectToAction("listcategory","admn");
        }


        public ActionResult delpro(int id)
        {
            product p = ac.products.Find(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult delpro(product p)
        {
            ac.Entry(p).State = EntityState.Deleted;
            try
            {
                ac.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.err = ex.ToString();
            }
            return View(p);
        }
    }
}
