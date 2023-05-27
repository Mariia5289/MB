using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MB.ModelEF;
using System.IO;

namespace MB
{
    public partial class FormShowMot2 : Form
    {
        public FormShowMot2()
        {
            InitializeComponent();
        }
        private string Pic_Name;
        private List<Table_Motorbike> vsMotorbike = Form1.DB.Table_Motorbike.ToList();

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormShowMot2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//Кнопка назад
        {
            Form1 form = new Form1();
            form.Visible = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//Кнопка добавить
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Заполните все поля Модель и Марка!");
                return;
            }
            try
            {
                Convert.ToInt32(textBox4.Text);
                Convert.ToInt32(textBox5.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("В полях Л/С и Пробег, могут быть только целочисленные данные");
                return;
            }
            try
            {
                Convert.ToSingle(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("В поле цена, могут быть только числа с плавующей точкой");
                return;
            }
            if (!File.Exists(Pic_Name))
            {
                MessageBox.Show("Невозможно найти файл!");
                return;
            }  
            Table_Motorbike NMotorbike = new Table_Motorbike();
            NMotorbike.ID = FLplus1();
            NMotorbike.Brand = textBox1.Text;
            NMotorbike.Model = textBox2.Text;
            NMotorbike.Price = Convert.ToSingle(textBox3.Text);
            NMotorbike.Horsepower = Convert.ToInt32(textBox4.Text);
            NMotorbike.Mileage = Convert.ToInt32(textBox5.Text);
            NMotorbike.Picture = $@"{FLplus1()}{Path.GetExtension(Pic_Name)}";
            File.Copy(Pic_Name, $@"Pictures\{FLplus1()}{Path.GetExtension(Pic_Name)}");
            try
            {
                Form1.DB.Table_Motorbike.Add(NMotorbike);
                Form1.DB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Данные успешно добавлены!");
            Form1 form= new Form1();
            form.Visible = true;
            this.Close();
        }
      


        private void pictureBox1_Click(object sender, EventArgs e)//картинка
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            DialogResult result = openFileDialog.ShowDialog();
            if (DialogResult.OK == result)
            {
                Pic_Name= openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)//Цена
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != ',')
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)//Л/С
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)//Пробег
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
                e.Handled = true;
        }

        private int FLplus1()//находит последний ID и прибавляет 1
        {
            int max = 0;
            foreach (Table_Motorbike TB in vsMotorbike)
                if (max> TB.ID) max = TB.ID;
            return ++max;
        }
       
    }
}
