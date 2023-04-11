// See https://aka.ms/new-console-template for more information

using WorkflowCore.Interface;
using WorkflowCore.Models;

using Microsoft.Extensions.DependencyInjection;
using WFE.Console;

// Create the service container
var builder = new ServiceCollection()
    .AddLogging()

    .AddWorkflow()
    
    .BuildServiceProvider();

var workflowHost = builder.GetRequiredService<IWorkflowHost>();
workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
workflowHost.RegisterWorkflow<AddNumberWorkflow,PassingData>();
workflowHost.Start();

workflowHost.StartWorkflow("HelloWorld", 1, null);
workflowHost.StartWorkflow("NumberWorkflow", new PassingData(){Value1= 1, Value2= 2} , null);

//workflowHost.PublishEvent("EndProgram", "0", null);

Console.ReadLine();
workflowHost.Stop();

