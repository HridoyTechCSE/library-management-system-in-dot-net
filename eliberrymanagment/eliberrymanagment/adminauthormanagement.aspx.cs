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
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        ///add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                Response.Write("<script>alert('author with this id already exist. you cannot add another author with the same author ID'); </script>");


            }
            else {
                addNewAuthor();
            }
        }
        ///update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                updateAuthor();
                
            }
            else
            {
                Response.Write("<script>alert('author does not exist'); </script>");
            }
        }
        ///delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
               deleteAuthor();

            }
            else
            {
                Response.Write("<script>alert('author does not exist'); </script>");
            }
        }
        // go button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorById();
        }





        void getAuthorById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('invalide author id '); </script>");
                }




            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");

            }

        }





        void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("delete from author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'", con);



                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());


                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('author deleted successfully'); </script>");
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



        void updateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("update author_master_tbl SET author_name=@author_name WHERE author_id='"+ TextBox1.Text.Trim()+"'", con);


               
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());


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





        void addNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id,author_name) values(@author_id,@author_name)", con);


                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
                

                cmd.ExecuteNonQuery();
                con.Close();
                clearForm();
                Response.Write("<script>alert('author add successfully'); </script>");
                clearForm();

                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + " '); </script>");
            }

        }


        bool checkIfAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);

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


    }
}