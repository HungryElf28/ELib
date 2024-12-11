namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.administrators")]
    public partial class administrators
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string login { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string password { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string name { get; set; }

        [StringLength(20)]
        public string surname { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime reg_date { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8000)]
        public string e_mail { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int bonuses { get; set; }

        public bool? red_references { get; set; }

        public bool? red_books { get; set; }

        public bool? red_users { get; set; }
    }
}
