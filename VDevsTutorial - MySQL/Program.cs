using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace VDevsTutorial___MySQL
{
    class Program
    {
        public static int option { get; set; }
        public static void Main()
        {
            Restart:
            Database.Connect(); // Задейства Connect метода от Database.cs
            if (Database.connected == true) // Ако свързването е било успешно
            {
                Console.WriteLine("[1] Изпращане на команда");
                Console.WriteLine("[2] Извличане на данни от софтуера");
                Console.WriteLine("[3] [DEMO] Login");
                Console.WriteLine("[4] [DEMO] Register");
                try
                {
                    option = int.Parse(Console.ReadLine());
                }catch(Exception error)
                {
                    Console.WriteLine($"Грешка , опитайте отново :{error.Message}");
                    Await(3000);
                }
                if(option == 1)
                {
                    Console.WriteLine("[1] Начин 1 ");
                    Console.WriteLine("[2] Начин 2 ");
                    var nachin = int.Parse(Console.ReadLine());
                    if(nachin == 1)
                    {
                        Console.WriteLine("Команда : ");
                        var command = Console.ReadLine(); // Примерна команда : INSERT INTO users(email,password)VALUES('test123','test');
                        ExecuteCommand(command);
                    }
                    if(nachin == 2)
                    {
                        Console.WriteLine("Команда : ");
                        var command2 = Console.ReadLine(); // Примерна команда : INSERT INTO users(email,password)VALUES('test123','test');
                        ECommandTwo(command2);
                    }
                }
                if(option == 2)
                {
                    Console.WriteLine("[1] Начин 1 ");
                    Console.WriteLine("[2] Начин 2 ");
                    var nachin = int.Parse(Console.ReadLine());
                    if(nachin == 1)
                    {
                        Console.WriteLine("Команда : ");
                        MySqlCommand setCommand = new MySqlCommand(Console.ReadLine(), Database.connection); // Примерна команда : SELECT * FROM USERS WHERE email = 'test'
                        GetData(setCommand);
                    }
                    if(nachin == 2)
                    {
                        Console.WriteLine("Команда : ");
                        MySqlCommand setCommand = new MySqlCommand(Console.ReadLine(), Database.connection); // Примерна команда : SELECT * FROM USERS WHERE email = 'test'
                        GetDataW2(setCommand);
                    }
                }
                if(option == 3)
                {
                    Console.WriteLine("Потребител : ");
                    var user = Console.ReadLine();
                    Console.WriteLine("Парола : ");
                    var password = Console.ReadLine();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE email='" + user + "' AND password ='" + password + "';", Database.connection);
                    using (MySqlDataReader readUser = command.ExecuteReader())
                    {
                        if (readUser.HasRows)
                        {
                            while (readUser.Read())
                            {
                                Console.WriteLine("Успешно вписване, " + readUser.GetString(0));
                            }
                            readUser.Close(); // Изключваме MySQLDataReader object-a
                        }
                        else
                        {
                            Console.WriteLine("Грешен username / Парола");
                        }
                        Await(4000);
                    }
                }
                if(option == 4)
                {
                    Console.WriteLine("Потребител: ");
                    var user = Console.ReadLine();
                    Console.WriteLine("Парола: ");
                    var password = Console.ReadLine();
                    Console.WriteLine("Повторете паролата : ");
                    var repass = Console.ReadLine();
                    if(password == repass) // Ако паролите съвпадат
                    {
                        MySqlCommand checkUserExist = new MySqlCommand("SELECT email FROM users WHERE email='" + user + "';",Database.connection); // Правим проверка дали съществува такъв потребител
                        using (MySqlDataReader readUser = checkUserExist.ExecuteReader())
                        {
                            if (readUser.HasRows) // Ако е намерен потребител със такъв email/username
                            {
                                Console.WriteLine("Грешка ! Вече съществува такъв потребител !");
                                Await(3000);
                            }
                            else{
                                readUser.Close(); // Изключваме MySQLDataReader object-a
                                MySqlCommand insertUser = new MySqlCommand($"INSERT INTO users(email,password)VALUES('{user}','{password}');", Database.connection);
                                try
                                {
                                    insertUser.ExecuteScalar();
                                    Console.WriteLine("Успешно !");
                                    Await(3000);
                                }catch(MySqlException error)
                                {
                                    Console.WriteLine("Грешка ! " + error.Message);
                                    Await(3000);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("00");
            }
        }
        public static void Await(int seconds) // Този метод се използва за да рестартираме софтуера
        {
            System.Threading.Thread.Sleep(seconds);
            Console.Clear();
            Main();
        }
        public static void ExecuteCommand(string command) // Начин 1 за изпращане на команда
        {
            try
            {
                using (MySqlCommand sqlCommand = new MySqlCommand(command,Database.connection)) // Даваме аргументи към софтуера за подготовката към въвеждането на данните , параметъра command е командата, която ще използваме, а Database.connection служи за да поясни на софтуера коя mysql връзка ще се използва
                {
                    sqlCommand.ExecuteScalar(); // Правим опит за въвеждане на командата
                    Console.WriteLine("Успешно изпратихте команда " + sqlCommand + " !");
                    Await(3000);
                }
            }catch(MySqlException error)
            {
                Console.WriteLine($"Грешка при изпращането на команда {command} {Environment.NewLine} Възможна причина: {error.Message} ");
                Await(3000);
            }
        }
        public static void ECommandTwo(string command) // Начин 2 за изпращане на команда
        {
            try
            {
                MySqlCommand commandtwo = new MySqlCommand(command, Database.connection);
                commandtwo.ExecuteScalar();
                Console.WriteLine("Успешно изпратихте команда " + command + " !");
                Await(3000);

            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Грешка при изпращането на команда {command} {Environment.NewLine} Възможна причина: {error.Message} ");
                Await(3000);
            }
        }
        public static void GetData(MySqlCommand command) // Начин 1
        {
            try
            {
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                if (reader.HasRows) // Ако в БД има намерени данни от командата
                {
                    while (reader.Read()) // Докато се четат данните
                    {
                        Console.WriteLine("----- Резултат -----");
                        Console.WriteLine(reader.GetString(0)); // reader-a взима index-a от таблицата където 0 = username (Първия ред от таблицата ) 1 = password ( Втория ред от таблицата )
                        Console.WriteLine(reader.GetString(1)); // reader-a взима index-a от таблицата където 0 = username (Първия ред от таблицата ) 1 = password ( Втория ред от таблицата )
                        Console.WriteLine("----- Резултат -----");
                    }
                    reader.Close(); // Изключваме MySQLDataReader object-a
                }
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Грешка при изпълнението на командата {command} {Environment.NewLine} Възможна причина: {error.Message} ");
                Await(3000);
            }
        }
        public static void GetDataW2(MySqlCommand command) // Начин 2
        {
            try
            {
                using (MySqlDataReader reader = command.ExecuteReader()) // Правим опит да излечем данни от БД използвайли командата
                {
                    if (reader.HasRows) // Ако в БД има намерени данни от командата
                    {
                        while (reader.Read()) // Докато се четат данните
                        {
                            Console.WriteLine("----- Резултат -----");
                            Console.WriteLine(reader.GetString(0)); // reader-a взима index-a от таблицата където 0 = username (Първия ред от таблицата ) 1 = password ( Втория ред от таблицата )
                            Console.WriteLine(reader.GetString(1)); // reader-a взима index-a от таблицата където 0 = username (Първия ред от таблицата ) 1 = password ( Втория ред от таблицата )
                            Console.WriteLine("----- Резултат -----");
                        }
                        reader.Close(); // Изключваме MySQLDataReader object-a
                    }
                }
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Грешка при изпълнението на командата {command} {Environment.NewLine} Възможна причина: {error.Message} ");
                Await(3000);
            }
        }
    }
}
