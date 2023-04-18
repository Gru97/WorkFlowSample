namespace WFE.Console.TransferMoneyExample.Domain;

public interface IAccountRepository
{
    Account Get(string accountNumber);
    void Update(Account account);

}