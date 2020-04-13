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
    public class TravelOrganizerController : Controller
    {
        readonly TourismDB.TourismDB db = new TourismDB.TourismDB();

        public async Task<ActionResult> GetAllTO()
        {
            TravelOrganizersViewModel TO = new TravelOrganizersViewModel();

            await Task.Run(
                () => TO.TravelOrganizers = db.TravelOrganizers.ToList()
            );

            if (TO == null)
            {
                return HttpNotFound();
            }

            return View(TO);
        }

        public async Task<ActionResult> GetTOByName(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TravelOrganizer to = null;

            await Task.Run(() =>
                to = db.TravelOrganizers.Where(x => x.Name == name).FirstOrDefault()
            );

            if (to == null)
            {
                return HttpNotFound();
            }

            return Redirect($"GetTOById/{to.Id}");
        }

        public async Task<ActionResult> GetTOById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TravelOrganizerViewModel toVM = new TravelOrganizerViewModel();

            await Task.Run(() =>
            {
                toVM.TravelOrganizer = db.TravelOrganizers.Find(id);
            });

            if (toVM.TravelOrganizer == null)
            {
                return HttpNotFound();
            }

            await Task.Run(() =>
                toVM.Travels = db.Travels.Where(x => x.TravelOrganizerName == toVM.TravelOrganizer.Name).ToList()
            );

            return View(toVM);
        }

        public ActionResult CreateTO()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTO([Bind(Include = "Name, Email, PhoneNumber, ImgUrl, Desc")]TravelOrganizer travelOrganizer)
        {
            if (ModelState.IsValid)
            {
                db.TravelOrganizers.Add(travelOrganizer);
                db.SaveChanges();
            }

            return RedirectToAction("GetAllTO");
        }

        public ActionResult EditTO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var travelOrganizer = db.TravelOrganizers.Find(id);
            if (travelOrganizer == null)
            {
                return HttpNotFound();
            }

            return View(travelOrganizer);
        }

        [HttpPost, ActionName("EditTO")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTOPut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var travelOrganizerToUpdate = db.TravelOrganizers.Find(id);
            if (TryUpdateModel(travelOrganizerToUpdate, "",
                new string[] { "Name", "Email", "PhoneNumber", "ImgUrl", "Desc" }))
            {
                db.SaveChanges();
                return RedirectToAction("GetAllTO");
            }
            //разобраться что тут вернется
            return View(travelOrganizerToUpdate);
        }

        public ActionResult DeleteTO(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Ошибка удаления!";
            }

            var travelOrganizer = db.TravelOrganizers.Find(id);
            if (travelOrganizer == null)
            {
                return HttpNotFound();
            }

            return View(travelOrganizer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var travelOrganizer = db.TravelOrganizers.Find(id);
            if (travelOrganizer == null)
            {
                return HttpNotFound();
            }

            db.Entry(travelOrganizer).State = EntityState.Deleted;
            db.TravelOrganizers.Remove(travelOrganizer);
            db.SaveChanges();

            return RedirectToAction("GetAllTO");
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