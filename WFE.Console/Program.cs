// See https://aka.ms/new-console-template for more information

using WorkflowCore.Interface;
using WorkflowCore.Models;

using Microsoft.Extensions.DependencyInjection;
using WFE.Console;
using WFE.Console.DefineContractExample.ACL;
using WFE.Console.DefineContractExample.Domain;

// Create the service container
var builder = new ServiceCollection()
    .AddLogging()
    .AddScoped<IIdentityService, IdentityService>()
    .AddScoped<IBankAccountService, BankAccountService>()
    .AddScoped<ITaxService, TaxService>()
    .AddTransient<CheckNameValidationStep>()
    .AddTransient<CheckBankAccountValidationStep>()
    .AddTransient<CheckTaxDataValidationStep>()
    .AddTransient<SendRequestToExternalApiStep>()
    .AddWorkflow()
    .BuildServiceProvider();

var workflowHost = builder.GetRequiredService<IWorkflowHost>();
//workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
//workflowHost.RegisterWorkflow<AddNumberWorkflow,PassingData>();
workflowHost.RegisterWorkflow<DefineContractWorkflow, DefineContractWorkflowData>();
workflowHost.Start();

//await workflowHost.StartWorkflow("HelloWorld", 1, null);
//await workflowHost.StartWorkflow("NumberWorkflow", new PassingData(){Value1= 1, Value2= 2} , null);

try
{
    await workflowHost.StartWorkflow("DefineContractWorkflow", new DefineContractWorkflowData()
    {
        Contract = new Contract(){
            AccountNo = "00256666",
            BirthData = new System.DateTime(1997, 08, 25),
            ContractNo = "14020025366",
            LastName = "Hasani",
            Name = "MashtHasan",
            NationalCode = "12366363",
            TaxCode = "12542",
            State = State.Imported
        }

    }, null);

    Console.ReadLine();
    workflowHost.Stop();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
//workflowHost.PublishEvent("EndProgram", "0", null);



