using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WILAPI.Models;

public partial class WilDbContext : DbContext
{
    public WilDbContext()
    {
    }

    public WilDbContext(DbContextOptions<WilDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblModule> TblModules { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblStaff> TblStaffs { get; set; }

    public virtual DbSet<TblStaffLecture> TblStaffLectures { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblStudentLecture> TblStudentLectures { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:wilapidbserver.database.windows.net;User ID=ST10085210;Password=Treepair521;Database=WIL-DB;Trusted_Connection=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblModule>(entity =>
        {
            entity.HasKey(e => e.ModuleCode).HasName("PK__tblModul__EB27D4322C4E50B5");

            entity.ToTable("tblModule");

            entity.Property(e => e.ModuleCode)
                .HasMaxLength(8)
                .IsFixedLength();
            entity.Property(e => e.ModuleName)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasMany(d => d.tblUsers).WithMany(p => p.ModuleCodes)
                .UsingEntity<Dictionary<string, object>>(
                    "TblUserModule",
                    r => r.HasOne<TblUser>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__tblUserMo__UserI__19DFD96B"),
                    l => l.HasOne<TblModule>().WithMany()
                        .HasForeignKey("ModuleCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__tblUserMo__Modul__18EBB532"),
                    j =>
                    {
                        j.HasKey("ModuleCode", "UserId").HasName("PK__tblUserM__3A5F58F8D34BA0AF");
                        j.ToTable("tblUserModules");
                        j.IndexerProperty<string>("ModuleCode")
                            .HasMaxLength(8)
                            .IsFixedLength();
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(8)
                            .IsFixedLength()
                            .HasColumnName("UserID");
                    });
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRole__8AFACE3A60A78ADE");

            entity.ToTable("tblRole");

            entity.Property(e => e.RoleId)
                .HasMaxLength(5)
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(25);
        });

        modelBuilder.Entity<TblStaff>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblStaff__96D4AAF7F85862C2");

            entity.ToTable("tblStaff");

            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.RoleId)
                .HasMaxLength(5)
                .HasColumnName("RoleID");
            entity.Property(e => e.StaffId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("StaffID");

            entity.HasOne(d => d.Role).WithMany(p => p.TblStaffs)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblStaff__RoleID__693CA210");

            entity.HasOne(d => d.User).WithOne(p => p.TblStaff)
                .HasForeignKey<TblStaff>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblStaff__UserID__6A30C649");
        });

        modelBuilder.Entity<TblStaffLecture>(entity =>
        {
            entity.HasKey(e => e.LectureId);

            entity.ToTable("TblStaffLecture");

            entity.Property(e => e.LectureId)
                .HasMaxLength(10)
                .HasColumnName("LectureID");
            entity.Property(e => e.ClassroomCode).HasMaxLength(5);
            entity.Property(e => e.Finish).HasColumnName("finish");
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(8)
                .IsFixedLength();
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.ModuleCodeNavigation).WithMany(p => p.TblStaffLectures)
                .HasForeignKey(d => d.ModuleCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStaffLecture_tblModule");

            entity.HasOne(d => d.User).WithMany(p => p.TblStaffLectures)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStaffLecture_tblStaff");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblStude__32C4C02A7885B30D");

            entity.ToTable("tblStudent");

            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.StudentNo)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithOne(p => p.TblStudent)
                .HasForeignKey<TblStudent>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblStuden__UserI__6D0D32F4");
        });

        modelBuilder.Entity<TblStudentLecture>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblStudentLecture");

            entity.Property(e => e.ClassroomCode).HasMaxLength(5);
            entity.Property(e => e.LectureId)
                .HasMaxLength(10)
                .HasColumnName("LectureID");
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(8)
                .IsFixedLength();
            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.Lecture).WithMany()
                .HasForeignKey(d => d.LectureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblStudentLecture_TblStaffLecture1");

            entity.HasOne(d => d.ModuleCodeNavigation).WithMany()
                .HasForeignKey(d => d.ModuleCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblLectur__Modul__7A672E12");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblStudentLecture_tblStudent");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUser__1788CCAC9CEE190B");

            entity.ToTable("tblUser");

            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
