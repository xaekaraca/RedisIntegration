using System.Diagnostics;
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
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        var session = _sessionService.GetSession(userId);
        
        stopwatch.Stop();
        
        if (session == null)
            return NotFound(stopwatch.Elapsed);
        
        return Ok(new {session , stopwatch.Elapsed});
    }
    
    [HttpPost]
    public IActionResult CreateSession([FromForm] UserModel user)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        var session = _sessionService.CreateSession(user);
        
        stopwatch.Stop();
        
        return Ok(new {session , stopwatch.Elapsed});
    }
    
    [HttpPost("{userId}")]
    public IActionResult RefreshSession([FromRoute] string userId)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        _sessionService.RefreshSession(userId);
        
        stopwatch.Stop();
        
        return Ok(stopwatch.Elapsed);
    }
    
    [HttpPut("{userId}")]
    public IActionResult UpdateSession([FromRoute] string userId, [FromBody] SessionUpdateModel sessionUpdateModel)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        var session = _sessionService.UpdateAndRefreshSession(userId, sessionUpdateModel);
        
        stopwatch.Stop();
        
        if (session == null)
            return NotFound(stopwatch.Elapsed);
        
        return Ok(new {session , stopwatch.Elapsed});
    }
    
    [HttpDelete("{userId}")]
    public IActionResult KillSession([FromRoute] string userId)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        _sessionService.KillSession(userId);
        
        stopwatch.Stop();
        
        return Ok(stopwatch.Elapsed);
    }
}