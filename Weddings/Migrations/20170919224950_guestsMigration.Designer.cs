using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Weddings.Models;

namespace Weddings.Migrations
{
    [DbContext(typeof(WeddingContext))]
    [Migration("20170919224950_guestsMigration")]
    partial class guestsMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Weddings.Models.Guest", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("userId");

                    b.Property<int>("weddingId");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.HasIndex("weddingId");

                    b.ToTable("guests");
                });

            modelBuilder.Entity("Weddings.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<string>("first");

                    b.Property<string>("last");

                    b.Property<string>("password");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Weddings.Models.Wedding", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<DateTime>("date");

                    b.Property<int>("userId");

                    b.Property<string>("wedder1");

                    b.Property<string>("wedder2");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("weddings");
                });

            modelBuilder.Entity("Weddings.Models.Guest", b =>
                {
                    b.HasOne("Weddings.Models.User", "user")
                        .WithMany("events")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Weddings.Models.Wedding", "wedding")
                        .WithMany("guests")
                        .HasForeignKey("weddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Weddings.Models.Wedding", b =>
                {
                    b.HasOne("Weddings.Models.User", "user")
                        .WithMany("weddings")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
