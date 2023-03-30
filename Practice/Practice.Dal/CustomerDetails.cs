namespace Practice.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomerDetails
    {
        public Guid Id { get; set; }

        [Column(TypeName = "text")]
        public string Commentary { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
