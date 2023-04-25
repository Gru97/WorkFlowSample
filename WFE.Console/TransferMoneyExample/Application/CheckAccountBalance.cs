using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.TransferMoneyExample.Application;

public class CheckAccountBalance : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        //call a service or check database
        HasEnoughMoney = true;
        return ExecutionResult.Next();
    }

    public string AccountNo { get; set; }
    public bool HasEnoughMoney { get; set; }
}