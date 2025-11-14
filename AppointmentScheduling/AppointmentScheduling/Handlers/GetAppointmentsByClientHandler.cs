using AppointmentScheduling.Models;
using AppointmentScheduling.Queries;
using AppointmentScheduling.Repositories;

namespace AppointmentScheduling.Handlers
{
    public class GetAppointmentsByClientHandler
    {
        private readonly IAppointmentRepository _repository;

        public GetAppointmentsByClientHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Appointment>> HandleAsync(GetAppointmentsByClientQuery query)
        {
            var all = await _repository.GetAllAsync();
            return all.Where(a =>
                (string.IsNullOrEmpty(query.Name) || a.Client.Name == query.Name) &&
                (string.IsNullOrEmpty(query.Phone) || a.Client.Phone == query.Phone));
        }
    }
}
