using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFE.Console
{
    public class AddNumberWorkflow : IWorkflow<PassingData>
    {
        public void Build(IWorkflowBuilder<PassingData> builder)
        {
            builder.StartWith<CalculationStep>()
                .Input(step => step.Input1, data => data.Value1)
                .Input(step => step.Input2, data => data.Value2)
                .Output(data => data.Answer, step => step.Output)
                .Then<CustomMessage>()
                .Input(step => step.Message, data => "The answer is " + data.Answer.ToString());
        }

        public string Id  => "NumberWorkflow";

        public int Version => 1;

    }

    public class CustomMessage : StepBody
    {
        public string Message { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            System.Console.WriteLine(Message);
            return ExecutionResult.Next();
        }
    }

    public class CalculationStep : StepBody
    {
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Output { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Output = Input1 + Input2;
            return ExecutionResult.Next();
        }
    }

    public class PassingData
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Answer { get; set; }
    }
}
