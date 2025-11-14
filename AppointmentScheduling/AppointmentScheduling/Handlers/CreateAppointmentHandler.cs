using AppointmentScheduling.Commands;
using AppointmentScheduling.Models;
using AppointmentScheduling.Repositories;

namespace AppointmentScheduling.Handlers
{
    public class CreateAppointmentHandler
    {
        private readonly IAppointmentRepository _repository;

        public CreateAppointmentHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> HandleAsync(CreateAppointmentCommand command)
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Client = command.Client,
                ServiceType = command.ServiceType,
                Date = command.Date,
                Notes = command.Notes,
                Status = command.Status
            };
            await _repository.AddAsync(appointment);
            return appointment.Id;
        }
    }
}
