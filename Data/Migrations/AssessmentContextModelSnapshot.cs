﻿// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(AssessmentContext))]
    partial class AssessmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Models.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CRN")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<byte[]>("File")
                        .HasColumnType("longblob");

                    b.Property<string>("FilePath")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("LearningObjective")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("Level")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("RubricId")
                        .HasColumnType("int");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<string>("Term")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RubricId");

                    b.HasIndex("SchoolId");

                    b.HasIndex("UserId");

                    b.ToTable("Artifacts");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Models.Rubric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ArtifactId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.Property<string>("Data")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("File")
                        .HasColumnType("longblob");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Rubrics");
                });

            modelBuilder.Entity("Models.RubricData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RubricId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RubricId");

                    b.ToTable("RubricData");
                });

            modelBuilder.Entity("Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CRN")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Models.Artifact", b =>
                {
                    b.HasOne("Models.Rubric", "Rubric")
                        .WithMany()
                        .HasForeignKey("RubricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Rubric", b =>
                {
                    b.HasOne("Models.Artifact", null)
                        .WithMany("Rubrics")
                        .HasForeignKey("ArtifactId");

                    b.HasOne("Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.RubricData", b =>
                {
                    b.HasOne("Models.Rubric", "Rubric")
                        .WithMany("RubricData")
                        .HasForeignKey("RubricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Section", b =>
                {
                    b.HasOne("Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Session", b =>
                {
                    b.HasOne("Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.HasOne("Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.UserRole", b =>
                {
                    b.HasOne("Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
