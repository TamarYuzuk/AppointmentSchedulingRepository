using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppointmentScheduling.Models
{
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ClientInfo Client { get; set; }
        public string ServiceType { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } = "Scheduled";

    }

    public class ClientInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
