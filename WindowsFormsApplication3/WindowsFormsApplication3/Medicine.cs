using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication3
{
    public partial class Medicine : Form
    {
        DataSet ds;
        public Medicine()
        {
            InitializeComponent();
            LoadGrid();
        }
        public void LoadGrid()
        {
            oleDbConnection1.Open();
            String myQuery = "SELECT * FROM [Лекарства]";
            OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);

            OleDbDataReader myReader;
            myReader = myCommand.ExecuteReader();
            ds = new DataSet();
            oleDbDataAdapter1.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            oleDbConnection1.Close();

        }

        private void UpdateGrid()
        {
            oleDbConnection1.Open();
            OleDbCommandBuilder db = new OleDbCommandBuilder(oleDbDataAdapter1);
            oleDbDataAdapter1.Update(ds);
            oleDbConnection1.Close();
            LoadGrid();
        }
        private void Medicine_Load(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }


    }
}
