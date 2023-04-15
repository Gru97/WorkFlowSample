namespace WFE.Console.DefineContractExample.ACL;

public interface IBankAccountService
{
    public string Get(string accountNo);
}

public class BankAccountService : IBankAccountService
{
    public string Get(string accountNo)
    {
        return "";
    }
}