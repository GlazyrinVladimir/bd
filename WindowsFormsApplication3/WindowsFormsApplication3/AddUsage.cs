using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class AddUsage : Form
    {
        public AddUsage()
        {
            InitializeComponent();
            LoadBoxes();
        }

        private
        string name = "";
        string date = "";
        

        public string GetName()
        {
            return name;
        }

        public string GetDate()
        {
            if (date != "")
                return date.Substring(0, date.Length - 2);
            return date;
        }

        void LoadBoxes()
        {
            DataTable tbl = new DataTable();
            oleDbDataAdapter1.Fill(tbl);
            sickComboBox.DataSource = tbl;
            sickComboBox.DisplayMember = "Фамилия";// столбец для отображения
            sickComboBox.ValueMember = "Код";//столбец с id
            sickComboBox.Text = "";
        }

        private void CloseForm()
        {
#pragma warning disable CS0164 // Отсутствует ссылка на эту метку.
        Application: Close();
#pragma warning restore CS0164 // Отсутствует ссылка на эту метку.
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            name = sickComboBox.Text;
            date = dateTimePicker1.Text;
            CloseForm();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void sickComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
