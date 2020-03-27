namespace Anca.Rizan.WebRentC.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation : BaseEntity
    {
        public int CarID { get; set; }

        public int CostumerID { get; set; }

        public byte ReservStatsID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [StringLength(10)]
        public string CouponCode { get; set; }

        public int LocationID { get; set; }

        public int ReservationID { get; set; }

        public virtual Car Car { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}
