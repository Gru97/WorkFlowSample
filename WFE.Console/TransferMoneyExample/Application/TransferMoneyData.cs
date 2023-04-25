namespace WFE.Console.TransferMoneyExample.Application;

public class TransferMoneyData
{
    public string FromAccount { get; set; }
    public string ToAccount { get; set; }
    public decimal Amount { get; set; }
    public bool HasEnoughMoney { get; set; }
    public bool WithdrawStatus { get; set; }
    public bool DepositStatus { get; set; }
    public decimal FromAccountInitialBalance { get; set; }
    public decimal ToAccountInitialBalance { get; set; }
}