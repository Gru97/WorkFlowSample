using Microsoft.Extensions.Logging;
using WFE.Console.DefineContractExample.ACL;
using WorkflowCore.Interface;
using WorkflowCore.Models;

public class CheckTaxDataValidationStep : StepBody
{
    public string TaxCode { get; set; }
    public string NationalCode { get; set; }
    public bool IsValid { get; set; }
    private readonly ITaxService _taxService;
    private readonly ILogger<CheckTaxDataValidationStep> _logger;
    public CheckTaxDataValidationStep(ITaxService taxService, ILogger<CheckTaxDataValidationStep> logger)
    {
        _taxService = taxService;
        _logger = logger;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        _logger.LogInformation("Checking tax data validation...");
        var taxCode = _taxService.Get(NationalCode);
        if (taxCode == TaxCode)
            IsValid = true;
        return ExecutionResult.Next();
    }
}