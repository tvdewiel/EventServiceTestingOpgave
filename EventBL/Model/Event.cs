namespace EventBL
{
    public class Event
    {
        private Dictionary<int, Visitor> _visitors = new Dictionary<int, Visitor>();

        public string Name { get; private set; }
        public DateTime Date { get; set; }
        public string Location { get; private set; }
        public int MaxVisitors { get; private set; }
        public IReadOnlyList<Visitor> Visitors => _visitors.Values.ToList().AsReadOnly(); 
        public Event(string name, DateTime date, string location, int maxVisitors)
        {
            SetName(name);
            Date = date;
            SetLocation(location);
            SetMaxVisitors(maxVisitors);
        }
        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("AddVisitor");
            if (_visitors.ContainsKey(visitor.Id)) throw new EventException("AddVisitor - duplicate");
            if (_visitors.Values.Count == MaxVisitors) throw new EventException("AddVisitor - max");
            _visitors.Add(visitor.Id,visitor);            
        }
        public void RemoveVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("RemoveVisitor");
            if (!_visitors.ContainsKey(visitor.Id)) throw new EventException("RemoveVisitor - duplicate");
            _visitors.Remove(visitor.Id);
        }
        //public IReadOnlyList<Visitor> GetVisitors()
        //{
        //    return Visitors.AsReadOnly();
        //}
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EventException("Event - SetName");
            Name = name;
        }
        public void SetLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) throw new EventException("Event - SetLocation");
            Location = location;
        }
        public void SetMaxVisitors(int max)
        {
            if (max <= 0) throw new EventException("Event - SetMaxVisitor");
            MaxVisitors = max;
        }

        public override bool Equals(object? obj)
        {
            return obj is Event @event &&
                   Name == @event.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}