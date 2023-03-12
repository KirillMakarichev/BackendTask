﻿// <auto-generated />
using System;
using BackendTask.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackendTask.Migrations
{
    [DbContext(typeof(TreeContext))]
    partial class TreeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackendTask.DataBase.Models.Exception", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExceptionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("exceptions");
                });

            modelBuilder.Entity("BackendTask.DataBase.Models.ExceptionData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ExceptionId")
                        .HasColumnType("bigint");

                    b.Property<string[]>("Headers")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("QueryParameters")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("TraceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExceptionId")
                        .IsUnique();

                    b.ToTable("exceptions_data");
                });

            modelBuilder.Entity("BackendTask.DataBase.Models.TreeNode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ParentNodeId")
                        .HasColumnType("bigint");

                    b.Property<string>("RootName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentNodeId");

                    b.HasIndex("Name", "ParentNodeId")
                        .IsUnique();

                    b.ToTable("nodes");
                });

            modelBuilder.Entity("BackendTask.DataBase.Models.ExceptionData", b =>
                {
                    b.HasOne("BackendTask.DataBase.Models.Exception", null)
                        .WithOne("Data")
                        .HasForeignKey("BackendTask.DataBase.Models.ExceptionData", "ExceptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendTask.DataBase.Models.TreeNode", b =>
                {
                    b.HasOne("BackendTask.DataBase.Models.TreeNode", "ParentNode")
                        .WithMany("Children")
                        .HasForeignKey("ParentNodeId");

                    b.Navigation("ParentNode");
                });

            modelBuilder.Entity("BackendTask.DataBase.Models.Exception", b =>
                {
                    b.Navigation("Data")
                        .IsRequired();
                });

            modelBuilder.Entity("BackendTask.DataBase.Models.TreeNode", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
