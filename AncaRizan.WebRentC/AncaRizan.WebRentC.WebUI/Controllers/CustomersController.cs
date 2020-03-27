using Anca.Rizan.WebRentC.Core.Contracts;
using Anca.Rizan.WebRentC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AncaRizan.WebRentC.WebUI.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        IRepository<Customer> context;

        public CustomersController(IRepository<Customer> customerContext)
        {
            context = customerContext;

        }
        public ActionResult Index()
        {
            List<Customer> customers = context.Collection().ToList();
            return View(customers);
        }

        public ActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            else
            {
                context.Insert(customer);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int Id)
        {
            Customer customer = context.Find(Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult Edit(Customer customer, int Id)
        {
            Customer customerToEdit = context.Find(Id);

            if (customerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                customerToEdit.Name = customer.Name;
                customerToEdit.BirthDate = customer.BirthDate;
                

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int Id)
        {
            Customer customerToDelete = context.Find(Id);

            if (customerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customerToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            Customer reservationToDelete = context.Find(Id);

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