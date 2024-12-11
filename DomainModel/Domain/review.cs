namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.review")]
    public partial class review
    {
        public int id { get; set; }

        public int mark { get; set; }

        [StringLength(8000)]
        public string reviewText { get; set; }

        public int userId { get; set; }

        public int bookId { get; set; }

        public virtual books books { get; set; }

        public virtual users users { get; set; }
    }
}
