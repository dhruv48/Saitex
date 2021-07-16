using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ApplicationConfiguration
/// </summary>
public static class ApplicationConfiguration
{

    //catches the connection string

    private static string dbConnectionString;

    // Catches the data providername 

    private static string dbProviderName;

    // store the number of products per page
    //private readonly static int productPerPage;
    //store the product description length for product lists
    //private readonly static int productDescriptionLength;
    // store the name of your shop

   

    // Catches the connection string
    // private static string dbConnectionString;
    // Caches the data provider name
    // private static string dbProviderName;

    static ApplicationConfiguration()
    {
        dbConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        dbProviderName = ConfigurationManager.ConnectionStrings["csTextile"].ProviderName;
    }

    //Return the connection string for the Library database
    public static string DbConnectionString
    {
        get
        {
            return dbConnectionString;
        }
    }

    // Return the data provider name
    public static string DbProviderName
    {
        get
        {
            return dbProviderName;
        }
    }
}
