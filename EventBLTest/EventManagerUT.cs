using EventBL;
using EventBL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EventBLTest
{
    public class EventManagerUT
    {

        [Fact]
        public void AddEvent_NullEvent_EventException()
        {
            EventManager EM=new EventManager();
            Assert.Throws<EventException> (() => EM.AddEvent(null));
        }
        [Fact]
        public void AddEvent_SameNullEvent_EventException()
        {
            EventManager EM = new EventManager();
            Event ev = new Event("e1", DateTime.Parse("9/10/2022"), "Gent", 20);
            EM.AddEvent(ev);
            Assert.Throws<EventException>(() => EM.AddEvent(ev));
            //test of equals is aangemaakt
            Event ev2 = new Event("e1", DateTime.Parse("9/10/2022"), "Gent", 20);
            Assert.Throws<EventException>(() => EM.AddEvent(ev));
        }
        [Fact]
        public void AddEvent_ValidEvent_Added()
        {
            EventManager EM = new EventManager();
            Event ev = new Event("e1", DateTime.Parse("9/10/2022"), "Gent", 20);
            var events=EM.GetAllEvents();
            int c1=EM.GetAllEvents().Count; 
            EM.AddEvent(ev);
            int c2=EM.GetAllEvents().Count;
            Assert.Equal(ev, EM.GetEvent("e1"));
            Assert.Equal(c1+1, c2);
            Assert.True((new HashSet<Event>(EM.GetAllEvents())).IsSupersetOf(new HashSet<Event>(events)));
        }
        [Fact]
        public void SubscribeVisitor_ExceptionCatched_EventException()
        {
            EventManager EM = new EventManager();
            Event ev = new Event("e1", DateTime.Parse("9/10/2022"), "Gent", 20);
            EM.AddEvent(ev);
            Visitor v1 = new Visitor("jos", DateTime.Parse("18/04/2002"), 101);
            Visitor v2 = new Visitor("jos", DateTime.Parse("18/04/2002"), 101);
            //TODO moq gebruiken
            EM.SubscribeVisitor(v1, ev);
            Assert.Throws<EventException>(() => EM.SubscribeVisitor(v2, ev));
        }
        [Fact]
        public void SubscribeVisitor_Valid()
        {
            EventManager EM = new EventManager();
            Event ev = new Event("e1", DateTime.Parse("9/10/2022"), "Gent", 20);
            EM.AddEvent(ev);
            Visitor v1 = new Visitor("jos", DateTime.Parse("18/04/2002"), 101);
            var visitors = EM.GetEvent(ev.Name).Visitors;
            int c1=EM.GetEvent(ev.Name).Visitors.Count;
            EM.SubscribeVisitor(v1, ev);
            int c2 = EM.GetEvent(ev.Name).Visitors.Count;
            Assert.Contains(v1,EM.GetEvent(ev.Name).Visitors);
            Assert.Equal(c1 + 1,c2);
            Assert.True((new HashSet<Visitor>(EM.GetEvent(ev.Name).Visitors).IsSupersetOf(new HashSet<Visitor>(visitors))));
        }
    }
}