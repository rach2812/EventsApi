namespace EventsApi.Services;
using EventsApi.Models;
using Microsoft.EntityFrameworkCore;

public class EventsService : IEventsService
{
    private EventsContext _context;

    public EventsService(EventsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetEventsForDate(DateTime selectedDate)
    {
        return await _context.Events.Where(x => x.StartDate == selectedDate).ToListAsync();
    }

    public async Task<Event> GetEventById(int id)
    {
        var retrievedEvent = await _context.Events.FindAsync(id);
        if (retrievedEvent == null) {
            throw new KeyNotFoundException("Event not found");
        }
        return retrievedEvent;
    }

    public async Task CreateEvent(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEvent(int id, Event eventRequest)
    {
        var retrievedEvent = await _context.Events.FindAsync(id);
        if (retrievedEvent == null) {
            throw new KeyNotFoundException("No event found");
        }
        // TODO: Mappers
        retrievedEvent.Description = eventRequest.Description;
        retrievedEvent.Location = eventRequest.Location;
        retrievedEvent.Title = eventRequest.Title;
        retrievedEvent.StartDate = eventRequest.StartDate;
        retrievedEvent.EndDate = eventRequest.EndDate;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEvent(int id)
    {
        var retrievedEvent = await _context.Events.FindAsync(id);
        if (retrievedEvent == null) {
            return;
        }
        _context.Events.Remove(retrievedEvent);
        await _context.SaveChangesAsync();
    }
}