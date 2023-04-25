using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console.TransferMoneyExample.Application
{
    public class TransferMoneyWorkflow : IWorkflow<TransferMoneyData>
    {
        private readonly ILogger<TransferMoneyWorkflow> _logger;

        public TransferMoneyWorkflow(ILogger<TransferMoneyWorkflow> logger)
        {
            _logger = logger;
        }

        public string Id => nameof(TransferMoneyWorkflow);

        public int Version => 1;

        public void Build(IWorkflowBuilder<TransferMoneyData> builder)
        
        {
            builder.StartWith<CheckAccountBalance>()
                .Input(step => step.AccountNo, data => data.FromAccount)
                .Output(data => data.HasEnoughMoney, step => step.HasEnoughMoney)
                .OnError(WorkflowErrorHandling.Suspend)
                .If(data => data.HasEnoughMoney)
                .Do(then => then
                    .Saga(saga => saga
                        .StartWith<WithdrawMoney>()
                        .Input(step => step.Amount, data => data.Amount)
                        .Input(step => step.Account, data => data.FromAccount)
                        .Then<DepositMoney>()
                        .Input(step => step.Amount, data => data.Amount)
                        .Input(step => step.Account, data => data.ToAccount))
                    .CompensateWith<UndoTransferMoney>());

        }
    }
}
