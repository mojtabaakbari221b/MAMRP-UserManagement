﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserManagement.Infrastructure.Persistence;

#nullable disable

namespace _04_Api.Migrations
{
    [DbContext(typeof(UserManagementDbContext))]
    [Migration("20241026104100_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserManagement.Domain.Entities.Member", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<long?>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.MenuGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MenuGroups");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.Service", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<long?>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.ServiceGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServiceGroups");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PersianRecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PersianUpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordDatetime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RegisteringUser")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdaterUser")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.Menu", b =>
                {
                    b.HasOne("UserManagement.Domain.Entities.Role", null)
                        .WithMany("AllowedMenus")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.Service", b =>
                {
                    b.HasOne("UserManagement.Domain.Entities.Role", null)
                        .WithMany("AllowedServices")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("UserManagement.Domain.Entities.Role", b =>
                {
                    b.Navigation("AllowedMenus");

                    b.Navigation("AllowedServices");
                });
#pragma warning restore 612, 618
        }
    }
}
