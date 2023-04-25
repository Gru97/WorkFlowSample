using WFE.Console.TransferMoneyExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.TransferMoneyExample.Application;

public class UndoWithdrawMoney: StepBody
{
    private readonly IAccountRepository _repository;

    public UndoWithdrawMoney(IAccountRepository account)
    {
        _repository = account;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        throw new NotImplementedException();
    }
}