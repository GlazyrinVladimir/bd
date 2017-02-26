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
    public partial class Usage : Form
    {
        DataSet ds;
        public Usage()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            oleDbConnection1.Open();
            String myQuery = "SELECT * FROM [Назначение]";
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

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddUsage form = new AddUsage();
            form.ShowDialog();
            string name = form.GetName();
            string date = form.GetDate();
            LoadGrid();

            oleDbConnection1.Open();
            string myQuery = "SELECT Код FROM Больные WHERE [Фамилия] = '" + name + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);
            OleDbDataReader myReader;
            myReader = myCommand.ExecuteReader();
            myReader.Close();
            try
            {
                name = myCommand.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
            }
            oleDbConnection1.Close();

            if (name != "" && date != "")
            {
                try
                {
                    oleDbConnection1.Open();

                    oleDbDataAdapter1.InsertCommand.CommandText = "INSERT INTO [Назначение] ([id Больного], Дата) VALUES (" + name + ", '" + date + "')";

                    oleDbDataAdapter1.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                };
            }
            oleDbConnection1.Close();
            UpdateGrid();
        }
    }
}
