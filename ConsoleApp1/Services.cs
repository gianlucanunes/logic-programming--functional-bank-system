public class Services : Account
{
    public int value { get; set; }
    public double Deposit()
    {
        return amount + value;
    }
}

