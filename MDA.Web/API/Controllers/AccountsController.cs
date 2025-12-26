using MDA.Web.API.DTOs;
using MDA.Web.Application.Accounts.Contracts;
using MDA.Web.Application.Accounts.Interfaces;
using MDA.Web.Application.Accounts.Services;
using MDA.Web.Application.Holdings.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MDA.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AccountsController : ControllerBase
    {
        private readonly IAccountQueries _accountQueryService;
        public AccountsController(IAccountQueries accountQueryService, IHoldingQueries holdingQueryService)
        {
            _accountQueryService = accountQueryService;
        }

        //[HttpGet("active")]
        //public async Task<ActionResult<string>> GetActiveAccountForUser(CancellationToken ct)
        //{
        //    var activeAccount = await _accountQueryService.GetActiveAccountForUserAsync(User, ct);

        //    string result = activeAccount.IbkrAccountId;

        //    return Ok(result);
        //}

        [HttpGet("{accountId:guid}")]
        public async Task<ActionResult<AccountResponse>> GetAccountById(Guid accountId, CancellationToken ct)
        {
            var result = await _accountQueryService.GetAccountAsync(accountId, ct);

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

        [HttpGet("{accountId:guid}/summary")]
        public async Task<ActionResult<AccountSummaryResponse>> GetAccountSummary(Guid accountId,  CancellationToken ct)
        {
            var result = await _accountQueryService.GetAccountSummaryAsync(accountId, ct);

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
