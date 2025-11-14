using AppointmentScheduling.Models;

namespace AppointmentScheduling.Repositories
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Guid id);
        Task<Appointment> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetByClientAsync(string name, string phone);
        Task<IEnumerable<Appointment>> GetByDateAndServiceAsync(DateTime date, string serviceType);
    }
}
