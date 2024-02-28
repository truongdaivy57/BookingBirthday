using BookingBirthday.Data.Entities;
using BookingBirthday.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using static NuGet.Packaging.PackagingConstants;
using System;
using BookingBirthday.Data.EF;
using BookingBirthday.Application.Payment.Models;
using BookingBirthday.Application.Payment.Services;
using Microsoft.EntityFrameworkCore;


namespace BookingBirthday.Server.Controllers
{
    public class CartController : Controller
    {
        private readonly BookingDbContext _appContext;
        private readonly IVnPayService _vnPayService;
        public CartController(BookingDbContext appContext, IVnPayService vnPayService)
        {
            _appContext = appContext;
            _vnPayService = vnPayService;
        }

        public const string CARTKEY = "cart";
        public List<CartModel> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY)!;
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartModel>>(jsoncart)!;
            }
            return new List<CartModel>();
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        void SaveCartSession(List<Models.CartModel> ls)
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
                else if (role == "Host")
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
                cart.Add(new Models.CartModel() {  Package = product });
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Index));
        }
        
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
        public async Task<IActionResult> AddOrder(BookingModel request)
        {
            //var maxBookingId = _appContext.Bookings.Select(x => x.Id).DefaultIfEmpty().Max();
            //var nextBookingId = maxBookingId + 1;

            //var maxBookingPackageId = _appContext.BookingPackages.Select(x => x.Id).DefaultIfEmpty().Max();
            //var nextBookingPackageId = maxBookingPackageId + 1;


            if (HttpContext.Session.GetString("user_id") != null)
            {

                try
                {
                    request.CartModels = GetCartItems();
                    request.UserId = int.Parse(HttpContext.Session.GetString("user_id")!);
                    var donHang = new Booking();
                    donHang.UserId = request.UserId;
                    donHang.Date_order = DateTime.Now;
                    donHang.BookingStatus= "Processing";
                    donHang.Phone = request.Phone;
                    donHang.Note = request.Note;
                    donHang.Email = request.Email;
                    donHang.Total = request.Total;
                    donHang.PaymentId = null;

                    await _appContext.AddAsync(donHang);
                    await _appContext.SaveChangesAsync();

                    if (request.CartModels != null)
                    {
                        foreach (var item in request.CartModels)
                        {
                            var chiTietDonHang = new BookingPackage();
                            chiTietDonHang.BookingId = donHang.Id;
                            chiTietDonHang.PackageId = item.Package!.Id;
                            chiTietDonHang.Price = item.Package!.Price;
                            await _appContext.AddAsync(chiTietDonHang);
                            await _appContext.SaveChangesAsync();

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
                catch (Exception)
                {
                    TempData["Message"] = "Lỗi";
                    TempData["Success"] = false;
                    return RedirectToAction("", "Cart");
                }
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Cart/Payment
        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}
