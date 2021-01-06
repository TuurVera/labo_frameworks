using Microsoft.AspNetCore.Mvc;
using Rest_deel2.Data;
using Rest_deel2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_deel2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsMessagesMemoryController : ControllerBase
    {

        private NewsMessagesRepository messageRepo;

        public NewsMessagesMemoryController(NewsMessagesRepository messageRepo)
        {
            this.messageRepo = messageRepo;
        }


        // GET: api/<NewsMessagesMemoryController>
        [HttpGet]
        public IEnumerable<NewsMessage> Get()
        {
            return this.messageRepo.messages.Values;  
        }

        // GET api/<NewsMessagesMemoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (messageRepo.messages.Keys.Contains(id))
            {
                return new JsonResult(this.messageRepo.messages[id]);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<NewsMessagesMemoryController>
        [HttpPost]
        public IActionResult Post([FromBody] NewsMessage value)
        {
            int id = messageRepo.getUnusedId();
            value.id = id;
            messageRepo.messages.Add(id, value);
            string url = $"{Request.Scheme}://{Request.Host}{Request.Path}/{id}";
            Response.Headers.Add("Location", url);
            return new StatusCodeResult(201);
        }

        // PUT api/<NewsMessagesMemoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] NewsMessage value)
        {
            if (messageRepo.messages.Keys.Contains(id))
            {
                value.id = id;
                messageRepo.messages[id] = value;
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<NewsMessagesMemoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            messageRepo.messages.Remove(id);
            return NoContent();
        }
    }
}
