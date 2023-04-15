﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFE.Console;
using WFE.Console.DefineContractExample.ACL;
using WFE.Console.DefineContractExample.Domain;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console
{
    public class DefineContractWorkflow:IWorkflow<DefineContractWorkflowData>
    {
        public void Build(IWorkflowBuilder<DefineContractWorkflowData> builder)
        {
            builder
                .StartWith<CheckNameValidationStep>()
                .Input(step=>step.BirthDate, data=>data.Contract.BirthData)
                .Input(step=>step.Name, data=>data.Contract.Name)
                .Input(step=>step.NationalCode, data=>data.Contract.NationalCode)
                .Output(data=>data.IsNameValid, step=>step.IsValid)
                .If(data=>data.IsNameValid)
                    .Do(then=>then
                        .StartWith<CheckBankAccountValidationStep>()
                        .Input(step => step.Name, data => data.Contract.Name)
                        .Input(step => step.AccountNo, data => data.Contract.AccountNo)
                        .Output(data => data.IsAccountNoValid, step => step.IsValid)
                        .If(data=>data.IsAccountNoValid)
                            .Do(then=>then
                                .StartWith<CheckTaxDataValidationStep>()
                                .Input(step => step.TaxCode, data => data.Contract.TaxCode)
                                .Output(data => data.IsTaxValid, step => step.IsValid)
                                .If(data => data.IsTaxValid)
                                    .Do(then => then
                                        .StartWith<SendRequestToExternalApiStep>()
                                        .Input(data=>data.Contract, step=>step.Contract)
                                        .Output(data=>data.IsSentToPsp, step=>step.SentToPsp)
                                        .EndWorkflow())))
               
                

                .Then(context => System.Console.WriteLine("Rejected"));
        }

        public string Id => "DefineContractWorkflow";
        public int Version => 1;
    }

    public class DefineContractWorkflowData
    {
        public Contract Contract{ get; set; }
        public bool IsNameValid { get; set; }
        public bool IsAccountNoValid { get; set; }
        public bool IsTaxValid { get; set; }
        public bool IsSentToPsp { get; set; }

        public DefineContractWorkflowData()
        {
            Contract=new Contract();
        }
    }

   
}

public class CheckNameValidationStep : StepBody
{
    public string Name { get; set; }
    public string NationalCode { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsValid { get; set; }
    private readonly IIdentityService _identityValidator;

    public CheckNameValidationStep(IIdentityService identityValidator)
    {
        _identityValidator = identityValidator;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Checking name validation...");
        var name= _identityValidator.GetIdentity(NationalCode, BirthDate);
        if (name == Name)
            IsValid = true;
        return ExecutionResult.Next();
    }
}

public class CheckBankAccountValidationStep : StepBody
{
    public string Name { get; set; }
    public string AccountNo { get; set; }
    public bool IsValid { get; set; }
    private readonly IBankAccountService _bankAccountService;

    public CheckBankAccountValidationStep(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Checking bank account validation...");
        var name = _bankAccountService.Get(AccountNo);
        if (name == Name)
            IsValid = true;
        return ExecutionResult.Next();
    }
}

public class CheckTaxDataValidationStep : StepBody
{
    public string TaxCode { get; set; }
    public string NationalCode { get; set; }
    public bool IsValid { get; set; }
    private readonly ITaxService _taxService;

    public CheckTaxDataValidationStep(ITaxService taxService)
    {
        _taxService = taxService;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Checking tax data validation...");
        var taxCode= _taxService.Get(NationalCode);
        if (taxCode == TaxCode)
            IsValid = true;
        return ExecutionResult.Next();
    }
}



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

//public class RejectRequest:StepBody
//{
//    public string Message { get; set; }
//    public State State{ get; set; }
//    public override ExecutionResult Run(IStepExecutionContext context)
//    {
//        State = State.Reject;
        
//    }
//}



