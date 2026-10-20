using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Services
{
    public class DoctorService
    {
        public List<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    DoctorName = "Dr. Yash Chaudhari",
                    Specialization = "Cardiologist",
                    Experience = 10,
                    ConsultationFee = 500
                },
                new Doctor
                {
                    DoctorId = 2,
                    DoctorName = "Dr. Vikas Mishra",
                    Specialization = "Dermatologist",
                    Experience = 7,
                    ConsultationFee = 400
                },
                new Doctor
                {
                    DoctorId = 3,
                    DoctorName = "Dr. Tilak Verma",
                    Specialization = "Neurologist",
                    Experience = 12,
                    ConsultationFee = 700
                },
                new Doctor
                {
                    DoctorId = 4,
                    DoctorName = "Dr. Rani Singh",
                    Specialization = "Pediatrician",
                    Experience = 5,
                    ConsultationFee = 350
                }
            };
        }
    }
}