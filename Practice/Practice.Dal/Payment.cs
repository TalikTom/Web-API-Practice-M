namespace Practice.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public Guid Id { get; set; }

        public DateTime PaymentTime { get; set; }

        public decimal PaymentAmount { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
