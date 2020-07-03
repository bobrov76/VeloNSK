using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriYars> CategoriYars { get; set; }
        public virtual DbSet<Competentions> Competentions { get; set; }
        public virtual DbSet<Distantion> Distantion { get; set; }
        public virtual DbSet<HelthStatus> HelthStatus { get; set; }
        public virtual DbSet<Ofise> Ofise { get; set; }
        public virtual DbSet<Participation> Participation { get; set; }
        public virtual DbSet<ResultParticipation> ResultParticipation { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlServer("Data Source=DESKTOP-2KQSE8S\\VELOAPI;Initial Catalog=test;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriYars>(entity =>
            {
                entity.HasKey(e => e.IdCategori)
                    .HasName("PK_Categori_Yars");

                entity.Property(e => e.IdCategori).HasColumnName("id_categori");

                entity.Property(e => e.Do).HasColumnName("do");

                entity.Property(e => e.Ot).HasColumnName("ot");
            });

            modelBuilder.Entity<Competentions>(entity =>
            {
                entity.HasKey(e => e.IdCompetentions)
                   .HasName("PK_Competentions");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.IdCompetentions)
                    .HasColumnName("id_competentions")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdDistantion).HasColumnName("id_distantion");
            });

            modelBuilder.Entity<Distantion>(entity =>
            {
                entity.HasKey(e => e.IdDistantion);

                entity.Property(e => e.IdDistantion).HasColumnName("id_distantion");

                entity.Property(e => e.Discriptions)
                    .HasColumnName("discriptions")
                    .IsUnicode(false);

                entity.Property(e => e.Lengs)
                    .HasColumnName("lengs")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NameDistantion)
                    .IsRequired()
                    .HasColumnName("name_distantion")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HelthStatus>(entity =>
            {
                entity.HasKey(e => e.IdHealth)
                    .HasName("PK_User_helth");

                entity.Property(e => e.IdHealth).HasColumnName("id_health");

                entity.Property(e => e.NameHealth)
                    .IsRequired()
                    .HasColumnName("name_health")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ofise>(entity =>
            {
                entity.HasKey(e => e.IdOfis);

                entity.Property(e => e.IdOfis).HasColumnName("id_ofis");

                entity.Property(e => e.Descriptio)
                    .IsRequired()
                    .HasColumnName("descriptio")
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasColumnName("latitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasColumnName("longitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfis)
                    .IsRequired()
                    .HasColumnName("name_ofis")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeClouse).HasColumnName("time_clouse");

                entity.Property(e => e.TimeOpen).HasColumnName("time_open");
            });

            modelBuilder.Entity<Participation>(entity =>
            {
                entity.HasKey(e => e.IdParticipation);

                entity.Property(e => e.IdParticipation).HasColumnName("id_participation");

                entity.Property(e => e.IdCompetentions).HasColumnName("id_competentions");

                entity.Property(e => e.IdStatusVerification).HasColumnName("id_status_verification");

                entity.Property(e => e.IdUser).HasColumnName("id_user");
            });

            modelBuilder.Entity<ResultParticipation>(entity =>
            {
                entity.HasKey(e => e.IdResultParticipation)
                    .HasName("PK_Result_Participation");

                entity.Property(e => e.IdResultParticipation).HasColumnName("id_result_participation");

                entity.Property(e => e.IdParticipation).HasColumnName("id_participation");

                entity.Property(e => e.Mesto).HasColumnName("mesto");

                entity.Property(e => e.ResultTime).HasColumnName("result_time");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.IdUsers)
                    .HasName("PK_Login_Users");

                entity.Property(e => e.IdUsers).HasColumnName("id_users");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fam)
                    .IsRequired()
                    .HasColumnName("fam")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdHelth).HasColumnName("id_helth");

                entity.Property(e => e.Isman).HasColumnName("isman");

                entity.Property(e => e.Jtoken)
                    .HasColumnName("jtoken")
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Patronimic)
                    .IsRequired()
                    .HasColumnName("patronimic")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnName("rol")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Years).HasColumnName("years");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
