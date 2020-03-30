using Anca.Rizan.WebRentC.Core.Contracts;
using Anca.Rizan.WebRentC.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace Anca.Rizan.WebRentC.Core.ViewModels
{
    public class RentalsViewModel: IValidatableObject
    {
        public int CarID { get; set; }

        [Remote("CheckCustomer", "Rentals", HttpMethod = "POST", ErrorMessage = "Client is not in the database")]
        public int CostumerID { get; set; }

        public byte ReservStatsID { get; set; }

     
        public DateTime StartDate { get; set; }

       
        public DateTime EndDate { get; set; }

        public int LocationID { get; set; }

        public int ReservationID { get; set; }

        public int? CuponID { get; set; }

        public virtual Car Car { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Location Location { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }


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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return
                  new ValidationResult(errorMessage: "EndDate must be greater than StartDate",
                                       memberNames: new[] { "EndDate" });
            }


        }

    }
}


