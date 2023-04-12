using Microsoft.AspNetCore.Mvc;
using EventsApi.Models;
using EventsApi.Services;

namespace EventsApi.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var responseBody = await _eventsService.GetAllEvents();
            return new ActionResult<IEnumerable<Event>>(responseBody);
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
          var retrievedEvent = await _eventsService.GetEventById(id);
          return new ActionResult<Event>(retrievedEvent);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event eventRequest)
        {
            try 
            {
                await _eventsService.UpdateEvent(id, eventRequest);
            }
            catch (KeyNotFoundException) 
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateEvent(Event eventRequest)
        {
            try 
            {
                await _eventsService.CreateEvent(eventRequest);
            }
            catch (Exception) 
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventsService.DeleteEvent(id);
            return NoContent();
        }
    }
}
