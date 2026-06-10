using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Models;
using evoNaplo.Services;

namespace evoNaplo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditController : ControllerBase
{
    private readonly IAuditService _auditService;

    public AuditController(IAuditService auditService)
    {
        _auditService = auditService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AuditLog>>> Get([FromQuery] AuditQueryParams query)
    {
        var result = await _auditService.QueryAsync(query);
        return Ok(result);
    }
}
