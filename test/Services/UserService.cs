using Microsoft.EntityFrameworkCore;
using test.DbModels;
using test.QueryModels;

namespace test.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<User> GetAll()
    {
        return _context.Users
            .Include(o=>o.Accounts)
            .ToList();
    }

    public async Task AddUserAsync(CreateUser request)
    {
        if (_context.Users.Any(o => o.PersonalId == request.PersonalId))
        {
            throw new Exception( ("User already exists"));
        }

        if (_context.Users.Any(o => o.MobileNumber == request.MobileNumber))
        {
            throw new Exception("Mobile number in use");
        }

        if (request.Age < 21)
        {
            throw new Exception("User must be at least 18 years old");
        }

        var userToCreate = new User(firstName: request.FirstName, lastName: request.LastName,
            age: request.Age, personalId: request.PersonalId, mobileNumber: request.MobileNumber);

        await _context.Users.AddAsync(userToCreate);

        await _context.SaveChangesAsync();
    }

    //get user by id
    public async Task<User> GetUserById(int id)
    {
        if (_context.Users.Any(o => o.Id != id))
        {
            throw new Exception("User does not exist");
        }
        
        return await _context.Users
            .Include(o => o.Accounts)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    //get user by first name
    public List<User> GetUserByFirstName(string firstName)
    {
        if (_context.Users.Any(o => !o.FirstName.ToLower().Contains(firstName.ToLower())))
        {
            throw new Exception("User does not exist");
        }

        return _context.Users
            .Where(o => o.FirstName
                .ToLower()
                .Contains(firstName
                    .ToLower()))
            .Include(o=>o.Accounts)
            .ToList();
    }

    //get user by personal id
    public async Task<User> GetUserByPersonalId(string personalId)
    {
        if (_context.Users.Any(o => o.PersonalId != personalId))
        {
            throw new Exception("User does not exist");
        }

        return await _context.Users
            .Include(o=>o.Accounts)
            .FirstOrDefaultAsync(o => o.PersonalId == personalId);
    }

    //get user by mobile number
    public async Task<User> GetUserByMobileNumber(string mobileNumber)
    {
        if (_context.Users.Any(o => o.MobileNumber != mobileNumber))
        {
            throw new Exception("User does not exist");
        }

        return await _context.Users
            .Include(o => o.Accounts)
            .FirstOrDefaultAsync(o => o.MobileNumber == mobileNumber);
    }

    //get user by last name
    public List<User> GetUserByLastName(string lastName)
    {
        if (!_context.Users.Any(o => o.LastName.ToLower().Contains(lastName.ToLower())))
        {
            throw new Exception("User does not exist");
        }

        return _context.Users
            .Where(o => o.LastName
                .ToLower()
                .Contains(lastName
                    .ToLower()))
            .Include(o=>o.Accounts)
            .ToList();
    }

    //get user by age
    public List<User> GetUserByAge(int age)
    {
        if (_context.Users.Any(o => o.Age != age))
        {
            throw new Exception("User does not exist");
        }

        return _context.Users
            .Where(o => o.Age == age)
            .Include(o=>o.Accounts)
            .ToList();
    }

    //update user
    public async Task<User> UpdateUser(UpdateUserCommand request)
    {
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(o => o.Id == request.Id);
        if (userToUpdate is null)
        {
            throw new Exception("User with this id does not exist");
        }

        userToUpdate.FirstName = request.FirstName;
        userToUpdate.LastName = request.LastName;
        userToUpdate.Age = request.Age;
        
        if (_context.Users.Any(o => o.PersonalId == request.PersonalId && o.Id != request.Id))
        {
            throw new Exception("Personal id already in use");
        }
        userToUpdate.PersonalId = request.PersonalId;
        
        if (_context.Users.Any(o => o.MobileNumber == request.MobileNumber && o.Id != request.Id))
        {
            throw new Exception("Mobile number already in use");
        }
        userToUpdate.MobileNumber = request.MobileNumber;
        
        _context.Users.Update(userToUpdate);
        await _context.SaveChangesAsync();

        return userToUpdate;
    }

    //delete user
    public async Task<User> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(o => o.Id == id);
        if (user == null)
        {
            throw new Exception("User does not exist");
        }
        var accountToDelete = await _context.Accounts.FirstOrDefaultAsync(o => o.UserId == id);
        _context.Users.Remove(user);
        _context.Accounts.Remove(accountToDelete);
        await _context.SaveChangesAsync();
        return user;
    }
}