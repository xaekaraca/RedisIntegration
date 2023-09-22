using Microsoft.AspNetCore.Mvc;
using RedisIntegration.Models;
using RedisIntegration.Services;

namespace RedisIntegration.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly SessionService _sessionService;
    
    public SessionController(SessionService sessionService)
    {
        _sessionService = sessionService;
    }
    
    [HttpGet]
    public IActionResult GetSession([FromQuery] string userId)
    {
        var session = _sessionService.GetSession(userId);
        
        if (session == null)
            return NotFound();
        
        return Ok(session);
    }
    
    [HttpPost]
    public IActionResult CreateSession([FromForm] UserModel user)
    {
        var session = _sessionService.CreateSession(user);
        return Ok(session);
    }
    
    [HttpPost("{userId}")]
    public IActionResult RefreshSession([FromRoute] string userId)
    {
        _sessionService.RefreshSession(userId);
        return Ok();
    }
    
    [HttpPut("{userId}")]
    public IActionResult UpdateSession([FromRoute] string userId, [FromBody] SessionUpdateModel sessionUpdateModel)
    {
        var session = _sessionService.UpdateAndRefreshSession(userId, sessionUpdateModel);
        
        if (session == null)
            return NotFound();
        
        return Ok(session);
    }
    
    [HttpDelete("{userId}")]
    public IActionResult KillSession([FromRoute] string userId)
    {
        _sessionService.KillSession(userId);
        return Ok();
    }
}