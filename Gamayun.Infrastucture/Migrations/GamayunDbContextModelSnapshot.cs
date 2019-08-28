﻿// <auto-generated />
using System;
using Gamayun.Infrastucture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gamayun.Infrastucture.Migrations
{
    [DbContext(typeof(GamayunDbContext))]
    partial class GamayunDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gamayun.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsObsolete");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserID");

                    b.HasKey("ID");

                    b.HasIndex("AppUserID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Presence", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PresenceDateID");

                    b.Property<int?>("StudentID");

                    b.Property<bool>("WasPresent");

                    b.HasKey("ID");

                    b.HasIndex("PresenceDateID");

                    b.HasIndex("StudentID");

                    b.ToTable("Presences");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.PresenceDate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("SectionID");

                    b.HasKey("ID");

                    b.HasIndex("SectionID");

                    b.ToTable("PresenceDates");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Section", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("SemesterID");

                    b.Property<int>("State");

                    b.Property<int?>("TopicID");

                    b.HasKey("ID");

                    b.HasIndex("SemesterID");

                    b.HasIndex("TopicID");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Semester", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("FinishedOn");

                    b.Property<bool>("IsObsolete");

                    b.Property<string>("Major");

                    b.HasKey("ID");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserID");

                    b.HasKey("ID");

                    b.HasIndex("AppUserID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.StudentSection", b =>
                {
                    b.Property<int>("StudentID");

                    b.Property<int>("SectionID");

                    b.Property<int?>("Grade");

                    b.HasKey("StudentID", "SectionID");

                    b.HasIndex("SectionID");

                    b.ToTable("StudentSections");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Teacher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserID");

                    b.HasKey("ID");

                    b.HasIndex("AppUserID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Topic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("TeacherID");

                    b.HasKey("ID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Admin", b =>
                {
                    b.HasOne("Gamayun.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserID");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Presence", b =>
                {
                    b.HasOne("Gamayun.Infrastucture.Entities.PresenceDate")
                        .WithMany("Presences")
                        .HasForeignKey("PresenceDateID");

                    b.HasOne("Gamayun.Infrastucture.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.PresenceDate", b =>
                {
                    b.HasOne("Gamayun.Infrastucture.Entities.Section", "Section")
                        .WithMany("PresenceDates")
                        .HasForeignKey("SectionID");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Section", b =>
                {
                    b.HasOne("Gamayun.Infrastucture.Entities.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterID");

                    b.HasOne("Gamayun.Infrastucture.Entities.Topic", "Topic")
                        .WithMany("Sections")
                        .HasForeignKey("TopicID");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Student", b =>
                {
                    b.HasOne("Gamayun.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserID");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.StudentSection", b =>
                {
                    b.HasOne("Gamayun.Infrastucture.Entities.Section", "Section")
                        .WithMany("StudentSections")
                        .HasForeignKey("SectionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gamayun.Infrastucture.Entities.Student", "Student")
                        .WithMany("StudentSections")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Teacher", b =>
                {
                    b.HasOne("Gamayun.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserID");
                });

            modelBuilder.Entity("Gamayun.Infrastucture.Entities.Topic", b =>
                {
                    b.HasOne("Gamayun.Infrastucture.Entities.Teacher", "Teacher")
                        .WithMany("Topics")
                        .HasForeignKey("TeacherID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Gamayun.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Gamayun.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gamayun.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Gamayun.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
