﻿using BookingBirthday.Data.Entities;
using BookingBirthday.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using static NuGet.Packaging.PackagingConstants;
using System;
using BookingBirthday.Data.EF;


namespace BookingBirthday.Server.Controllers
{
    public class CartController : Controller
    {
        private readonly BookingDbContext _appContext;
        public CartController(BookingDbContext appContext)
        {
            _appContext = appContext;
        }

        public const string CARTKEY = "cart";
        public List<Cart> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY)!;
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart)!;
            }
            return new List<Cart>();
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        void SaveCartSession(List<Cart> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        [Route("/cart", Name = "cart")]
        public IActionResult Index()
        {
            var session = HttpContext.Session;
            var role = HttpContext.Session.GetString("role");
            List<Category_requests> category_request = null!;

            if (role != null)
            {

                if (role == "Admin")
                {
                    category_request = _appContext.Category_Requests
                        .Where(x => x.is_approved == 0 && x.is_deleted_by_admin == false)
                        .OrderByDescending(x => x.created_at)
                        .ToList();
                }
                else if (role == "Store Owner")
                {

                    var user_id = int.Parse(HttpContext.Session.GetString("user_id")!);
                    category_request = _appContext.Category_Requests
                        .Where(x => x.is_deleted_by_owner == false && x.requester_id == user_id && (x.is_approved == -1 || x.is_approved == 1))
                        .OrderByDescending(x => x.created_at)
                        .ToList();
                }

                if (category_request != null)
                {
                    var jsonNotification = JsonConvert.SerializeObject(category_request);
                    session.SetString("notification", jsonNotification);
                }
            }

            return View(GetCartItems());
        }
        public IActionResult Succ()
        {
            return View();
        }
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _appContext.Packages
                .Where(p => p.Id == productid )
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Package!.Id == productid);
            if (cartitem != null)
            {
                TempData["Message"] = "Sản phẩm đã được thêm";
                TempData["Success"] = false;
                return Ok();
            }
            else
            {
                cart.Add(new Cart() {  Package = product });
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Index));
        }
        //[Route("/updatecart", Name = "updatecart")]
        //[HttpPost]
        //public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        //{
        //    var cart = GetCartItems();
        //    var cartitem = cart.Find(p => p.Package!.Id == productid);
        //    if (cartitem != null)
        //    {
        //        var product = _appContext.Packages
        //        .Where(p => p.product_id == productid)
        //        .FirstOrDefault();
        //        if (product != null)
        //        {
        //            if (product.quantity >= quantity)
        //            {
        //                cartitem.Quantity = quantity;
        //            }
        //            else
        //            {
        //                TempData["Message"] = "Sản phẩm không đủ số lượng";
        //                TempData["Success"] = false;
        //                return Ok();
        //            }
        //        }
        //        else
        //        {
        //            TempData["Message"] = "Sản phẩm không tồn tại";
        //            TempData["Success"] = false;
        //            return Ok();
        //        }
        //    }
        //    SaveCartSession(cart);
        //    return Ok();
        //}
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Package!.Id == productid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Bill request)
        {
            if (HttpContext.Session.GetString("user_id") != null)
            {

                try
                {
                    request.Cart = GetCartItems();
                    request.Id = int.Parse(HttpContext.Session.GetString("user_id")!);
                    var donHang = new Booking();
                    donHang.Id = request.Id;
                    donHang.Date_order = DateTime.Now;
                    donHang.BookingStatus= Data.Enums.BookingStatus.Processing;
                    donHang.phone = request.phone;
                    donHang.note = request.note;
                    donHang.email = request.email;
                    donHang.Total = request.Total;
                    await _appContext.AddAsync(donHang);
                    await _appContext.SaveChangesAsync();

                    if (request.Cart != null)
                    {
                        foreach (var item in request.Cart)
                        {
                            var chiTietDonHang = new Cart();
                            chiTietDonHang.Id = donHang.Id;
                            chiTietDonHang.PackageId = item.Package!.Id;
                            chiTietDonHang.Price = item.Package!.Price;
                            await _appContext.AddAsync(chiTietDonHang);
                            await _appContext.SaveChangesAsync();

                            var product = _appContext.Packages
                                .Where(p => p.Id == item.Package!.Id)
                                .FirstOrDefault();
                            if (product != null)
                            {
                                TempData["Message"] = "Sản phẩm đã được thêm";
                                TempData["Success"] = false;
                                await _appContext.SaveChangesAsync();
                            }
                        }

                        TempData["Message"] = "Đặt hàng thành công";
                        TempData["Success"] = true;
                        ClearCart();
                        return RedirectToAction("Succ", "Cart");
                    }
                    else
                    {
                        TempData["Message"] = "Đặt hàng không thành công";
                        TempData["Success"] = false;
                    }
                    return RedirectToAction("", "Cart");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Lỗi";
                    TempData["Success"] = false;
                    return RedirectToAction("", "Cart");
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
