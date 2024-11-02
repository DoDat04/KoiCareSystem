using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject;

public partial class KoiCareContext : DbContext
{
    public KoiCareContext()
    {
    }

    public KoiCareContext(DbContextOptions<KoiCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fish> Fish { get; set; }

    public virtual DbSet<FoodCalculator> FoodCalculators { get; set; }

    public virtual DbSet<FoodType> FoodTypes { get; set; }

    public virtual DbSet<GrowthRecord> GrowthRecords { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Pond> Ponds { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<WaterParameter> WaterParameters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
        string connectString = config["ConnectionStrings:DefaultConnection"];
        return connectString;
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fish>(entity =>
        {
            entity.HasKey(e => e.FishId).HasName("PK__Fish__F82A5BF990A56431");

            entity.Property(e => e.FishId).HasColumnName("FishID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.Breed).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Length).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Member).WithMany(p => p.Fish)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fish_Member");

            entity.HasOne(d => d.Pond).WithMany(p => p.Fish)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fish_Ponds");
        });

        modelBuilder.Entity<FoodCalculator>(entity =>
        {
            entity.HasKey(e => e.CalcId).HasName("PK__FoodCalc__6C3690346CE23E45");

            entity.ToTable("FoodCalculator");

            entity.Property(e => e.CalcId).HasColumnName("CalcID");
            entity.Property(e => e.CalculationDate).HasColumnType("datetime");
            entity.Property(e => e.FishId).HasColumnName("FishID");
            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.RecommendedAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Fish).WithMany(p => p.FoodCalculators)
                .HasForeignKey(d => d.FishId)
                .HasConstraintName("FK_FoodCalculator_Fish");

            entity.HasOne(d => d.FoodType).WithMany(p => p.FoodCalculators)
                .HasForeignKey(d => d.FoodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FoodCalculator_FoodType");
        });

        modelBuilder.Entity<FoodType>(entity =>
        {
            entity.HasKey(e => e.FoodTypeId).HasName("PK__FoodType__D3D1546C639E7035");

            entity.ToTable("FoodType");

            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<GrowthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__GrowthRe__FBDF78C91CDA85C8");

            entity.ToTable("GrowthRecord");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FishId).HasColumnName("FishID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Length).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Fish).WithMany(p => p.GrowthRecords)
                .HasForeignKey(d => d.FishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrowthRecord_Fish");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B38EF44899C");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ_Member_Email").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasPrecision(6);
        });

        modelBuilder.Entity<Pond>(entity =>
        {
            entity.HasKey(e => e.PondId).HasName("PK__Ponds__D18BF854745490E6");

            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.CreateBy).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Depth).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Length).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(255);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Width).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Member).WithMany(p => p.Ponds)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ponds_Member");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A708FB82B");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ_Role_RoleName").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasPrecision(6);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasPrecision(6);
        });

        modelBuilder.Entity<WaterParameter>(entity =>
        {
            entity.HasKey(e => e.ParameterId).HasName("PK__WaterPar__F80C6297B0D9B3F2");

            entity.Property(e => e.ParameterId).HasColumnName("ParameterID");
            entity.Property(e => e.Ammonia).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.MeasurementDate).HasColumnType("datetime");
            entity.Property(e => e.Nitrate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nitrite).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PH)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("pH");
            entity.Property(e => e.PondId).HasColumnName("PondID");
            entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Pond).WithMany(p => p.WaterParameters)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WaterParameters_Ponds");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
