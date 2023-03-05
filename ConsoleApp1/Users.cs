public class Users : Account
{

    public string name { get; set; }
    public string lastname { get; set; }
    public int age { get; set; }
    public string password { get; set; }

    public string ShowInfo()
    {
        return $"Name: {name} {lastname} - Age: {age} years old - Password: {password}";
    }

}