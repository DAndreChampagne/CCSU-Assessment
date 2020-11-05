﻿// <auto-generated />
using System;
using Assessment.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assessment.Data.Migrations
{
    [DbContext(typeof(AssessmentContext))]
    [Migration("20201105213457_updatedModel2")]
    partial class updatedModel2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("CRN")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<string>("FacultyId")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<byte[]>("File")
                        .HasColumnType("longblob");

                    b.Property<string>("FilePath")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("Level")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("RubricId")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.Property<string>("StudentId")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<string>("Term")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("RubricId");

                    b.ToTable("Artifacts");
                });

            modelBuilder.Entity("Assessment.Models.Rubric", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.Property<int?>("ArtifactId")
                        .HasColumnType("int");

                    b.Property<byte[]>("File")
                        .HasColumnType("longblob");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.ToTable("Rubrics");
                });

            modelBuilder.Entity("Assessment.Models.RubricCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Desciption0")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Desciption1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Desciption2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Desciption3")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Desciption4")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RubricId")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RubricId");

                    b.ToTable("RubricCriteria");
                });

            modelBuilder.Entity("Assessment.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArtifactId")
                        .HasColumnType("int");

                    b.Property<string>("RubricId")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4");

                    b.Property<int?>("Score01")
                        .HasColumnType("int");

                    b.Property<int?>("Score02")
                        .HasColumnType("int");

                    b.Property<int?>("Score03")
                        .HasColumnType("int");

                    b.Property<int?>("Score04")
                        .HasColumnType("int");

                    b.Property<int?>("Score05")
                        .HasColumnType("int");

                    b.Property<int?>("Score06")
                        .HasColumnType("int");

                    b.Property<int?>("Score07")
                        .HasColumnType("int");

                    b.Property<int?>("Score08")
                        .HasColumnType("int");

                    b.Property<int?>("Score09")
                        .HasColumnType("int");

                    b.Property<int?>("Score10")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.HasIndex("RubricId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("Assessment.Models.Artifact", b =>
                {
                    b.HasOne("Assessment.Models.Rubric", "Rubric")
                        .WithMany()
                        .HasForeignKey("RubricId");
                });

            modelBuilder.Entity("Assessment.Models.Rubric", b =>
                {
                    b.HasOne("Assessment.Models.Artifact", null)
                        .WithMany("Rubrics")
                        .HasForeignKey("ArtifactId");
                });

            modelBuilder.Entity("Assessment.Models.RubricCriteria", b =>
                {
                    b.HasOne("Assessment.Models.Rubric", "Rubric")
                        .WithMany("RubricCriteria")
                        .HasForeignKey("RubricId");
                });

            modelBuilder.Entity("Assessment.Models.Score", b =>
                {
                    b.HasOne("Assessment.Models.Artifact", "Artifact")
                        .WithMany("Scores")
                        .HasForeignKey("ArtifactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assessment.Models.Rubric", "Rubric")
                        .WithMany()
                        .HasForeignKey("RubricId");
                });
#pragma warning restore 612, 618
        }
    }
}
