using System.Data;
using System.Data.SqlClient;

namespace Database_Reader
{
    public partial class Form1 : Form
    {
        SqlConnection Connection;
        DataTable dataTable_databases;
        public Form1()
        {
            InitializeComponent();
            if (Global.db_ip != "db_ip_example")
            {
                Connection = Global.ConnectDB();
                dataTable_databases = GetAllDatabases();
                Global.Load_DataTable_Into_ListBox(dataTable_databases, listBox1);
                Connection.Close();
            }
            else
            {
                MessageBox.Show(@"Please change paremeters in Global.cs to start using this program.");
            }
        }

        private DataTable GetAllDatabases()
        {
            string query = @"SELECT name
                             FROM sys.databases
                             ORDER BY database_id
                             OFFSET 4 ROWS";
            DataTable dataTable = Global.ExecuteQuery(query, Connection);
            return dataTable;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string table_name = listBox1.SelectedItem.ToString();
            SqlConnection connection2 = Global.ConnectDB(table_name);
            Form2 form = new Form2(Connection);
            form.Text = table_name;
            form.Table = table_name;
            form.Show();
        }
    }
}