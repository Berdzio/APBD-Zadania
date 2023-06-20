using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cwiczenia8.Entites;

public partial class ApbdContext : DbContext
{
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Albumy> Albumies { get; set; }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FireTruck> FireTrucks { get; set; }

    public virtual DbSet<FireTruckAction> FireTruckActions { get; set; }

    public virtual DbSet<Firefighter> Firefighters { get; set; }

    public virtual DbSet<Muzycy> Muzycies { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<Utwory> Utwories { get; set; }

    public virtual DbSet<Wytwornie> Wytwornies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=apbd;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.IdAction).HasName("Action_pk");

            entity.ToTable("Action");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Albumy>(entity =>
        {
            entity.HasKey(e => e.IdAlbumu);

            entity.ToTable("albumy");

            entity.HasIndex(e => e.IdWytwornia, "IX_albumy_IdWytwornia");

            entity.HasOne(d => d.IdWytworniaNavigation).WithMany(p => p.Albumies).HasForeignKey(d => d.IdWytwornia);
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.IdAnimal).HasName("Animal_pk");

            entity.ToTable("Animal");

            entity.Property(e => e.Area).HasMaxLength(200);
            entity.Property(e => e.Category).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Client_pk");

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(120);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("Client_Trip_pk");

            entity.ToTable("Client_Trip");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("Country_pk");

            entity.ToTable("Country");

            entity.Property(e => e.IdCountry).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.IdTrips).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Trip"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Country"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdTrip").HasName("Country_Trip_pk");
                        j.ToTable("Country_Trip");
                    });
        });

        modelBuilder.Entity<FireTruck>(entity =>
        {
            entity.HasKey(e => e.IdFiretruck).HasName("FireTruck_pk");

            entity.ToTable("FireTruck");

            entity.Property(e => e.OperationNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<FireTruckAction>(entity =>
        {
            entity.HasKey(e => new { e.IdFiretruck, e.IdAction }).HasName("FireTruck_Action_pk");

            entity.ToTable("FireTruck_Action");

            entity.Property(e => e.AssignmentDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdActionNavigation).WithMany(p => p.FireTruckActions)
                .HasForeignKey(d => d.IdAction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_4_Action");

            entity.HasOne(d => d.IdFiretruckNavigation).WithMany(p => p.FireTruckActions)
                .HasForeignKey(d => d.IdFiretruck)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_4_FireTruck");
        });

        modelBuilder.Entity<Firefighter>(entity =>
        {
            entity.HasKey(e => e.IdFirefighter).HasName("Firefighter_pk");

            entity.ToTable("Firefighter");

            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasMany(d => d.IdActions).WithMany(p => p.IdFirefighters)
                .UsingEntity<Dictionary<string, object>>(
                    "FirefighterAction",
                    r => r.HasOne<Action>().WithMany()
                        .HasForeignKey("IdAction")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Firefighter_Action_Action"),
                    l => l.HasOne<Firefighter>().WithMany()
                        .HasForeignKey("IdFirefighter")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Firefighter_Action_Firefighter"),
                    j =>
                    {
                        j.HasKey("IdFirefighter", "IdAction").HasName("Firefighter_Action_pk");
                        j.ToTable("Firefighter_Action");
                    });
        });

        modelBuilder.Entity<Muzycy>(entity =>
        {
            entity.HasKey(e => e.IdMuzyk);

            entity.ToTable("muzycy");

            entity.Property(e => e.Imie).HasMaxLength(100);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("Trip_pk");

            entity.ToTable("Trip");

            entity.Property(e => e.IdTrip).ValueGeneratedNever();
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Utwory>(entity =>
        {
            entity.HasKey(e => e.IdUtwor);

            entity.ToTable("utwory");

            entity.HasIndex(e => e.IdStudies, "IX_utwory_IdStudies");

            entity.Property(e => e.NazwaUtworu).HasMaxLength(30);

            entity.HasOne(d => d.IdStudiesNavigation).WithMany(p => p.Utwories).HasForeignKey(d => d.IdStudies);

            entity.HasMany(d => d.MuzycyIdMuzyks).WithMany(p => p.UtworyIdUtwors)
                .UsingEntity<Dictionary<string, object>>(
                    "MuzykUtwor",
                    r => r.HasOne<Muzycy>().WithMany().HasForeignKey("MuzycyIdMuzyk"),
                    l => l.HasOne<Utwory>().WithMany().HasForeignKey("UtworyIdUtwor"),
                    j =>
                    {
                        j.HasKey("UtworyIdUtwor", "MuzycyIdMuzyk");
                        j.ToTable("MuzykUtwor");
                        j.HasIndex(new[] { "MuzycyIdMuzyk" }, "IX_MuzykUtwor_muzycyIdMuzyk");
                        j.IndexerProperty<int>("MuzycyIdMuzyk").HasColumnName("muzycyIdMuzyk");
                    });
        });

        modelBuilder.Entity<Wytwornie>(entity =>
        {
            entity.HasKey(e => e.IdWytworni);

            entity.ToTable("wytwornie");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
