﻿// <auto-generated />
using LibertyFamilySystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibertyFamilySystem.Migrations.MembersDb
{
    [DbContext(typeof(MembersDbContext))]
    [Migration("20191102124536_AddedIGroupAdminField")]
    partial class AddedIGroupAdminField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LibertyFamilySystem.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Name");

                    b.HasKey("GroupId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("LibertyFamilySystem.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("GroupId");

                    b.Property<bool>("IsGroupAdmin")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<int>("OccupationId");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("WhatsAppNumber");

                    b.HasKey("MemberId");

                    b.HasIndex("GroupId")
                        .IsUnique();

                    b.HasIndex("OccupationId")
                        .IsUnique();

                    b.ToTable("Member");
                });

            modelBuilder.Entity("LibertyFamilySystem.Models.Occupation", b =>
                {
                    b.Property<int>("OccupationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("OccupationId");

                    b.ToTable("Occupation");
                });

            modelBuilder.Entity("LibertyFamilySystem.Models.Member", b =>
                {
                    b.HasOne("LibertyFamilySystem.Models.Group", "Group")
                        .WithOne("Member")
                        .HasForeignKey("LibertyFamilySystem.Models.Member", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibertyFamilySystem.Models.Occupation", "Occupation")
                        .WithOne("Member")
                        .HasForeignKey("LibertyFamilySystem.Models.Member", "OccupationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
