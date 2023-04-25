using Microsoft.Extensions.Logging;
using WFE.Console.TransferMoneyExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.TransferMoneyExample.Application;

public class DepositMoney : StepBody
{
    private readonly ILogger<DepositMoney> _logger;
    private readonly IAccountRepository _accountRepository;
    public DepositMoney(ILogger<DepositMoney> logger, IAccountRepository accountRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        _logger.LogInformation("Deposit Money Step......");
        var account = _accountRepository.Get(Account);
        account.Deposit(Amount);
        _accountRepository.Update(account);
        throw new Exception();
        return ExecutionResult.Next();
    }

    public decimal Amount { get; set; }
    public string Account { get; set; }
}