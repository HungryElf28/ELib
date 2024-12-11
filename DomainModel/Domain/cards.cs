namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.cards")]
    public partial class cards
    {
        public int id { get; set; }

        [Required]
        [StringLength(16)]
        public string cardNum { get; set; }

        [Required]
        [StringLength(8000)]
        public string ownerName { get; set; }

        [Column(TypeName = "date")]
        public DateTime issuTerm { get; set; }

        public int userId { get; set; }

        public virtual users users { get; set; }
    }
}
