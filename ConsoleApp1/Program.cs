using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

List<Users> info = new List<Users>();
List<Login> loginList = new List<Login>();
List<Account> accountList = new List<Account>();
List<Identyfier> idObjs = new List<Identyfier>();

string optBeg = "Y";
int id = 1;


// While loop to register more than one user
while (optBeg == "Y")
{

// Asking the user to type the info
_reg:
    Login logObj = new Login();
    Users obj = new Users();
    Console.Write("\nPlease, enter with the info below.\n");
    Console.Write("Name: ");
    obj.name = Console.ReadLine();
    logObj.name = obj.name;

    Console.Write("Last Name: ");
    obj.lastname = Console.ReadLine();

_age:
    Console.Write("Age: ");

    int ag;
    if (int.TryParse(Console.ReadLine(), out ag))
        obj.age = ag;
    else
    {
        Console.WriteLine("\nIncorrect value! Please, try filling it again.\n");
        goto _age;
    }

_pass:
    Console.Write("Password (6 digits): ");
    obj.password = Console.ReadLine();

    if (obj.password.Length != 6)
    {
        Console.WriteLine("\nThe password you typed does not have 6 digits! Please, try filling it again.\n");
        goto _pass;
    }

    logObj.password = obj.password;
    logObj.accountId = id;
    id++;

    logObj.amount = 100;

    // If everything is alright, add the complete object (name, last name, age, password) to the list called info.
    info.Add(obj);
    loginList.Add(logObj);
    Console.WriteLine("\nUser registered successfully!\n");


    // Asks the user if he wants to create a new register. If so, he goes to the beggining, creating a new user type object.
_addReg:
    Console.WriteLine("\nDo you want to register another user?\n[Y] Yes\n[N] No\n");
    string opc = Console.ReadLine().ToUpper();

    if (opc != "Y" && opc != "N")
    {
        Console.WriteLine("\nERROR: You have selected an incorrect option! Please, try it again.\n");
        goto _addReg;
    }

    else if (opc == "Y")
        goto _reg;

    else
    {
        optBeg = "N";

        // Calculates the total of registers.
        int regTotal = info.Count();

        Console.WriteLine($"\nThe collection has a total of {regTotal} registers.\n");


    // Asks the user if he wants the program to display the info from each register.
    _regList:
        Console.WriteLine("\nDo you want to display the info?\n[Y] Yes \n[N] No");
        string listOpt = Console.ReadLine().ToUpper();

        if (listOpt != "Y" && listOpt != "N")
        {
            Console.WriteLine("\nERROR: You have selected an incorrect option! Please, try it again.\n");
            goto _regList;
        }

        else if (listOpt == "Y")
        {
            foreach (var item in info)
            {
                Console.WriteLine(item.ShowInfo());
            }
        }

        break;
    }

}

_login:
Login log = new Login();
Console.WriteLine("\nLOGIN\n");

Console.Write("Name: ");
log.givName = Console.ReadLine();

Console.Write("Password: ");
log.givPass = Console.ReadLine();


foreach (var item in loginList)
{
_menu:
    double amount;
    int givId;

    Services ser = new Services();

    if (log.givName == item.name && log.givPass == item.password)
    {
        _for:
        foreach (var tr in idObjs)
        {
            if (item.accountId == tr.idIden)
            {
                item.amount += tr.trValue;
                idObjs.Remove(tr);
                goto _for;
            }
        }

        Identyfier idObj = new Identyfier();

        Console.WriteLine($"Hello, {item.name}! Your total amount is US${item.amount}!\nWhich operation you want to do?\n\n[W] Withdrawal\n[D] Deposit\n[T] Transfer\n[L] Logoff\n");
        
    _op:
        string op = Console.ReadLine().ToUpper();

        switch (op)
        {
            case "W":
                _wd:
                Console.Write("\nPlease, type the amount of money to withdraw: US$");
                
                if (double.TryParse(Console.ReadLine(), out amount)) {
                    if (amount > item.amount)
                    {
                        Console.WriteLine("\nYou do not have the money! Type a less amount.\n");
                        goto _wd;
                    }

                    ser.value = amount;
                    ser.amount = item.amount;
                
                    item.amount = ser.Withdraw();

                    Console.WriteLine($"\nThe total of US${ser.value} has beeen withdrawed from your account!\n");
                    goto _menu;
                }
                else
                {
                    Console.WriteLine("\nERROR: Incorrect value! Try it again!\n");
                    goto _wd;
                }

            case "D":
            _de:
                Console.Write("\nPlease, type the amount of money to withdraw: US$");

                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    ser.value = amount;
                    ser.amount = item.amount;

                    item.amount = ser.Deposit();

                    Console.WriteLine($"\nThe total of US${ser.value} has beeen added to your account!\n");
                    goto _menu;
                }
                else
                {
                    Console.WriteLine("\nERROR: Incorrect value! Try it again!\n");
                    goto _de;
                }

            case "T":
                Console.Write("\nPlease, type the amount of money to transfer: ");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    _id:
                    Console.WriteLine("\nWhich is the account ID?");
                    Console.Write("ID: ");

                    if (int.TryParse(Console.ReadLine(), out givId))
                    {
                        if (givId == item.accountId)
                        {
                            Console.WriteLine("\nYou can't transfer money to yourself! Type another ID!\n");
                            goto _id;
                        }

                        ser.value = amount;
                        ser.amount = item.amount;
                        item.amount = ser.Withdraw();

                        idObj.idIden = givId;
                        idObj.trValue = amount;

                        idObjs.Add(idObj);

                        Console.WriteLine($"\nSucessfully trasnfered US${amount} from your account!");
                        goto _menu;
                        
                    }

                    else
                    {
                        Console.WriteLine("\nERROR: Incorrect value! Try it again!\n");
                        goto _id;
                    }
                }
                break;
            case "L":
                Console.WriteLine("\nLoggin off...\n");
                goto _login;

            default:
                Console.WriteLine("\nERROR: You have selected an incorrect option! Please, try it again.\n");
                goto _op;
        } 
    }
}
goto _login;
