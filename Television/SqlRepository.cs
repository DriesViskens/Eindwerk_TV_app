﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Television
{
    class SqlRepository
    {
        public SqlRepository()
        {

        }
        public delegate void Notify();  // delegate
        public event Notify DbChanged;
        public void StartProcess()
        {
            Console.WriteLine("Process Started!");
            // some code here..
            OnDbChanged();
        }

        protected virtual void OnDbChanged() //protected virtual method
        {
            Debug.WriteLine("ondbchanged");
            //if ProcessCompleted is not null then call delegate
            DbChanged?.Invoke();
        }



        public void CheckDatabaseOnchange()
        {
            string connectionString = Defaults.DbConnString;
            var changeListener = new DatabaseChangeListener(connectionString);

            changeListener.OnChange += () =>
            {
                changeListener.Start(@"SELECT [button] FROM [dbo].[Commands]");
                ReadRemoteDbCommands();
                
                Debug.WriteLine("inside");
            };
            changeListener.Start(@"SELECT [button] FROM [dbo].[Commands]");
        }
        private void ReadRemoteDbCommands()
        {
            Debug.WriteLine("There was a change");
            StartProcess();
        }
        public void DeleteFirst()
        {
            var sql = "delete TOP (1) FROM Commands";
            using (var connection = new SqlConnection(Defaults.DbConnString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteAll()
        {
            var sql = "delete FROM Commands";
            using (var connection = new SqlConnection(Defaults.DbConnString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Command GetFirst()
        {
            var cmd = new Command();
            var sql = "SELECT TOP(1) Commands.button, Commands.createTime FROM Commands";
            using (var connection = new SqlConnection(Defaults.DbConnString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string button = reader["button"].ToString();
                    string dateTime = reader["createTime"].ToString();

                    cmd.command = button;
                    cmd.DT = DateTime.Parse(dateTime);
                }
            }
            return cmd;
        }
    }
}
