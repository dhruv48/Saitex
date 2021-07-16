using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Data;

using System.Data.SqlClient;

using System.Collections.Specialized;

using System.Text;
public partial class arar : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();

    SqlCommand cmd = new SqlCommand();

    string connectionString = "Data Source=.; Initial Catalog=Master; Integrated Security=True";

 

    protected void Page_Load(object sender, EventArgs e)

    {

        if (!Page.IsPostBack)

            BindGridView();

    }

   

    private void BindGridView()

    {

        con = new SqlConnection(connectionString);

        cmd.Connection = con;

        cmd.CommandText = "Select * from employee";

        con.Open();

        GridView1.DataSource = cmd.ExecuteReader();

        GridView1.DataBind();

        con.Close();

    }

 

    private void DeleteRecords(StringCollection sc)

    {

        con = new SqlConnection(connectionString);

        StringBuilder sb = new StringBuilder(string.Empty);

        foreach (string item in sc)

        {

            const string sqlStatement = "DELETE FROM employee WHERE EmpId";

            sb.AppendFormat("{0}='{1}'; ", sqlStatement, item);

        }

        try

        {

            con.Open();

            SqlCommand cmd = new SqlCommand(sb.ToString(), con);
            
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();

        }

        catch (System.Data.SqlClient.SqlException ex)

        {

            string msg = "Deletion Error:";

            msg += ex.Message;

            throw new Exception(msg);

        }

        finally

        {

            con.Close();

        }

    }

    protected void ButtonDelete_Click(object sender, EventArgs e)

    {

        StringCollection sc = new StringCollection();

        string id = string.Empty;

        for (int i = 0; i < GridView1.Rows.Count; i++)

        {

            CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");

            if (cb != null)

            {

                if (cb.Checked)

                {

                    id = GridView1.Rows[i].Cells[1].Text;

                    sc.Add(id);

                }

            }

        }

        DeleteRecords(sc);

        BindGridView();

    }

 

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)

    {

        if (e.Row.RowType == DataControlRowType.Header)

        {

            Button b = (Button)e.Row.FindControl("ButtonDelete");

            b.Attributes.Add("onclick", "return ConfirmOnDelete();");

        }

    }

}