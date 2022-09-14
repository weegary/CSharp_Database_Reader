using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Database_Reader
{
    public partial class Form2 : Form
    {
        SqlConnection Connection;
        public string Table { get; set; }
        public Form2(SqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
            DataTable dataTable_table = GetTables();
            Global.Load_DataTable_Into_ListBox(dataTable_table, listBox1);
        }

        private DataTable GetTables()
        {
            string query = @"SELECT name FROM sys.Tables";
            DataTable dataTable = Global.ExecuteQuery(query, Connection);
            return dataTable;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt && e.KeyCode == Keys.X) || (e.KeyCode == Keys.F5))
            {
                btn_query.PerformClick();
            }
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            string query = richTextBox1.Text;
            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataAdapter adapter;
            try
            {
                adapter = new SqlDataAdapter(query, Connection);
                adapter.Fill(dataTable);
                adapter.Dispose();
                dgv_result.DataSource = dataTable;
            }
            catch(Exception error) {
                MessageBox.Show(error.Message);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string table_name = listBox1.SelectedItem.ToString();
            string query = String.Format(@"SELECT TOP(10) * 
FROM {0}", table_name);
            richTextBox1.Text = query;
        }
    }
}
