using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace japanese
{
    internal class Database
    {
        SqliteConnection con;
        public Database(string pathToDatabase)
        {
            con = new SqliteConnection($"Data Source={pathToDatabase}");
            con.Open();
        }
        public string AddUser(string login, string pwd)
        {
            string hash = String.Concat(SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(pwd))
                .Select(item => item.ToString("x2")));
            SqliteCommand cmd = new()
            {
                Connection = con,
                CommandText = $"INSERT INTO Users (login, password, signUpTime, isAdmin) VALUES ('{login}', '{hash}', '{0}', {0})"
            };
            try
            {
                int exec = cmd.ExecuteNonQuery();
                return "Пользователь зарегистрирован";
            }
            catch (SqliteException e)
            {
                if (e.Message.StartsWith("SQLite Error 19"))
                {
                    return "Пользователь уже существует";
                }
                else { return e.Message; }
            }
        }
        public string CheckUser(string login, string pwd)
        {
            SqliteCommand cmd = new()
            {
                Connection = con,
                CommandText = $"SELECT password FROM Users WHERE login = '{login}'",
            };
            SqliteDataReader exec = cmd.ExecuteReader();
            if (!exec.HasRows)
            {
                return "Пользователь не найден";
            }
            else
            {
                string hash = String.Concat(SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(pwd))
                .Select(item => item.ToString("x2")));
                exec.Read();
                string res = exec.GetString(0);
                
                if (res == hash)
                {
                    return "auth";
                }
                else { return "Неправильный пароль"; }
            }

        }
    }
}
