using AppointmentScheduling.Models;
using AppointmentScheduling.Queries;
using AppointmentScheduling.Repositories;

namespace AppointmentScheduling.Handlers
{
    public class GetAppointmentsByDateAndServiceHandler
    {
        private readonly IAppointmentRepository _repository;

        public GetAppointmentsByDateAndServiceHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Appointment>> HandleAsync(GetAppointmentsByDateAndServiceQuery query)
        {
            var all = await _repository.GetAllAsync();
            return all.Where(a =>
                (!query.Date.HasValue || a.Date.Date == query.Date.Value.Date) &&
                (string.IsNullOrEmpty(query.ServiceType) || a.ServiceType == query.ServiceType));
        }
    }
}
