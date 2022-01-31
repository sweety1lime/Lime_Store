using Lime_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace Lime_Store.Controllers
{
    public class HomeController : Controller
    {
        private EShopper_BaseContext db;

        public HomeController(EShopper_BaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            Filter filter = new Filter
            {
                brand = db.Brands.ToListAsync().Result
            };

            string brandName = Convert.ToString(Request.Query["BrandName"]);

            filter.product = brandName != null ? db.Products.ToListAsync().Result.Where(item => item.Brand == brandName).ToList() :
                db.Products.ToListAsync().Result;
                //.GetRange(0,6);

            return View(filter);
        }

        public IActionResult AddToCart()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);

            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart"))//Проверяем есть ли сохранённая корзина у пользователя
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart")); //десериализируем корзину из сессии
            cart.ProductCount = db.Products.Count();
            cart.CartLines.Add(db.Products.Find(ID)); //Добавляем в корзину элемент Product по первичному ключу
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart)); // Сохраняем новую корзину в сессию

            return Redirect("~/Home/Cart");
        }

        public IActionResult RemoveFromCart()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);

            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart")) // Проверяем есть ли сохранённая корзина у пользователя
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart")); // десериализируем корзину из сессии
            cart.ProductCount = db.Products.Count();
            cart.CartLines.Remove(cart.CartLines.FindLast(item => item.ProductId == ID));
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart)); // Сохраняем новую корзину в сессию

            return Redirect("~/Home/Cart");
        }

        public IActionResult RemoveAllFromCart()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);

            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart")) // Проверяем есть ли сохранённая корзина у пользователя
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart")); // десериализируем корзину из сессии
            cart.CartLines.RemoveAll(item => item.ProductId == ID);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart)); // Сохраняем новую корзину в сессию

            return Redirect("~/Home/Cart");
        }

        public IActionResult Cart() //не совсем сюда но в принципе да
        {
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart")) // Проверяем есть ли сохранённая корзина у пользователя
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart")); // десериализируем корзину из сессии
            return View(cart);
        }
        public IActionResult Reg()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            Cart cart = new Cart();
            if (HttpContext.Session.Keys.Contains("Cart")) // Проверяем есть ли сохранённая корзина у пользователя
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart")); // десериализируем корзину из сессии
            return View(cart);
        }

        [HttpPost]
        public IActionResult CheckOutBD(CheckOut checkOut)
        {
            Cart cart;
            if (HttpContext.Session.Keys.Contains("Cart"))
            { // Проверяем есть ли сохранённая корзина у пользователя
                cart = JsonSerializer.Deserialize<Cart>(HttpContext.Session.GetString("Cart")); // десериализируем корзину из сессии
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-H0VEMOV\\CLOWN;Initial Catalog=Lime_Shop;Persist Security Info=True;User ID=sa;Password=12345678");
                connection.Open();
                foreach (var item in cart.CartLines)
                {
                    SqlCommand command = new SqlCommand("insert into [Order](Email, LastName, Name, MiddleName, Address, [Index], Country, City, Phone, Product_Id) " +
                        $"values ('{checkOut.Email}','{checkOut.LastName}','{checkOut.Name}','{checkOut.MiddleName}','{checkOut.Address}','{checkOut.Index}','{checkOut.Country}', " +
                        $"'{checkOut.City}', {checkOut.Phone}, {item.ProductId})", connection);
                    command.ExecuteNonQuery();
                }
                cart = new Cart();
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                connection.Close();
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMail(Send send)
        {
            if (ModelState.IsValid)
            {
                MailAddress from = new MailAddress("bolshiye.goroda.00@mail.ru", "Lime-Store");
                MailAddress to = new MailAddress(send.Email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Обращение от пользователя";
                m.Body = "Сообщение отправлено. Спасибо Вам " + send.Name + ", мы скоро свяжемся с Вами.";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("bolshiye.goroda.00@mail.ru", "QWErty1056");
                smtp.EnableSsl = true;
                smtp.Send(m);


                MailAddress too = new MailAddress("bolshiye.goroda.00@mail.ru", "Lime-Store");
                MailMessage mo = new MailMessage(too, too);
                mo.Subject = "Обращение от пользователя " + send.Email;
                mo.Body = send.Message;
                mo.IsBodyHtml = true;

                smtp.Credentials = new NetworkCredential("bolshiye.goroda.00@mail.ru", "QWErty1056");
                smtp.EnableSsl = true;
                smtp.Send(mo);
            }
            return View("ContactUs");
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            int ID = Convert.ToInt32(Request.Query["ID"]);
            Filter filter = new Filter
            {
                product = db.Products.ToListAsync().Result.Where(item => item.ProductId == ID).ToList(),
                brand = db.Brands.ToListAsync().Result
            };
            return View(filter);
        }

        public IActionResult Shop()
        {
            Filter filter = new Filter
            {
                brand = db.Brands.ToListAsync().Result
            };

            string brandName = Convert.ToString(Request.Query["BrandName"]);

            filter.product = brandName != null ? db.Products.ToListAsync().Result.Where(item => item.Brand == brandName).ToList() :
                db.Products.ToListAsync().Result;
            return View(filter);
        }

    }
}
