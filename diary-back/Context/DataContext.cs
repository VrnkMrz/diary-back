using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using diary_back.Models;
using diary_back.Services;

namespace diary_back.Context;

public partial class DataContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantService _tenantService;
    public DataContext(
          DbContextOptions<DataContext> options,
          IHttpContextAccessor httpContextAccessor,
          ITenantService tenantService)
          : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantService = tenantService;
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Diaryentry> Diaryentries { get; set; }

    public virtual DbSet<Emotion> Emotions { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantId = _httpContextAccessor.HttpContext.Items["TenantId"]?.ToString();

        if (!string.IsNullOrEmpty(tenantId))
        {
            var connectionString = _tenantService.GetConnectionString(tenantId);
            optionsBuilder.UseNpgsql(connectionString);
        }
        else
        {
            throw new Exception("Tenant ID not found.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_pkey");

            entity.ToTable("company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commander).HasColumnName("commander");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CommanderNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.Commander)
                .HasConstraintName("fk_commander");
        });

        modelBuilder.Entity<Diaryentry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diaryentries_pkey");

            entity.ToTable("diaryentries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AiEmotionId).HasColumnName("ai_emotion_id");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.User).HasColumnName("user");
            entity.Property(e => e.UserEmotionId).HasColumnName("user_emotion_id");

            //entity.HasOne(d => d.AiEmotion).WithMany(p => p.DiaryentryAiEmotions)
            //    .HasForeignKey(d => d.AiEmotionId)
            //    .HasConstraintName("fk_ai_emotion");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Diaryentries)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("diaryentries_users_id_fk");

            //entity.HasOne(d => d.UserEmotion).WithMany(p => p.DiaryentryUserEmotions)
            //    .HasForeignKey(d => d.UserEmotionId)
            //    .HasConstraintName("fk_user_emotion");
        });

        modelBuilder.Entity<Emotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("emotions_pkey");

            entity.ToTable("emotions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmotionName)
                .HasMaxLength(50)
                .HasColumnName("emotion_name");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rank_pkey");

            entity.ToTable("rank");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RankName)
                .HasMaxLength(50)
                .HasColumnName("rank_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BirthdayDay).HasColumnName("birthday_day");
            entity.Property(e => e.BirthdayMonth).HasColumnName("birthday_month");
            entity.Property(e => e.BirthdayYear).HasColumnName("birthday_year");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_company");

            entity.HasOne(d => d.RankNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Rank)
                .HasConstraintName("fk_rank");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
