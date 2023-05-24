using ConsumeProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
namespace ConsumeProductApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string baseURL = "https://localhost:44398/api/Products/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            
                List<ProductList>? products = new List<ProductList>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    try
                    {
                        HttpResponseMessage getData = await client.GetAsync("GetProduct");

                        if (getData.IsSuccessStatusCode)
                        {
                            string result = getData.Content.ReadAsStringAsync().Result;
                            products = JsonConvert.DeserializeObject<List<ProductList>>(result);
                            return View(products);
                        }
                        else
                        {
                            return View(null);
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        ViewBag.ErrorMessage = e.Message;

                        return View("Error");
                    }
                }
            
            
            
        }
        
        public IActionResult AddNewProduct()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductList product)
        {
            
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    try
                    {
                        HttpResponseMessage getData = await client.PostAsJsonAsync("PostProduct", product);

                        if (getData.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        ViewBag.ErrorMessage = e.Message;

                        return View("Error");
                    }
                }
                
            }
            return View(null);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            ProductList? products = new ProductList();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                try
                {
                    HttpResponseMessage getData = await client.GetAsync("GetProduct/" + id);

                    if (getData.IsSuccessStatusCode)
                    {
                        string result = getData.Content.ReadAsStringAsync().Result;
                        products = JsonConvert.DeserializeObject<ProductList>(result);
                        return View(products);
                    }
                    else
                    {
                        Console.WriteLine("Error calling web api");
                    }
                }
                catch (HttpRequestException e)
                {
                    ViewBag.ErrorMessage = e.Message;

                    return View("Error");
                }
            }
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductList updateproduct)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    try
                    {
                        HttpResponseMessage getData = await client.PutAsJsonAsync("PutProduct/" + updateproduct.Id, updateproduct);

                        if (getData.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Console.WriteLine("Error calling web api");
                        }
                    }                    
                    catch (HttpRequestException e)
                    {
                        ViewBag.ErrorMessage = e.Message;

                        return View("Error");
                    }
                }
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct (int productid)
        {

           
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    try
                    {
                        HttpResponseMessage getData = await client.DeleteAsync("DeleteProduct/" + productid);
                        if (getData.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Console.WriteLine("Error calling web api");
                        }
                    }
                    
                    catch (HttpRequestException e)
                    {
                        ViewBag.ErrorMessage = e.Message;

                        return View("Error");
                    }
                }
            
            return View();

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}