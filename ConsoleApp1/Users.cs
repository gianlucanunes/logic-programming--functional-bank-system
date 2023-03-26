// Creating the public class Users, which will inherit from the super class Account
public class Users : Account
{
    // Creating the class properties
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    // Creating the ShowInfo() method to display the user info
    public string ShowInfo()
    {
        return $"----------------\nName: {Name} {LastName}\nAge: {Age} years old\nPassword: {Password}\nId: {AccountId}\n----------------\n";
    }
}