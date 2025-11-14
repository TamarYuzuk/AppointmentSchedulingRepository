using AppointmentScheduling.Commands;
using AppointmentScheduling.Handlers;
using AppointmentScheduling.Models;
using AppointmentScheduling.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly CreateAppointmentHandler _createHandler;
        private readonly GetAppointmentsHandler _getHandler;
        private readonly UpdateAppointmentHandler _updateHandler;
        private readonly DeleteAppointmentHandler _deleteHandler;
        private readonly GetAppointmentsByClientHandler _getByClientHandler;
        private readonly GetAppointmentsByDateAndServiceHandler _getByDateAndServiceHandler;

        public AppointmentsController(
            CreateAppointmentHandler createHandler,
            GetAppointmentsHandler getHandler,
            UpdateAppointmentHandler updateHandler,
            DeleteAppointmentHandler deleteHandler,
            GetAppointmentsByClientHandler getByClientHandler,
            GetAppointmentsByDateAndServiceHandler getByDateAndServiceHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getByClientHandler = getByClientHandler;
            _getByDateAndServiceHandler = getByDateAndServiceHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
        {
            var id = await _createHandler.HandleAsync(command);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var appointments = await _getHandler.HandleAsync();
            return Ok(appointments);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch.");

            var updated = await _updateHandler.HandleAsync(command);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleteHandler.HandleAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-client")]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByClient([FromQuery] string? name, [FromQuery] string? phone)
        {
            var query = new GetAppointmentsByClientQuery { Name = name, Phone = phone };
            var appointments = await _getByClientHandler.HandleAsync(query);
            return Ok(appointments);
        }

        [HttpGet("by-date-service")]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDateAndService([FromQuery] DateTime? date, [FromQuery] string? serviceType)
        {
            var query = new GetAppointmentsByDateAndServiceQuery { Date = date, ServiceType = serviceType };
            var appointments = await _getByDateAndServiceHandler.HandleAsync(query);
            return Ok(appointments);
        }
    }
}
