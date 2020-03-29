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
        IRepository<Location> locationContext;
        IRepository<Car> carContext;
        IRepository<Customer> customerContext;

        public RentalsController(IRepository<Reservation> reservationContext, IRepository<Location> locContext,
            IRepository<Car> cContext, IRepository<Customer> custContext)
        {
            context = reservationContext;
            carContext = cContext;
            locationContext = locContext;
            customerContext = custContext;


        }
        public ActionResult Index()
        {
            List<Reservation> reservations =context.Collection().ToList();
            return View(reservations);
        }

        [HttpGet]
        public ActionResult GetCars(int id)
        {
            RentalsViewModel viewModel = new RentalsViewModel();
            IEnumerable<SelectListItem> cars = viewModel.GetCars(id);
            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
      public JsonResult ActionResult(int customerID)
        {
            var customers = customerContext.Collection();
            if (customers.Any(c => c.CostumerID == customerID))
            {
                return Json("true");
            }
            else
            {
                return Json("Non-existing client");
            }
        }

        public ActionResult Create()
        {
            Reservation reservation = new Reservation();
            RentalsViewModel viewModel = RentalsViewModel.FromReservation(reservation);
            viewModel.Locations = viewModel.GetLocations();
            viewModel.Cars = viewModel.GetCars();
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            reservation.ReservStatsID = 1;

            if (ModelState.IsValid)
            {
                context.Insert(reservation);
                context.Commit();

                return RedirectToAction("Index");
            }
            return View(reservation);
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
                RentalsViewModel viewModel = RentalsViewModel.FromReservation(reservation);
                return View(viewModel);
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