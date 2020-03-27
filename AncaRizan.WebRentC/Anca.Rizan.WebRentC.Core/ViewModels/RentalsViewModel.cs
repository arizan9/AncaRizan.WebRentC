using Anca.Rizan.WebRentC.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anca.Rizan.WebRentC.Core.ViewModels
{
    public class RentalsViewModel
    {
        public Reservation Reservation{ get; set; }

        [Required]
        [Display(Name = "Location")]
        public int SelectedLocationID { get; set; }
        public IEnumerable<Location> Locations { get; set; }


        [Required]
        [Display(Name = "Car")]
        public int SelectedCarID { get; set; }
        public IEnumerable<Car> Cars { get; set; }


        /* [Display(Name = "Customer Number")]
        public string CustomerID { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(75)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string SelectedCountryIso3 { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [Display(Name = "State / Region")]
        public string SelectedRegionCode { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }*/

    }
}
