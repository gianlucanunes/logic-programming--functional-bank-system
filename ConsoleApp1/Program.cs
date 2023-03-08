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
    Console.Write("\nEntre com as informações abaixo.\n");
    Console.Write("Nome: ");
    obj.name = Console.ReadLine();
    logObj.name = obj.name;

    Console.Write("Sobrenome: ");
    obj.lastname = Console.ReadLine();

_age:
    Console.Write("Idade: ");

    int ag;
    if (int.TryParse(Console.ReadLine(), out ag))
        obj.age = ag;
    else
    {
        Console.WriteLine("\nValor incorreto!\n");
        goto _age;
    }

_pass:
    Console.Write("Senha,(6 digitos): ");
    obj.password = Console.ReadLine();

    if (obj.password.Length != 6)
    {
        Console.WriteLine("\nA senha não tem 6 digitos!\n");
        goto _pass;
    }

    logObj.password = obj.password;
    logObj.accountId = id;
    id++;

    logObj.amount = 100;

    // If everything is alright, add the complete object (name, last name, age, password) to the list called info.
    info.Add(obj);
    loginList.Add(logObj);
    Console.WriteLine("\nUsuário cadastrado com sucessso!\n");


    // Asks the user if he wants to create a new register. If so, he goes to the beggining, creating a new user type object.
_addReg:
    Console.WriteLine("\nQuer registrar um novo usuário?\n[S] Sim\n[N] Não\n");
    string opc = Console.ReadLine().ToUpper();

    if (opc != "S" && opc != "N")
    {
        Console.WriteLine("\nOpção inválida!\n");
        goto _addReg;
    }

    else if (opc == "S")
        goto _reg;

    else
    {
        optBeg = "N";

        // Calculates the total of registers.
        int regTotal = info.Count();

        Console.WriteLine($"\nA coleção tem {regTotal} registrosn\n");


    // Asks the user if he wants the program to display the info from each register.
    _regList:
        Console.WriteLine("\nQuer exibir as informações?\n[S] Sim\n[N] Não");
        string listOpt = Console.ReadLine().ToUpper();

        if (listOpt != "S" && listOpt != "N")
        {
            Console.WriteLine("\nOpção inválida!\n");
            goto _regList;
        }

        else if (listOpt == "S")
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

Console.Write("Nome: ");
log.givName = Console.ReadLine();

Console.Write("Senha: ");
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

        Console.WriteLine($"Olá, {item.name}! Seu saldo é R${item.amount}!\nQual operação deseja realizar?\n\n[S]Saque\n[D] Depósito\n[T] Transferência \n[L] Logoff\n");
        
    _op:
        string op = Console.ReadLine().ToUpper();

        switch (op)
        {
            case "S":
                _wd:
                Console.Write("\nDigite o valor a ser sacado: R$");
                
                if (double.TryParse(Console.ReadLine(), out amount)) {
                    if (amount > item.amount)
                    {
                        Console.WriteLine("\nVocê não tem dinheiro pra esse valor.\n");
                        goto _wd;
                    }

                    ser.value = amount;
                    ser.amount = item.amount;
                
                    item.amount = ser.Withdraw();

                    Console.WriteLine($"\nO total de R${ser.value} foi sacado da sua conta!\n");
                    goto _menu;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida!\n");
                    goto _wd;
                }

            case "D":
            _de:
                Console.Write("\nDigite o valor a ser depositado: R$");

                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    ser.value = amount;
                    ser.amount = item.amount;

                    item.amount = ser.Deposit();

                    Console.WriteLine($"\nO total de R${ser.value} foi depositado em sua conta!\n");
                    goto _menu;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida!\n");
                    goto _de;
                }

            case "T":
                Console.Write("\nDigite o valor a ser transferido: R$");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    _id:
                    Console.WriteLine("\nQual é o ID da conta que receberá a transferência?");
                    Console.Write("ID: ");

                    if (int.TryParse(Console.ReadLine(), out givId))
                    {
                        if (givId == item.accountId)
                        {
                            Console.WriteLine("\nVocê não pode transferir para si mesmo!\n");
                            goto _id;
                        }

                        ser.value = amount;
                        ser.amount = item.amount;
                        item.amount = ser.Withdraw();

                        idObj.idIden = givId;
                        idObj.trValue = amount;

                        idObjs.Add(idObj);

                        Console.WriteLine($"\nA transferência de R${amount} foi feita!");
                        goto _menu;
                        
                    }

                    else
                    {
                        Console.WriteLine("\nOpção inválida!\n");
                        goto _id;
                    }
                }
                break;
            case "L":
                Console.WriteLine("\nLoggin off...\n");
                goto _login;

            default:
                Console.WriteLine("\nOpção inválida.\n");
                goto _op;
        } 
    }
}
goto _login;
