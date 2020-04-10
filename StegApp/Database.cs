using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace StegApp
{
    class Database
    {
        public string ConnectionString { get; set; } //get set connection
        string connection;

        Database auth; //initialise database 
        public void GetConnection()
        {
            connection = @"Data Source=Database.db; Version=3";
            ConnectionString = connection;
        }
        public void CreateDatabase()
        {
            if (!File.Exists("Database.db")) //if database file doesn't exist already
            {
                try
                {
                    File.Create("Database.db"); //create database file
                    CreateTable();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                CreateTable();
            }
        }
        private void CreateTable() //creating a table
        {
            try
            {
                GetConnection();
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"CREATE TABLE IF NOT EXISTS users (ID INTEGER PRIMARY KEY AUTOINCREMENT, Username Text(25), Password Text(25))";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public bool VerifyUser(string username, string password)
        {
            auth = new Database();
            auth.CreateDatabase();
            auth.GetConnection();
            bool exist = true;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.ConnectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();

                    int cnt = 0;
                    string query = @"SELECT * FROM users WHERE Username='" + username + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        exist = true;
                        Console.WriteLine("existing user");
                    }
                    else if (cnt == 0)
                    {
                        exist = false;
                        Console.WriteLine("new user");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
                Console.WriteLine("check account error");
            }
            return exist;
        }

    }
}
