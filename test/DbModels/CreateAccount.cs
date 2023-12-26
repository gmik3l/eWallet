using System.Text.Json.Serialization;

namespace test.DbModels;

public class CreateAccount
{
    public int Id { get; }
    public string AccountNumber { get; set; }
    public string AccountCurrency { get; set; }
    public decimal AccountBalance { get; set; }
    public int UserId { get; set; }
    
    
    public CreateAccount(string accountNumber, string accountCurrency, int userId)
    {
        AccountNumber = accountNumber;
        AccountCurrency = accountCurrency;
        AccountBalance = 0;
        UserId = userId;
    }
}