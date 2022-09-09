using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

namespace Database
{
    public class DbHandler : IDisposable
    {
        private const string Host = "127.0.0.1";
        private const string Username = "root";
        private const string Password = "";
        private const string DB = "sog_dbengineering_gp21";

        private readonly MySqlConnection _connection;

        public DbHandler()
        {
            var sbConnection = new MySqlConnectionStringBuilder
            {
                Server = Host,
                UserID = Username,
                Password = Password,
                Database = DB
            };

            //_connection = new MySqlConnection($"server={_host};user={_username};password={_password};database={_db}");
            _connection = new MySqlConnection(sbConnection.ConnectionString);
            _connection.Open();
            Debug.Log("connected to db");
        }

        public void Dispose()
        {
            Debug.Assert(_connection != null);
            _connection.Close();
            Debug.Log("disconnected from db");
        }

        public bool TryLogin(string username, string password, out DbPlayer dbPlayer)
        {
            string sql = "SELECT * FROM player WHERE username = '" + username + "'";

            MySqlCommand command = new MySqlCommand(sql, _connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                dbPlayer = new DbPlayer
                {
                    id = reader.GetUInt32(0),
                    username = reader.GetString(1),
                    email = reader.GetString(2),
                    vorname = reader.GetString(3),
                    nachname = reader.GetString(4),
                    password = reader.GetString(5)
                };
            }
            else
            {
                dbPlayer = null;
                reader.Close();
                return false;
            }

            var actualPassword = dbPlayer.password;
            var hashedPassword = ToMd5(password);

            Debug.Log(actualPassword);
            Debug.Log(hashedPassword);

            if (actualPassword == hashedPassword)
            {
                reader.Close();
                return true;
            }

            // login => failed
            dbPlayer = null;
            reader.Close();
            return false;
        }

        public bool LogoutPlayer()
        {
            Debug.Log("logged out");

            return true;
        }

        public string ToMd5(string input)
        {
            byte[] abBytes = Encoding.ASCII.GetBytes(input);
            byte[] abHashedBytes = MD5.Create().ComputeHash(abBytes);

            string sHash = BitConverter
                .ToString(abHashedBytes)
                .Replace("-", "")
                .ToLower();

            return sHash;
        }
    }
}