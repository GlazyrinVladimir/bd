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
    
    public partial class MedicineUse : Form
    {
        DataSet ds;
        public MedicineUse()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            oleDbConnection1.Open();
            String myQuery = "SELECT * FROM [НазначениеЛекарств]";
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

        private void MedicineUse_Load(object sender, EventArgs e)
        {

        }

        private void sicksButton_Click(object sender, EventArgs e)
        {
            SicksForm form = new SicksForm();
            form.ShowDialog();
        }

        private void MedicineButton_Click(object sender, EventArgs e)
        {
            Medicine form = new Medicine();
            form.ShowDialog();
        }

        private void useButton_Click(object sender, EventArgs e)
        {
            Usage form = new Usage();
            form.ShowDialog();
        }

        private void sumButton_Click(object sender, EventArgs e)
        {
            operationTextBox.Text = "";
            string myQuery = "SELECT sum(Val(Период)) FROM НазначениеЛекарств;";
            MakeQuery(myQuery);
        }

        private void maxButton_Click(object sender, EventArgs e)
        {
            operationTextBox.Text = "";
            string myQuery = "SELECT Max(Val(Период)) FROM НазначениеЛекарств;";
            MakeQuery(myQuery);
        }

        private void avgButton_Click(object sender, EventArgs e)
        {
            operationTextBox.Text = "";
            string myQuery = "SELECT avg(Val(Период)) FROM НазначениеЛекарств;";
            MakeQuery(myQuery);
        }

        private void MakeQuery(string myQuery)
        {
            oleDbConnection1.Open();
            OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);
            OleDbDataReader myReader;
            myReader = myCommand.ExecuteReader();
            myReader.Close();
            operationTextBox.Text = myCommand.ExecuteScalar().ToString();
            oleDbConnection1.Close();

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddMedicineUse form = new AddMedicineUse();
            form.ShowDialog();
            string medicine = form.GetMedicine();
            string usage = form.GetUsage();
            string period = form.GetPeriod();
            LoadGrid();

            oleDbConnection1.Open();  
            string myQuery = "SELECT Код FROM Лекарства WHERE [Название] = '" + medicine + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);
            OleDbDataReader myReader;
            myReader = myCommand.ExecuteReader();
            myReader.Close();
            try
            {
                medicine = myCommand.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
            }
            oleDbConnection1.Close();

            if (medicine != "" && usage != "" && period != "")
             {
                 try
                 {
                     oleDbConnection1.Open();

                     oleDbDataAdapter1.InsertCommand.CommandText = "INSERT INTO [НазначениеЛекарств] (idНазначения, idЛекарств, Период) VALUES ('" + medicine + "', '" + usage + "', '" + period + "')";

                     oleDbDataAdapter1.InsertCommand.ExecuteNonQuery();
                 }
                 catch (Exception)
                 {
                 };
             }
            oleDbConnection1.Close();
            UpdateGrid();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private string GetStringValue(string myQuery)
        {
            oleDbConnection1.Open();
            string str = "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);
            OleDbDataReader myReader;
            myReader = myCommand.ExecuteReader();
            myReader.Close();
            try
            {
                str = myCommand.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
            }
            oleDbConnection1.Close();
            return str;
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            AddMedicineUse form = new AddMedicineUse();

            string myQuery = "SELECT Название FROM Лекарства WHERE Код = " + Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
            string s1 = GetStringValue(myQuery);
            form.SetBoxes(Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value),
                          s1,
                          Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value));

            form.ShowDialog();
            string s2 = form.GetUsage();
            string s3 = form.GetMedicine();
            myQuery = "SELECT Код FROM Лекарства WHERE [Название] = '" + s3 + "'";
            s3 = GetStringValue(myQuery);
            string s4 = form.GetPeriod();

            dataGridView1.CurrentRow.Cells[1].Value = s2;
            dataGridView1.CurrentRow.Cells[2].Value = s3;
            dataGridView1.CurrentRow.Cells[3].Value = s4;

            BindingSource bs = new BindingSource(ds, "НазначениеЛекарств");
            dataGridView1.DataSource = bs;
            UpdateGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string number = textBox1.Text;
            dataGridView2.Columns.Clear();
            if (number != "")
            {
                oleDbConnection1.Open();
                try
                {
                    string myQuery = "SELECT * FROM [НазначениеЛекарств] WHERE [idЛекарств] LIKE " + number + ";";
                    OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);

                    OleDbDataReader myReader;
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    oleDbDataAdapter1.SelectCommand = myCommand;
                    DataSet ds2 = new DataSet();
                    oleDbDataAdapter1.Fill(ds2);

                    dataGridView2.DataSource = ds2.Tables[0];

                }
                catch (Exception)
                { }
                oleDbConnection1.Close();
            }
        }
    }
}
