using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Author: Harris Moattar
/// Set the model for persons data with proper display and types.
/// </summary>
namespace assignment2.Models
{
    [Table("persons")]
    public partial class Persons
    {
        public Persons()
        {
            Games = new HashSet<Games>();
        }

        [Column("person_id")]
        public int PersonId { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("last_name")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        [Column("division_id")]
        public int? DivisionId { get; set; }
        [EmailAddress]
        [Column("email")]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }
        [Column("gender")]
        [StringLength(1)]
        public string Gender { get; set; }
        [Required]
        [Column("birth_date", TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        [Column("address_line_1")]
        [StringLength(50)]
        public string AddressLine1 { get; set; }
        [Column("address_line_2")]
        [StringLength(50)]
        public string AddressLine2 { get; set; }
        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }
        [Column("province_id")]
        [StringLength(2)]
        public string ProvinceId { get; set; }
        [Column("postal_code")]
        [StringLength(7)]
        public string PostalCode { get; set; }
        [Column("phone")]
        [StringLength(20)]
        public string Phone { get; set; }
        [Column("player")]
        public bool? Player { get; set; }
        [Column("skill_level")]
        [StringLength(1)]
        public string SkillLevel { get; set; }
        [Column("team_id")]
        public int? TeamId { get; set; }
        [Column("jersey_number")]
        public int? JerseyNumber { get; set; }
        [Column("coach")]
        public bool? Coach { get; set; }
        [Column("coaching_experience")]
        [StringLength(500)]
        public string CoachingExperience { get; set; }
        [Column("referee")]
        public bool? Referee { get; set; }
        [Column("referee_experience")]
        [StringLength(500)]
        public string RefereeExperience { get; set; }
        [Column("administrator")]
        public bool? Administrator { get; set; }
        [Column("user_password")]
        [StringLength(20)]
        public string UserPassword { get; set; }

        [ForeignKey("DivisionId")]
        [InverseProperty("Persons")]
        public virtual Divisions Division { get; set; }
        [ForeignKey("ProvinceId")]
        [InverseProperty("Persons")]
        public virtual Provinces Province { get; set; }
        [ForeignKey("SkillLevel")]
        [InverseProperty("Persons")]
        public virtual Skills SkillLevelNavigation { get; set; }
        [ForeignKey("TeamId")]
        [InverseProperty("Persons")]
        public virtual Teams Team { get; set; }
        [InverseProperty("Referee")]
        public virtual ICollection<Games> Games { get; set; }
    }
}
