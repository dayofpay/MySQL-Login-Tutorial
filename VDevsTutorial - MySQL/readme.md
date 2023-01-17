* ВАЖНО !!!!!!!! В КОДА ИМА SQL VULNERABILITY , ЗА ДА ИЗБЕГНЕТЕ ПОТЕНЦИАЛЕН НЕОТОРИЗИРАН ДОСТЪП ОТ ТРЕТИ ЛИЦА ВИ СЪВЕТВАМЕ ДА ИЗПОЛЗВАТЕ ПАРАМЕТРИ ПРИ ИЗПРАЩАНЕ НА ЛОГИН / РЕГИСТЪР ЗАЯВКА

ПРИМЕР ЗА ТАКАВА :

```csharp
if(option == 1)
{
    Console.WriteLine("[1] Method 1 ");
    Console.WriteLine("[2] Method 2 ");
    var method = int.Parse(Console.ReadLine());
    if(method == 1)
    {
        Console.WriteLine("Command : ");
        var command = Console.ReadLine(); 
        ExecuteCommand(command);
    }
    if(method == 2)
    {
        Console.WriteLine("Command : ");
        var command2 = Console.ReadLine(); 
        ExecuteCommandTwo(command2);
    }
}
if(option == 2)
{
    Console.WriteLine("[1] Method 1 ");
    Console.WriteLine("[2] Method 2 ");
    var method = int.Parse(Console.ReadLine());
    if(method == 1)
    {
        Console.WriteLine("Command : ");
        MySqlCommand setCommand = new MySqlCommand("SELECT * FROM USERS WHERE email = @email", Database.connection); 
        setCommand.Parameters.AddWithValue("@email", Console.ReadLine());
        GetData(setCommand);
    }
    if(method == 2)
    {
        Console.WriteLine("Command : ");
        MySqlCommand setCommand = new MySqlCommand("SELECT * FROM USERS WHERE email = @email", Database.connection); 
        setCommand.Parameters.AddWithValue("@email", Console.ReadLine());
        GetDataW2(setCommand);
    }
}
if(option == 3)
{
    Console.WriteLine("Username : ");
    var user = Console.ReadLine();
    Console.WriteLine("Password : ");
    var password = Console.ReadLine();
    MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE email=@user AND password = @password", Database.connection);
    command.Parameters.AddWithValue("@user", user);
    command.Parameters.AddWithValue("@password", password);
    using (MySqlDataReader readUser = command.ExecuteReader())
    {
        if (readUser.HasRows)
        {
            while (readUser.Read())
            {
                Console.WriteLine("Successful login, " + readUser.GetString(0));
            }
            readUser.Close(); 
        }
        else
        {
            Console.WriteLine("Incorrect username / password");
        }
        Await(4000);
    }
}
```
