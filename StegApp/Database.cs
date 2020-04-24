using System;
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
                    CreateTable(); //call create table function
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
                GetConnection(); //get connection
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    con.Open(); //open connection
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"CREATE TABLE IF NOT EXISTS users (ID INTEGER PRIMARY KEY AUTOINCREMENT, Username Text(25), Password Text(25))"; //create table 
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery(); //execute
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        internal void createDatabase()
        {
            throw new NotImplementedException();
        }

        public bool VerifyUser(string username)//check if user exists already 
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
                    string query = @"SELECT * FROM users WHERE Username='" + username + "'"; //find where entered username equals existing one on the database
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        cnt++;
                    }
                    if (cnt == 1) //means there is one
                    {
                        exist = true;
                        Console.WriteLine("existing user");
                    }
                    else if (cnt == 0) //means there is none
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

        public void CreateAccount(string username, string password)
        {
            auth.CreateDatabase();
            auth = new Database();//call database
            auth.GetConnection();
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.ConnectionString))
                {
                    con.Open();
                    string ePassword;
                    ePassword = PasswordEncrypt.Encryption(password);
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"INSERT INTO users(Username, Password) VALUES (@username, @password)";
                    Console.WriteLine(ePassword);
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@username", username));
                    cmd.Parameters.Add(new SQLiteParameter("@password", ePassword));
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("New user added.");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("insert data error");
            }
        }

        public bool LogIn(string username, string password)
        {
            auth = new Database();
            auth.GetConnection();
            bool login = false; //check if account logs in
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.ConnectionString))
                {
                    con.Open();
                    string ePassword = PasswordEncrypt.Encryption(password); //encrypt password
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"SELECT * FROM users WHERE Username='" + username + "' and Password='" + ePassword + "'"; //check the input username and password match the database
                    int count = 0;
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    SQLiteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        count++; 
                    }
                    if (count == 1)
                    {
                        login = true; //return true, log in user.
                        Console.WriteLine("Log in sucessful.");
                    }
                    else
                    {
                        login = false; //return false, do not login user.
                        Console.WriteLine("Log in failed.");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "check account error");
            }
            return login;
        }

    }
}
