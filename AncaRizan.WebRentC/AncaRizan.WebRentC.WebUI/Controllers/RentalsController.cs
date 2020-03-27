using Anca.Rizan.WebRentC.Core.Contracts;
using Anca.Rizan.WebRentC.Core.Models;
using Anca.Rizan.WebRentC.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AncaRizan.WebRentC.WebUI.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals

        IRepository<Reservation> context;
        IRepository<Location> location;
        IRepository<Car> car;

        public RentalsController(IRepository<Reservation> reservationContext, IRepository<Location> locationContext,
            IRepository<Car> carContext)
        {
            context = reservationContext;
            car = carContext;
           location = locationContext;
         
        }
        public ActionResult Index()
        {
            List<Reservation> reservations= context.Collection().ToList();
            return View(reservations);
        }

        public ActionResult Create()
        {
          
            using (var db = new RentCDataContext())
            {
                RentalsViewModel viewModel = new RentalsViewModel();


                viewModel.Reservation = new Reservation();
                viewModel.Locations = location.Collection();
                var selectedLocation = viewModel.SelectedLocationID;
                viewModel.Cars = new List<Car>();
                    
                    
                    /*from c in db.Cars
                            where c.LocationID == selectedLocation
                            select c;*/

                return View(viewModel);
            }

           


            //    return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            else
            {
                context.Insert(reservation);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int Id)
        {
            Reservation reservation= context.Find(Id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(reservation);
            }
        }

        [HttpPost]
        public ActionResult Edit(Reservation reservation, int Id)
        {
            Reservation reservationToEdit = context.Find(Id);

            if (reservationToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(reservation);
                }

                reservationToEdit.Car.Plate = reservation.Car.Plate;
                reservationToEdit.CostumerID = reservation.CostumerID;
                reservationToEdit.StartDate = reservation.StartDate;
                reservationToEdit.EndDate = reservation.EndDate;
                reservationToEdit.LocationID = reservation.LocationID;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int Id)
        {
            Reservation reservationToDelete = context.Find(Id);

            if (reservationToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(reservationToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            Reservation reservationToDelete = context.Find(Id);

            if (reservationToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}