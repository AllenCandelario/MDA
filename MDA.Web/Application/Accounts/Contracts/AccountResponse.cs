using MDA.Web.Application.Shared;
using MDA.Web.Domain.Accounts;
using System.Text;

namespace MDA.Web.Application.Accounts.Contracts
{
    public sealed record AccountResponse(Guid Id, Guid UserId, string IbkrAccountId, string BaseCurrency, string AccountType)
    {
        public static AccountResponse From(Account account)
        {
            return new AccountResponse(
                account.Id,
                account.UserId,
                account.IbkrAccountId,
                account.BaseCurrency,
                account.AccountType
            );
        }
    }
}
