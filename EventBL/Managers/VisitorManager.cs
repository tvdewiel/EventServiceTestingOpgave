using EventBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Managers
{
    public class VisitorManager : IVisitorManager
    {
        private int _id = 1;
        private Dictionary<int, Visitor> _visitors = new Dictionary<int, Visitor>();

        public VisitorManager()
        {
            _visitors.Add(_id, new Visitor("John", DateTime.Parse("12/3/1975"), _id++));
            _visitors.Add(_id, new Visitor("Jane", DateTime.Parse("18/7/1995"), _id++));
            _visitors.Add(_id, new Visitor("David", DateTime.Parse("2/4/2001"), _id++));
            _visitors.Add(_id, new Visitor("Chris", DateTime.Parse("12/9/1999"), _id++));
        }
        public void SubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("RegisterVisitor");
            if (_visitors.ContainsKey(visitor.Id)) throw new EventException("RegisterVisitor");
            _visitors.Add(visitor.Id, visitor);
        }
        public Visitor RegisterVisitor(Visitor visitor)
        {
            visitor.Id = _id++;
            return visitor;
        }
        public void UnsubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("UnsubscribeVisitor");
            if (!_visitors.ContainsKey(visitor.Id)) throw new EventException("UnsubscribeVisitor");
            _visitors.Remove(visitor.Id);
        }
        public IReadOnlyList<Visitor> GetAllVisitors()
        {
            return _visitors.Values.ToList().AsReadOnly();
        }
        public Visitor GetVisitor(int id)
        {
            if (!_visitors.ContainsKey(id)) throw new EventException("GetVisitor");
            return _visitors[id];
        }
        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("UpdateVisitor");
            if (!_visitors.ContainsKey(visitor.Id)) throw new EventException("UpdateVisitor");
            if (visitor.Equals(_visitors[visitor.Id])) throw new EventException("UpdateVisitor");
            _visitors[visitor.Id] = visitor;
        }
        public bool ExistsVisitor(int id)
        {
            return _visitors.ContainsKey(id);
        }
    }
}
