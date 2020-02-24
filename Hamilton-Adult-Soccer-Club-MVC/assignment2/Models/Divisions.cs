using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment2.Models
{
    [Table("divisions")]
    public partial class Divisions
    {
        public Divisions()
        {
            Persons = new HashSet<Persons>();
            Teams = new HashSet<Teams>();
        }

        [Column("division_id")]
        public int DivisionId { get; set; }
        [Column("division_name")]
        [StringLength(50)]
        public string DivisionName { get; set; }
        [Column("teams_made")]
        public bool? TeamsMade { get; set; }

        [InverseProperty("Division")]
        public virtual ICollection<Persons> Persons { get; set; }
        [InverseProperty("Division")]
        public virtual ICollection<Teams> Teams { get; set; }
    }
}
