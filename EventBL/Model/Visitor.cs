using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL
{
    public class Visitor
    {
        public Visitor(string name, DateTime birthDay, int id)
        {
            Name = name;
            BirthDay = birthDay;
            Id = id;
        }

        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public int Id { get; set; }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EventException("Visitor - SetName");
            Name = name;
        }
        public void SetId(int id)
        {
            if (id <= 0) throw new EventException("Visitor - SetId");
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Visitor visitor &&
                   Name == visitor.Name &&
                   BirthDay == visitor.BirthDay &&
                   Id == visitor.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, BirthDay, Id);
        }
    }
}
