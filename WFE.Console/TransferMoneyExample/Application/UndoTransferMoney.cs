using WFE.Console.TransferMoneyExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.TransferMoneyExample.Application;

public class UndoTransferMoney: StepBody
{
    private readonly IAccountRepository _repository;

    public UndoTransferMoney(IAccountRepository account)
    {
        _repository = account;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        //TODO: implement a way to handle concurrency issues
        var senderAccountNumber = ((TransferMoneyData)context.Workflow.Data).FromAccount;
        var senderAccountBalance = ((TransferMoneyData)context.Workflow.Data).FromAccountInitialBalance;
        var receiverAccountNumber = ((TransferMoneyData)context.Workflow.Data).ToAccount;
        var receiverAccountBalance = ((TransferMoneyData)context.Workflow.Data).ToAccountInitialBalance;
        var fromAccount = _repository.Get(senderAccountNumber);
        var toAccount = _repository.Get(receiverAccountNumber);
        fromAccount.SetBalance(senderAccountBalance); 
        toAccount.SetBalance(receiverAccountBalance); 
        _repository.Update(fromAccount);
        _repository.Update(toAccount);
        return ExecutionResult.Next();
    }
}