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
    public partial class AddSickForm : Form
    {
        public void SetTextBoxes(string s1, string s2, string s3, string s4, string s5, string s6)
        {
            surnameBox.Text = s1;
            nameBox.Text = s2;
            patrBox.Text = s3;
            numberBox.Text = s4;
            seriaBox.Text = s5;
            filePath = s6;
            loadPicture(s6);

            old1 = s1;
            old2 = s2;
            old3 = s3;
            old4 = s4;
            old5 = s5;
            old6 = s6;
        }

        public AddSickForm()
        {
            InitializeComponent();
        }

        public string GetSurname()
        {
            return surname;
        }

        public string GetName()
        {
            return name;
        }

        public string GetPatr()
        {
            return patr;
        }

        public string GetNumber()
        {
            return number;
        }

        public string GetSeria()
        {
            return seria;
        }

        public string GetPath()
        {
            return filePath;
        }

        private
        string surname = "";
        string name = "";
        string patr = "";
        string number = "";
        string seria = "";
        string filePath = "";
        string old1 = "";
        string old2 = "";
        string old3 = "";
        string old4 = "";
        string old5 = "";
        string old6 = "";


        private void CloseForm()
        {
 
#pragma warning disable CS0164 // Отсутствует ссылка на эту метку.
        Application: Close();
#pragma warning restore CS0164 // Отсутствует ссылка на эту метку.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            surname = surnameBox.Text;
            name = nameBox.Text;
            patr = patrBox.Text;
            number = numberBox.Text;
            seria = seriaBox.Text;
            CloseForm();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            surname = old1;
            name = old2;
            patr = old3;
            number = old4;
            seria = old5;
            filePath = old6;
            CloseForm();
        }
        private void loadPicture(string path)
        {
            Bitmap image; //Bitmap для открываемого изображения
            try
            {
                path = System.IO.Directory.GetCurrentDirectory() + path;
                image = new Bitmap(@path);
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Invalidate();
            }
            catch (Exception)
            { }
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            openFileDialog1.ShowDialog();
            filePath = openFileDialog1.FileName;
            loadPicture(openFileDialog1.FileName);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void AddSickForm_Load(object sender, EventArgs e)
        {

        }
    }
}
