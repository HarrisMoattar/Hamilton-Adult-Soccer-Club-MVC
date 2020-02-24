using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment2.Models
{
    [Table("teams")]
    public partial class Teams
    {
        public Teams()
        {
            GamesAwayTeam = new HashSet<Games>();
            GamesHomeTeam = new HashSet<Games>();
            Persons = new HashSet<Persons>();
        }

        [Column("team_id")]
        public int TeamId { get; set; }
        [Column("team_name")]
        [StringLength(50)]
        public string TeamName { get; set; }
        [Column("jersey_colour")]
        [StringLength(50)]
        public string JerseyColour { get; set; }
        [Column("division_id")]
        public int? DivisionId { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("Teams")]
        public virtual Divisions Division { get; set; }
        [InverseProperty("AwayTeam")]
        public virtual ICollection<Games> GamesAwayTeam { get; set; }
        [InverseProperty("HomeTeam")]
        public virtual ICollection<Games> GamesHomeTeam { get; set; }
        [InverseProperty("Team")]
        public virtual ICollection<Persons> Persons { get; set; }
    }
}
