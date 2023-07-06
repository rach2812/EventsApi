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
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(string selectedDate)
        {
            var date = DateTime.Parse(selectedDate);
            var responseBody = await _eventsService.GetEventsForDate(date);
            return new ActionResult<IEnumerable<Event>>(responseBody);
        }

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

        [HttpPost]
        public async Task<ActionResult> CreateEvent(EventRequest eventRequest)
        {
            var newEvent = new Event();
            newEvent.Description = eventRequest.Description;
            newEvent.Title = eventRequest.Title;
            newEvent.Location = eventRequest.Location;
            newEvent.StartDate = DateTime.Parse(eventRequest.StartDate);
            newEvent.EndDate = DateTime.Parse(eventRequest.EndDate);
            
            try 
            {
                await _eventsService.CreateEvent(newEvent);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
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
