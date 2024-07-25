using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Operations.Models;
using System.Data;
using System.Web.Mvc;

namespace CRUD_Operations.Models.Repository
{
    public class TicketDAL
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConfig"].ConnectionString);

        public bool InsertTicket(QueryModel ticket)
        {
                int i;
                SqlCommand cmd = new SqlCommand("InsertTicket", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectName", ticket.ProjectName);
                cmd.Parameters.AddWithValue("@Subject", ticket.Subject);
                cmd.Parameters.AddWithValue("@Message", ticket.Message);
                cmd.Parameters.AddWithValue("@Screenshot", ticket.FileUpload);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
                return i >= 1; 
        }

        public List<QueryModel> GetQueries()
        {
            List<QueryModel> list = new List<QueryModel>();
            SqlCommand cmd = new SqlCommand("GetQueries", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);
            foreach(DataRow dr in data.Rows)
            {
                list.Add(new QueryModel
                {
                    TicketNumber = Convert.ToInt32(dr[0]),
                    ProjectName = Convert.ToString(dr[1]),
                    Subject = Convert.ToString(dr[2]),
                    Message = Convert.ToString(dr[3]),
                    Priority = Convert.ToString(dr[4]),
                    FileUpload = Convert.ToString(dr[5]),
                    QueryRaiseDate = Convert.ToDateTime(dr[6]),
                    IsActive = Convert.ToBoolean(dr[7]),
                    AssignedTo = Convert.ToString(dr[8])
                });
            }
            return list;
        }

        public bool AddressQuery(QueryModel ticket)
        {
            int i;
            SqlCommand cmd = new SqlCommand("AddressQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TicketNumber", ticket.TicketNumber); 
            cmd.Parameters.AddWithValue("@Priority", ticket.Priority); 
            cmd.Parameters.AddWithValue("@isActive", ticket.IsActive);
            cmd.Parameters.AddWithValue("@AssignedTo", ticket.AssignedTo);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            return i >= 1; ;
        }

        public IEnumerable<SelectListItem>GetDevs()
        {
            var devs = new List<SelectListItem>();

            using (SqlCommand cmd = new SqlCommand("GetDevs", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    devs.Add(new SelectListItem
                    {
                        Value = reader["id"].ToString(),  
                        Text = reader["name"].ToString()
                    });
                }
                con.Close();
            }
            return devs;
        }

    }
}