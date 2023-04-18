using Microsoft.Extensions.Logging;
using WFE.Console.DefineContractExample.ACL;
using WorkflowCore.Interface;
using WorkflowCore.Models;

public class CheckBankAccountValidationStep : StepBody
{
    public string Name { get; set; }
    public string AccountNo { get; set; }
    public bool IsValid { get; set; }
    private readonly IBankAccountService _bankAccountService;
    private readonly ILogger<CheckBankAccountValidationStep> _logger;
    public CheckBankAccountValidationStep(IBankAccountService bankAccountService, ILogger<CheckBankAccountValidationStep> logger)
    {
        _bankAccountService = bankAccountService;
        _logger = logger;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        //throw new Exception("Bug Occurred");
        _logger.LogInformation("Checking bank account validation...");
        var name = _bankAccountService.Get(AccountNo);
        if (name == Name)
            IsValid = true;
        return ExecutionResult.Next();
    }
}