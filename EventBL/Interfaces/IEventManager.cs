namespace EventBL.Interfaces
{
    public interface IEventManager
    {
        void AddEvent(Event ev);
        bool ExistsEvent(string name);
        IReadOnlyList<Event> GetAllEvents();
        Event GetEvent(string name);
        IReadOnlyList<Event> GetEventsForDate(DateTime dateTime);
        IReadOnlyList<Event> GetEventsForLocation(string location);
        void RemoveEvent(Event ev);
        void SubscribeVisitor(Visitor visitor, Event ev);
        void UnsubscribeVisitor(Visitor visitor, Event ev);
        void UpdateEvent(Event ev);
    }
}