using AppointmentScheduling.Commands;
using AppointmentScheduling.Repositories;

namespace AppointmentScheduling.Handlers
{
    public class UpdateAppointmentHandler
    {
        private readonly IAppointmentRepository _repository;

        public UpdateAppointmentHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(UpdateAppointmentCommand command)
        {
            var appointment = await _repository.GetByIdAsync(command.Id);
            if (appointment == null)
                return false;

            appointment.Date = command.Date;
            appointment.Client = command.Client;
            appointment.ServiceType = command.ServiceType;
            appointment.Notes = command.Notes;
            appointment.Status = command.Status;

            await _repository.UpdateAsync(appointment);
            return true;
        }
    }
}
