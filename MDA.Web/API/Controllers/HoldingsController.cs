using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Application.Holdings.Contracts;
using MDA.Web.Application.Holdings.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MDA.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class HoldingsController : ControllerBase
    {
        private readonly IHoldingQueries _holdingQueryService;

        public HoldingsController(IHoldingQueries holdingQueryService)
        {
            _holdingQueryService = holdingQueryService;
        }

        [HttpGet("{accountId:guid}")]
        public async Task<ActionResult<HoldingsResponse>> GetAllHoldingDetailsUnderAccount(Guid accountId, CancellationToken ct)
        {

            var result = await _holdingQueryService.GetAllHoldingDetailsUnderAccount(accountId, ct);

            if (result != null)
            {
                return Ok(result);
            }
            // TODO: Middleware for this
            else
            {
                return NotFound();
            }
        }
    }
}
