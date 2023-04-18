using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFE.Console.TransferMoneyExample.Domain;
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
                        .Input(step => step.Account, data => data.ToAccount)));

        }
    }

    public class DepositMoney : StepBody
    {
        private readonly ILogger<DepositMoney> _logger;
        private readonly IAccountRepository _accountRepository;
        public DepositMoney(ILogger<DepositMoney> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _logger.LogInformation("Deposit Money Step......");
            var account = _accountRepository.Get(Account);
            account.Deposit(Amount);
            return ExecutionResult.Next();
        }

        public decimal Amount { get; set; }
        public string Account { get; set; }
    }

    public class WithdrawMoney : StepBody
    {
        private readonly ILogger<WithdrawMoney> _logger;
        private readonly IAccountRepository _accountRepository;
        public WithdrawMoney(ILogger<WithdrawMoney> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _logger.LogInformation("Withdraw Money Step......");
            var account = _accountRepository.Get(Account);
            account.Withdraw(Amount);
            return ExecutionResult.Next();
        }

        public decimal Amount { get; set; }
        public string Account { get; set; }
    }

    public class CheckAccountBalance : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            //call a service or check database
            HasEnoughMoney = true;
            return ExecutionResult.Next();
        }

        public string AccountNo { get; set; }
        public bool HasEnoughMoney { get; set; }
    }

    public class TransferMoneyData
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public bool HasEnoughMoney { get; set; }
    }
}
