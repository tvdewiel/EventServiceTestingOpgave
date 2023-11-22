using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBL.Interfaces;

namespace EventBL.Managers
{
    public class EventManager : IEventManager
    {
        private Dictionary<string, Event> _events = new Dictionary<string, Event>();

        public EventManager()
        {
            _events.Add("ASP.NET Boot", new Event("ASP.NET Boot", DateTime.Parse("24/10/2022"), "Schoonmeersen Lokaal 1.0012", 20));
            _events.Add("Bijscholing async", new Event("Bijscholing async", DateTime.Parse("14/11/2022"), "Mercator", 10));
            _events.Add("MongoDB", new Event("MongoDB", DateTime.Parse("1/12/2022"), "Mercator", 4));
        }

        public void AddEvent(Event ev)
        {
            if (ev == null) throw new EventException("AddEvent");
            if (_events.ContainsKey(ev.Name)) throw new EventException("AddEvent");
            _events.Add(ev.Name, ev);
        }
        public void RemoveEvent(Event ev)
        {
            if (ev == null) throw new EventException("RemoveEvent");
            if (!_events.ContainsKey(ev.Name)) throw new EventException("RemoveEvent");
            _events.Remove(ev.Name);
        }
        public void SubscribeVisitor(Visitor visitor, Event ev)
        {
            try
            {
                _events[ev.Name].AddVisitor(visitor);
            }
            catch (Exception ex)
            {
                throw new EventException("SubscribeVisitor", ex);
            }
        }
        public void UnsubscribeVisitor(Visitor visitor, Event ev)
        {
            try
            {
                _events[ev.Name].RemoveVisitor(visitor);
            }
            catch (Exception ex)
            {
                throw new EventException("UnsubscribeVisitor", ex);
            }
        }
        public IReadOnlyList<Event> GetAllEvents()
        {
            return _events.Values.ToList().AsReadOnly();
        }
        public IReadOnlyList<Event> GetEventsForLocation(string location)
        {
            return _events.Values.Where(e => e.Location == location).ToList();
        }
        public IReadOnlyList<Event> GetEventsForDate(DateTime dateTime)
        {
            return _events.Values.Where(e => e.Date.Date == dateTime.Date).ToList();
        }
        public Event GetEvent(string name)
        {
            if (!_events.ContainsKey(name)) throw new EventException("GetEvent");
            return _events[name];
        }
        public bool ExistsEvent(string name)
        {
            return _events.ContainsKey(name);
        }
        public void UpdateEvent(Event ev)
        {
            if (ev == null) throw new EventException("UpdateEvent");
            if (!_events.ContainsKey(ev.Name)) throw new EventException("UpdateEvent");
            if (_events[ev.Name].Equals(ev)) throw new EventException("UpdateEvent");
            _events[ev.Name] = ev;
        }
    }
}
