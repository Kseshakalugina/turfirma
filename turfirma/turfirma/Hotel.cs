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

namespace turfirma
{
    public partial class Hotel : Form
    {
        // Menu menu = new Menu();
        private static string connectionString = "Data Source = ADCLG1;Initial Catalog =turfirma;" +
               "Integrated Security = true;"; // данные для подключения к базе
        private SqlConnection myConnection; // подключение к базе
        public Hotel()
        {
            InitializeComponent();
        }

        private void Update1(string sql)
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (sender == button2) // добавление продукта
            {
                this.Hide();
                Addhotel addhotel = new Addhotel(true);
                addhotel.ShowDialog();
                this.Show();
                Update1("select Код_отеля,Город, Name_unit,Название, Кол_звезд, Стоимость_проживания, Страна from ОТЕЛЬ, СТРАНА Where Страна=Код_страны");
            }
            else if (sender == button3) // редактирование продукта
            {
                this.Hide();
                Id edit = new Id(true, "ОТЕЛЬ");
                edit.ShowDialog();
                this.Show();
                Update1("select Код_отеля, Город, Name_unit, Название, Кол_звезд, Стоимость_проживания, Страна from ОТЕЛЬ, СТРАНА Where Страна=Код_страны");

            }
            else if (sender == button1) // удаление продукта
            {
                this.Hide();
                Id del = new Id(false, "ОТЕЛЬ");
                del.ShowDialog();
                this.Show();
                Update1("select Код_отеля,Город,Название, Кол_звезд, Стоимость_проживания,  Страна from ОТЕЛЬ, СТРАНА Where Страна=Код_страны");
            }
            else if (sender == button4) // выход в главное меню
            {
                Close();
            }
        }

        private void Hotel_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Код_отеля,Город,Название, Кол_звезд, Стоимость_проживания,  Страна from ОТЕЛЬ, СТРАНА Where Страна=Код_страны";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                dataGridView1.DataSource = data.Tables[0];
            }
        }
    }
}
