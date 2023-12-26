using test.DbModels;
using test.QueryModels;

namespace test.Services;

public interface IAccountService
{
    public Task AddAccountAsync(CreateAccount request);
    public List<Account> GetAll();
    public List<Account> GetAccountById(int id);
    public List<Account> GetAccountByUserId(int userId);
    public List<Account> GetAccountByAccountCurrency(string accountCurrency);
    public List<Account> GetAccountByAccountBalance(decimal bottomValue, decimal topValue, string accountCurrency);
    public Task DeleteAccount(int id);
    public Task<Account> UpdateAccount(UpdateAccountCommand request);
    public Task<Account> UpdateAccountBalance(UpdateAccountBalanceCommand request);
}
