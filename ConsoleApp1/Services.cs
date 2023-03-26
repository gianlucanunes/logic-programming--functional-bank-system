// Creating the public class Service
public class Services : Account
{
    // Creating the class properties
    public double value { get; set; }

    // Creating the Deposit() method to add money into your account
    public double Deposit()
    {
        return Amount + value;
    }

    // Creating the Withdraw() method to take out money from your account
    public double Withdraw()
    {
        return Amount -= value;
    }

    // Creating the Transfer() method to transfer your money to someone else's account
    public double Transfer()
    {
        return Amount += value;
    }
}