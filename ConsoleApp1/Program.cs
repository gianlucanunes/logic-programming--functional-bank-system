/*
 * 
 *      EN: Logic Programming Exercise: a basic and functional bank system where you can create accounts, login, make transactions between them, withdraw and
 *      deposit money to your account.
 *      PT-BR: Exercício de Lógica de Programação: um sistema de banco básico e funcional onde você pode criar contas, logar, realizar transferências entre elas, sacar e
 *      depositar dinheiro na sua conta.
 *      
 *      Created by / Feito por: Gianluca Nunes
 *
 */

// Creating the usersList, which will be used to store the registered user's basic info (Name, LastName and Age)
List<Users> usersList = new List<Users>();

// Creating the loginList, which will be used in the login process (given Name and given Password)
List<Login> loginList = new List<Login>();

// Creating the accountList, which will be used to store the user's Username and Password
List<Account> accountList = new List<Account>();

// Creating the transfersList, which will be used to store information about transactions (Id's)
List<Identyfier> transfersList = new List<Identyfier>();

// This string is being used to create a loop if the user wants to register another account
string optBeg = "Y";

// This id will be given to the user
int id = 1;


// While loop to register more than one user
while (optBeg == "Y")
{

_reg:
    // Creating the Login object to store the login info (username and password) once a user successfully creates an account
    Login logObj = new Login();

    // Creating the user object to store the user info (Name, LastName and Age)
    Users userObj = new Users();

    // Asking the user to type the info
    Console.Write("\nPlease, fill in the form bellow.\n");
    Console.Write("Name: ");

    userObj.Name = Console.ReadLine();
    logObj.Username = userObj.Name;

    Console.Write("Last name: ");
    userObj.LastName = Console.ReadLine();

_age:
    // Validating the age
    Console.Write("Age: ");

    int age;
    if (int.TryParse(Console.ReadLine(), out age))
        userObj.Age = age;
    else
    {
        Console.WriteLine("\nIncorrect value! Please, try it again.\n");
        goto _age;
    }

_pass:
    // Validating the password
    Console.Write("Password (it needs to have exactly 6 characters): ");
    userObj.Password = Console.ReadLine();

    if (userObj.Password.Length != 6)
    {
        Console.WriteLine("\nThe password does not have 6 characters! Please, try it again.\n");
        goto _pass;
    }

    // Adding the password to the Login object
    logObj.Password = userObj.Password;

    // Adding the account id to the Login object
    userObj.AccountId = id;
    logObj.AccountId = id;

    // Increases the id by 1 for the next account
    id++;

    // Adding an initial amount of US$100.00 to the account
    logObj.Amount = 100;

    // If everything is alright, adds the complete object to their respective lists
    usersList.Add(userObj);
    loginList.Add(logObj);
    
    Console.WriteLine("\n-= ! =- User added to the system succesfully. -= ! =-\n");

_newReg:
    // Asks the user if he wants to create a new register. If so, he goes to the beggining, creating a new user type object.
    Console.WriteLine("\nDo you want to create another user?\n[Y] Yes\n[N] No\n");
    string opc = Console.ReadLine().ToUpper();

    // Validating the user input
    if (opc != "Y" && opc != "N")
    {
        Console.WriteLine("\nIncorrect option! Please, try it again.\n");
        goto _newReg;
    }

    else if (opc == "Y")
        goto _reg;

    else
    {
        optBeg = "N";

        // Calculates the total of users stored inside the system
        int totalUsers = usersList.Count();

        // Display the amount of users
        Console.WriteLine($"\n-= ! =- A total of {totalUsers} users has been stored inside the system. -= ! =-\n");

    _regList:
        // Asks the user if he wants to display the info for each account
        Console.WriteLine("\nDo you want do display the info for each user?\n[Y] Yes\n[N] No");
        string listOpt = Console.ReadLine().ToUpper();

        // Validating the user input
        if (listOpt != "Y" && listOpt != "N")
        {
            Console.WriteLine("\nIncorrect option! Please, try it again.\n");
            goto _regList;
        }

        else if (listOpt == "Y")
        {
            // Displaying the info on the screen
            foreach (var item in usersList)
            {
                Console.WriteLine(item.ShowInfo());
            }
        }

        // Breaks the switch structure and go to the login
        break;
    }
}

// Start the login process.
Console.WriteLine("\n\n---=--- LOGIN SYSTEM ---=---\n\n");

_login:
// Create the log object, to store the given Name and given Password from the user
Login log = new Login();

// Asks for the username
Console.Write("Username: ");
log.GivName = Console.ReadLine();

// Asks for the password
Console.Write("Password: ");
log.GivPass = Console.ReadLine();

// Uses foreach in order to compare the given Name and the given Password to the accounts stored in the system. If it finds a match, the user can have access to its account
foreach (var item in loginList)
{

_menu:
    // Creates the amount variable, in order to work with the bank operations
    double Amount;

    // Creates the givId, where the user will say to which account he wants to transfer his money
    int givId;

    // Creates the ser object to call the bank operations
    Services ser = new Services();

    // Entering the user account
    if (log.GivName == item.Username && log.GivPass == item.Password)
    {
        _transferCheck:
        // It first tries to see if there is any pendent transfer from someone else
        // If there is, adds the value to the account and delete the stored transfer info from the system
        foreach (var tr in transfersList)
        {
            if (item.AccountId == tr.IdIden)
            {
                item.Amount += tr.TrValue;
                transfersList.Remove(tr);
                goto _transferCheck;
            }
        }

        // Creates the id object to work with transfers
        Identyfier idObj = new Identyfier();

        // Welcome message and current ammount
        Console.WriteLine($"Hello, {item.Username}! Your current total amount is {item.Amount:c}!\nWhich operation do you want to do?\n\n" +
        "[W] Withdraw\n[D] Deposit\n[T] Transfer \n[L] Logoff\n");
        
    _op:
        string op = Console.ReadLine().ToUpper();

        switch (op)
        {
            // Withdraw operation
            case "W":
                _wd:
                // Asks the user how much he wants to withdraw
                Console.Write("\nType the amount to withdraw: ");
                
                // Validating the ammount
                if (double.TryParse(Console.ReadLine(), out Amount)) {

                    // Checks if the user is trying to withdraw more money than he has
                    if (Amount > item.Amount)
                    {
                        Console.WriteLine("\nYou do not have sufficient money to do that. Please, try again.\n");
                        goto _wd;
                    }

                    // If everything is okay, withdraw the ammount from the user's account
                    ser.value = Amount;
                    ser.Amount = item.Amount;
                
                    item.Amount = ser.Withdraw();

                    // Display a warning message
                    Console.WriteLine($"\nThe total of {ser.value:c} has been withdrawed from your account!\n");
                    goto _menu;
                }
                else
                {
                    Console.WriteLine("\nIncorrect value! Please, try it again.\n");
                    goto _wd;
                }

            // Deposit operation
            case "D":
            _dt:
                // Asks the user how much he wants to deposit
                Console.Write("\nType the ammount to deposit: ");

                // Validating the ammount
                if (double.TryParse(Console.ReadLine(), out Amount))
                {
                    // If everything is okay, add the ammount to the user's account
                    ser.value = Amount;
                    ser.Amount = item.Amount;

                    item.Amount = ser.Deposit();

                    // Display a warning message
                    Console.WriteLine($"\nThe total of {ser.value:c} has been added to your account!\n");
                    goto _menu;
                }
                else
                {
                    Console.WriteLine("\nIncorrect value! Please, try it again.\n");
                    goto _dt;
                }

            // Transfer operation
            case "T":
            _tr:
                // Asks the user how much he wants to transfer
                Console.Write("\nType the ammount to transfer: ");

                // Validating the ammount
                if (double.TryParse(Console.ReadLine(), out Amount))
                {
                
                    _id:
                    // Asks the user what is the account Id he wants to transfer to
                    Console.WriteLine("\nWhat is the account Id you want to transfer to?");
                    Console.Write("ID: ");

                    // Validating the id
                    if (int.TryParse(Console.ReadLine(), out givId))
                    {
                        // Checks if the user is trying to transfer money to itself
                        if (givId == item.AccountId)
                        {
                            Console.WriteLine("\nYou cannot transfer money from your account to your account! Please, try it again.\n");
                            goto _id;
                        }
                        
                        // Checks if the user is trying to transfer money to an inexistent account
                        if (givId > (id - 1)) 
                        {
                            Console.WriteLine("\nThere is no account with this id. Please, try it again.\n");
                            goto _id;
                        }

                        // Checks if the user is trying to transfer more money than he has
                        if (Amount > item.Amount)
                        {
                            Console.WriteLine("\nYou do not have sufficient money to do that. Please, try again.\n");
                            goto _tr;
                        }

                        // If everything is okay, take out the ammount to the user's account
                        ser.value = Amount;
                        ser.Amount = item.Amount;
                        item.Amount = ser.Withdraw();

                        // Adds the id and the transfer ammount to the idObj
                        idObj.IdIden = givId;
                        idObj.TrValue = Amount;

                        // Adds the idOj to the transfersList
                        transfersList.Add(idObj);

                        // Display a warning message
                        Console.WriteLine($"\nYou have succesfully transferred {Amount:c} from your account.");
                        goto _menu;
                    }

                    // Validating the user input
                    else
                    {
                        Console.WriteLine("\nIncorrect option! Please, try it again.\n");
                        goto _id;
                    }
                }
                else 
                {
                    Console.WriteLine("\nIncorrect value! Please, try it again.\n");
                    goto _tr;
                }

            case "L":
                // Logs off the user
                Console.WriteLine("\nLoggin off...\n");
                goto _login;

            default:
                // Validating the user input
                Console.WriteLine("\nIncorrect option! Please, try it again.\n");
                goto _op;
        } 
    }
}
Console.WriteLine("\nIncorrect info! Please, try it again.\n");
goto _login;