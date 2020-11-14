﻿// <auto-generated />
using System;
using Assessment.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assessment.Data.Migrations
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

            modelBuilder.Entity("Assessment.Models.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CRN")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<byte[]>("File")
                        .HasColumnType("longblob");

                    b.Property<string>("Level")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("RubricId")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.Property<string>("StudentId")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<string>("Term")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("CRN");

                    b.HasIndex("FacultyId");

                    b.HasIndex("RubricId");

                    b.ToTable("Artifacts");
                });

            modelBuilder.Entity("Assessment.Models.CourseSection", b =>
                {
                    b.Property<int>("CRN")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasKey("CRN");

                    b.HasIndex("FacultyId");

                    b.ToTable("CourseSections");
                });

            modelBuilder.Entity("Assessment.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("RubricId")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RubricId");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("Assessment.Models.Rubric", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.Property<byte[]>("File")
                        .HasColumnType("longblob");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Rubrics");
                });

            modelBuilder.Entity("Assessment.Models.RubricCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CriteriaText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RubricId")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RubricId");

                    b.ToTable("RubricCriteria");
                });

            modelBuilder.Entity("Assessment.Models.RubricCriteriaElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CriteriaText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RubricCriteriaId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("RubricCriteriaId1")
                        .HasColumnType("int");

                    b.Property<int>("ScoreValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RubricCriteriaId1");

                    b.ToTable("RubricCriteriaElements");
                });

            modelBuilder.Entity("Assessment.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArtifactId")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("RubricCriteriaId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("RubricCriteriaId1")
                        .HasColumnType("int");

                    b.Property<int?>("ScoreValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("RubricCriteriaId1");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("Assessment.Models.Artifact", b =>
                {
                    b.HasOne("Assessment.Models.CourseSection", "CourseSection")
                        .WithMany()
                        .HasForeignKey("CRN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assessment.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assessment.Models.Rubric", "Rubric")
                        .WithMany("Artifacts")
                        .HasForeignKey("RubricId");
                });

            modelBuilder.Entity("Assessment.Models.CourseSection", b =>
                {
                    b.HasOne("Assessment.Models.Faculty", "Faculty")
                        .WithMany("CourseSections")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assessment.Models.Faculty", b =>
                {
                    b.HasOne("Assessment.Models.Rubric", "Rubric")
                        .WithMany("Faculty")
                        .HasForeignKey("RubricId");
                });

            modelBuilder.Entity("Assessment.Models.RubricCriteria", b =>
                {
                    b.HasOne("Assessment.Models.Rubric", "Rubric")
                        .WithMany("RubricCriteria")
                        .HasForeignKey("RubricId");
                });

            modelBuilder.Entity("Assessment.Models.RubricCriteriaElement", b =>
                {
                    b.HasOne("Assessment.Models.RubricCriteria", "RubricCriteria")
                        .WithMany("RubricCriteriaElements")
                        .HasForeignKey("RubricCriteriaId1");
                });

            modelBuilder.Entity("Assessment.Models.Score", b =>
                {
                    b.HasOne("Assessment.Models.Artifact", "Artifact")
                        .WithMany("Scores")
                        .HasForeignKey("ArtifactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assessment.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assessment.Models.RubricCriteria", "RubricCriteria")
                        .WithMany("Scores")
                        .HasForeignKey("RubricCriteriaId1");
                });
#pragma warning restore 612, 618
        }
    }
}
