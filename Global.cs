using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Database_Reader
{
    internal class Global
    {
        // Please Change "DatabaseIP", "UserID", "UserPW" and "DatabaseName" to start using this program.
        public static string db_ip = @"db_ip_example";  //CHANGE
        static string user_id = "user_id_example"; //CHANGE
        static string user_pw = "user_pw_example"; //CHANGE

        public static SqlConnection ConnectDB(string db_name = "db_name_example")  //CHANGE db_name
        {
            string connection_string = String.Format(@"Data Source = {0}; Initial Catalog = {1}; User ID = {2}; Password = {3}"
                                                   , db_ip, db_name, user_id, user_pw);
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connection_string;
            connection.Open();
            return connection;
        }

        public static DataTable ExecuteQuery(string query, SqlConnection connection)
        {
            DataTable dataTable = new DataTable();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(dataTable);
            adapter.Dispose();
            return dataTable;
        }

        public static void Load_DataTable_Into_ListBox(DataTable dataTable, ListBox listBox)
        {
            listBox.Items.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                listBox.Items.Add(dataTable.Rows[i][0].ToString());
            }
            listBox.Sorted = true;
        }

    }
}
