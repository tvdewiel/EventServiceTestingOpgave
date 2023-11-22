using EventBL;
using EventBL.Interfaces;
using EventBL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private IVisitorManager VM;
        private ILogger Logger;
        public VisitorController(IVisitorManager vM,ILogger<VisitorController> logger)
        {
            VM = vM;
            Logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Visitor>> GetAll()
        {
            try
            {
                Logger.LogInformation("GetAll called");
                return Ok(VM.GetAllVisitors());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(int id)
        {
            try
            {
                return Ok(VM.GetVisitor(id));
            }
            catch (Exception ex)
            {
                ErrorInfo errorInfo = new ErrorInfo();
                errorInfo.AddInfo("id", id.ToString());
                errorInfo.AddInfo("method", "VM.GetVisitor");
                errorInfo.AddInfo("message", ex.Message);
                return NotFound(errorInfo);
            }
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Visitor>> GetAsync(int id)
        //{
        //    try
        //    {
        //        return Ok(VM.GetVisitor(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorInfo errorInfo = new ErrorInfo();
        //        errorInfo.AddInfo("id",id.ToString());
        //        errorInfo.AddInfo("method", "VM.GetVisitor");
        //        errorInfo.AddInfo("message", ex.Message);
        //        return NotFound(errorInfo);
        //    }
        //}
        [HttpPost]
        public ActionResult<Visitor> Post([FromBody] Visitor visitor)
        {
            if (visitor == null) return BadRequest("invalid visitor");
            try
            {
                visitor=VM.RegisterVisitor(visitor);
                VM.SubscribeVisitor(visitor);
                return CreatedAtAction(nameof(Get),new {id=visitor.Id},visitor);
            }
            catch(Exception ex)
            {                
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Visitor> Put(int id, [FromBody] Visitor visitor)
        {
            if ((visitor == null) || (visitor.Id != id))
                return BadRequest();
            try
            {
                VM.UpdateVisitor(visitor);
                //return NoContent();
                return Ok(visitor);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!VM.ExistsVisitor(id))
                {
                    return NotFound();
                }
                VM.UnsubscribeVisitor(VM.GetVisitor(id));
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
