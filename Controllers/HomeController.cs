using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DANGER.Models;

namespace DANGER.Controllers
{
    public class HomeController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;

        List<EntityEmp> list = new List<EntityEmp>();
        public ActionResult Index()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("SP_MVC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@NAME", "");
                cmd.Parameters.AddWithValue("@EMAIL", "");
                cmd.Parameters.AddWithValue("@PHONE", "");
                cmd.Parameters.AddWithValue("@SALARY", 0);
                cmd.Parameters.AddWithValue("@OPERATION", 1);

                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(new EntityEmp
                    {
                        ID = Convert.ToInt16(sdr["ID"]),
                        Name = sdr["NAME"].ToString(),
                        Phone = sdr["PHONE"].ToString(),
                        Email = sdr["EMAIL"].ToString(),
                        Salary = Convert.ToDecimal(sdr["SALARY"])
                    });

                }
                con.Close();
            }
            return View(list);
        }

        public ActionResult ShowIndex()
        {
            return View();
        }

        public ActionResult InsertData(string Name, string Email, string Phone, string Salary)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand("SP_MVC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@NAME", Name);
                cmd.Parameters.AddWithValue("@EMAIL", Email);
                cmd.Parameters.AddWithValue("@PHONE", Phone);
                cmd.Parameters.AddWithValue("@SALARY", Convert.ToDecimal(Salary));
                cmd.Parameters.AddWithValue("@OPERATION", 2);

                int retval = cmd.ExecuteNonQuery();
               
                con.Close();
            }
            return Content("Data saved successfully!");
        }

        public ActionResult DeleteData(string ID)
        {
            string[] IDs = ID.Split(',');
            if(IDs.Length > 0)
            {
                for (int i = 0; i < IDs.Length; i++)
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand("SP_MVC", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(IDs[i]));
                        cmd.Parameters.AddWithValue("@NAME", "");
                        cmd.Parameters.AddWithValue("@EMAIL", "");
                        cmd.Parameters.AddWithValue("@PHONE", "");
                        cmd.Parameters.AddWithValue("@SALARY", 0);
                        cmd.Parameters.AddWithValue("@OPERATION", 4);

                        int retval = cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }
              
            }
            
            return Content("Data saved successfully!");
        }
    }
}