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
    public partial class adminpublishermanagment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        ///add button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfpublisherExists())
            {
                Response.Write("<script>alert('publisher with this id already exist. you cannot add another author with the same author ID'); </script>");


            }
            else
            {
                addNewpublisher();
            }
        }
        ///update button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfpublisherExists())
            {
                updatepublisher();

            }
            else
            {
                Response.Write("<script>alert('publisher does not exist'); </script>");
            }
        }
        ///delete button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfpublisherExists())
            {
                deletepublisher();

            }
            else
            {
                Response.Write("<script>alert('publisher does not exist'); </script>");
            }
        }




        void getpublisherById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl where publisher_id='" + TextBox1.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('invalide publisher id '); </script>");
                }




            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");

            }

        }





        void deletepublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("delete from publisher_master_tbl WHERE publisher_id='" + TextBox1.Text.Trim() + "'", con);



                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());


                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('publisher deleted successfully'); </script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");
            }

        }




        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }



        void updatepublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("update publisher_master_tbl SET publisher_name=@publisher_name WHERE publisher_id='" + TextBox1.Text.Trim() + "'", con);



                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());


                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('author update successfully'); </script>");
                GridView1.DataBind();
                clearForm();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");
            }

        }





        void addNewpublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
               
                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id,publisher_name) values(@publisher_id,@publisher_name)", con);


                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());


                cmd.ExecuteNonQuery();
                con.Close();
                clearForm();
                Response.Write("<script>alert('publisher add successfully'); </script>");
                clearForm();

                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");
            }

        }


        bool checkIfpublisherExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl where publisher_id='" + TextBox1.Text.Trim() + "';", con);

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            getpublisherById();
        }
    }
}