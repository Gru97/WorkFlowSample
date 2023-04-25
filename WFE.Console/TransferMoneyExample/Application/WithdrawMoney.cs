using Microsoft.Extensions.Logging;
using WFE.Console.TransferMoneyExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.TransferMoneyExample.Application;

public class WithdrawMoney : StepBody
{
    private readonly ILogger<WithdrawMoney> _logger;
    private readonly IAccountRepository _accountRepository;
    public decimal Amount { get; set; }
    public string Account { get; set; }
    public WithdrawMoney(ILogger<WithdrawMoney> logger, IAccountRepository accountRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        _logger.LogInformation("Withdraw Money Step......");
        var account = _accountRepository.Get(Account);
        account.Withdraw(Amount);
        _accountRepository.Update(account);
        return ExecutionResult.Next();
    }
}