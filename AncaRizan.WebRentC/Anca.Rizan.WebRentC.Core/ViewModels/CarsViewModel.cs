using Anca.Rizan.WebRentC.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anca.Rizan.WebRentC.Core.ViewModels
{
    public class CarsViewModel
    {
        public int CarID { get; set; }

        public string Plate { get; set; }

        [Required]
        [StringLength(30)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Column(TypeName = "money")]
        public decimal PricePerDay { get; set; }

        public int LocationID { get; set; }

        public virtual Location Location { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }


        public static CarsViewModel FromCar(Car car)
        {
            return new CarsViewModel
            {
                CarID = car.CarID,
                Plate = car.Plate,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                PricePerDay = car.PricePerDay,
                LocationID = car.LocationID,
            };
        }
        public static List<Car> GetCarsFromWebServer()
        {
            localhost.WebService1 proxy = new localhost.WebService1();
            var jsonAvailableCars = proxy.AvalialbleCars();

            return JsonConvert.DeserializeObject<List<Car>>(jsonAvailableCars);

        }
    


    }
}
