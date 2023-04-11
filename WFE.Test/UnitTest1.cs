using Microsoft.Extensions.DependencyInjection;
using WFE.Console;
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
            var serviceProvider = serviceCollection.BuildServiceProvider();
            host = serviceProvider.GetRequiredService<IWorkflowHost>();


            host.RegisterWorkflow<DefineContractWorkflow, DefineContractWorkflowData>();
            host.Start();

        }
        [Fact]
        public void Reject_Contract_When_Identity_Is_Not_Valid()
        {

            var data = new DefineContractWorkflowData()
            {

            };
            host.StartWorkflow("DefineContractWorkflow", data, null);
            




        }
    }
}