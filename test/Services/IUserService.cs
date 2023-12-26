using test.DbModels;
using test.QueryModels;

namespace test.Services;

public interface IUserService
{
    public List<User> GetAll();

    public Task AddUserAsync(CreateUser request);

    public Task<User> GetUserById(int id);
    public List<User> GetUserByFirstName(string firstName);
    public Task<User> GetUserByPersonalId(string personalId);
    public Task<User> GetUserByMobileNumber(string mobileNumber);
    public List<User> GetUserByLastName(string lastName);
    public List<User> GetUserByAge(int age);
    

   public Task<User> UpdateUser(UpdateUserCommand request);
   public Task<User> DeleteUser(int id);
}