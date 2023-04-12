using EventsApi.Models;

namespace EventsApi.Services;

public interface IEventsService
{
    Task<IEnumerable<Event>> GetAllEvents();
    Task<Event> GetEventById(int id);
    Task CreateEvent(Event eventRequest);
    Task UpdateEvent(int id, Event eventRequest);
    Task DeleteEvent (int id);
}