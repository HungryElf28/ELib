namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.offline_book")]
    public partial class offline_book
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int book_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime download_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime delete_date { get; set; }

        public virtual books books { get; set; }

        public virtual users users { get; set; }
    }
}
