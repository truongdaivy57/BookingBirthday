﻿using BookingBirthday.Data.Entities;
using BookingBirthday.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingBirthday.Data
{
    public static class SeedData
    {
        private static readonly string GUEST_FILE_PATH = "Resources/guests.txt";
        private static readonly string HOST_FILE_PATH = "Resources/hosts.txt";
        private static readonly string PACKAGE_FILE_PATH = "Resources/packages.txt";
        private static readonly string CART_FILE_PATH = "Resources/carts.txt";
        private static readonly string BILL_FILE_PATH = "Resources/bills.txt";
        private static readonly string BOOKING_FILE_PATH = "Resources/bookings.txt";
        private static readonly string SERVICE_FILE_PATH = "Resources/services.txt";
        private static readonly string PROMOTION_FILE_PATH = "Resources/promotions.txt";
        private static readonly string PAYMENT_FILE_PATH = "Resources/payments.txt";
        private static readonly string PACKAGE_SERVICE_FILE_PATH = "Resources/package-service.txt";
        private static readonly string CART_SERVICE_FILE_PATH = "Resources/cart-service.txt";
        private static readonly string CART_PACKAGE_FILE_PATH = "Resources/cart-package.txt";
        private static readonly string BOOKING_PACKAGE_FILE_PATH = "Resources/booking-package.txt";
        private static readonly string BOOKING_SERVICE_FILE_PATH = "Resources/booking-service.txt";
        private static readonly string USER_FILE_PATH = "Resources/users.txt";

        public static void Initialize(ModelBuilder modelBuilder)
        {
            InitializeGuest(modelBuilder);
            InitializeHost(modelBuilder);
            InitializePackage(modelBuilder);
            InitializeCart(modelBuilder);
            InitializeBill(modelBuilder);
            InitializeBooking(modelBuilder);
            InitializeService(modelBuilder);
            InitializePromotion(modelBuilder);
            InitializePayment(modelBuilder);
            InitializePackageService(modelBuilder);
            InitializeCartService(modelBuilder);
            InitializeCartPackage(modelBuilder);
            InitializeBookingPackage(modelBuilder);
            InitializeBookingService(modelBuilder);
            InitializeUser(modelBuilder);
        }

        private static void InitializeGuest(ModelBuilder modelBuilder)
        {
            var guest = new List<Guest>();

            if (File.Exists(GUEST_FILE_PATH))
            {
                using StreamReader sr = new(GUEST_FILE_PATH);
                int guestId = 1;
                string? guestLine;

                while ((guestLine = sr.ReadLine()) != null)
                {
                    string[]? guestData = guestLine!.Split('|');

                    guest.Add(new Guest
                    {
                        Id = guestId++,
                        Name = guestData[0].Trim(),
                        Gender = (int.Parse(guestData[1].Trim()) == 0) ? Gender.Male : Gender.Female,
                        DateOfBirth = DateTime.ParseExact(guestData[2].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Email = guestData[3].Trim(),
                        Address = guestData[4].Trim(),
                        Phone = guestData[5].Trim(),
                        CartId = int.Parse(guestData[6].Trim())
                    });
                }
                modelBuilder.Entity<Guest>().HasData(guest);
            }
        }

        private static void InitializeHost(ModelBuilder modelBuilder)
        {
            var host = new List<Host>();

            if (File.Exists(HOST_FILE_PATH))
            {
                using StreamReader sr = new(HOST_FILE_PATH);
                int hostId = 1;
                string? hostLine;

                while ((hostLine = sr.ReadLine()) != null)
                {
                    string[]? hostData = hostLine!.Split('|');

                    host.Add(new Host
                    {
                        Id = hostId++,
                        Name = hostData[0].Trim(),
                        Gender = (int.Parse(hostData[1].Trim()) == 0) ? Gender.Male : Gender.Female,
                        DateOfBirth = DateTime.ParseExact(hostData[2].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Email = hostData[3].Trim(),
                        Address = hostData[4].Trim(),
                        Phone = hostData[5].Trim()
                    });
                }
                modelBuilder.Entity<Host>().HasData(host);
            }
        }

        private static void InitializePackage(ModelBuilder modelBuilder)
        {
            var package = new List<Package>();

            if (File.Exists(PACKAGE_FILE_PATH))
            {
                using StreamReader sr = new(PACKAGE_FILE_PATH);
                int packageId = 1;
                string? packageLine;

                while ((packageLine = sr.ReadLine()) != null)
                {
                    string[]? packageeData = packageLine!.Split('|');

                    package.Add(new Package
                    {
                        Id = packageId++,
                        Name = packageeData[0].Trim(),
                        Price = double.Parse(packageeData[1].Trim()),
                        Venue = packageeData[2].Trim(),
                        Detail = packageeData[3].Trim(),
                        PromotionId = string.IsNullOrEmpty(packageeData[4].Trim()) ? (int?)null : int.Parse(packageeData[4].Trim())
                    });
                }
                modelBuilder.Entity<Package>().HasData(package); ;
            }
        }

        private static void InitializeCart(ModelBuilder modelBuilder)
        {
            var cart = new List<Cart>();

            if (File.Exists(CART_FILE_PATH))
            {
                using StreamReader sr = new(CART_FILE_PATH);
                int cartID = 1;
                string? cartLine;

                while ((cartLine = sr.ReadLine()) != null)
                {
                    string[]? cartData = cartLine!.Split('|');

                    cart.Add(new Cart
                    {
                        Id = cartID++,
                        Total = double.Parse(cartData[0].Trim()),
                        GuestId = int.Parse(cartData[1].Trim())
                    });
                }
                modelBuilder.Entity<Cart>().HasData(cart);
            }
        }

        private static void InitializeBill(ModelBuilder modelBuilder)
        {
            var bill = new List<Bill>();

            if (File.Exists(BILL_FILE_PATH))
            {
                using StreamReader sr = new(BILL_FILE_PATH);
                int billId = 1;
                string? billLine;

                while ((billLine = sr.ReadLine()) != null)
                {
                    string[]? billData = billLine!.Split('|');

                    bill.Add(new Bill
                    {
                        Id = billId++,
                        Date = DateTime.ParseExact(billData[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Discount = double.Parse(billData[1].Trim()),
                        Total = double.Parse(billData[2].Trim()),
                        BookingId = int.Parse(billData[3].Trim())
                    });
                }
                modelBuilder.Entity<Bill>().HasData(bill);
            }
        }

        public static void InitializeBooking(ModelBuilder modelBuilder)
        {
            var booking = new List<Booking>();

            if (File.Exists(BOOKING_FILE_PATH))
            {
                using StreamReader sr = new(BOOKING_FILE_PATH);
                int bookingId = 1;
                string? bookingLine;

                while ((bookingLine = sr.ReadLine()) != null)
                {
                    string[]? bookingData = bookingLine!.Split('|');

                    BookingStatus bookingStatus = BookingStatus.Accepted;
                    if (int.Parse(bookingData[1].Trim()) == 2)
                    {
                        bookingStatus = BookingStatus.Declined;
                    }
                    else if (int.Parse(bookingData[1].Trim()) == 3)
                    {
                        bookingStatus = BookingStatus.Processing;
                    }

                    booking.Add(new Booking
                    {
                        Id = bookingId++,
                        Date = DateTime.ParseExact(bookingData[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        BookingStatus = bookingStatus,
                        Total = double.Parse(bookingData[2].Trim()),
                        GuestId = int.Parse(bookingData[3].Trim()),
                        HostId = int.Parse(bookingData[4].Trim()),
                        PaymentId = int.Parse(bookingData[5].Trim()),
                        BillId = int.Parse(bookingData[6].Trim())
                    });
                }
                modelBuilder.Entity<Booking>().HasData(booking);
            }
        }

        public static void InitializeService(ModelBuilder modelBuilder)
        {
            var service = new List<Service>();

            if (File.Exists(SERVICE_FILE_PATH))
            {
                using StreamReader sr = new(SERVICE_FILE_PATH);
                int serviceId = 1;
                string? serviceLine;

                while ((serviceLine = sr.ReadLine()) != null)
                {
                    string[]? serviceData = serviceLine!.Split('|');

                    service.Add(new Service
                    {
                        Id = serviceId++,
                        Name = serviceData[0].Trim(),
                        Price = double.Parse(serviceData[1].Trim()),
                        Detail = serviceData[2].Trim(),
                    });
                }
                modelBuilder.Entity<Service>().HasData(service);
            }
        }

        public static void InitializePromotion(ModelBuilder modelBuilder)
        {
            var promotion = new List<Promotion>();

            if (File.Exists(PROMOTION_FILE_PATH))
            {
                using StreamReader sr = new(PROMOTION_FILE_PATH);
                int promotionId = 1;
                string? promotionLine;

                while ((promotionLine = sr.ReadLine()) != null)
                {
                    string[]? promotionData = promotionLine!.Split('|');

                    promotion.Add(new Promotion
                    {
                        Id = promotionId++,
                        Name = promotionData[0].Trim(),
                        FromDate = DateTime.ParseExact(promotionData[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ToDate = DateTime.ParseExact(promotionData[2].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DiscountPercent = double.Parse(promotionData[3].Trim()),
                        Status = (int.Parse(promotionData[4].Trim()) == 0) ? Status.Active : Status.Inactive,
                        HostId = int.Parse(promotionData[5].Trim()),
                    });
                }
                modelBuilder.Entity<Promotion>().HasData(promotion);
            }
        }

        public static void InitializePayment(ModelBuilder modelBuilder)
        {
            var payment = new List<Payment>();

            if (File.Exists(PAYMENT_FILE_PATH))
            {
                using StreamReader sr = new(PAYMENT_FILE_PATH);

                int paymentId = 1;
                string? paymentLine;

                while ((paymentLine = sr.ReadLine()) != null)
                {
                    string[] paymentData = paymentLine!.Split("|");

                    Types types = Types.Banking;
                    if (int.Parse(paymentData[2].Trim()) == 1)
                    {
                        types = Types.ByCast;
                    }
                    else if (int.Parse(paymentData[2].Trim()) == 2)
                    {
                        types = Types.Installment;
                    }

                    payment.Add(new Payment
                    {
                        Id = paymentId++,
                        Amount = double.Parse(paymentData[0].Trim()),
                        Date = DateTime.ParseExact(paymentData[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Types = types,
                        BookingId = int.Parse(paymentData[3].Trim())
                    });
                }
                modelBuilder.Entity<Payment>().HasData(payment);
            }
        }

        public static void InitializePackageService(ModelBuilder modelBuilder)
        {
            var packageService = new List<PackageService>();

            if (File.Exists(PACKAGE_SERVICE_FILE_PATH))
            {
                using StreamReader sr = new(PACKAGE_SERVICE_FILE_PATH);

                string? packageServiceLine;

                while ((packageServiceLine = sr.ReadLine()) != null)
                {
                    string[] packageServiceData = packageServiceLine!.Split("|");

                    packageService.Add(new PackageService
                    {
                        PackageId = int.Parse(packageServiceData[0].Trim()),
                        ServiceId = int.Parse(packageServiceData[1].Trim())
                    });
                }
                modelBuilder.Entity<PackageService>().HasData(packageService);
            }
        }

        public static void InitializeCartService(ModelBuilder modelBuilder)
        {
            var cartService = new List<CartService>();

            if (File.Exists(CART_SERVICE_FILE_PATH))
            {
                using StreamReader sr = new(CART_SERVICE_FILE_PATH);
                string? cartServiceLine;

                while ((cartServiceLine = sr.ReadLine()) != null)
                {
                    string[]? cartServiceData = cartServiceLine!.Split('|');

                    cartService.Add(new CartService
                    {
                        CartId = int.Parse(cartServiceData[0].Trim()),
                        ServiceId = int.Parse(cartServiceData[1].Trim())
                    });
                }
                modelBuilder.Entity<CartService>().HasData(cartService);
            }
        }

        public static void InitializeCartPackage(ModelBuilder modelBuilder)
        {
            var cartPackage = new List<CartPackage>();

            if (File.Exists(CART_PACKAGE_FILE_PATH))
            {
                using StreamReader sr = new(CART_PACKAGE_FILE_PATH);
                string? cartPackageLine;

                while ((cartPackageLine = sr.ReadLine()) != null)
                {
                    string[]? cartPackageData = cartPackageLine!.Split('|');

                    cartPackage.Add(new CartPackage
                    {
                        CartId = int.Parse(cartPackageData[0].Trim()),
                        PackageId = int.Parse(cartPackageData[1].Trim())
                    });
                }
                modelBuilder.Entity<CartPackage>().HasData(cartPackage);
            }
        }

        public static void InitializeBookingService(ModelBuilder modelBuilder)
        {
            var bookingService = new List<BookingService>();

            if (File.Exists(BOOKING_SERVICE_FILE_PATH))
            {
                using StreamReader sr = new(BOOKING_SERVICE_FILE_PATH);

                string? bookingServiceLine;

                while ((bookingServiceLine = sr.ReadLine()) != null)
                {
                    string[]? bookingServiceData = bookingServiceLine!.Split('|');

                    bookingService.Add(new BookingService
                    {
                        BookingId = int.Parse(bookingServiceData[0].Trim()),
                        ServiceId = int.Parse(bookingServiceData[1].Trim())
                    });
                }
                modelBuilder.Entity<BookingService>().HasData(bookingService);
            }
        }

        public static void InitializeBookingPackage(ModelBuilder modelBuilder)
        {
            var bookingPackage = new List<BookingPackage>();

            if (File.Exists(BOOKING_PACKAGE_FILE_PATH))
            {
                using StreamReader sr = new(BOOKING_PACKAGE_FILE_PATH);
                string? bookingPackageLine;

                while ((bookingPackageLine = sr.ReadLine()) != null)
                {
                    string[]? bookingPackageData = bookingPackageLine!.Split('|');

                    bookingPackage.Add(new BookingPackage
                    {
                        BookingId = int.Parse(bookingPackageData[0].Trim()),
                        PackageId = int.Parse(bookingPackageData[1].Trim())
                    });
                }
                modelBuilder.Entity<BookingPackage>().HasData(bookingPackage);
            }
        }

        public static void InitializeUser(ModelBuilder modelBuilder)
        {
            var users = new List<User>();

            if (File.Exists(USER_FILE_PATH))
            {
                using StreamReader sr = new(USER_FILE_PATH);
                
                int userId = 1;
                string? userLine;

                while ((userLine = sr.ReadLine()) != null)
                {
                    string[]? userData = userLine!.Split('|');

                    Role role = Role.Admin;
                    if (int.Parse(userData[3].Trim()) == 1)
                    {
                        role = Role.Guest;
                    }
                    else if (int.Parse(userData[3].Trim()) == 2)
                    {
                        role = Role.Host;
                    }

                    users.Add(new User
                    {
                        Id = userId++,
                        Username = userData[0].Trim(),
                        Password = userData[1].Trim(),
                        Email = userData[2].Trim(),
                        Role = role
                    });
                }

                modelBuilder.Entity<User>().HasData(users);
            }
        }
    }
}