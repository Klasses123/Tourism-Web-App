using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TourismWebApp.Models;
using TourismWebApp.ViewModels;

namespace TourismWebApp.Controllers
{
    public class TourismTypeController : Controller
    {
        readonly TourismDB.TourismDB db = new TourismDB.TourismDB();
        
        public async Task<ActionResult> GetAllTT()
        {
            TourismTypesViewModel TT = new TourismTypesViewModel();

            await Task.Run(() =>
                TT.TourismTypes = db.TourismTypes.ToList()
            );

            if (TT == null)
            {
                return HttpNotFound();
            }

            return View(TT);
        }

        public async Task<ActionResult> GetTTById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TourismTypeViewModel ttVM = new TourismTypeViewModel();
            await Task.Run(() =>
            {
                ttVM.TourismType = db.TourismTypes.Find(id);
            });

            if (ttVM.TourismType == null)
            {
                return HttpNotFound();
            }

            await Task.Run(() =>
                ttVM.Travels = db.Travels.Where(x => x.TourismTypeName == ttVM.TourismType.Name).ToList()
            );

            return View(ttVM);
        }

        public async Task<ActionResult> GetTTByName(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TourismType tt = null;
            await Task.Run(() =>
                tt = db.TourismTypes.Where(x => x.Name == name).FirstOrDefault()
            );

            if (tt == null)
            {
                return HttpNotFound();
            }

            return Redirect($"GetTTById/{tt.Id}");
        }

        public ActionResult CreateTT()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTT([Bind(Include = "Name, Desc, ImgUrl")]TourismType tourismType)
        {
            if (ModelState.IsValid)
            {
                db.TourismTypes.Add(tourismType);
                db.SaveChanges();
            }

            return RedirectToAction("GetAllTT");
        }

        public ActionResult EditTT(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tourismType = db.TourismTypes.Find(id);
            if (tourismType == null)
            {
                return HttpNotFound();
            }

            return View(tourismType);
        }

        [HttpPost, ActionName("EditTT")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTTPut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tourismTypeToUpdate = db.TourismTypes.Find(id);
            if (TryUpdateModel(tourismTypeToUpdate, "",
                new string[] { "Name", "Desc", "ImgUrl" }))
            {
                db.SaveChanges();
                return RedirectToAction("GetAllTT");
            }
            //разобраться что тут вернется
            return View(tourismTypeToUpdate);
        }

        public ActionResult DeleteTT(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Ошибка удаления!";
            }

            var tourismType = db.TourismTypes.Find(id);
            if (tourismType == null)
            {
                return HttpNotFound();
            }

            return View(tourismType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTT(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tourismType = db.TourismTypes.Find(id);
            if (tourismType == null)
            {
                return HttpNotFound();
            }

            db.Entry(tourismType).State = EntityState.Deleted;
            db.TourismTypes.Remove(tourismType);
            db.SaveChanges();

            return RedirectToAction("GetAllTT");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TourismDB.TourismDB db = new TourismDB.TourismDB();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}