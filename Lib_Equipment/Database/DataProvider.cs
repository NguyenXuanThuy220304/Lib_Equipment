using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Equipment.Database
{
    public class DataProvider
    {
        // 1. Chuỗi kết nối đến CSDL Lib_EquipmentDB
        private readonly string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Lib_EquipmentDB;Integrated Security=True";

        // Mẫu Singleton Pattern: Giúp chỉ tạo 1 kết nối duy nhất trong suốt vòng đời phần mềm
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        // HÀM 1: ExecuteQuery - Dùng cho lệnh SELECT (Trả về một bảng dữ liệu)
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi truy xuất dữ liệu: " + ex.Message);
                    }
                }
            }
            return dataTable;
        }

        // HÀM 2: ExecuteNonQuery - Dùng cho INSERT, UPDATE, DELETE (Trả về số dòng bị ảnh hưởng)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    data = command.ExecuteNonQuery();
                }
            }
            return data;
        }

        // HÀM 3: ExecuteScalar - Dùng cho COUNT, MAX, SUM... (Trả về 1 ô dữ liệu duy nhất)
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    data = command.ExecuteScalar();
                }
            }
            return data;
        }
    }
}
