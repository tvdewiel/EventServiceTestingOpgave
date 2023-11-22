using EventBL;
using EventBL.Interfaces;
using EventBL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventManager EM;
        private IVisitorManager VM;

        public EventController(IEventManager eM, IVisitorManager vM)
        {
            EM = eM;
            VM = vM;
        }
        //[HttpGet]
        //public ActionResult<List<Event>> GetAll()
        //{
        //    try
        //    {
        //        return Ok(EM.GetAllEvents());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet]
        public ActionResult<List<Event>> GetAll([FromQuery] string? dateString, [FromQuery] string? location)
        {
            try
            {
                if ((!string.IsNullOrWhiteSpace(location)) && (!string.IsNullOrWhiteSpace(dateString)))
                    return BadRequest("to many filters");
                if (!string.IsNullOrWhiteSpace(location))
                    return Ok(EM.GetEventsForLocation(location));
                if (!string.IsNullOrWhiteSpace(dateString))
                    return Ok(EM.GetEventsForDate(DateTime.Parse(dateString)));                
                return Ok(EM.GetAllEvents());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("date")]
        public ActionResult<List<Event>> GetWithDate([FromQuery] string dateString)
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(dateString))
                //{
                    return Ok(EM.GetEventsForDate(DateTime.Parse(dateString)));
                //}
                //else
                //    return Ok(EM.GetAllEvents());                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("location")]
        public ActionResult<List<Event>> GetWithLocation([FromQuery]string location)
        {
            try
            {               
                //if (string.IsNullOrEmpty(location))
                //    return Ok(EM.GetAllEvents());
                return Ok(EM.GetEventsForLocation(location));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{name}")]
        public ActionResult<Visitor> Get(string name)
        {
            try
            {
                return Ok(EM.GetEvent(name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Event> Post([FromBody] Event ev)
        {
            if (ev == null) return BadRequest("invalid event");
            try
            {                
                EM.AddEvent(ev);
                return CreatedAtAction(nameof(Get), new {name = ev.Name }, ev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{name}")]
        public IActionResult Put(string name, [FromBody] Event ev)
        {
            if ((ev == null) || !ev.Name.Equals(name))
                return BadRequest();
            try
            {
                EM.UpdateEvent(ev);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                if (!EM.ExistsEvent(name))
                {
                    return NotFound();
                }
                EM.RemoveEvent(EM.GetEvent(name));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("{name}/Visitor")]
        public ActionResult<Event> SubscribeVisitor(string name,[FromBody] int visitorId)
        {
            try
            {
                Visitor v=VM.GetVisitor(visitorId);
                Event ev=EM.GetEvent(name);
                EM.SubscribeVisitor(v, ev);
                return CreatedAtAction(nameof(Get),new {name=name},ev);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("{name}/Visitor/{id}")]
        public IActionResult UnsubscribeVisitor(string name,int visitorId)
        {
            try
            {
                Visitor v = VM.GetVisitor(visitorId);
                Event ev=EM.GetEvent(name);
                EM.UnsubscribeVisitor(v, ev);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
