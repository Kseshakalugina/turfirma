using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary1
{

    public class Database
    {
        private static string connectionString = "Data Source =ADCLG1;Initial Catalog =turfirma;" +
              "Integrated Security = true;";

        /// <summary>
        /// Вся прибыль с продаж
        /// </summary>
        /// <returns></returns>
        public double Zapros(){
            using (SqlConnection sqlConnection=new SqlConnection(connectionString)) {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText= " select sum (Цена_путевки*Кол_во_прод_путевок) from ПРОДАЖИ ";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlConnection.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }
        /// <summary>
        /// Отель с самым дорогим проживанием
        /// </summary>
        /// <returns></returns>
        public double Max()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = " select Название, Стоимость_проживания From ОТЕЛЬ where  Стоимость_проживания=(select max(Стоимость_проживания) From ОТЕЛЬ)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlConnection.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }
        /// <summary>
        /// Стоимость самой дорогой визы
        /// </summary>
        /// <returns></returns>
        public double Max_viza()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = " select Название_страны, Стоимость_визы From СТРАНА where  Стоимость_визы=(select max(Стоимость_визы) From СТРАНА)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlConnection.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }
        /// <summary>
        /// Наиболее продаваемый маршрут
        /// </summary>
        /// <returns></returns>
        public double Max_count()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = " select Наименование_маршрута From ПРОДАЖИ,МАРШРУТ where Маршрут=Код_маршрута and Кол_во_прод_путевок=(select count(Кол_во_прод_путевок) From ПРОДАЖИ))";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlConnection.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }

    }
}


