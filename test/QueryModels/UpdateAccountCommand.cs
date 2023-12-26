namespace test.QueryModels;
[Serializable]

public class UpdateAccountCommand
{
    public int Id { get; }
    public string AccountNumber { get; set; }
    public string AccountCurrency { get; set; }
    public int UserId { get; }
    

    public UpdateAccountCommand(int id, string accountNumber, string accountCurrency, int userId)
    {
        Id = id;
        AccountNumber = accountNumber;
        AccountCurrency = accountCurrency;
        UserId = userId;
    }
}
