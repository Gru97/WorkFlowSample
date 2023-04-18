using WFE.Console.DefineContractExample.Domain;

namespace WFE.Console.DefineContractExample.Application;

public class DefineContractWorkflowData
{
    public Contract Contract { get; set; }
    public bool IsNameValid { get; set; }
    public bool IsAccountNoValid { get; set; }
    public bool IsTaxValid { get; set; }
    public bool IsSentToPsp { get; set; }

    public DefineContractWorkflowData()
    {
        Contract = new Contract();
    }
}