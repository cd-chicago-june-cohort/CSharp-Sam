using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ECommerce.Models;

namespace ECommerce.Migrations
{
    [DbContext(typeof(ECommerceContext))]
    partial class ECommerceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("ECommerce.Models.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("ECommerce.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("description");

                    b.Property<string>("image");

                    b.Property<string>("name");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("ECommerce.Models.Purchase", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<int>("customerId");

                    b.Property<int>("productId");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.HasIndex("productId");

                    b.ToTable("purchases");
                });

            modelBuilder.Entity("ECommerce.Models.Purchase", b =>
                {
                    b.HasOne("ECommerce.Models.Customer", "customer")
                        .WithMany("purchases")
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Models.Product", "product")
                        .WithMany("orders")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
