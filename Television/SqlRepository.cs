using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Television
{
    class SqlRepository
    {
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
