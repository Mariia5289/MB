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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Model1 DB = new Model1();

        private void Form1_Load(object sender, EventArgs e)
        {
            tableMotorbikeBindingSource.DataSource = DB.Table_Motorbike.ToList();

            if (DB.Table_Motorbike.ToList().Count == 0) return;
            int button1 = (int)dataGridView1.CurrentRow.Cells[0].Value;
            pictureBox1.Image = Image.FromFile($@"Pictures\{DB.Table_Motorbike.Find(button1).Picture}");

        }

        

        private void buttonAdd_Click(object sender, EventArgs e)//Кнопка добавить
        {
            FormShowMot2 form= new FormShowMot2();
            this.Visible = false;
            form.Show();
        }

        private void buttonDel_Click(object sender, EventArgs e)//Кнопка удалить
        {
            if (DB.Table_Motorbike.ToList().Count == 0)
            {
                MessageBox.Show("Данные отсутствуют!");
                return;
            }
            Table_Motorbike CurrentMoto = DB.Table_Motorbike.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show(
                $@"Вы действитьно хотите удалить объект с ID - {CurrentMoto.ID}",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DB.Table_Motorbike.Remove(CurrentMoto);
                    DB.SaveChanges();
                    File.Delete($@"Pictures\ {CurrentMoto.Picture}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    tableMotorbikeBindingSource.DataSource = DB.Table_Motorbike.ToList();
                    pictureBox1.Image= null;
                }
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)//Картинки
        {
            if (DB.Table_Motorbike.ToList().Count == 0) return;
            int button1 = (int)dataGridView1.CurrentRow.Cells[0].Value;
            pictureBox1.Image = Image.FromFile($@"Pictures\{DB.Table_Motorbike.Find(button1).Picture}");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
