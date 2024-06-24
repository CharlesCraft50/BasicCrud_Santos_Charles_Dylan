using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BasicCrud_Santos_Charles_Dylan
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
                pnlAddNewUser.Visible = false;
                pnlEditUser.Visible = false;
            }

            btnSaveNewUser.Click += btnSaveNewUser_Click;
        }

        protected void BindGridview()
        {
            string constr = ConfigurationManager.ConnectionStrings["DB_UserAccountsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_employeeInfo", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridViewUsers.DataSource = dt;
                        GridViewUsers.DataBind();
                    }
                }
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlAddNewUser.Visible = true;
        }

        protected void btnSaveNewUser_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["DB_UserAccountsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO tbl_employeeInfo (LastName, FirstName, Age, Birthday, ContactNumber, Address) VALUES (@LastName, @FirstName, @Age, @Birthday, @ContactNumber, @Address)", con))
                {
                    cmd.Parameters.AddWithValue("@LastName", txtNewLastName.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtNewFirstName.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(txtNewAge.Text));
                    cmd.Parameters.AddWithValue("@Birthday", txtNewBirthday.Text);
                    cmd.Parameters.AddWithValue("@ContactNumber", txtNewContactNumber.Text);
                    cmd.Parameters.AddWithValue("@Address", txtNewAddress.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            pnlAddNewUser.Visible = false;
            BindGridview();
            ClearNewUserFields();
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            Button btnUpdateUser = (Button)sender;
            GridViewRow row = (GridViewRow)btnUpdateUser.NamingContainer;
            int userID = Convert.ToInt32(GridViewUsers.DataKeys[row.RowIndex].Values[0]);
            string lastName = ((TextBox)row.Cells[0].Controls[0]).Text;
            string firstName = ((TextBox)row.Cells[1].Controls[0]).Text;
            int age = int.Parse(((TextBox)row.Cells[2].Controls[0]).Text);
            string birthday = ((TextBox)row.Cells[3].Controls[0]).Text;
            string contactNumber = ((TextBox)row.Cells[4].Controls[0]).Text;
            string address = ((TextBox)row.Cells[5].Controls[0]).Text;

            string constr = ConfigurationManager.ConnectionStrings["DB_UserAccountsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE tbl_employeeInfo SET LastName = @LastName, FirstName = @FirstName, Age = @Age, Birthday = @Birthday, ContactNumber = @ContactNumber, Address = @Address WHERE UserID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Birthday", birthday);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            GridViewUsers.EditIndex = -1;
            BindGridview();
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            BindGridview();
        }

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUsers.EditIndex = e.NewEditIndex;
            BindGridview();
        }

        protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            BindGridview();
        }

        protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < GridViewUsers.Rows.Count)
            {
                int userID = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
                string constr = ConfigurationManager.ConnectionStrings["DB_UserAccountsConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM tbl_employeeInfo WHERE UserID = @UserID", con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                BindGridview();
            }
        }


        protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32
(e.CommandArgument);
                GridViewUsers.EditIndex = index;
                BindGridview();
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewUsers.Rows[index];
                int userID = Convert.ToInt32(GridViewUsers.DataKeys[row.RowIndex].Value);
                string constr = ConfigurationManager.ConnectionStrings["DB_UserAccountsConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM tbl_employeeInfo WHERE UserID = @UserID", con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                BindGridview();
            }
        }

        private void ClearNewUserFields()
        {
            txtNewLastName.Text = "";
            txtNewFirstName.Text = "";
            txtNewAge.Text = "";
            txtNewBirthday.Text = "";
            txtNewContactNumber.Text = "";
            txtNewAddress.Text = "";
        }

        private void ClearEditUserFields()
        {
            txtEditLastName.Text = "";
            txtEditFirstName.Text = "";
            txtEditAge.Text = "";
            txtEditBirthday.Text = "";
            txtEditContactNumber.Text = "";
            txtEditAddress.Text = "";
        }

        protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userID = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Values[0]);
            string lastName = ((TextBox)GridViewUsers.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string firstName = ((TextBox)GridViewUsers.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            int age = int.Parse(((TextBox)GridViewUsers.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            string birthday = ((TextBox)GridViewUsers.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string contactNumber = ((TextBox)GridViewUsers.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string address = ((TextBox)GridViewUsers.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

            string constr = ConfigurationManager.ConnectionStrings["DB_UserAccountsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE tbl_employeeInfo SET LastName = @LastName, FirstName = @FirstName, Age = @Age, Birthday = @Birthday, ContactNumber = @ContactNumber, Address = @Address WHERE UserID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Birthday", birthday);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            GridViewUsers.EditIndex = -1;
            BindGridview();
        }
    }
}
