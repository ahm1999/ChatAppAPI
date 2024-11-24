using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Services;

namespace ChatApp.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessegesController : ControllerBase
    {
        private readonly IMessegeService _messageService;
        public MessegesController(IMessegeService messegeService)
        {
            _messageService = messegeService;
        }
        [HttpGet("{userId1}/{userId2}")]
        public async Task<IActionResult> Get([FromRoute] string userId1, [FromRoute] string userId2) { 
            
            var result = await _messageService.GetMessagesBetween(userId1, userId2);

            return Ok(result);
        
        }
    }
}
