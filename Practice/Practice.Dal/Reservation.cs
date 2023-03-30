namespace Practice.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        public Guid Id { get; set; }

        public DateTime ReservationTime { get; set; }

        public int NoOfGuests { get; set; }

        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
