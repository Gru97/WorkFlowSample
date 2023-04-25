using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFE.Console.DefineContractExample.Domain;
using WorkflowCore.Interface;

namespace WFE.Console
{
    public class InquiryWorker : BackgroundService
    {
        private readonly ILogger<InquiryWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public InquiryWorker(ILogger<InquiryWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork(stoppingToken);
                await Task.Delay(100000, stoppingToken);
            }
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var host = scope.ServiceProvider.GetRequiredService<IWorkflowHost>();
            var activity = host.GetPendingActivity("activity-1", "worker1", TimeSpan.FromMinutes(10)).Result;

            if (activity != null)
            {
                //Call psp and inquiry data
                await host.SubmitActivitySuccess(activity.Token, State.Accepted);
            }

        }

    }
}
