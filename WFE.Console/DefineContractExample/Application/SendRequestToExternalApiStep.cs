using WFE.Console.DefineContractExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

public class SendRequestToExternalApiStep : StepBody
{
    public Contract Contract { get; set; }
    public bool SentToPsp { get; set; }
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Sending request to external API...");
        // Send request to external API here
        return ExecutionResult.Next();
    }
}