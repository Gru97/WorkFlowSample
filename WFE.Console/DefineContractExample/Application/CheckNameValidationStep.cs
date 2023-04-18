using Microsoft.Extensions.Logging;
using WFE.Console.DefineContractExample.ACL;
using WorkflowCore.Interface;
using WorkflowCore.Models;

public class CheckNameValidationStep : StepBody
{
    public string Name { get; set; }
    public string NationalCode { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsValid { get; set; }
    private readonly IIdentityService _identityValidator;
    private readonly ILogger<CheckNameValidationStep> _logger;

    public CheckNameValidationStep(IIdentityService identityValidator, ILogger<CheckNameValidationStep> logger)
    {
        _identityValidator = identityValidator;
        _logger = logger;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        _logger.LogInformation("Checking name validation...");
        var name = _identityValidator.GetIdentity(NationalCode, BirthDate);
        if (name == Name)
            IsValid = true;
        return ExecutionResult.Next();
    }
}