namespace WFE.Console.DefineContractExample.ACL;

public interface IIdentityService
{
    string GetIdentity(string nationalCode, DateTime birthDate);
}