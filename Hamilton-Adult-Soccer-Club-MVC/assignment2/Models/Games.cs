using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Author: Harris Moattar
/// Set the model for games data with proper display and types.
/// </summary>
namespace assignment2.Models
{
    [Table("games")]
    public partial class Games
    {
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("game_date", TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime? GameDate { get; set; }
        [Column("field")]
        [StringLength(50)]
        public string Field { get; set; }
        [Column("home_team_id")]
        public int? HomeTeamId { get; set; }
        [Column("home_team_score")]
        [Display(Name = "Score")]
        public int? HomeTeamScore { get; set; }
        [Column("away_team_id")]
        public int? AwayTeamId { get; set; }
        [Column("away_team_score")]
        [Display(Name = "Score")]
        public int? AwayTeamScore { get; set; }
        [Column("referee_id")]
        public int? RefereeId { get; set; }
        [Column("game_notes")]
        [StringLength(1000)]
        [Display(Name = "Game Notes")]
        public string GameNotes { get; set; }

        [ForeignKey("AwayTeamId")]
        [InverseProperty("GamesAwayTeam")]
        [Display(Name = "Away")]
        public virtual Teams AwayTeam { get; set; }
        [ForeignKey("HomeTeamId")]
        [InverseProperty("GamesHomeTeam")]
        [Display(Name = "Home")]
        public virtual Teams HomeTeam { get; set; }
        [ForeignKey("RefereeId")]
        [InverseProperty("Games")]
        public virtual Persons Referee { get; set; }
    }
}
