using Microsoft.AspNetCore.Mvc;
using test.DbModels;
using test.QueryModels;
using test.Services;

namespace test.Controllers;

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }



    [HttpPost("Add-Account")]
    public async Task AddAccount([FromBody] CreateAccount account)
    {
        await _accountService.AddAccountAsync(account);
    }
    
    [HttpGet("Get-Accounts")]
    public List<Account> GetAccounts()
    {
       return _accountService.GetAll().ToList();
    }
    [HttpGet("Get-Account-By-Id")]
    public List<Account> GetAccountById(int id)
    {
        return _accountService.GetAccountById(id);
    }
    [HttpGet("Get-Account-By-UserId")]
    public List<Account> GetAccountByUserId(int userId)
    {
        return _accountService.GetAccountByUserId(userId);
    }
    [HttpGet("Get-Account-By-AccountCurrency")]
    public List<Account> GetAccountByAccountCurrency(string accountCurrency)
    {
        return _accountService.GetAccountByAccountCurrency(accountCurrency);
    }
    [HttpGet("Get-Account-By-AccountBalance")]
    public List<Account> GetAccountByAccountBalance(decimal bottomValue, decimal topValue, string accountCurrency)
    {
        return _accountService.GetAccountByAccountBalance(bottomValue, topValue, accountCurrency);
    }
    [HttpDelete("Delete-Account")]
    public async Task DeleteAccount(int id)
    {
        await _accountService.DeleteAccount(id);
    }
    [HttpPut("Update-Account")]
    public async Task UpdateAccount([FromBody]UpdateAccountCommand request)
    {
        await _accountService.UpdateAccount(request);
    }
    [HttpPut("Update-Account-Balance")]
    public async Task UpdateAccountBalance([FromBody]UpdateAccountBalanceCommand request)
    {
        await _accountService.UpdateAccountBalance(request);
    }
}