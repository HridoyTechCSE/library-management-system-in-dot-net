﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eliberrymanagment
{
    public partial class userlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl where member_id='" + TextBox1.Text.Trim() + "' AND PASSWORD='"+ TextBox2.Text.Trim() +"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Write("<script>alert('login successfull'); </script>");
                        Session["username"] = dr.GetValue(1).ToString();
                        Session["fullname"] = dr.GetValue(2).ToString();
                        Session["role"] = "user";
                        Session["status"] = "pending";
                    }
                    Response.Redirect("homepage.aspx");

                }
                else {
                    Response.Write("<script>alert('invalide user'); </script>");
                }



            }
            catch (Exception ex)
            {

            }




          ///  Response.Write("<script>alert('button click  '); </script>");
        }
    }
}