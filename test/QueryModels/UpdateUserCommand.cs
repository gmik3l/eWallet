namespace test.QueryModels;
[Serializable]

public class UpdateUserCommand
{
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string PersonalId { get; set; }
    public string MobileNumber { get; set; }
    public int AccountId { get; set; }
    

    public UpdateUserCommand(int id, string firstName, string lastName, int age, string personalId, string mobileNumber, int accountId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        PersonalId = personalId;
        MobileNumber = mobileNumber;
        AccountId = accountId;
    }
}