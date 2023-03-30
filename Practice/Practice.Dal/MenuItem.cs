namespace Practice.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItem")]
    public partial class MenuItem
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Guid OrderItemId { get; set; }

        public Guid MenuId { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}
