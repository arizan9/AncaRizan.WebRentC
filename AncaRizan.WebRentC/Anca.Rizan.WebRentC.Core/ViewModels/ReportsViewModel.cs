using Anca.Rizan.WebRentC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anca.Rizan.WebRentC.Core.ViewModels
{
    public class ReportsViewModel
    {
        public List<Customer> GoldCustomers { get; set; }

        public List<Customer> SilverCustomers { get; set; }

        public Car MostRentedCar { get; set; }

        public Car LeastRetedCar { get; set; }

        public List<Customer> GetGoldAndSilverCustomers(int count)
        {
            using (var db= new RentCDataContext())
            {
                var gsCustomers = from c in db.Customers
                                    where c.Reservations.Count >= count
                                    select c;
                return gsCustomers.ToList();
            }
        }

   

        public (Car, Car) GetMostAndLeastRentedCar()
        {
            using (var db = new RentCDataContext())
            {
                var cars = from c in db.Cars
                           orderby c.Reservations.Count
                           select c;
                Car leastRentedCar = cars.First();
              //  Car mostRentedCar = cars.LastOrDefault();

                var carsDescending = from c in db.Cars
                           orderby c.Reservations.Count descending
                           select c;

                Car mostRentedCar = cars.First();

                return (mostRentedCar, leastRentedCar);
            }
        }

        public ReportsViewModel()
        {
            GoldCustomers = GetGoldAndSilverCustomers(4);
            SilverCustomers = GetGoldAndSilverCustomers(2);
            MostRentedCar = GetMostAndLeastRentedCar().Item1;
            LeastRetedCar = GetMostAndLeastRentedCar().Item2;

        }
    }
}
