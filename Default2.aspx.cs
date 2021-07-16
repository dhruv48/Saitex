using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Common;

using System.Windows;
using System.Net.NetworkInformation;
using System.Xml;
public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowData();
        }

        string newTitle = GetMACAddress();
        string oldTitle = readXML();
        lblmacaddressStored.Text = newTitle;
        lblmacaddressNew.Text = oldTitle;

        if (!string.IsNullOrEmpty(oldTitle))
        {
            if (oldTitle.Equals(newTitle))
            {
                
            }
            else 
            {
               
            }
        
        }
        else 
        {

            createXML(newTitle);
        }
        




    }

    private void ShowData()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand("sampledatapack.Fetchdata123", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Return value parameter has to be added first !
                    command.Parameters.Add("p_recordset", OracleType.Cursor).Direction = ParameterDirection.Output;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataTable myset = new DataTable();
                            myset.Load(reader);
                            GridView1.DataSource = myset;
                            GridView1.DataBind();
                            // Read the current record's fields
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    //First Way to get MAC Address
    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }  
    public void createXML(string m)
    {
        using (XmlWriter writer = XmlWriter.Create("Module/Admin/Help1/books.xml"))
         {
          writer.WriteStartElement("book");
          writer.WriteElementString("title", m);   
          writer.WriteEndElement();
          writer.Flush();
       }
    }
    public string readXML()
    {
        string title = string.Empty;      
        var dt = new DataSet();      
        dt.ReadXml(MapPath("Module/Admin/Help1/books.xml"));
        if (dt.Tables[0].Rows.Count > 0 && dt != null)
        {
            title = dt.Tables[0].Rows[0]["title"].ToString();
        }
        return title;

    }

}
