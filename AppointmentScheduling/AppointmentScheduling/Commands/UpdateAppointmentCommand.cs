using AppointmentScheduling.Models;

namespace AppointmentScheduling.Commands
{
    public class UpdateAppointmentCommand
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ClientInfo Client { get; set; }
        public string ServiceType { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}
