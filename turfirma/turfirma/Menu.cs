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
    public partial class Menu : Form
    {
        private static string connectionString = "Data Source = ADCLG1;Initial Catalog =turfirma;" +
              "Integrated Security = true;"; // данные для подключения к базе
      
        private int id;
        public Menu(int id)
        {
            InitializeComponent();
            this.id = id;
        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (sender==button1)
            {
                this.Hide();
                Hotel hotel = new Hotel();
                hotel.ShowDialog();
                this.Show();

            }
            else if (sender==button2)
            {
                this.Hide();
                Sales sales = new Sales();
                sales.ShowDialog();
                this.Show();
            }
            else if (sender==button3)
            {
                this.Hide();
                Client clients = new Client();
                clients.ShowDialog();
                this.Show();

            }
            else if (sender==button4)
            {
                this.Hide();
                History history = new History();
                history.ShowDialog();
                this.Show();
            }
            else if (sender==button5)
            {
                Close();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = $"select Фамилия_сотрудника, Имя_сотрудника,Отчество_сотрудника, Должности, Логин, Пароль, Код_сотрудника from СОТРУДНИК,ДОЛЖНОСТЬ  where Код_сотрудника = {id} and Код_должности=Должность";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                label1.Text = data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0].ToString();
                label2.Text = data.Tables[0].Columns[0].Table.Rows[0].ItemArray[1].ToString();
                label3.Text = data.Tables[0].Columns[0].Table.Rows[0].ItemArray[3].ToString();
            }
        }
    }

}
