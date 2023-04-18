// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WFE.Console.DefineContractExample.ACL;
using WFE.Console.DefineContractExample.Application;
using WFE.Console.DefineContractExample.Domain;
using WFE.Console.TransferMoneyExample.Application;
using WFE.Console.TransferMoneyExample.Domain;
using WFE.Console.TransferMoneyExample.Infra;
using WorkflowCore.Interface;
using WorkflowCore.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("appsettings.json");
        config.AddEnvironmentVariables();
        config.Build();
    })
    .ConfigureServices(async (context, services) =>
    {
        var sp = services.AddLogging(configure => configure.AddConsole())
            //.AddSingleton<Application, Application>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddScoped<IBankAccountService, BankAccountService>()
            .AddScoped<ITaxService, TaxService>()
            .AddTransient<CheckNameValidationStep>()
            .AddTransient<CheckBankAccountValidationStep>()
            .AddTransient<CheckTaxDataValidationStep>()
            .AddTransient<SendRequestToExternalApiStep>()
            .AddTransient<DepositMoney>()
            .AddTransient<WithdrawMoney>()
            .AddTransient<CheckAccountBalance>()
            .AddTransient<IAccountRepository, AccountRepository>()
            .AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(context.Configuration.GetConnectionString("Database"));
            })
            .AddWorkflow(x => x.UseSqlServer(context.Configuration.GetConnectionString("Workflow"), true, true))
            .BuildServiceProvider();

    }).Build();

var workflowHost = host.Services.GetRequiredService<IWorkflowHost>();
//workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
//workflowHost.RegisterWorkflow<AddNumberWorkflow,PassingData>();
workflowHost.RegisterWorkflow<DefineContractWorkflow, DefineContractWorkflowData>();
workflowHost.RegisterWorkflow<TransferMoneyWorkflow, TransferMoneyData>();
workflowHost.Start();
await workflowHost.StartWorkflow("TransferMoneyWorkflow", new TransferMoneyData(){FromAccount = "5245688" , ToAccount = "1258823" , Amount = 100},null);
//await host.Services.GetRequiredService<Application>().Starter();
Console.ReadKey();


//public class Application
//{
//    private readonly IWorkflowHost workflowHost;

//    public Application(IWorkflowHost host)
//    {
//        workflowHost = host;
//    }

//    public async Task Starter()
//    {

//        await workflowHost.StartWorkflow("TransferMoneyWorkflow", new DefineContractWorkflowData()
//        {

//        });

//        //try
//        //{
//        //    await workflowHost.StartWorkflow("DefineContractWorkflow", new DefineContractWorkflowData()
//        //    {
//        //        Contract = new Contract()
//        //        {
//        //            AccountNo = "00256666",
//        //            BirthData = new System.DateTime(1997, 08, 25),
//        //            ContractNo = "14020025366",
//        //            LastName = "Hasani",
//        //            Name = "MashtHasan",
//        //            NationalCode = "12366363",
//        //            TaxCode = "12542",
//        //            State = State.Imported
//        //        }
//        //    }, null);


//        //    var workflowId = "405c4bf7-1b29-43ac-8b0e-0fa8ccca3b3e";
//        //    await workflowHost.ResumeWorkflow(workflowId);

//        //    Console.ReadKey();
//        //    workflowHost.Stop();
//        //}
//        //catch (Exception e)
//        //{
//        //    Console.WriteLine(e);
//        //    throw;
//        //}
//    }
//    // Create the service container

//    //await workflowHost.StartWorkflow("HelloWorld", 1, null);
//    //await workflowHost.StartWorkflow("NumberWorkflow", new PassingData(){Value1= 1, Value2= 2} , null);
//    //workflowHost.PublishEvent("EndProgram", "0", null);

//}





