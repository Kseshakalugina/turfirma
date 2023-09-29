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
    public partial class AddClients : Form
    {
        private bool add;
        public int id; // id изменяемой строки
        private static string connectionString = "Data Source =ADCLG1;Initial Catalog =turfirma;" +
              "Integrated Security = true;";
        public string[] row;
        public AddClients(bool flag)
        {
            InitializeComponent();
            this.add = flag;
            if (flag)
            {
                button1.Text = "ADD";
                this.Text = "Добавить клиента";
            }
            else if (!flag)
            {
                button1.Text = "EDIT";
                this.Text = "Изменить клиента";
            }
        }

        private void AddRow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO КЛИЕНТ values ('{textBox1.Text}', '{textBox2.Text}','{textBox3.Text}',{int.Parse(textBox4.Text)}, {int.Parse(textBox5.Text)})");
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        private void ChangeRow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"UPDATE КЛИЕНТ SET Фамилия_клиента = {textBox1.Text}, Имя_клиента = '{textBox1.Text}', Отчество_клиента = {textBox1.Text}, Телефон={int.Parse(textBox4.Text)} ,Паспорт={int.Parse(textBox5.Text)} WHERE [Код товара] = {id}");
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
