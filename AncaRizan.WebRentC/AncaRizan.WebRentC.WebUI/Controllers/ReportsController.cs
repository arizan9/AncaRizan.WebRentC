using Anca.Rizan.WebRentC.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AncaRizan.WebRentC.WebUI.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            ReportsViewModel viewModel = new ReportsViewModel();
            

            return View(viewModel);
        }
    }
}