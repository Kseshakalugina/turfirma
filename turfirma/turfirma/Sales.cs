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
using ClassLibrary1;

namespace turfirma
{
    public partial class Sales : Form
    {
        
       
        private static string connectionString = "Data Source =ADCLG1;Initial Catalog =turfirma;" +
               "Integrated Security = true;"; // данные для подключения к базе
        private SqlConnection myConnection; // подключение к базе
        
        public Sales()
        {
            InitializeComponent();
        }
        private void Update1(string sql) // обновление
        {
            dataGridView1.Columns.Clear();
            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            SqlCommand cmd = myConnection.CreateCommand(); // создается команда
            cmd.CommandText = sql; // добавление текста запроса 
            SqlDataAdapter adapter = new SqlDataAdapter(cmd); // мост между DataSet и SQL Server
            DataSet m_set = new DataSet(); // хранилище таблицы
            adapter.Fill(m_set); // заполнение DataSet
            dataGridView1.DataSource = m_set.Tables[0]; // заполнение dataGridView1 из таблицы
            myConnection.Close();
        }

        private void Sales_Load(object sender, EventArgs e)
        {

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Код_продаж, Цель_путешествия, Цена_путевки, Кол_во_прод_путевок, Дата_продажи,Фамилия_клиента, Фамилия_сотрудника, Наименование_маршрута from ПРОДАЖИ, КЛИЕНТ, МАРШРУТ, СОТРУДНИК where Код_клиента = Клиент and Сотрудник=Код_сотрудника and Код_маршрута=Маршрут ";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                dataGridView1.DataSource = data.Tables[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sender == button2)
            {
                Hide();
                AddSales addSales = new AddSales();
                addSales.ShowDialog();
                Update1("select Код_продаж, Цель_путешествия, Цена_путевки, Кол_во_прод_путевок, Дата_продажи,Фамилия_клиента, Фамилия_сотрудника, Наименование_маршрута from ПРОДАЖИ, КЛИЕНТ, МАРШРУТ, СОТРУДНИК where Код_клиента = Клиент and Сотрудник=Код_сотрудника and Код_маршрута=Маршрут ");
            }
            else if (sender==button1)
            {
                Close();
            }
        }

        
        
    }
}
