using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace turfirma
{
    public partial class Addhotel : Form
    {
        public int id; // id изменяемой строки
        private static string connectionString = "Data Source = ADCLG1;Initial Catalog =turfirma;" +
               "Integrated Security = true;"; // данные для подключения к базе
        private SqlConnection myConnection;
        private bool add;
        public string[] row; // строка, которая будет изменятся
        public Addhotel(bool flag)
        {
            InitializeComponent();
          
        }

        private void AddRow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO ОТЕЛЬ values ('{textBox1.Text}', '{textBox2.Text}','{textBox3.Text}', '{int.Parse(textBox4.Text)}',{int.Parse(textBox5.Text)})");
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        private void ChangeRow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"UPDATE ОТЕЛЬ SET Город = {textBox1.Text}, Название = '{textBox2.Text}', Кол_звезд = {int.Parse(textBox3.Text)}, Стоимость_проживания = {int.Parse(textBox4.Text)},Страна={int.Parse(textBox5.Text)}   WHERE [Код_отеля] = {id}");
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (add)
            {
                AddRow();
            }
            else if (!add)
            {
                ChangeRow();
            }
        }
        public int setId
        {
            set { id = value; }
        }
        public string[] Row
        {
            set { row = value; }
        }
    }
}
