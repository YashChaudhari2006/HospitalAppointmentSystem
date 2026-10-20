using Microsoft.AspNetCore.Mvc;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Services;
using HospitalAppointmentSystem.Extensions;

namespace HospitalAppointmentSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [ResponseCache(
            Duration = 60,
            Location = ResponseCacheLocation.Client,
            NoStore = true)]
        public IActionResult Details(int DoctorId)
        {
            var doctor = _doctorService.GetDoctors()
                .FirstOrDefault(d => d.DoctorId == DoctorId);

            if (doctor == null)
                return NotFound("Doctor not found.");

            return View(doctor);
        }

        [HttpGet]
        public IActionResult Book(int DoctorId)
        {
            var doctor = _doctorService.GetDoctors()
                .FirstOrDefault(d => d.DoctorId == DoctorId);

            if (doctor == null)
                return NotFound("Doctor not found.");

            var patientName = Request.Cookies["PatientName"];

            var appointment = new Appointment
            {
                DoctorId = doctor.DoctorId,
                PatientName = patientName ?? string.Empty,
                Specialization = doctor.Specialization
            };

            HttpContext.Session.SetString(
                "SelectedSpecialization", doctor.Specialization);

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(Appointment appointment)
        {
            var doctor = _doctorService.GetDoctors()
                .FirstOrDefault(d => d.DoctorId == appointment.DoctorId);

            if (doctor == null)
                return NotFound("Doctor not found.");

            if (string.IsNullOrWhiteSpace(appointment.PatientName))
                ModelState.AddModelError(
                    nameof(appointment.PatientName),
                    "Patient name is required.");

            if (string.IsNullOrWhiteSpace(appointment.AppointmentDate))
                ModelState.AddModelError(
                    nameof(appointment.AppointmentDate),
                    "Appointment date is required.");

            if (!ModelState.IsValid)
            {
                appointment.Specialization = doctor.Specialization;
                return View(appointment);
            }

            Response.Cookies.Append(
                "PatientName",
                appointment.PatientName.Trim(),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                });

            HttpContext.Session.SetString(
                "SelectedSpecialization", doctor.Specialization);

            TempData["Message"] =
                $"Appointment request submitted for {doctor.DoctorName}.";

            TempData["PatientName"] = appointment.PatientName;
            TempData["DoctorName"] = doctor.DoctorName;
            TempData["AppointmentDate"] = appointment.AppointmentDate;
            TempData["TotalCharge"] = doctor.CalculateTotalCharge().ToString("0.00");

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            if (TempData["Message"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}