using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace VDevsTutorial___MySQL
{
    class Database
    {
        public static MySqlConnection connection { get; set; }
        public static string server { get; set; }
        public static string database { get; set; }
        public static string user { get; set; }
        public static string password { get; set; }
        public static string port { get; set; }
        public static string connectionString { get; set; }
        public static string sslM { get; set; }
        public static string conString { get; set; }
        public static bool connected { get; set; }
        public static void Connect()
        {
            server = "localhost"; // Адрес на сървъра
            database = "tutorial"; // Име на БД
            user = "root"; // Потребител
            password = ""; // Парола
            port = "3306"; // Порт
            sslM = "none"; // SSL
            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4};", server, port, user, password, database, sslM);
            conString = connectionString;
            connection = new MySqlConnection(conString);
            try // Правим опит за връзка с базата данни
            {
                connection.Open(); // Правим опит за комуникация със сървъра
                connected = true; // Успешно свързване
                Console.WriteLine("Връзката към MySQL Съръвъра беше успешна ...");
                System.Threading.Thread.Sleep(1000); // Изчакваме 1 секунда
            }
            catch (MySqlException error) // Този exception се задейства, ако софтуера не успее да направи връзка със MySQL Сървъра поради грешка
            {
                connected = false;
                Console.WriteLine($"Имаше проблем със опита за връзка със сървъра {Environment.NewLine} Вероятна причина: {error.Message}");
            }
        }
    }
}