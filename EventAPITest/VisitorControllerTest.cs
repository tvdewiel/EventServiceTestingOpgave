using Castle.Core.Logging;
using EventAPI.Controllers;
using EventBL;
using EventBL.Interfaces;
using EventBL.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace EventAPITest
{
    public class VisitorControllerTest
    {
        private readonly VisitorController visitorController;
        private readonly Mock<IVisitorManager> VM;
        private Mock<ILogger<VisitorController>> logger;

        public VisitorControllerTest()
        {
            VM = new Mock<IVisitorManager>();
            logger=new Mock<ILogger<VisitorController>>();
            visitorController = new VisitorController(VM.Object,logger.Object);
        }

        [Fact]
        public void GET_InvalidId_BadRequestResult()
        {
            VM.Setup(x => x.GetVisitor(7)).Throws(new EventException("invalid id"));
            var result = visitorController.Get(7);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GET_ValidId_OkResult()
        {
            Visitor v = new("jos", DateTime.Parse("1/1/1989"),5);
            VM.Setup(x => x.GetVisitor(7)).Returns(v);
            var result = visitorController.Get(7);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(v,((OkObjectResult)result.Result).Value);
        }
        [Fact]
        public void POST_NoVisitor_BadRequest()
        {
            var result = visitorController.Post(null);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}