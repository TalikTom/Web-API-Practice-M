namespace Practice.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerOrder")]
    public partial class CustomerOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerOrder()
        {
            CustomerOrderItem = new HashSet<CustomerOrderItem>();
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid ChefId { get; set; }

        public Guid WaiterId { get; set; }

        public virtual Chef Chef { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerOrderItem> CustomerOrderItem { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Waiter Waiter { get; set; }
    }
}
