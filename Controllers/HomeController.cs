using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Services;

namespace HospitalAppointmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly DoctorService _doctorService;

        public HomeController(
            IMemoryCache cache,
            DoctorService doctorService)
        {
            _cache = cache;
            _doctorService = doctorService;
        }

        public IActionResult Index()
        {
            const string cacheKey = "DoctorList";

            if (!_cache.TryGetValue(cacheKey, out List<Doctor>? doctors))
            {
                doctors = _doctorService.GetDoctors();

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                _cache.Set(cacheKey, doctors, options);
            }

            return View(doctors);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 60,
            Location = ResponseCacheLocation.Any,
            NoStore = false)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}