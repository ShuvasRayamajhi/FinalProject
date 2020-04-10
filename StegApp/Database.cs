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
        public void getConnection()
        {
            connection = @"Data Source=Database.db; Version=3";
            ConnectionString = connection;
        }
        public void createDatabase()
        {
            if (!File.Exists("Database.db")) //if database file doesn't exist already
            {
                try
                {
                    File.Create("Database.db"); //create database file
                    createTable();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                createTable();
            }
        }
        private void createTable() //creating a table
        {
            try
            {
                getConnection();
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

    }
}
