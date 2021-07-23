using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace projectPRN.Modelss
{
    public partial class FAP_PRN_ProjectContext : DbContext
    {
        public FAP_PRN_ProjectContext()
        {
        }

        public FAP_PRN_ProjectContext(DbContextOptions<FAP_PRN_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<ExamSchedule> ExamSchedules { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<StudentInfo> StudentInfos { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<TermStudent> TermStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = FAP_PRN_Project;uid=ducminh288;pwd=ducminh2882000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.ToTable("Account");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentID")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Student Info");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("courseID");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("studentID")
                    .IsFixedLength(true);

                entity.Property(e => e.SubjectId).HasColumnName("subjectID");

                entity.Property(e => e.TermId).HasColumnName("termID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Student Info");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Subject");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Term");
            });

            modelBuilder.Entity<ExamSchedule>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ExamSchedule");

                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.ExamDate)
                    .HasColumnType("datetime")
                    .HasColumnName("examDate");

                entity.Property(e => e.ExamRoom)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("examRoom")
                    .IsFixedLength(true);

                entity.Property(e => e.ExamTime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("examTime");

                entity.Property(e => e.ExamType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("examType")
                    .IsFixedLength(true);

                entity.Property(e => e.PublishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("publishDate");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("studentID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Course)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamSchedule_Course");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam Schedule_studentInfo");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.GradeId)
                    .ValueGeneratedNever()
                    .HasColumnName("gradeID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .HasColumnName("comment");

                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.GradeCategory)
                    .HasMaxLength(50)
                    .HasColumnName("gradeCategory");

                entity.Property(e => e.GradeType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("gradeType")
                    .IsFixedLength(true);

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("studentID")
                    .IsFixedLength(true);

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(2, 1)")
                    .HasColumnName("value");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mark_Student Info");
            });

            modelBuilder.Entity<StudentInfo>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK_studentInfo");

                entity.ToTable("Student Info");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("studentID")
                    .IsFixedLength(true);

                entity.Property(e => e.Major)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("major")
                    .IsFixedLength(true);

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("studentName")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("subjectID");

                entity.Property(e => e.SubjectMajor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("subjectMajor");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("subjectName");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("Term");

                entity.Property(e => e.TermId)
                    .ValueGeneratedNever()
                    .HasColumnName("termID");

                entity.Property(e => e.TermName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("termName");
            });

            modelBuilder.Entity<TermStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Term_Student");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("studentID")
                    .IsFixedLength(true);

                entity.Property(e => e.TermId).HasColumnName("termID");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Term_Student_Student Info");

                entity.HasOne(d => d.Term)
                    .WithMany()
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Term_Student_Term");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
