using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

//using Xamarin.Forms;

namespace Television
{
    public class DatabaseChangeListener
    {
        #region Constructor

        public DatabaseChangeListener(string connectionString)
        {
            this.connectionString = connectionString;
            SqlDependency.Stop(connectionString);
            SqlDependency.Start(connectionString);
            connection = new SqlConnection(connectionString);
        }

        #endregion Constructor

        #region Finalizer       //destructor

        ~DatabaseChangeListener()
        {
            SqlDependency.Stop(connectionString);
        }

        #endregion Finalizer       //destructor

        #region Properties

        private readonly string connectionString;
        private readonly SqlConnection connection;

        public delegate void NewMessage();
        public event NewMessage OnChange;

        #endregion Properties

        #region Methods


        public DataTable Start(string changeQuery)
        {
            using (SqlCommand cmd = new SqlCommand(changeQuery, connection) { Notification = null })
            {
                SqlDependency dependency = new SqlDependency(cmd);
                dependency.OnChange += NotifyOnChange;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (DataTable dt = new DataTable())
                {
                    dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
                    return dt;
                }
            }
        }

        void NotifyOnChange(object sender, SqlNotificationEventArgs e)
        {
            var dependency = sender as SqlDependency;
            if (dependency != null) dependency.OnChange -= NotifyOnChange;
            {
                OnChange();
            }
        }

        #endregion Methods

    }
}