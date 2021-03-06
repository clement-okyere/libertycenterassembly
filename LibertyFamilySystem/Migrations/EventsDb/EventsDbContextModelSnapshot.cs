﻿// <auto-generated />
using LibertyFamilySystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibertyFamilySystem.Migrations.EventsDb
{
    [DbContext(typeof(EventsDbContext))]
    partial class EventsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LibertyFamilySystem.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("Name");

                    b.HasKey("EventId");

                    b.ToTable("Event");
                });
#pragma warning restore 612, 618
        }
    }
}
