namespace test.QueryModels;
[Serializable]

public class UpdateAccountBalanceCommand
{
    public int Id { get; }
    public decimal AccountBalance { get; set; }
    

    public UpdateAccountBalanceCommand(int id, decimal accountBalance)
    {
        Id = id;
        AccountBalance = accountBalance;
    }
}