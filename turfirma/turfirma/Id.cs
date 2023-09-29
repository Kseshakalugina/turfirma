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
    public partial class Id : Form
    {
        private static string connectionString = "Data Source = ADCLG1;Initial Catalog =turfirma;" +
               "Integrated Security = true;"; // данные для подключения к базе
        private SqlConnection myConnection; // подключение к базе
        private bool edit;
        private int id;
        private string name_table;
        public Id(bool flag, string name_table)
        {
            InitializeComponent();
        }
        public int getId
        {
            get { return id; }

        }

        public string getName
        {
            get { return name_table; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                string message = "Вы действительно хотите редактировать выбранную запись?";
                if (MessageBox.Show(message, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                id = Convert.ToInt32(textBox1.Text);
                Close();
            }
            else if (!edit)
            {
                string message = "Вы действительно хотите удалить выбранную запись?";
                if (MessageBox.Show(message, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (name_table == "КЛИЕНТ")
                {
                    myConnection = new SqlConnection(connectionString);
                    myConnection.Open();
                    string cmdDelFromTovari = "Delete from КЛИЕНТ where Код_клиента = @code";
                    SqlCommand cmd1 = new SqlCommand(cmdDelFromTovari, myConnection);
                    SqlParameter pr1 = new SqlParameter("@code", textBox1.Text);
                    cmd1.Parameters.Add(pr1); // добавление параметра в команду
                    cmd1.ExecuteNonQuery(); // выполнение запроса 
                    myConnection.Close();
                }
                else if (name_table == "ОТЕЛЬ")
                {
                    myConnection = new SqlConnection(connectionString);
                    myConnection.Open();
                    string cmdDelFromTovari = "Delete from ОТЕЛЬ where Код_отеля = @code";
                    SqlCommand cmd1 = new SqlCommand(cmdDelFromTovari, myConnection);
                    SqlParameter pr1 = new SqlParameter("@code", textBox1.Text);
                    cmd1.Parameters.Add(pr1); // добавление параметра в команду
                    cmd1.ExecuteNonQuery(); // выполнение запроса 
                    myConnection.Close();
                }
                Close();
            }
        }

        private void Id_Load(object sender, EventArgs e)
        {
            if (name_table == "ОТЕЛЬ")
            {
                label1.Text = "Введите код отеля";
            }
            else if (name_table == "КЛИЕНТ")
            {
                label1.Text = "Введите код клиента";
            }
        }
    }
}
