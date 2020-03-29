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
    public class CarsController : Controller
    {

        IRepository<Car> context;
        IRepository<Location> locationContext;
        // GET: Cars


        public CarsController(IRepository<Car> carContext, IRepository<Location> locContext)
        {
            context = carContext;
            locationContext = locContext;
        }

        public ActionResult Index(string sortBy)
        {
            IEnumerable<Car> cars = CarsViewModel.GetCarsFromWebServer();

            ViewBag.CarIDSort = String.IsNullOrEmpty(sortBy) ? "CarID_desc" : "";
            ViewBag.ManufacturerSort = sortBy == "Manufacturer" ? "Manufacturer_desc" : "Manufacturer";
            ViewBag.PriceSort = sortBy == "Price" ? "Price_desc" : "Price";
            ViewBag.LocationSort = sortBy == "Location" ? "Location_desc" : "Location";

            //@Html.ActionLink("Manufacturer", "Index", new { sortOrder = ViewBag.ManufacturerSortParam })
            //    @Html.DisplayNameFor(model => model.CarID)
            // @Html.DisplayNameFor(model => model.Manufacturer)
            // @Html.DisplayNameFor(model => model.PricePerDay)
            //@Html.DisplayNameFor(model => model.LocationID)

            switch (sortBy)
            {
                case "CarID_desc":
                    cars = cars.OrderByDescending(c => c.CarID);
                    break;
                case "Manufacturer":
                    cars = cars.OrderBy(c => c.Manufacturer);
                    break;
                case "Manufacturer_desc":
                    cars = cars.OrderByDescending(c => c.Manufacturer);
                    break;
                case "Price":
                    cars = cars.OrderBy(c => c.PricePerDay);
                    break;
                case "Price_desc":
                    cars = cars.OrderByDescending(c => c.PricePerDay);
                    break;
                case "Location":
                    cars = cars.OrderBy(c => c.LocationID);
                    break;
                case "Location_desc":
                    cars = cars.OrderByDescending(c => c.LocationID);
                    break;
                default:
                    cars = cars.OrderBy(c => c.CarID);
                    break;
            }


            return View(cars.ToList());
        }


        public ActionResult Create()
        {
            Car car= new Car();
            CarsViewModel viewModel = CarsViewModel.FromCar(car);
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(Car car)
        {
            Car currentCar = car;

            if (ModelState.IsValid)
            {
                context.Insert(car);
                context.Commit();

                return RedirectToAction("Index");
            }
            return View(car);
        }

        public ActionResult Edit(int Id)
        {
            Car car= context.Find(Id);
            CarsViewModel viewModel = CarsViewModel.FromCar(car);
            if (car == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Car car, int Id)
        {
            Car carToEdit= context.Find(Id);
            CarsViewModel viewModelToEdit = CarsViewModel.FromCar(carToEdit);

            if (carToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModelToEdit);
                }

                carToEdit.Plate = car.Plate;
                carToEdit.Manufacturer = car.Manufacturer;
                carToEdit.Model = car.Model;
                carToEdit.PricePerDay = car.PricePerDay;
                carToEdit.LocationID = car.LocationID;
              
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int Id)
        {
            Car carToDelete = context.Find(Id);

            if (carToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(carToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            Car carToDelete = context.Find(Id);

            if (carToDelete == null)
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