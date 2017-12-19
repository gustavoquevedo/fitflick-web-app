using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitFlickWebApp.DAL;
using FitFlickWebApp.Models;
using System.Net.Mail;

namespace NewsletterForm.Controllers
{
    [Authorize]
    public class AdvertisementController : Controller
    {
        private AdvertisementFormContext db = new AdvertisementFormContext();

        // GET: Advertisements
        public ActionResult Index()
        {
            return View(db.Advertisements.ToList());
        }

        // GET: Advertisements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Advertisements/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var ad = new Advertisement
            {
                WebsiteUrl = "http://",
                TwitterLink = "http://"
            };
            return View(ad);
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PaymentOption,BusinessName,EmailAddress,WebsiteUrl,AdvertisementType,TwitterLink,InstagramUserName,Description")] Advertisement advertisement, HttpPostedFileBase ImageFile)
        {
            advertisement.CreateDate = DateTime.Now;
            advertisement.ModifyDate = DateTime.Now;

            if (ModelState.IsValid)
            {            
                if (LoadImageFile(advertisement, ImageFile))
                {
                    //image loaded successfully
                    db.Advertisements.Add(advertisement);
                    db.SaveChanges();
                    return View("Preview", advertisement);
                }
            }
            return View(advertisement);
        }

        // GET: Advertisements/Preview/5
        public ActionResult Preview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        private static bool LoadImageFile(Advertisement advertisement, HttpPostedFileBase ImageFile)
        {
            //check if file > 2MB
            if (ImageFile.ContentLength > (2 * 1024 * 1024)) 
            {
                return false;
            }
            try
            {
                advertisement.ImageFileName = ImageFile.FileName;
                advertisement.ImageData = new byte[ImageFile.ContentLength];
                ImageFile.InputStream.Read(advertisement.ImageData, 0, ImageFile.ContentLength);
            }
            catch
            {
                return false;
            }
            return true;
        }

        // GET: Advertisements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PaymentOption,BusinessName,EmailAddress,WebsiteUrl,AdvertisementType,TwitterLink,InstagramUserName,Description")] Advertisement advertisement, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            db.Advertisements.Remove(advertisement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Advertisements/SetApproval/5
        [HttpPost, ActionName("SetApproval")]
        [ValidateAntiForgeryToken]
        public ActionResult SetApproval(int id, bool value)
        {
            Advertisement ad = db.Advertisements.Find(id);
            ad.Approved = value;
            db.SaveChanges();
            if(ad.Approved)
            {
                SendApprovalEmail(ad.EmailAddress);
            }
            return View("Index", db.Advertisements.ToList());
        }

        // GET: Advertisements/Policy
        [AllowAnonymous]
        public ActionResult Policy()
        {
            return View();
        }

        private void SendApprovalEmail(string emailAddress)
        {
            using (MailMessage mm = new MailMessage("fitflickwebapp@gmail.com", emailAddress))
            {
                mm.Subject = "FitFlick - Advertisement approved";
                mm.Body = "<p>Congratulations! Your ad has been approved</p><br><p>Kind regards,</p><p>The FitFlick team</p>";
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("fitflickwebapp", "Fit321Flick!");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
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
