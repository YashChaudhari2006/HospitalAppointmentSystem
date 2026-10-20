using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Controllers
{
    public class PostsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PostsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var posts = await client.GetFromJsonAsync<List<Post>>(
                    "https://jsonplaceholder.typicode.com/posts");

                return View(posts ?? new List<Post>());
            }
            catch (HttpRequestException)
            {
                ViewBag.Error = "Unable to retrieve posts. Please try again.";
                return View(new List<Post>());
            }
            catch (JsonException)
            {
                ViewBag.Error = "The API returned invalid data.";
                return View(new List<Post>());
            }
        }
    }
}