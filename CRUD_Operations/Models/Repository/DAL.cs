using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Operations.Models;
using System.Data;

namespace CRUD_Operations.Models.Repository
{
    public class DAL
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConfig"].ConnectionString);

        public List<UserRegModel> GetDataList()
        {
            List<UserRegModel> lst = new List<UserRegModel>();
            SqlCommand cmd = new SqlCommand("sp_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable data = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(data);
            foreach(DataRow dr in data.Rows)
            {
                lst.Add(new UserRegModel {
                    id = Convert.ToInt32(dr[0]),
                    email = Convert.ToString(dr[1]),
                    password = Convert.ToString(dr[2]),
                    name = Convert.ToString(dr[3]),
                    role = Convert.ToString(dr[4]),
                });
            }
            return lst;
        }

        public bool InsertData(UserRegModel userReg)
        {
            int i;
            SqlCommand cmd = new SqlCommand("sp_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", userReg.email);
            cmd.Parameters.AddWithValue("@password", userReg.password);
            cmd.Parameters.AddWithValue("@name", userReg.name);
            cmd.Parameters.AddWithValue("@role", userReg.role);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i >= 1;;
        }

        public bool UpdateData(UserRegModel userReg)
        {
            int i;
            SqlCommand cmd = new SqlCommand("sp_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", userReg.id);
            cmd.Parameters.AddWithValue("@email", userReg.email);
            cmd.Parameters.AddWithValue("@password", userReg.password);
            cmd.Parameters.AddWithValue("@name", userReg.name);
            cmd.Parameters.AddWithValue("@role", userReg.role);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i >= 1; ;
        }

        public bool DeleteData(UserRegModel userReg)
        {
            int i;
            SqlCommand cmd = new SqlCommand("sp_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", userReg.id);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i >= 1; ;
        }

        public bool LoginUser(UserRegModel userReg)
        {
            bool isValidUser = false;
            SqlCommand cmd = new SqlCommand("sp_login", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", userReg.email);
            cmd.Parameters.AddWithValue("@password", userReg.password);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    isValidUser = true;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return isValidUser;
        }

        public string GetRole(UserRegModel userReg)
        {
            string role = string.Empty;
            SqlCommand cmd = new SqlCommand("GetRole", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", userReg.email);
            try
            {
                con.Open();
                role = cmd.ExecuteScalar()?.ToString();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new ApplicationException("An error occurred while getting the role.", ex);
            }
            return role;
        }
    }
}