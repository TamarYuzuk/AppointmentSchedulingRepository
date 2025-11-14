namespace AppointmentScheduling.Queries
{
    public class GetAppointmentsByDateAndServiceQuery
    {
        public DateTime? Date { get; set; }
        public string? ServiceType { get; set; }
    }
}
