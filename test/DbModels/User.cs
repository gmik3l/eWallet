using System.Text.Json.Serialization;

namespace test.DbModels;

[Serializable]
public class User
{
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string PersonalId { get; set; }
    public string MobileNumber { get; set; }
    public List<Account>? Accounts { get; set; }
    

    public User(
        string firstName, 
        string lastName, 
        int age, 
        string personalId, 
        string mobileNumber
        )
        
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        PersonalId = personalId;
        MobileNumber = mobileNumber;
    }
}