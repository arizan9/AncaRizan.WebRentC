using Anca.Rizan.WebRentC.Core.Contracts;
using Anca.Rizan.WebRentC.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace Anca.Rizan.WebRentC.Core.ViewModels
{
    public class RentalsViewModel 
    {
        public int CarID { get; set; }

        [Required]
        public int CostumerID { get; set; }

        public byte ReservStatsID { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [StringLength(10)]
        public string CouponCode { get; set; }

        public int LocationID { get; set; }

        public int ReservationID { get; set; }

        public IEnumerable<SelectListItem> Cars { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        
        

        public IEnumerable<SelectListItem> GetLocations()
        {
            using (var context = new RentCDataContext())
            {
                List<SelectListItem> locations = context.Locations.AsNoTracking()
                    .OrderBy(l => l.Name)
                        .Select(l =>
                        new SelectListItem
                        {
                            Value = l.LocationID.ToString(),
                            Text = l.Name
                        }).ToList();
                var locationtip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select location ---"
                };
                locations.Insert(0, locationtip);
                return new SelectList(locations, "Value", "Text");
            }
        }

        public IEnumerable<SelectListItem> GetCars()
        {
            List<SelectListItem> cars = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return cars;
        }

        public IEnumerable<SelectListItem> GetCars(int id)
        {
            if (id > 0)
            {
                using (var context = new RentCDataContext())
                {
                    IEnumerable<SelectListItem> cars = context.Cars.AsNoTracking()
                        .Where(c => c.LocationID == id)
                        .Select(n =>
                           new SelectListItem
                           {
                               Value = n.CarID.ToString(),
                               Text = n.Plate
                           }).ToList();
                    return new SelectList(cars, "Value", "Text");
                }
            }
            return null;
        }

        public static RentalsViewModel FromReservation(Reservation reservation)
        {
            return new RentalsViewModel
            {
                CarID = reservation.CarID,
                CostumerID = reservation.CostumerID,
                ReservStatsID = reservation.ReservStatsID,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                LocationID = reservation.LocationID
            };
        }

    }
}


