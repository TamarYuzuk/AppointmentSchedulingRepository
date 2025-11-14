using AppointmentScheduling.Repositories;

namespace AppointmentScheduling.Handlers
{
    public class DeleteAppointmentHandler
    {
        private readonly IAppointmentRepository _repository;

        public DeleteAppointmentHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(Guid id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            if (appointment == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
