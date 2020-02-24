using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment2.Models
{
    [Table("skills")]
    public partial class Skills
    {
        public Skills()
        {
            Persons = new HashSet<Persons>();
        }

        [Column("skill_level")]
        [StringLength(1)]
        public string SkillLevel { get; set; }
        [Column("skill_description")]
        [StringLength(50)]
        public string SkillDescription { get; set; }

        [InverseProperty("SkillLevelNavigation")]
        public virtual ICollection<Persons> Persons { get; set; }
    }
}
