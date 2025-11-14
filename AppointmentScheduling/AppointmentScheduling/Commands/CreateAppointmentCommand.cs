using AppointmentScheduling.Models;

namespace AppointmentScheduling.Commands
{
    public class CreateAppointmentCommand
    {
        public DateTime Date { get; set; }
        public ClientInfo Client { get; set; }
        public string ServiceType { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }

}
