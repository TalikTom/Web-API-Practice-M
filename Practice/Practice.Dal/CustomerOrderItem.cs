namespace Practice.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerOrderItem")]
    public partial class CustomerOrderItem
    {
        public Guid Id { get; set; }

        public Guid CustomerOrderId { get; set; }

        public Guid OrderItemId { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}
