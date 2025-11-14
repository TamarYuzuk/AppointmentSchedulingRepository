using AppointmentScheduling.Models;
using AppointmentScheduling.Queries;
using AppointmentScheduling.Repositories;

namespace AppointmentScheduling.Handlers
{
    public class GetAppointmentsHandler
    {
        private readonly IAppointmentRepository _repository;

        public GetAppointmentsHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Appointment>> HandleAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
