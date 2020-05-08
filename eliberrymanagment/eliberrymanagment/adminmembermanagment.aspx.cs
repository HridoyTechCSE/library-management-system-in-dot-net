using System;
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
    public partial class adminmembermanagment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        ///go button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            getMemberById();
            
        }
        ///active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updateMemberStatusById("active");
            GridView1.DataBind();
        }
        ///deactive button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updateMemberStatusById("deactive");
            GridView1.DataBind();
        }
        ///pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updateMemberStatusById("pending");
            GridView1.DataBind();
        }







        bool checkIMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl where member_id='" + TextBox1.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }




            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");
                return false;
            }
        }




        void deleteMemberById()
        {
            if (checkIMemberExists())
            {


                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("delete from member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "'", con);



                    cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());


                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('member deleted successfully'); </script>");
                    clearForm();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {

                    Response.Write("<script>alert('" + ex.Message + " '); </script>");
                }


                
            }
            else
            {
                Response.Write("<script>alert('member id con not be blank'); </script>");
            }

            
        }





        void clearForm()
        {
           
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox3.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";

            TextBox6.Text = "";
        }



        void getMemberById()
        {

            if (checkIMemberExists())
            {

                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl where member_id='" + TextBox1.Text.Trim() + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            TextBox2.Text = dr.GetValue(0).ToString();
                            TextBox7.Text = dr.GetValue(10).ToString();
                            TextBox8.Text = dr.GetValue(1).ToString();
                            TextBox3.Text = dr.GetValue(3).ToString();
                            TextBox9.Text = dr.GetValue(5).ToString();
                            TextBox10.Text = dr.GetValue(6).ToString();
                            TextBox11.Text = dr.GetValue(7).ToString();

                            TextBox6.Text = dr.GetValue(8).ToString();



                        }


                    }
                    else
                    {

                    }



                }
                catch (Exception ex)
                {

                }
            }

            else {
                Response.Write("<script>alert('invalide user'); </script>");
            }
                
        }



        void updateMemberStatusById(string status)
        {

            if (checkIMemberExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status = '" + status + "' WHERE member_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Member status updated')</script>");

                }
                catch (Exception ex)
                {

                }
            }
            else {
                Response.Write("<script>alert('invalied member id')</script>");
            }

          
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteMemberById();
        }
    }
}