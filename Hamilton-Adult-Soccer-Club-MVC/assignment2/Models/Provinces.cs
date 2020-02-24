using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment2.Models
{
    [Table("provinces")]
    public partial class Provinces
    {
        public Provinces()
        {
            Persons = new HashSet<Persons>();
        }

        [Column("province_id")]
        [StringLength(2)]
        public string ProvinceId { get; set; }
        [Column("province_name")]
        [StringLength(50)]
        public string ProvinceName { get; set; }

        [InverseProperty("Province")]
        public virtual ICollection<Persons> Persons { get; set; }
    }
}
