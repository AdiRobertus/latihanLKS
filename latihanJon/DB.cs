using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;

namespace latihanJon
{
    
    public class DB
    {
        private static SqlConnection con;
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PC_XX;Integrated Security=True;";
        private static bool IsOpen;

        public static void open()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                IsOpen = true;
            }
            catch
            {
                IsOpen = false;
                return;
            }
        }

        public static string SHA256Hash(string text)
        {
            string hashPass = default(string);

            using (HashAlgorithm hash = new SHA256Managed())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var result = hash.ComputeHash(bytes);
                hashPass = string.Join(string.Empty, result.Select(x => x.ToString("x2")));
            }
            return hashPass;
        }

        public static DataSet Login(string username, string password)
        {
            if (!IsOpen)
                open();
            string hash = SHA256Hash(password);

            string sql = $"SELECT * FROM Employee WHERE Username = '{username}' AND Password = '{hash}'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(new SqlCommand(sql, con));
            da.Fill(ds);

            return ds;
        }

        public static DataSet GetDataSet(string sql)
        {
            if (!IsOpen) open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(new SqlCommand(sql, con));
            da.Fill(ds);

            return ds;
        }

        public static void ExecuNoQue(string sql)
         {
             if (!IsOpen) open();

             SqlCommand cmd = new SqlCommand(sql, con);
             cmd.ExecuteNonQuery();
         }
    }
}
