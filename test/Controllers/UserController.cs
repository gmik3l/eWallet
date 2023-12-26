using Microsoft.AspNetCore.Mvc;
using test.DbModels;
using test.QueryModels;
using test.Services;

namespace test.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("Add-User")]
    public async Task AddUser([FromBody] CreateUser request)
    {
        await _userService.AddUserAsync(request);
    }
    
    [HttpGet("Get-Users")]
    public List<User> GetUsers()
    {
       return _userService.GetAll();
    }
    [HttpGet("Get-User-By-Id")]
    public async Task<User> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }
    [HttpGet("Get-User-By-FirstName")]
    public List<User> GetUserByFirstName(string firstName)
    {
        return _userService.GetUserByFirstName(firstName);
    }
    [HttpGet("Get-User-By-LastName")]
    public List<User> GetUserByLastName(string lastName)
    {
        return _userService.GetUserByLastName(lastName);
    }
    [HttpGet("Get-User-By-Age")]
    public List<User> GetUserByAge(int age)
    {
        return _userService.GetUserByAge(age);
    }
    [HttpGet("Get-User-By-PersonalId")]
    public async Task<User> GetUserByPersonalId(string personalId)
    {
        return await _userService.GetUserByPersonalId(personalId);
    }
    [HttpGet("Get-User-By-MobileNumber")]
    public async Task<User> GetUserByMobileNumber(string mobileNumber)
    {
        return await _userService.GetUserByMobileNumber(mobileNumber);
    }
    
    [HttpPut("Update-User")]
    public async Task<User> UpdateUser([FromBody] UpdateUserCommand user)
    {
        return await _userService.UpdateUser(user);
    }
    
    [HttpDelete("Delete-User")]
    public async Task<User> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }
}