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
    public partial class AddMedicineUse : Form
    {
        public AddMedicineUse()
        {
            InitializeComponent();
            LoadBoxes();
        }

        public void SetBoxes(string s1, string s2, string s3)
        {
            usageComboBox.Text = s1;
            medicineComboBox.Text = s2;
            periodTextBox.Text = s3;
            old1 = s1;
            old2 = s2;
            old3 = s3;
        }

        private
            string medicine = "";
            string usage = "";
            string period = "";
        string old1 = "";
        string old2 = "";
        string old3 = "";

        public string GetMedicine()
        {
            return medicine;
        }

        public string GetUsage()
        {
            return usage;
        }

        public string GetPeriod()
        {
            return period;
        }

        void LoadBoxes()
        {
            DataTable tbl = new DataTable();
            oleDbDataAdapter1.Fill(tbl);
            medicineComboBox.DataSource = tbl;
            medicineComboBox.DisplayMember = "Название";// столбец для отображения
            medicineComboBox.ValueMember = "Код";//столбец с id
            medicineComboBox.Text = "";

            DataTable tbl2 = new DataTable();
            oleDbDataAdapter2.Fill(tbl2);
            usageComboBox.DataSource = tbl2;
            usageComboBox.DisplayMember = "Код";// столбец для отображения
            usageComboBox.ValueMember = "Код";//столбец с id
            usageComboBox.Text = "";
       
        }

        private void CloseForm()
        {

#pragma warning disable CS0164 // Отсутствует ссылка на эту метку.
        Application: Close();
#pragma warning restore CS0164 // Отсутствует ссылка на эту метку.
        }

        private void usageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            medicine = medicineComboBox.Text;
            usage = usageComboBox.Text;
            period = periodTextBox.Text;
            CloseForm();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            medicine = old2;
            usage = old1;
            period = old3;
            CloseForm();
        }

        private void AddMedicineUse_Load(object sender, EventArgs e)
        {

        }
    }
}
