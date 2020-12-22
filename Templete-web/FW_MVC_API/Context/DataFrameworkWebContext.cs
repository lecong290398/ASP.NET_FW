using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FW_MVC_API.Models;

namespace FW_MVC_API.Context
{
    public partial class DataFrameworkWebContext : DbContext
    {
        public DataFrameworkWebContext()
        {
        }

        public DataFrameworkWebContext(DbContextOptions<DataFrameworkWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountObject> AccountObject { get; set; }
        public virtual DbSet<Code> Code { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Department_Account> Department_Account { get; set; }
        public virtual DbSet<Department_NhomYeuCau> Department_NhomYeuCau { get; set; }
        public virtual DbSet<FileAttachment> FileAttachment { get; set; }
        public virtual DbSet<FileIssue> FileIssue { get; set; }
        public virtual DbSet<FileIssueActivity> FileIssueActivity { get; set; }
        public virtual DbSet<FileThaoLuan> FileThaoLuan { get; set; }
        public virtual DbSet<FileTinNhan> FileTinNhan { get; set; }
        public virtual DbSet<FileWiki> FileWiki { get; set; }
        public virtual DbSet<FileYeuCau> FileYeuCau { get; set; }
        public virtual DbSet<FirebaseUser> FirebaseUser { get; set; }
        public virtual DbSet<HoatDongYeuCau> HoatDongYeuCau { get; set; }
        public virtual DbSet<InfomationUser> InfomationUser { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<IssueActivity> IssueActivity { get; set; }
        public virtual DbSet<IssueStatus> IssueStatus { get; set; }
        public virtual DbSet<Issue_AccountObject_Watcher> Issue_AccountObject_Watcher { get; set; }
        public virtual DbSet<LoaiYeuCau> LoaiYeuCau { get; set; }
        public virtual DbSet<LoaiYeuCauQuanTrong> LoaiYeuCauQuanTrong { get; set; }
        public virtual DbSet<LoaiYeuCau_DuAn> LoaiYeuCau_DuAn { get; set; }
        public virtual DbSet<MSC_TableCode> MSC_TableCode { get; set; }
        public virtual DbSet<MasterNguoiDuyet> MasterNguoiDuyet { get; set; }
        public virtual DbSet<MenuFunction> MenuFunction { get; set; }
        public virtual DbSet<MenuFunctionGroup> MenuFunctionGroup { get; set; }
        public virtual DbSet<MenuFunctionSubGroup> MenuFunctionSubGroup { get; set; }
        public virtual DbSet<MenuFunction_Account> MenuFunction_Account { get; set; }
        public virtual DbSet<MenuFunction_Role> MenuFunction_Role { get; set; }
        public virtual DbSet<NguoiDuyet> NguoiDuyet { get; set; }
        public virtual DbSet<NguoiTheoDoi> NguoiTheoDoi { get; set; }
        public virtual DbSet<NhomYeuCau> NhomYeuCau { get; set; }
        public virtual DbSet<NhomYeuCau_Account> NhomYeuCau_Account { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Project_AccountObject> Project_AccountObject { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Role_AccountObject> Role_AccountObject { get; set; }
        public virtual DbSet<Role_NhomYeuCau> Role_NhomYeuCau { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<TinNhanYeuCau> TinNhanYeuCau { get; set; }
        public virtual DbSet<Tracker> Tracker { get; set; }
        public virtual DbSet<ViewNhomYeuCau_Account> ViewNhomYeuCau_Account { get; set; }
        public virtual DbSet<ViewRole_NhomYeuCau> ViewRole_NhomYeuCau { get; set; }
        public virtual DbSet<View_IssueOwnerAssignWatcher> View_IssueOwnerAssignWatcher { get; set; }
        public virtual DbSet<View_IssueReport> View_IssueReport { get; set; }
        public virtual DbSet<View_ListAccountOject_ProjectRole> View_ListAccountOject_ProjectRole { get; set; }
        public virtual DbSet<View_LoaiYeuCauAccountAccess> View_LoaiYeuCauAccountAccess { get; set; }
        public virtual DbSet<View_LoaiYeuCauAccountAccessS3> View_LoaiYeuCauAccountAccessS3 { get; set; }
        public virtual DbSet<View_LoaiYeuCauDuAn> View_LoaiYeuCauDuAn { get; set; }
        public virtual DbSet<View_LoaiYeuCauInDepartmentS2> View_LoaiYeuCauInDepartmentS2 { get; set; }
        public virtual DbSet<View_LoaiYeuCauInProjectS1> View_LoaiYeuCauInProjectS1 { get; set; }
        public virtual DbSet<View_LoaiYeuCau_DuAN> View_LoaiYeuCau_DuAN { get; set; }
        public virtual DbSet<View_MasterNguoiDuyet> View_MasterNguoiDuyet { get; set; }
        public virtual DbSet<View_MenuFunction> View_MenuFunction { get; set; }
        public virtual DbSet<View_PermissionFunction> View_PermissionFunction { get; set; }
        public virtual DbSet<View_YeuCauReport> View_YeuCauReport { get; set; }
        public virtual DbSet<Wiki> Wiki { get; set; }
        public virtual DbSet<WikiActivity> WikiActivity { get; set; }
        public virtual DbSet<WikiHistory> WikiHistory { get; set; }
        public virtual DbSet<YeuCau> YeuCau { get; set; }
        public virtual DbSet<YeuCauActivity> YeuCauActivity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=101.99.32.48,9899;Database=DataFrameworkWeb;User Id=sa;Password=1@qweQAZ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountObject>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AccountObjectName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Code>(entity =>
            {
                entity.HasKey(e => e.KeyCode)
                    .HasName("PK__Code__5B86B6EE36847C5E");

                entity.Property(e => e.KeyCode).HasMaxLength(10);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Department_Account>(entity =>
            {
                entity.HasKey(e => new { e.FK_Department, e.FK_AccountObject });

                entity.Property(e => e.FK_Department).HasMaxLength(50);

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Department_NhomYeuCau>(entity =>
            {
                entity.HasKey(e => new { e.FK_Department, e.FK_NhomYeuCau });

                entity.Property(e => e.FK_Department).HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FK_DepartmentNavigation)
                    .WithMany(p => p.Department_NhomYeuCau)
                    .HasForeignKey(d => d.FK_Department)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_NhomYeuCau_Department");
            });

            modelBuilder.Entity<FileAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentID);

                entity.Property(e => e.AttachmentID).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.DocumentName).HasMaxLength(255);

                entity.Property(e => e.EditVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FileExtension).HasMaxLength(25);

                entity.Property(e => e.FileMIMEType).HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RefID).HasMaxLength(50);
            });

            modelBuilder.Entity<FileIssue>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FK_Issue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.FK_IssueNavigation)
                    .WithMany(p => p.FileIssue)
                    .HasForeignKey(d => d.FK_Issue)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_AttachFile_Issue");
            });

            modelBuilder.Entity<FileIssueActivity>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FK_IssueActivity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.FK_IssueActivityNavigation)
                    .WithMany(p => p.FileIssueActivity)
                    .HasForeignKey(d => d.FK_IssueActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileIssueActivity_IssueActivity");
            });

            modelBuilder.Entity<FileThaoLuan>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkThaoLuan)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Loai)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<FileTinNhan>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkTinNhan)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Loai)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.FkTinNhanNavigation)
                    .WithMany(p => p.FileTinNhan)
                    .HasForeignKey(d => d.FkTinNhan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileTinNhan_TinNhanYeuCau");
            });

            modelBuilder.Entity<FileWiki>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FK_Wiki)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.FK_WikiNavigation)
                    .WithMany(p => p.FileWiki)
                    .HasForeignKey(d => d.FK_Wiki)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttachFile_Wiki");
            });

            modelBuilder.Entity<FileYeuCau>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Loai)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.FkYeuCauNavigation)
                    .WithMany(p => p.FileYeuCau)
                    .HasForeignKey(d => d.FkYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileDinhKemYeuCau_YeuCau");
            });

            modelBuilder.Entity<FirebaseUser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FirebaseToken })
                    .HasName("PK_FirebaseUser_1");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirebaseToken)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HoatDongYeuCau>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoiDung).IsRequired();

                entity.HasOne(d => d.FkYeuCauNavigation)
                    .WithMany(p => p.HoatDongYeuCau)
                    .HasForeignKey(d => d.FkYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoatDongYeuCau_YeuCau");
            });

            modelBuilder.Entity<InfomationUser>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DiemSo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FistName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Fk_AccountObject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.RowVesion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Fk_AccountObjectNavigation)
                    .WithMany(p => p.InfomationUser)
                    .HasForeignKey(d => d.Fk_AccountObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InfomationUser_AccountObject");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ActualDueDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.FK_Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.STT).ValueGeneratedOnAdd();

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.AtCreatedByNavigation)
                    .WithMany(p => p.IssueAtCreatedByNavigation)
                    .HasForeignKey(d => d.AtCreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_AccountObject1");

                entity.HasOne(d => d.FK_AccountObjectNavigation)
                    .WithMany(p => p.IssueFK_AccountObjectNavigation)
                    .HasForeignKey(d => d.FK_AccountObject)
                    .HasConstraintName("FK_Issue_AccountObject");

                entity.HasOne(d => d.FK_ProjectNavigation)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.FK_Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_Project");

                entity.HasOne(d => d.FK_StatusNavigation)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.FK_Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_IssueStatus");

                entity.HasOne(d => d.FK_TrackerNavigation)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.FK_Tracker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_Tracker");
            });

            modelBuilder.Entity<IssueActivity>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ActivityContent).HasColumnType("ntext");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.Comment).HasMaxLength(2000);

                entity.Property(e => e.FK_AccountObject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_Issue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FK_IssueNavigation)
                    .WithMany(p => p.IssueActivity)
                    .HasForeignKey(d => d.FK_Issue)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IssueActivity_Issue");
            });

            modelBuilder.Entity<IssueStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Issue_AccountObject_Watcher>(entity =>
            {
                entity.HasKey(e => new { e.FK_Issue, e.FK_AccountObjetWatcher })
                    .HasName("PK_Issue_AccountObject_Watcher_1");

                entity.Property(e => e.FK_Issue).HasMaxLength(50);

                entity.Property(e => e.FK_AccountObjetWatcher).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FK_AccountObjetWatcherNavigation)
                    .WithMany(p => p.Issue_AccountObject_Watcher)
                    .HasForeignKey(d => d.FK_AccountObjetWatcher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_AccountObject_Watcher_AccountObject");

                entity.HasOne(d => d.FK_IssueNavigation)
                    .WithMany(p => p.Issue_AccountObject_Watcher)
                    .HasForeignKey(d => d.FK_Issue)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issue_AccountObject_Watcher_Issue");
            });

            modelBuilder.Entity<LoaiYeuCau>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FK_NhomYeuCauNavigation)
                    .WithMany(p => p.LoaiYeuCau)
                    .HasForeignKey(d => d.FK_NhomYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoaiYeuCau_NhomYeuCau");
            });

            modelBuilder.Entity<LoaiYeuCauQuanTrong>(entity =>
            {
                entity.HasKey(e => new { e.FkLoai, e.FkNguoiDung });

                entity.Property(e => e.FkLoai).HasMaxLength(50);

                entity.Property(e => e.FkNguoiDung).HasMaxLength(50);

                entity.HasOne(d => d.FkLoaiNavigation)
                    .WithMany(p => p.LoaiYeuCauQuanTrong)
                    .HasForeignKey(d => d.FkLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoaiYeuCauQuanTrong_LoaiYeuCau");

                entity.HasOne(d => d.FkNguoiDungNavigation)
                    .WithMany(p => p.LoaiYeuCauQuanTrong)
                    .HasForeignKey(d => d.FkNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoaiYeuCauQuanTrong_AccountObject");
            });

            modelBuilder.Entity<LoaiYeuCau_DuAn>(entity =>
            {
                entity.HasKey(e => new { e.FkLoai, e.FkDuAn });

                entity.Property(e => e.FkLoai).HasMaxLength(50);

                entity.Property(e => e.FkDuAn).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FkDuAnNavigation)
                    .WithMany(p => p.LoaiYeuCau_DuAn)
                    .HasForeignKey(d => d.FkDuAn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoaiYeuCau_DuAn_Project");

                entity.HasOne(d => d.FkLoaiNavigation)
                    .WithMany(p => p.LoaiYeuCau_DuAn)
                    .HasForeignKey(d => d.FkLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoaiYeuCau_DuAn_LoaiYeuCau");
            });

            modelBuilder.Entity<MSC_TableCode>(entity =>
            {
                entity.HasKey(e => e.TableID);

                entity.Property(e => e.TableID).HasMaxLength(100);

                entity.Property(e => e.CurrentValue).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Prefix).HasMaxLength(50);

                entity.Property(e => e.TableCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TableName).HasMaxLength(500);

                entity.Property(e => e.UnsignName).HasMaxLength(500);
            });

            modelBuilder.Entity<MasterNguoiDuyet>(entity =>
            {
                entity.HasKey(e => new { e.FkLoaiYeuCau, e.FkVaiTro, e.BuocDuyet });

                entity.Property(e => e.FkLoaiYeuCau).HasMaxLength(50);

                entity.Property(e => e.FkVaiTro).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FkLoaiYeuCauNavigation)
                    .WithMany(p => p.MasterNguoiDuyet)
                    .HasForeignKey(d => d.FkLoaiYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MasterNguoiDuyet_LoaiYeuCau");
            });

            modelBuilder.Entity<MenuFunction>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AcctionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AcctionNameView)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerNameView)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CssClass).HasMaxLength(100);

                entity.Property(e => e.FK_MenuSubGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Parrent).HasMaxLength(50);

                entity.Property(e => e.RouteId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.FK_MenuSubGroupNavigation)
                    .WithMany(p => p.MenuFunction)
                    .HasForeignKey(d => d.FK_MenuSubGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuFunction_MenuFunctionSubGroup");
            });

            modelBuilder.Entity<MenuFunctionGroup>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CssClass).HasMaxLength(100);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);
            });

            modelBuilder.Entity<MenuFunctionSubGroup>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CssClass).HasMaxLength(100);

                entity.Property(e => e.FK_MenuGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.SubGroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.FK_MenuGroupNavigation)
                    .WithMany(p => p.MenuFunctionSubGroup)
                    .HasForeignKey(d => d.FK_MenuGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuFunctionSubGroup_MenuFunctionGroup");
            });

            modelBuilder.Entity<MenuFunction_Account>(entity =>
            {
                entity.HasKey(e => new { e.FK_AccountObject, e.FK_MenuFunction });

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.FK_MenuFunction).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.FK_AccountObjectNavigation)
                    .WithMany(p => p.MenuFunction_Account)
                    .HasForeignKey(d => d.FK_AccountObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuFunction_Account_AccountObject");

                entity.HasOne(d => d.FK_MenuFunctionNavigation)
                    .WithMany(p => p.MenuFunction_Account)
                    .HasForeignKey(d => d.FK_MenuFunction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuFunction_Account_MenuFunction");
            });

            modelBuilder.Entity<MenuFunction_Role>(entity =>
            {
                entity.HasKey(e => new { e.FK_Role, e.FK_MenuFunction });

                entity.Property(e => e.FK_Role).HasMaxLength(50);

                entity.Property(e => e.FK_MenuFunction).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.FK_MenuFunctionNavigation)
                    .WithMany(p => p.MenuFunction_Role)
                    .HasForeignKey(d => d.FK_MenuFunction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuFunction_Role_MenuFunction");

                entity.HasOne(d => d.FK_RoleNavigation)
                    .WithMany(p => p.MenuFunction_Role)
                    .HasForeignKey(d => d.FK_Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuFunction_Role_Role");
            });

            modelBuilder.Entity<NguoiDuyet>(entity =>
            {
                entity.HasKey(e => new { e.FkYeuCau, e.FkNguoiDung, e.BuocDuyet });

                entity.Property(e => e.FkYeuCau).HasMaxLength(50);

                entity.Property(e => e.FkNguoiDung).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.NgayDuyet).HasColumnType("datetime");

                entity.Property(e => e.PhienChuyenTiep).HasMaxLength(50);

                entity.HasOne(d => d.FkYeuCauNavigation)
                    .WithMany(p => p.NguoiDuyet)
                    .HasForeignKey(d => d.FkYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NguoiDuyet_YeuCau");
            });

            modelBuilder.Entity<NguoiTheoDoi>(entity =>
            {
                entity.HasKey(e => new { e.FkYeuCau, e.FkNguoiDung })
                    .HasName("PK_TheoDoi");

                entity.Property(e => e.FkYeuCau).HasMaxLength(50);

                entity.Property(e => e.FkNguoiDung).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FkYeuCauNavigation)
                    .WithMany(p => p.NguoiTheoDoi)
                    .HasForeignKey(d => d.FkYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TheoDoi_YeuCau");
            });

            modelBuilder.Entity<NhomYeuCau>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NhomYeuCau_Account>(entity =>
            {
                entity.HasKey(e => new { e.FK_AccountObject, e.FK_NhomYeuCau })
                    .HasName("PK_AccountObject_NhomYeuCau");

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FK_AccountObjectNavigation)
                    .WithMany(p => p.NhomYeuCau_Account)
                    .HasForeignKey(d => d.FK_AccountObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NhomYeuCau_Account_AccountObject");

                entity.HasOne(d => d.FK_NhomYeuCauNavigation)
                    .WithMany(p => p.NhomYeuCau_Account)
                    .HasForeignKey(d => d.FK_NhomYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NhomYeuCau_Account_NhomYeuCau");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Project_AccountObject>(entity =>
            {
                entity.HasKey(e => new { e.FK_AccountObject, e.FK_Project, e.FK_ProjectRole });

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.FK_Project).HasMaxLength(50);

                entity.Property(e => e.FK_ProjectRole).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FK_AccountObjectNavigation)
                    .WithMany(p => p.Project_AccountObject)
                    .HasForeignKey(d => d.FK_AccountObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_AccountObject_AccountObject");

                entity.HasOne(d => d.FK_ProjectNavigation)
                    .WithMany(p => p.Project_AccountObject)
                    .HasForeignKey(d => d.FK_Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_AccountObject_Project");

                entity.HasOne(d => d.FK_ProjectRoleNavigation)
                    .WithMany(p => p.Project_AccountObject)
                    .HasForeignKey(d => d.FK_ProjectRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_AccountObject_Role");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Role_AccountObject>(entity =>
            {
                entity.HasKey(e => new { e.FK_Role, e.FK_AccountObject });

                entity.Property(e => e.FK_Role).HasMaxLength(50);

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FK_AccountObjectNavigation)
                    .WithMany(p => p.Role_AccountObject)
                    .HasForeignKey(d => d.FK_AccountObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_AccountObject_AccountObject");

                entity.HasOne(d => d.FK_RoleNavigation)
                    .WithMany(p => p.Role_AccountObject)
                    .HasForeignKey(d => d.FK_Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_AccountObject_Role");
            });

            modelBuilder.Entity<Role_NhomYeuCau>(entity =>
            {
                entity.HasKey(e => new { e.FK_Role, e.FK_NhomYeuCau });

                entity.Property(e => e.FK_Role).HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.FK_NhomYeuCauNavigation)
                    .WithMany(p => p.Role_NhomYeuCau)
                    .HasForeignKey(d => d.FK_NhomYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_NhomYeuCau_NhomYeuCau");

                entity.HasOne(d => d.FK_RoleNavigation)
                    .WithMany(p => p.Role_NhomYeuCau)
                    .HasForeignKey(d => d.FK_Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_NhomYeuCau_Role");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Id2).HasMaxLength(50);

                entity.Property(e => e.ImageSlug).HasMaxLength(200);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<TinNhanYeuCau>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkYeuCauNavigation)
                    .WithMany(p => p.TinNhanYeuCau)
                    .HasForeignKey(d => d.FkYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinNhanYeuCau_YeuCau");
            });

            modelBuilder.Entity<Tracker>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ViewNhomYeuCau_Account>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewNhomYeuCau_Account");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.CodeNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_AccountObject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ViewRole_NhomYeuCau>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewRole_NhomYeuCau");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.CodeNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_IssueOwnerAssignWatcher>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_IssueOwnerAssignWatcher");

                entity.Property(e => e.ActualDueDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.AssignName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.FK_Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TrackerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WatcherAccountId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_IssueReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_IssueReport");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AccountObjectName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ActualDueDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.FK_AccountObject).HasMaxLength(50);

                entity.Property(e => e.FK_AccountObjetWatcher)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<View_ListAccountOject_ProjectRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_ListAccountOject_ProjectRole");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AccountObjectName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FK_Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id_Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_LoaiYeuCauAccountAccess>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_LoaiYeuCauAccountAccess");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNhom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_LoaiYeuCauAccountAccessS3>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_LoaiYeuCauAccountAccessS3");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNhom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_LoaiYeuCauDuAn>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_LoaiYeuCauDuAn");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkDuAn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_LoaiYeuCauInDepartmentS2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_LoaiYeuCauInDepartmentS2");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectId)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNhom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_LoaiYeuCauInProjectS1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_LoaiYeuCauInProjectS1");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_NhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_LoaiYeuCau_DuAN>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_LoaiYeuCau_DuAN");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkDuAn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkLoai)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id_LoaiYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenLoaiYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNhom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_MasterNguoiDuyet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_MasterNguoiDuyet");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FkLoaiYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkVaiTro)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View_MenuFunction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_MenuFunction");

                entity.Property(e => e.AcctionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AcctionNameView)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerNameView)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CssClass).HasMaxLength(100);

                entity.Property(e => e.CssClassGroup).HasMaxLength(100);

                entity.Property(e => e.CssClassSubGroup).HasMaxLength(100);

                entity.Property(e => e.FK_MenuSubGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.IconGroup).HasMaxLength(100);

                entity.Property(e => e.IconSubGroup).HasMaxLength(100);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdAccount)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdSubGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Parrent).HasMaxLength(50);

                entity.Property(e => e.RouteId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubGroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<View_PermissionFunction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_PermissionFunction");

                entity.Property(e => e.AcctionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AcctionNameView)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerNameView)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CssClass).HasMaxLength(100);

                entity.Property(e => e.FK_MenuSubGroup)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdAccount)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Parrent).HasMaxLength(50);

                entity.Property(e => e.RouteId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<View_YeuCauReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_YeuCauReport");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Fk_DuAn).HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaLoaiYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaNguoiDuyet)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoiDung).HasMaxLength(50);

                entity.Property(e => e.TenLoaiYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenNguoiDuyet)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.TenNhomYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ThoiHan).HasColumnType("datetime");
            });

            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.FK_AccountApproved).HasMaxLength(50);

                entity.Property(e => e.Fk_Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WikiCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WikiName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WikiNameDemo).HasMaxLength(100);

                entity.HasOne(d => d.Fk_ProjectNavigation)
                    .WithMany(p => p.Wiki)
                    .HasForeignKey(d => d.Fk_Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wiki_Project");
            });

            modelBuilder.Entity<WikiActivity>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CreateBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FK_Wiki)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tile)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WikiHistory>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtRowversion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.CreateBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Fk_Wiki)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<YeuCau>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AtCreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AtCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.AtLastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.AtLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.AtRowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_LoaiYeuCau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Fk_DuAn).HasMaxLength(50);

                entity.Property(e => e.NoiDung).HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ThoiHan).HasColumnType("datetime");

                entity.HasOne(d => d.FK_LoaiYeuCauNavigation)
                    .WithMany(p => p.YeuCau)
                    .HasForeignKey(d => d.FK_LoaiYeuCau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_YeuCau_LoaiYeuCau");
            });

            modelBuilder.Entity<YeuCauActivity>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ActivityContent).HasColumnType("ntext");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.Comment).HasMaxLength(2000);

                entity.Property(e => e.FK_AccountObject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FK_YeuCau)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
