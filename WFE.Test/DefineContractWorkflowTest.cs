using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WFE.Console;
using WFE.Console.DefineContractExample.ACL;
using WFE.Console.DefineContractExample.Domain;
using WorkflowCore.Interface;
using Xunit;

namespace WFE.Test
{
    public class DefineContractWorkflowTest
    {
        private IWorkflowHost host;
        public DefineContractWorkflowTest()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddWorkflow();
            serviceCollection.AddTransient<CheckNameValidationStep>()
                .AddTransient<CheckBankAccountValidationStep>()
                .AddTransient<CheckTaxDataValidationStep>()
                .AddTransient<SendRequestToExternalApiStep>()
                .AddScoped<IIdentityService, IdentityService>()
                .AddScoped<IBankAccountService, BankAccountService>()
                .AddScoped<ITaxService, TaxService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            host = serviceProvider.GetRequiredService<IWorkflowHost>();


            host.RegisterWorkflow<DefineContractWorkflow, DefineContractWorkflowData>();
            host.Start();

        }
        [Fact]
        public async Task Reject_Contract_When_Identity_Is_Not_Valid()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            identityServiceMock.Setup(x => x.GetIdentity("12366363", new System.DateTime(1997, 08, 25))).Returns("MashtHasan");
            var bankAccountServiceMock = new Mock<IBankAccountService>();
            bankAccountServiceMock.Setup(x => x.Get("12366363")).Returns("MashtHasan");
            var taxServiceMock = new Mock<ITaxService>();
            taxServiceMock.Setup(x => x.Get("12366363")).Returns("12366363");

            var data = new DefineContractWorkflowData()
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

            };

            await host.StartWorkflow("DefineContractWorkflow", data, null);

        }
    }
}