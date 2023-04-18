using Microsoft.EntityFrameworkCore;
using WFE.Console.TransferMoneyExample.Domain;

namespace WFE.Console.TransferMoneyExample.Infra;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationContext _context;

    public AccountRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Account Get(string accountNumber)
    {
        var account = _context.Accounts.SingleOrDefault(e => e.AccountNumber == accountNumber);
        return account;
    }

    public void Update(Account account)
    {
        throw new NotImplementedException();
    }
}