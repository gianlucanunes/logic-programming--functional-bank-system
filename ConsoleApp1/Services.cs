public class Services : Account
{
    public double value { get; set; }
    public double Deposit()
    {
        return amount + value;
    }

    public double Withdraw()
    {
        return amount -= value;
    }

    public double Transfer()
    {
        return amount += value;
    }
}

