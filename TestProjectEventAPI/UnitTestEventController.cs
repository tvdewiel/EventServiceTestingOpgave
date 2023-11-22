using EventAPI.Controllers;
using EventBL;
using EventBL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.Metrics;

namespace TestProjectEventAPI
{
    public class UnitTestEventController
    {
        private readonly Mock<IVisitorManager> mockVM;
        private readonly Mock<IEventManager> mockEM;
        private readonly EventController eventController;
        private List<Event> events=new List<Event>();

        public UnitTestEventController()
        {
            mockVM = new Mock<IVisitorManager>();
            mockEM = new Mock<IEventManager>();
            eventController = new EventController(mockEM.Object,mockVM.Object);
            dataSetUpEvents();
        }

        [Fact]
        public void Test_GetAll_Valid()
        {
            mockEM.Setup(em => em.GetAllEvents())
                .Returns(events);
            var result = eventController.GetAll(null,null);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Event>>(((OkObjectResult)result.Result).Value);
            OkObjectResult r= (OkObjectResult)result.Result;
            Assert.Equal(events,r.Value);
        }
        private void dataSetUpEvents()
        {
            events.Add(new Event("ASP.NET Boot", DateTime.Parse("24/10/2022"), "Schoonmeersen Lokaal 1.0012", 20));
            events.Add(new Event("Bijscholing async", DateTime.Parse("14/11/2022"), "Mercator", 10));
            events.Add(new Event("MongoDB", DateTime.Parse("1/12/2022"), "Mercator", 4));
        }
    }
}