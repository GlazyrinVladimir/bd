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
    
    public partial class Form2 : Form
    {
        string name;
        public Form2(string s1, string s2, string s3)
        {
            name = s1;
            InitializeComponent();
            LoadPictureBox(s2);
            label1.Text = "паспорт: " + s3;
        }

        void LoadPictureBox(string path)
        {
            Bitmap image;
            try
            {
                path = System.IO.Directory.GetCurrentDirectory() + path;
                image = new Bitmap(@path);
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Invalidate();
            }
            catch(Exception)
            {
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_4_2DataSet.Больные". При необходимости она может быть перемещена или удалена.
            this.БольныеTableAdapter.Fill(this._4_2DataSet.Больные, name);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
