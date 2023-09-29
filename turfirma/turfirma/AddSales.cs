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
    public partial class AddSales : Form
    {
        private static string connectionString = "Data Source =ADCLG1;Initial Catalog =turfirma;" +
              "Integrated Security = true;"; // данные для подключения к базе
        public bool change; // вызов функции для добавления/изменения
        public int id; // id изменяемой строки
        public string[] row; // строка, которая будет изменятся
        public AddSales()
        {
            InitializeComponent();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select Фамилия_клиента from КЛИЕНТ";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                conn.Close();
                comboBox1.Items.Add("<Выберите клиента>");
                for (int i = 0; i < data.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox1.Items.Add(data.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString());
                }
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "select Фамилия_сотрудника from СОТРУДНИК";
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                DataSet data1 = new DataSet();
                dataAdapter1.Fill(data1);
                conn.Close();
                comboBox2.Items.Add("<Выберите сотрудника>");
                for (int i = 0; i < data1.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox2.Items.Add(data1.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString());
                }
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "select Наименование_маршрута from МАРШРУТ";
                SqlDataAdapter dataAdapter2= new SqlDataAdapter(cmd2);
                DataSet data2 = new DataSet();
                dataAdapter2.Fill(data2);
                conn.Close();
                comboBox3.Items.Add("<Выберите маршрут>");
                for (int i = 0; i < data2.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox3.Items.Add(data2.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString());
                }
                SqlCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select Цель_путешествия from ПРОДАЖИ";
                SqlDataAdapter dataAdapter3 = new SqlDataAdapter(cmd3);
                DataSet data3 = new DataSet();
                dataAdapter3.Fill(data3);
                conn.Close();
                comboBox4.Items.Add("<Выберите цель путешествия>");
                for (int i = 0; i < data3.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox4.Items.Add(data3.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString());
                }
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
        private void AddRow()
        {
            
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand($"INSERT INTO ПРОДАЖИ(Цель_путешествия,Цена_путевки,Кол_во_прод_путевок, Дата_продажи, Клиент, Сотрудник, Маршрут) values ({comboBox4.SelectedIndex},{int.Parse(textBox2.Text)},{int.Parse(textBox3.Text)},'{dateTimePicker1.Text}',{comboBox1.SelectedIndex},{comboBox2.SelectedIndex}, {comboBox3.SelectedIndex+16})");
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully", " ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Close();
                }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRow();
        }
    }
}
