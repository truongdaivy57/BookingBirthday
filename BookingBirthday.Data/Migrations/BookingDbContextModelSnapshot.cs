﻿// <auto-generated />
using System;
using BookingBirthday.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingBirthday.Data.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    partial class BookingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingBirthday.Data.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("Date_order")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.BookingPackage", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.HasKey("BookingId", "PackageId");

                    b.HasIndex("PackageId");

                    b.ToTable("BookingPackage", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("PackageId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.CartPackage", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.HasKey("CartId", "PackageId");

                    b.HasIndex("PackageId");

                    b.ToTable("CartPackage", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Category_requests", b =>
                {
                    b.Property<int>("category_request_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("category_request_id"));

                    b.Property<string>("category_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("host_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("is_approved")
                        .HasColumnType("int");

                    b.Property<bool>("is_deleted_by_admin")
                        .HasColumnType("bit");

                    b.Property<bool>("is_deleted_by_owner")
                        .HasColumnType("bit");

                    b.Property<bool>("is_viewed_by_admin")
                        .HasColumnType("bit");

                    b.Property<bool>("is_viewed_by_owner")
                        .HasColumnType("bit");

                    b.Property<string>("rejection_reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("requester_id")
                        .HasColumnType("int");

                    b.HasKey("category_request_id");

                    b.ToTable("Category_Requests");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detail")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("PromotionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image_url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.HasIndex("UserId");

                    b.ToTable("Package", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Types")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Promotion", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Image_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Address");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("Phone");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Booking", b =>
                {
                    b.HasOne("BookingBirthday.Data.Entities.Payment", "Payment")
                        .WithOne("Booking")
                        .HasForeignKey("BookingBirthday.Data.Entities.Booking", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingBirthday.Data.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.BookingPackage", b =>
                {
                    b.HasOne("BookingBirthday.Data.Entities.Booking", "Booking")
                        .WithMany("BookingPackages")
                        .HasForeignKey("BookingId")
                        .IsRequired();

                    b.HasOne("BookingBirthday.Data.Entities.Package", "Package")
                        .WithMany("BookingPackages")
                        .HasForeignKey("PackageId")
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Cart", b =>
                {
                    b.HasOne("BookingBirthday.Data.Entities.Booking", "Booking")
                        .WithMany("cart")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookingBirthday.Data.Entities.Package", "Package")
                        .WithMany("Cart")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.CartPackage", b =>
                {
                    b.HasOne("BookingBirthday.Data.Entities.Cart", "Cart")
                        .WithMany("CartPackages")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingBirthday.Data.Entities.Package", "Package")
                        .WithMany("CartPackages")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Package", b =>
                {
                    b.HasOne("BookingBirthday.Data.Entities.Promotion", "Promotion")
                        .WithMany("Services")
                        .HasForeignKey("PromotionId");

                    b.HasOne("BookingBirthday.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Promotion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Promotion", b =>
                {
                    b.HasOne("BookingBirthday.Data.Entities.User", "User")
                        .WithMany("Promotions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Booking", b =>
                {
                    b.Navigation("BookingPackages");

                    b.Navigation("cart");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Cart", b =>
                {
                    b.Navigation("CartPackages");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Package", b =>
                {
                    b.Navigation("BookingPackages");

                    b.Navigation("Cart");

                    b.Navigation("CartPackages");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Payment", b =>
                {
                    b.Navigation("Booking")
                        .IsRequired();
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.Promotion", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("BookingBirthday.Data.Entities.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Promotions");
                });
#pragma warning restore 612, 618
        }
    }
}
