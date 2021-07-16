using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
/// <summary>
/// Summary description for GenericDataAccess
/// </summary>
public class GenericDataAccess
{
    public GenericDataAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //create and prepares a new DbCommand object on a new connection
    public static DbCommand CreateCommand()
    {
        //obtain the database provider name
        try
        {
            string dataProviderName = ApplicationConfiguration.DbProviderName;
            //obtain the database connection string
            string connectionstring = ApplicationConfiguration.DbConnectionString;
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

            //Obtaion a database specic connection object
            DbConnection conn = factory.CreateConnection();
            //Set the connection string
            conn.ConnectionString = connectionstring;
            //Create a databse specific command object
            DbCommand comm = conn.CreateCommand();
            //Set the command type to stored procedure
            comm.CommandType = CommandType.StoredProcedure;
            //return the initialized command object
            return comm;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static DataTable ExecuteSelectCommand(DbCommand command)
    {
        //the Data Table To Be return
        DataTable table = null;
        DbDataReader reader = null;
        //Execute the command making sure the comnnection gets closed in the end
        try
        {
            //Open the data connection
            command.Connection.Open();
            //Execute the command and save the results in a DataTable
            reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            return table;


        }
        finally
        {
            //Close the connection
            command.Connection.Close();
            if (reader != null)
            {
                reader.Dispose();
                reader.Close();
                reader = null;
            }
        }

    }

    public static DataSet CreateAdapterSelect(string queryString)
    {
        try
        {
            DataSet dataSet = new DataSet();

            string dataProviderName = ApplicationConfiguration.DbProviderName;
            //obtain the database connection string
            string connectionstring = ApplicationConfiguration.DbConnectionString;
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);
            //Obtaion a database specic connection object
            DbConnection conn = factory.CreateConnection();
            //Set the connection string
            conn.ConnectionString = connectionstring;
            //Create a databse specific command object

            conn.Open();
            DbCommand command = factory.CreateCommand();
            command.CommandText = queryString;
            command.Connection = conn;

            DbDataAdapter DataAdptr = factory.CreateDataAdapter();
            DataAdptr.SelectCommand = command;
            //DbCommandBuilder builder = new DbCommandBuilder(adapter);
            DataAdptr.Fill(dataSet);

            return dataSet;
            conn.Dispose();
            conn.Close();
            DataAdptr.Dispose();
            command.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /*********execute an update, delete, or insert command and return the number of affected row *********/
    public static int ExecuteNonQuery(DbCommand command)
    {
        // The number of affected rows
        int affectedRows = -1;
        //Execute the command making sure the connection gets closed in the end
        try
        {
            //open the connection of the command
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            affectedRows = command.ExecuteNonQuery();
            return affectedRows;
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        // return the nubmer of affected rows
    }
    /*********execute an update, delete, or insert command and return the number of affected row *********/


    /*******This is used to calculate max id of the table and save dem in table and den retrun value *****/
    public static int ExecuteNonQueryWithReturnValue(DbCommand command)
    {
        // The number of affected rows
        int affectedRows = -1;
        //Execute the command making sure the connection gets closed in the end
        try
        {
            //open the connection of the command
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            affectedRows = command.ExecuteNonQuery();
            int aa = Convert.ToInt16(command.Parameters["@vc_output"].Value.ToString());
            return aa;
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        // return the nubmer of affected rows
    }
    /*******This is used to calculate max id of the table and save dem in table and den retrun value *****/


    // execute a select command and return a single result as a string
    public static string ExecuteScalar(DbCommand command)
    {
        // The value to be return
        string value = "";
        // Execute the command making sure the connection gets closed in the end
        try
        {
            // open the connection of the command
            command.Connection.Open();
            //Ecxecute the command and get the number of affected rows
            value = command.ExecuteScalar().ToString();
            return value;
        }
        finally
        {
            //Close the connection
            command.Connection.Close();

        }
        //return the result

    }

    public static DataTable ExecuteReader(DbCommand command)
    {
        DataTable table = null;
        DbDataReader reader = null;
        try
        {
            command.Connection.Open();
            reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            return table;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            command.Connection.Close();
            if (reader != null)
            {
                reader.Dispose();
                reader.Close();
                reader = null;
            }
        }
    }

    public static void bindDropDownList(DropDownList ddl, string strSQL, string strDataValueFields, string strDataTextFields)
    {
        DbCommand comm = null;
        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = strSQL;
        comm.CommandType = CommandType.StoredProcedure;

        DataTable result;
        try
        {
            result = GenericDataAccess.ExecuteReader(comm);
            ddl.DataSource = result;
            ddl.DataValueField = strDataValueFields;
            ddl.DataTextField = strDataTextFields;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("---------Select----------", ""));

        }
        catch
        {
            throw;
        }
        finally
        {
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
        }
    }

}
