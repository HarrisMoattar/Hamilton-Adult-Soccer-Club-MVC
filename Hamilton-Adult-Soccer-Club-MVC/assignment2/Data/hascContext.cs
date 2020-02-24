using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace assignment2.Models
{
    public partial class hascContext : DbContext
    {
        public hascContext()
        {
        }

        public hascContext(DbContextOptions<hascContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Divisions> Divisions { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=hasc");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Divisions>(entity =>
            {
                entity.HasKey(e => e.DivisionId)
                    .HasName("PK__division__90E7A125C95FF7BD");

                entity.Property(e => e.DivisionId).ValueGeneratedNever();

                entity.Property(e => e.DivisionName).IsUnicode(false);
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PK__games__FFE11FCF633647E3");

                entity.Property(e => e.GameId).ValueGeneratedNever();

                entity.Property(e => e.Field).IsUnicode(false);

                entity.Property(e => e.GameNotes).IsUnicode(false);

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.GamesAwayTeam)
                    .HasForeignKey(d => d.AwayTeamId)
                    .HasConstraintName("FK__games__away_team__46E78A0C");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.GamesHomeTeam)
                    .HasForeignKey(d => d.HomeTeamId)
                    .HasConstraintName("FK__games__home_team__45F365D3");

                entity.HasOne(d => d.Referee)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.RefereeId)
                    .HasConstraintName("FK__games__referee_i__47DBAE45");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__persons__543848DFBD2356F8");

                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.Property(e => e.AddressLine1).IsUnicode(false);

                entity.Property(e => e.AddressLine2).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.CoachingExperience).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.PostalCode).IsUnicode(false);

                entity.Property(e => e.ProvinceId).IsUnicode(false);

                entity.Property(e => e.RefereeExperience).IsUnicode(false);

                entity.Property(e => e.SkillLevel).IsUnicode(false);

                entity.Property(e => e.UserPassword).IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("FK__persons__divisio__403A8C7D");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__persons__provinc__412EB0B6");

                entity.HasOne(d => d.SkillLevelNavigation)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.SkillLevel)
                    .HasConstraintName("FK__persons__skill_l__4222D4EF");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__persons__team_id__4316F928");
            });

            modelBuilder.Entity<Provinces>(entity =>
            {
                entity.HasKey(e => e.ProvinceId)
                    .HasName("PK__province__08DCB60FDFCAC5AB");

                entity.Property(e => e.ProvinceId)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvinceName).IsUnicode(false);
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.HasKey(e => e.SkillLevel)
                    .HasName("PK__skills__4BDFF0B491C5BFA3");

                entity.Property(e => e.SkillLevel)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SkillDescription).IsUnicode(false);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .HasName("PK__teams__F82DEDBCF4E604A6");

                entity.Property(e => e.TeamId).ValueGeneratedNever();

                entity.Property(e => e.JerseyColour).IsUnicode(false);

                entity.Property(e => e.TeamName).IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("FK__teams__division___3D5E1FD2");
            });
        }
    }
}
