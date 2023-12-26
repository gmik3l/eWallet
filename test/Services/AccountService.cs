using Microsoft.EntityFrameworkCore;
using test.DbModels;
using test.QueryModels;


namespace test.Services;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAccountAsync(CreateAccount request)
    {
        //check if user with this id and currency already has an account
        if(_context.Accounts.Any(o=>o.UserId==request.UserId && o.AccountCurrency.ToLower()==request.AccountCurrency.ToLower()))
        {
            throw new Exception(message: "This user already has an account with this currency");
        }
        //check if account with this account number already exists
        if(_context.Accounts.FirstOrDefault(o=>o.AccountNumber==request.AccountNumber)!=null)
        {
            throw new Exception(message: "Account with this account number already exists");
        }
        //check if currency is valid
        if(request.AccountCurrency.ToLower()!="eur" && request.AccountCurrency.ToLower()!="usd" && request.AccountCurrency.ToLower()!="gel")
        {
            throw new Exception(message: "Invalid currency");
        }
        
        var accountToCreate = new Account(accountNumber: request.AccountNumber, accountCurrency: request.AccountCurrency, userId: request.UserId);
        
        var accountNumber = new Random();
        accountToCreate.AccountNumber = accountNumber.Next(100000000, 999999999).ToString();
        accountToCreate.AccountBalance = 0;
        
        var user= await _context.Users
            .Include(o=>o.Accounts)
            .FirstOrDefaultAsync(o => o.Id == request.UserId);
        if (user is null)
        {
            throw new Exception("User does not exist");
        }
        user.Accounts.Add(accountToCreate);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    public List<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public List<Account> GetAccountById(int id)
    {
        //check is account with this id exists
        if (_context.Accounts.FirstOrDefault(o=>o.Id==id)==null)
        {
            throw new Exception(message: "Account does not exist");
        }
        return _context.Accounts.Where(o => o.Id == id).ToList();
    }

    public List<Account> GetAccountByUserId(int userId)
    {
        //check is account with this user id exists
        if (_context.Accounts.FirstOrDefault(o => o.UserId == userId)==null)
        {
            throw new Exception(message: "Account does not exist");
        }
        return _context.Accounts.Where(o => o.UserId == userId).ToList();
    }

    public List<Account> GetAccountByAccountCurrency(string accountCurrency)
    {
        //check is account with this currency exists
        if (_context.Accounts.FirstOrDefault(o => o.AccountCurrency.ToLower() == accountCurrency.ToLower())==null)
        {
            throw new Exception(message: "Invalid currency");
        }
        return _context.Accounts.Where(o => o.AccountCurrency == accountCurrency).ToList();
    }
    
    public List<Account> GetAccountByAccountBalance(decimal bottomValue, decimal topValue, string accountCurrency)
    { 
        //check if account with this currency exists
        if (_context.Accounts.FirstOrDefault(o => o.AccountCurrency.ToLower() == "gel" || o.AccountCurrency.ToLower() == "usd" && o.AccountCurrency.ToLower() == "eur")==null)
        {
            throw new Exception(message: "Invalid currency");
        }
        
        //check if range is valid
        if(bottomValue > topValue)
        {
             throw new Exception(message: "Invalid range");
        }
        return _context.Accounts.Where(o => o.AccountBalance >= bottomValue && o.AccountBalance <= topValue && o.AccountCurrency==accountCurrency).ToList();
    }
    
    public async Task DeleteAccount(int id)
    {
        //check if account with this id exists
        if (_context.Accounts.FirstOrDefault(o => o.Id == id)==null)
        {
            throw new Exception(message: "Account does not exist");
        }
        var account = _context.Accounts.FirstOrDefault(o => o.Id == id);
        if (account != null) _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Account> UpdateAccount(UpdateAccountCommand request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(o => o.Id == request.UserId);
        var accountToUpdate = _context.Accounts.FirstOrDefault(o => o.Id == request.Id);
        //check if account with this id exists
        if (accountToUpdate is null)
        {
            throw new Exception(message: "Account does not exist");
        }
        //check if currency is valid
        if(request.AccountCurrency.ToLower()!="eur" && request.AccountCurrency.ToLower()!="usd" && request.AccountCurrency.ToLower()!="gel")
        {
            throw new Exception(message: "Invalid currency");
        }
        //check if user with this id and currency already has an account
        if(_context.Accounts.Any(o=>o.UserId==request.UserId && o.AccountCurrency.ToLower() == request.AccountCurrency.ToLower()))
        {
            throw new Exception(message: "This user already has an account with this currency");
        }
        //check if account with this account number already exists
        if(_context.Accounts.FirstOrDefault(o=>o.AccountNumber==request.AccountNumber)!=null && accountToUpdate.Id!=request.Id)
        {
            throw new Exception(message: "Account with this account number already exists");
        }
        
        accountToUpdate.AccountCurrency= request.AccountCurrency;
        
        accountToUpdate.AccountNumber = request.AccountNumber;
        
        accountToUpdate.UserId = request.UserId;
        
        _context.Accounts.Update(accountToUpdate);
        await _context.SaveChangesAsync();
        
        return accountToUpdate;
    }

    public async Task<Account> UpdateAccountBalance(UpdateAccountBalanceCommand request)
    {
        var accountToUpdate = _context.Accounts.FirstOrDefault(o => o.Id == request.Id);
        //check if account with this id exists
        if (accountToUpdate is null)
        {
            throw new Exception(message: "Account does not exist");
        }
        accountToUpdate.AccountBalance = request.AccountBalance;
        _context.Accounts.Update(accountToUpdate);
        await _context.SaveChangesAsync();

        return accountToUpdate;
    }
}

