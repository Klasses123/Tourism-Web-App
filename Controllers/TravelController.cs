using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TourismWebApp.Models;
using TourismWebApp.ViewModels;

namespace TourismWebApp.Controllers
{
    public class TravelController : Controller
    {
        readonly TourismDB.TourismDB db = new TourismDB.TourismDB();

        public async Task<ActionResult> GetAllTravels()
        {
            TravelsViewModel travels = new TravelsViewModel();
            await Task.Run(() =>
                travels.Travels = db.Travels.ToList()
            );

            if (travels == null)
            {
                return HttpNotFound();
            }

            return View(travels);
        }

        public async Task<ActionResult> GetTravelById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TravelViewModel travel = null;
            await Task.Run(() =>
            {
                travel = new TravelViewModel { travel = db.Travels.Find(id) };
            });

            if (travel.travel == null)
            {
                return HttpNotFound();
            }

            await Task.Run(() =>
            {
                TravelOrganizer to = db.TravelOrganizers.Where(x => x.Name == travel.travel.TravelOrganizerName).FirstOrDefault();
                travel.TravelOrganizerEmail = to.Email;
                travel.TravelOrganizerPhone = to.PhoneNumber;
            });

            if (travel == null)
            {
                return HttpNotFound();
            }

            return View(travel);
        }

        public ActionResult CreateTravel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTravel([Bind(Include = "TourismTypeName, TravelOrganizerName, Name, ImgUrl, Desc")]Travel travel)
        {
            if (ModelState.IsValid)
            {
                db.Travels.Add(travel);
                db.SaveChanges();
            }

            return RedirectToAction("GetAllTravels");
        }

        public ActionResult EditTravel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }

            return View(travel);
        }

        [HttpPost, ActionName("EditTravel")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTravelPut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var travelToUpdate = db.Travels.Find(id);
            if (TryUpdateModel(travelToUpdate, "",
                new string[] { "TourismTypeId", "TravelOrganizerId", "Name", "ImgUrl", "Desc" }))
            {
                db.SaveChanges();
                return RedirectToAction("GetAllTravels");
            }
            //разобраться что тут вернется
            return View(travelToUpdate);
        }

        public ActionResult DeleteTravel(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Ошибка удаления!";
            }

            var travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }

            return View(travel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTravel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }

            db.Entry(travel).State = EntityState.Deleted;
            db.Travels.Remove(travel);
            db.SaveChanges();

            return RedirectToAction("GetAllTravels");
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