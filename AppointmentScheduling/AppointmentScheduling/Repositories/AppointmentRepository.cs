using AppointmentScheduling.Models;
using AppointmentScheduling.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AppointmentScheduling.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IMongoCollection<Appointment> _collection;

        public AppointmentRepository(IOptions<MongoDbSettings> settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Appointment>(settings.Value.AppointmentsCollection);
        }
            
        public async Task AddAsync(Appointment appointment) =>
            await _collection.InsertOneAsync(appointment);

        public async Task UpdateAsync(Appointment appointment) =>
            await _collection.ReplaceOneAsync(a => a.Id == appointment.Id, appointment);

        public async Task DeleteAsync(Guid id) =>
            await _collection.DeleteOneAsync(a => a.Id == id);

        public async Task<Appointment> GetByIdAsync(Guid id) =>
            await _collection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Appointment>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByClientAsync(string name, string phone)
        {
            var filterBuilder = Builders<Appointment>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
                filter &= filterBuilder.Eq(a => a.Client.Name, name);

            if (!string.IsNullOrEmpty(phone))
                filter &= filterBuilder.Eq(a => a.Client.Phone, phone);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDateAndServiceAsync(DateTime date, string serviceType)
        {
            var filterBuilder = Builders<Appointment>.Filter;
            var filter = filterBuilder.Eq(a => a.Date, date.Date);

            if (!string.IsNullOrEmpty(serviceType))
                filter &= filterBuilder.Eq(a => a.ServiceType, serviceType);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}
