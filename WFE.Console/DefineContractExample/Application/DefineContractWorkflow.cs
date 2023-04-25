using Microsoft.Extensions.Logging;
using WFE.Console.DefineContractExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.DefineContractExample.Application
{
    public class DefineContractWorkflow : IWorkflow<DefineContractWorkflowData>
    {
        private readonly ILogger<DefineContractWorkflow> _logger;

        public DefineContractWorkflow(ILogger<DefineContractWorkflow> logger)
        {
            _logger = logger;
        }

        public void Build(IWorkflowBuilder<DefineContractWorkflowData> builder)
        {
            _logger.LogInformation("Beginning of Workflow ");
            builder
                .StartWith<CheckNameValidationStep>()
                .Input(step => step.BirthDate, data => data.Contract.BirthData)
                .Input(step => step.Name, data => data.Contract.Name)
                .Input(step => step.NationalCode, data => data.Contract.NationalCode)
                .Output(data => data.IsNameValid, step => step.IsValid)
                .OnError(WorkflowErrorHandling.Suspend)
                .If(data => data.IsNameValid)
                    .Do(then => then
                        .StartWith<CheckBankAccountValidationStep>()
                        .Input(step => step.Name, data => data.Contract.Name)
                        .Input(step => step.AccountNo, data => data.Contract.AccountNo)
                        .Output(data => data.IsAccountNoValid, step => step.IsValid)
                        .OnError(WorkflowErrorHandling.Suspend)
                        .If(data => data.IsAccountNoValid)
                            .Do(then => then
                                .StartWith<CheckTaxDataValidationStep>()
                                .Input(step => step.TaxCode, data => data.Contract.TaxCode)
                                .Output(data => data.IsTaxValid, step => step.IsValid)
                                .If(data => data.IsTaxValid)
                                    .Do(then => then
                                        .StartWith<SendRequestToExternalApiStep>()
                                        .Input(data => data.Contract, step => step.Contract)
                                        .Output(data => data.IsSentToPsp, step => step.SentToPsp)
                                        .Activity("activity-1", (data) => data.Contract.ContractNo)
                                        .Output(data=>data.Contract.State, step=> step.Result)
                                        .Then(context=> { System.Console.WriteLine("sdfds"); })
                                        .EndWorkflow())))



                .Then(context => System.Console.WriteLine("Rejected"));
        }

        public string Id => "DefineContractWorkflow";
        public int Version => 1;
    }
}