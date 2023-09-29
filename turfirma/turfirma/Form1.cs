using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace turfirma
{
    public partial class Form1 : Form
    {

        private static string connectionString = "Data Source = ADCLG1;Initial Catalog =turfirma;" +
               "Integrated Security = true;"; // данные для подключения к базе
        
        private string text = String.Empty;
        private int attempts = 0;
        private int time;
        private int time_whole = 180;
        public Form1()
        {
            InitializeComponent();
            

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Not all fields are filled in", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandText = "select Фамилия_сотрудника, Имя_сотрудника,Отчество_сотрудника, Должность, Логин, Пароль, Код_сотрудника from СОТРУДНИК";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataSet data = new DataSet();
                    dataAdapter.Fill(data);
                    sqlcon.Close();
                    bool flag = false;
                    textBox1.Text = textBox1.Text.Trim(' ');
                    textBox2.Text = textBox2.Text.Trim(' ');
                    for (int i = 0; i < data.Tables[0].Columns[0].Table.Rows.Count; i++)
                    {
                        if (textBox1.Text == data.Tables[0].Columns[2].Table.Rows[i].ItemArray[4].ToString().Trim(' ') && textBox2.Text == data.Tables[0].Columns[3].Table.Rows[i].ItemArray[5].ToString().Trim(' '))
                        {
                            sqlcon.Open();
                            SqlCommand cmd_in = sqlcon.CreateCommand();
                            cmd.CommandText = $"INSERT into ИСТОРИЯ VALUES ('{DateTime.Now}', '{(data.Tables[0].Columns[2].Table.Rows[i].ItemArray[6].ToString())}', 'True' )";
                            cmd.ExecuteNonQuery();
                            sqlcon.Close();
                            flag = true;
                            Menu menu = new Menu(Convert.ToInt32(data.Tables[0].Columns[2].Table.Rows[i].ItemArray[6]));
                            menu.ShowDialog();



                        }
                    }
                    if (!flag)
                    {
                        MessageBox.Show("Invalid username or password", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        textBox2.Clear();
                        attempts++;
                        for (int i = 0; i < data.Tables[0].Columns[0].Table.Rows.Count; i++)
                        {
                            if (data.Tables[0].Columns[0].Table.Rows[i].ItemArray[4].ToString().Trim(' ') == textBox1.Text)
                            {
                                sqlcon.Open();
                                SqlCommand cmd_in = sqlcon.CreateCommand();
                                cmd.CommandText = $"INSERT into СОТРУДНИК VALUES ('{DateTime.Now}','{(data.Tables[0].Columns[2].Table.Rows[i].ItemArray[4].ToString())}', 'false' )";
                                cmd.ExecuteNonQuery();
                                sqlcon.Close();
                            }
                        }

                    }
                    if (attempts == 1)
                    {
                        button2.Visible = true;
                        textBox3.Visible = true;
                        pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                    }
                    if (attempts == 2)
                    {
                        textBox3.Clear();
                        button1.Enabled = false;
                        timer1.Start();
                        textBox3.Visible = false;
                        label4.Visible = true;
                        label4.Text = $"BLOCKING! Time left: {time_whole - time}";
                    }
                    if (attempts == 3)
                    {
                        System.Windows.Forms.Application.Restart();
                    }
                }
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         new Font("Arial", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label4.Text = $"BLOCKING! Time left: {time_whole - time} sec";
            if (time == 180)
            {
                timer1.Stop();
                button1.Enabled = true;
                label4.Visible = false;
            }
        }
    }

}
