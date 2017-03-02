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
    public partial class SicksForm : Form
    {
        DataSet ds;
        public SicksForm()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            oleDbConnection1.Open();
            String myQuery = "SELECT * FROM [Больные]";
            OleDbCommand myCommand = new OleDbCommand(myQuery, oleDbConnection1);

            OleDbDataReader myReader;
            myReader = myCommand.ExecuteReader();
            ds = new DataSet();
            oleDbDataAdapter1.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            oleDbConnection1.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void UpdateGrid()
        {
            oleDbConnection1.Open();
            OleDbCommandBuilder db = new OleDbCommandBuilder(oleDbDataAdapter1);
            oleDbDataAdapter1.Update(ds);
            oleDbConnection1.Close();
            LoadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSickForm form = new AddSickForm();
            form.ShowDialog();
            string surname = form.GetSurname();
            string name = form.GetName();
            string patr = form.GetPatr();
            string num = form.GetNumber();
            string seria = form.GetSeria();
            string path = form.GetPath();
            LoadGrid();
            if (surname != "" && name != "" && patr != "" && num != "" && seria != "")
            {
                try
                {
                    oleDbConnection1.Open();

                    oleDbDataAdapter1.InsertCommand.CommandText = "INSERT INTO [Больные] (Фамилия, Имя, Отчество, [Номер паспорта], [Серия паспорта], фото) VALUES ('" + surname + "', '" + name + "', '" + patr + "', '" + num + "', '" + seria + "', '" + path + "')";

                    oleDbDataAdapter1.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                };
            }
            oleDbConnection1.Close();
            UpdateGrid();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            AddSickForm form = new AddSickForm();
            form.SetTextBoxes(Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value),
                                Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value),
                                Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value),
                                Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value),
                                Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value),
                                Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value));

            form.ShowDialog();
            string surname = form.GetSurname();
            string name = form.GetName();
            string patr = form.GetPatr();
            string num = form.GetNumber();
            string seria = form.GetSeria();
            string path = form.GetPath();
            dataGridView1.CurrentRow.Cells[1].Value = surname;
            dataGridView1.CurrentRow.Cells[2].Value = name;
            dataGridView1.CurrentRow.Cells[3].Value = patr;
            dataGridView1.CurrentRow.Cells[4].Value = num;
            dataGridView1.CurrentRow.Cells[5].Value = seria;
            dataGridView1.CurrentRow.Cells[6].Value = path;
            BindingSource bs = new BindingSource(ds, "Больные");
            dataGridView1.DataSource = bs;
            UpdateGrid();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            string text = "";
            string path = "";
            string passport = "";

            text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            
            passport = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value) + " " + Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
            path = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
            if (path != "" && passport != "")
            {
                Form2 form = new Form2(text, path, passport);
                form.ShowDialog();
            }
        }
    }
}

