using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using System.Data;

namespace CRUD_Operations.Models.Repository
{
    public class ProjectDAL
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConfig"].ConnectionString);

        public IEnumerable<SelectListItem> GetClients()
        {
            var clients = new List<SelectListItem>();

            using (SqlCommand cmd = new SqlCommand("GetClients", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clients.Add(new SelectListItem
                    {
                        Value = reader["id"].ToString(),
                        Text = reader["name"].ToString()
                    });
                }
                con.Close();
            }
        return clients;
        }

        public bool AddProject(ProjectsModel projects)
        {
            int i;
            SqlCommand cmd = new SqlCommand("InsertProject", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@projectName", projects.ProjectName);
            cmd.Parameters.AddWithValue("@clientId", projects.ClientId);
            con.Open();
            i= cmd.ExecuteNonQuery();
            con.Close();
            return i >= 1; ;
        }

        public List<ProjectsModel> GetProjects()
        {
            var projects = new List<ProjectsModel>();

            using (SqlCommand cmd = new SqlCommand("GetProjects", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    projects.Add(new ProjectsModel
                    {
                        ProjectName = reader["projectname"].ToString(),
                        ClientName = reader["clientname"].ToString(),
                        ClientEmail = reader["clientemail"].ToString(),
                        DateAdded = Convert.ToDateTime(reader["dateadded"])
                    });
                }
                con.Close();
            }
            return projects;
        }

        public List<string> GetProjectsByEmail(string clientEmail)
        {
            List<string> projects = new List<string>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetCLientProjects", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clientemail", clientEmail);
                    SqlDataReader reader = cmd.ExecuteReader();
                   while (reader.Read())
                   {
                        projects.Add(reader["ProjectName"].ToString());
                   }
                    
                }
            }
            finally
            {
                con.Close();
            }

            return projects;
        }

    }
}