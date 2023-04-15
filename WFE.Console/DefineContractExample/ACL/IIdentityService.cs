namespace WFE.Console.DefineContractExample.ACL;

public interface IIdentityService
{
    string GetIdentity(string nationalCode, DateTime birthDate);
}

public class IdentityService : IIdentityService
{
    public string GetIdentity(string nationalCode, DateTime birthDate)
    {
        return "MashtHasan";
    }
}