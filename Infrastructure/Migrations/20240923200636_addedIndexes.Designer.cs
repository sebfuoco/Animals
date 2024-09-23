﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240923200636_addedIndexes")]
    partial class addedIndexes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("AnimalGroupsId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "dateofbirth");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasAnnotation("Relational:JsonPropertyName", "tag");

                    b.HasKey("Id");

                    b.HasIndex("Tag");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Domain.Entities.AnimalGroups", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("GroupId");

                    b.ToTable("AnimalGroups");
                });

            modelBuilder.Entity("Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "datecreated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Domain.Entities.AnimalGroups", b =>
                {
                    b.HasOne("Domain.Entities.Animal", "Animal")
                        .WithMany("AnimalGroups")
                        .HasForeignKey("AnimalId");

                    b.HasOne("Domain.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Domain.Entities.Animal", b =>
                {
                    b.Navigation("AnimalGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
