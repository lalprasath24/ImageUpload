using ImagsProj1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImagsProj1.Controllers
{
  
    public class HomeController : Controller
    {
         Entities db=new Entities();
        upload1 model=new upload1();
        
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult SaveImages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveImages(HttpPostedFileBase UploadImages)
        {
            if(UploadImages.ContentLength>0)
            {
                string ImageFileName = Path.GetFileName(UploadImages.FileName);

                string FolderPath = Path.Combine(Server.MapPath("~/UploadImages"),ImageFileName);


                
                UploadImages.SaveAs(FolderPath);

                model.image = UploadImages.FileName;
                
                 db.upload1.Add(model);
                db.SaveChanges();

                //Display the Images;

                var ImageObj=db.upload1.Where(u=>u.id==model.id).FirstOrDefault();
                if (ImageObj != null)
                {
                    model.image=ImageObj.image;
                }

                ViewBag.upload = "File Upload Successfully";
            }
            else
            {
                ViewBag.error = "File Not Upload";
            }

            return View(model);
        }


    }
}