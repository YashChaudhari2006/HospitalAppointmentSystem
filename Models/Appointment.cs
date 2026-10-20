namespace HospitalAppointmentSystem.Models
{
    public class Appointment
    {
        public int DoctorId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public string AppointmentDate { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
    }
}