﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web_2.Data;

#nullable disable

namespace Web_2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Data")
                .HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Web_2.Models.Carts.CartItemShoping", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartItemId"));

                    b.Property<int>("CartId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItemShoping", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Carts.CartShoping", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CartId");

                    b.ToTable("CartShoping", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Delivery.delivery", b =>
                {
                    b.Property<int>("deliveryid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("deliveryid"));

                    b.Property<int>("deliverystatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("deliverytime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("idnguoiban")
                        .HasColumnType("integer");

                    b.Property<int>("idnguoimua")
                        .HasColumnType("integer");

                    b.Property<int>("idshipper")
                        .HasColumnType("integer");

                    b.Property<DateTime>("pickuptime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("thanhtoanid")
                        .HasColumnType("integer");

                    b.HasKey("deliveryid");

                    b.HasIndex("idshipper");

                    b.HasIndex("thanhtoanid");

                    b.ToTable("delivery", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Delivery.shipper", b =>
                {
                    b.Property<int>("idshipper")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idshipper"));

                    b.Property<string>("shippername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("shipperphone")
                        .HasColumnType("integer");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.Property<int>("userid")
                        .HasColumnType("integer");

                    b.HasKey("idshipper");

                    b.HasIndex("userid")
                        .IsUnique();

                    b.ToTable("shipper", "Data");
                });

            modelBuilder.Entity("Web_2.Models.InformationUser", b =>
                {
                    b.Property<int>("Idname")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idname"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Phone")
                        .HasColumnType("integer");

                    b.Property<int>("User_id")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Idname");

                    b.HasIndex("User_id")
                        .IsUnique();

                    b.ToTable("InformationUser", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Product.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("Daycreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Sellerid")
                        .HasColumnType("integer");

                    b.Property<int>("Stockquantity")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("product", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Thanhtoan.Donmua", b =>
                {
                    b.Property<int>("Iddonmua")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Iddonmua"));

                    b.Property<int>("dongia")
                        .HasColumnType("integer");

                    b.Property<int>("idnguoiban")
                        .HasColumnType("integer");

                    b.Property<int>("idnguoimua")
                        .HasColumnType("integer");

                    b.Property<int>("idproduct")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ngaydat")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("nguoiban")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nguoimua")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("phuongthucthanhtoan")
                        .HasColumnType("integer");

                    b.Property<int>("soluong")
                        .HasColumnType("integer");

                    b.Property<int>("tongtien")
                        .HasColumnType("integer");

                    b.HasKey("Iddonmua");

                    b.HasIndex("idnguoiban");

                    b.HasIndex("idnguoimua");

                    b.HasIndex("idproduct");

                    b.ToTable("Donmua", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Thanhtoan.ThanhToan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Dongia")
                        .HasColumnType("integer");

                    b.Property<int>("Idnguoiban")
                        .HasColumnType("integer");

                    b.Property<int>("Idnguoimua")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Ngaythanhtoan")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Soluong")
                        .HasColumnType("integer");

                    b.Property<int>("Tongtien")
                        .HasColumnType("integer");

                    b.Property<string>("nguoiban")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nguoimua")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("trangthaidonhang")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Idnguoiban");

                    b.HasIndex("Idnguoimua");

                    b.HasIndex("ProductId");

                    b.ToTable("ThanhToan", "Data");
                });

            modelBuilder.Entity("Web_2.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("account")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("role")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("USER", "Data");
                });

            modelBuilder.Entity("Web_2.Models.VnPaymentRequest.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InvoiceId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GivenName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("InvoiceId");

                    b.ToTable("invoice", "Data");
                });

            modelBuilder.Entity("Web_2.Models.VnPaymentRequest.InvoiceDetail", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("InvoiceId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoiceDetail", "Data");
                });

            modelBuilder.Entity("Web_2.Models.Carts.CartItemShoping", b =>
                {
                    b.HasOne("Web_2.Models.Carts.CartShoping", "CartShoping")
                        .WithMany("CartItem")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_2.Models.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartShoping");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Web_2.Models.Delivery.delivery", b =>
                {
                    b.HasOne("Web_2.Models.Delivery.shipper", "shipper")
                        .WithMany()
                        .HasForeignKey("idshipper")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Web_2.Models.Thanhtoan.ThanhToan", "Thanhtoan")
                        .WithMany()
                        .HasForeignKey("thanhtoanid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Thanhtoan");

                    b.Navigation("shipper");
                });

            modelBuilder.Entity("Web_2.Models.Delivery.shipper", b =>
                {
                    b.HasOne("Web_2.Models.User", "UserInfo")
                        .WithOne("Shipper")
                        .HasForeignKey("Web_2.Models.Delivery.shipper", "userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Web_2.Models.InformationUser", b =>
                {
                    b.HasOne("Web_2.Models.User", "User")
                        .WithOne("InformationUser")
                        .HasForeignKey("Web_2.Models.InformationUser", "User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_2.Models.Thanhtoan.Donmua", b =>
                {
                    b.HasOne("Web_2.Models.User", null)
                        .WithMany()
                        .HasForeignKey("idnguoiban")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Web_2.Models.User", null)
                        .WithMany()
                        .HasForeignKey("idnguoimua")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Web_2.Models.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("idproduct")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Web_2.Models.Thanhtoan.ThanhToan", b =>
                {
                    b.HasOne("Web_2.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Idnguoiban")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Web_2.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Idnguoimua")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Web_2.Models.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Web_2.Models.VnPaymentRequest.InvoiceDetail", b =>
                {
                    b.HasOne("Web_2.Models.VnPaymentRequest.Invoice", null)
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_2.Models.Product.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Web_2.Models.Carts.CartShoping", b =>
                {
                    b.Navigation("CartItem");
                });

            modelBuilder.Entity("Web_2.Models.User", b =>
                {
                    b.Navigation("InformationUser")
                        .IsRequired();

                    b.Navigation("Shipper")
                        .IsRequired();
                });

            modelBuilder.Entity("Web_2.Models.VnPaymentRequest.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
