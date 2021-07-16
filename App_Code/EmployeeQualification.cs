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
using System.Data.OracleClient;
/// <summary>
/// Summary description for Class1
/// </summary>
public class EmployeeQualification
{
    public EmployeeQualification()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GetData(int IN_EMPMASTERID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "select * from tblEmployeeQualification where ch_DeleteStatus=0 and IN_EMPLOYEEMASTERID=" + IN_EMPMASTERID;
        comm.CommandType = CommandType.Text;

        //DbParameter param = comm.CreateParameter();
        //param = comm.CreateParameter();
        //param.ParameterName = "IN_EMPMASTERID";
        //param.DbType = DbType.Int32;
        //param.Value = IN_EMPMASTERID;
        //comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteReader(comm);
    }
    public static bool Insert(int EmpMasId, int EmpQualId, string Exam, string Board, string school, string PassingYear, string Grade, string Pecentage, char Status)
    {
        try
        {
            DbCommand comm = GenericDataAccess.CreateCommand();
            comm.CommandText = "sp_Insert_tblEmpQualification";
            comm.CommandType = CommandType.StoredProcedure;
            //1
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "VC_EXAM";
            param.DbType = DbType.String;
            param.Value = Exam.Trim();
            comm.Parameters.Add(param);
            //2
            param = comm.CreateParameter();
            param.ParameterName = "VC_BOARDUNIVERSITY";
            param.DbType = DbType.String;
            param.Value = Board.Trim();
            comm.Parameters.Add(param);
            //3
            param = comm.CreateParameter();
            param.ParameterName = "VC_SCHOOLCOLLEGE";
            param.DbType = DbType.String;
            param.Value = school.Trim();
            comm.Parameters.Add(param);
            //4
            param = comm.CreateParameter();
            param.ParameterName = "CH_PASSINGYEAR";
            param.DbType = DbType.String;
            param.Value = PassingYear.Trim();
            comm.Parameters.Add(param);
            //5
            param = comm.CreateParameter();
            param.ParameterName = "CH_GRADE";
            param.DbType = DbType.String;
            param.Value = Grade.Trim();
            comm.Parameters.Add(param);
            //6
            param = comm.CreateParameter();
            param.ParameterName = "CH_PERCENT";
            param.DbType = DbType.String;
            param.Value = Pecentage.Trim();
            comm.Parameters.Add(param);
            //7
            param = comm.CreateParameter();
            param.ParameterName = "IN_EMPLOYEEMASTERID";
            param.DbType = DbType.Int32;
            param.Value = EmpMasId;
            comm.Parameters.Add(param);
            //8
            param = comm.CreateParameter();
            param.ParameterName = "DT_UPDATED";
            param.DbType = DbType.DateTime;
            param.Value = System.DateTime.Now.Date;
            comm.Parameters.Add(param);
            //9
            param = comm.CreateParameter();
            param.ParameterName = "DT_CRTEATED";
            param.DbType = DbType.DateTime;
            param.Value = System.DateTime.Now.Date;
            comm.Parameters.Add(param);
            //10
            param = comm.CreateParameter();
            param.ParameterName = "CH_STATUS";
            param.DbType = DbType.String;
            param.Value = Status;
            comm.Parameters.Add(param);
            //11
            param = comm.CreateParameter();
            param.ParameterName = "IN_EMPLOYEEQUALIFICATIONID";
            param.DbType = DbType.Int32;
            param.Value = EmpQualId + 1;
            comm.Parameters.Add(param); 
            //12
            param = comm.CreateParameter();
            param.ParameterName = "ch_DeleteStatus";
            param.DbType = DbType.String;
            param.Value = "0";
            comm.Parameters.Add(param);
            int iResult = GenericDataAccess.ExecuteNonQuery(comm);
            if (iResult > 0)
                return true;
            else
                return false;
        }       
        catch (Exception ex)
        { throw ex; }
       
    }
    public static bool Delete(int EmpMasId, int EmpQualId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_Delete_tblEmpQualification";
        comm.CommandType = CommandType.StoredProcedure;

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "IN_EMPLOYEEMASTERID";
        param.DbType = DbType.Int32;
        param.Value = EmpMasId;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "DT_UPDATED";
        param.DbType = DbType.DateTime;
        param.Value = System.DateTime.Now.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "IN_EMPLOYEEQUALIFICATIONID";
        param.DbType = DbType.Int32;
        param.Value = EmpQualId;
        comm.Parameters.Add(param);
        int iResult = GenericDataAccess.ExecuteNonQuery(comm);
        if (iResult > 0)
            return true;
        else
            return false;
    }
    public static bool Update(int EmpMasId, int EmpQualId, string Exam, string Board, string school, string PassingYear, string Grade, string Pecentage, char Status)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_Update_tblEmpQualification";
        comm.CommandType = CommandType.StoredProcedure;
        //1
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "VC_EXAM";
        param.DbType = DbType.String;
        param.Value = Exam.Trim();
        comm.Parameters.Add(param);
        //2
        param = comm.CreateParameter();
        param.ParameterName = "VC_BOARDUNIVERSITY";
        param.DbType = DbType.String;
        param.Value = Board.Trim();
        comm.Parameters.Add(param);
        //3
        param = comm.CreateParameter();
        param.ParameterName = "VC_SCHOOLCOLLEGE";
        param.DbType = DbType.String;
        param.Value = school.Trim();
        comm.Parameters.Add(param);
        //4
        param = comm.CreateParameter();
        param.ParameterName = "CH_PASSINGYEAR";
        param.DbType = DbType.String;
        param.Value = PassingYear.Trim();
        comm.Parameters.Add(param);
        //5
        param = comm.CreateParameter();
        param.ParameterName = "CH_GRADE";
        param.DbType = DbType.String;
        param.Value = Grade.Trim();
        comm.Parameters.Add(param);
        //6
        param = comm.CreateParameter();
        param.ParameterName = "CH_PERCENT";
        param.DbType = DbType.String;
        param.Value = Pecentage.Trim();
        comm.Parameters.Add(param);
        //7
        param = comm.CreateParameter();
        param.ParameterName = "IN_EMPLOYEEMASTERID";
        param.DbType = DbType.Int32;
        param.Value = EmpMasId;
        comm.Parameters.Add(param);
        //8
        param = comm.CreateParameter();
        param.ParameterName = "DT_UPDATED";
        param.DbType = DbType.DateTime;
        param.Value = System.DateTime.Now.Date;
        comm.Parameters.Add(param);
        //9
        param = comm.CreateParameter();
        param.ParameterName = "DT_CRTEATED";
        param.DbType = DbType.DateTime;
        param.Value = System.DateTime.Now.Date;
        comm.Parameters.Add(param);
        //10
        param = comm.CreateParameter();
        param.ParameterName = "CH_STATUS";
        param.DbType = DbType.String;
        param.Value = Status;
        comm.Parameters.Add(param);
        //11
        param = comm.CreateParameter();
        param.ParameterName = "IN_EMPLOYEEQUALIFICATIONID";
        param.DbType = DbType.Int32;
        param.Value = EmpQualId;
        comm.Parameters.Add(param);

        int iResult = GenericDataAccess.ExecuteNonQuery(comm);
        if (iResult > 0)
            return true;
        else
            return false;
    }
    public static int GetNewEmpQualId()
    {
        DbCommand ocmd = GenericDataAccess.CreateCommand();
        ocmd.CommandText = "select count(IN_EMPLOYEEQUALIFICATIONID) from tblEmployeeQualification";
        ocmd.CommandType = CommandType.Text;

        return int.Parse(GenericDataAccess.ExecuteScalar(ocmd));
    }
    
    public static DataTable GetLanguageData(int IN_EMPMASTERID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "select IN_EMPLOYEELANGUAGEID, IN_EMPLOYEEMASTERID ,VC_LANGUAGE ,VC_READ , VC_SPEAK , CH_STATUS, CH_DELETESTATUS, VC_WRITE from tblEmployeeLanguage where ch_DeleteStatus=0 and IN_EMPLOYEEMASTERID=" + IN_EMPMASTERID;
        comm.CommandType = CommandType.Text;
               
        return GenericDataAccess.ExecuteReader(comm);
    }
    public static bool InsertLanguage(int EmpMasId, int EmpLangId, bool vc_read, bool vc_Write, bool vc_Speak, string vc_Language, char Status)
    {
        try
        {
            DbCommand comm = GenericDataAccess.CreateCommand();
            comm.CommandText = "sp_Insert_tblEmpLanguage";
            comm.CommandType = CommandType.StoredProcedure;
            //1
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "vc_read";
            param.DbType = DbType.String;
            if (vc_read)
                param.Value = '1';
            else
                param.Value = '0';
            comm.Parameters.Add(param);
            //2
            param = comm.CreateParameter();
            param.ParameterName = "vc_Write";
            param.DbType = DbType.String;
            if (vc_Write)
                param.Value = '1';
            else
                param.Value = '0';
            comm.Parameters.Add(param);
            //3
            param = comm.CreateParameter();
            param.ParameterName = "vc_Speak";
            param.DbType = DbType.String;
            if (vc_Speak)
                param.Value = '1';
            else
                param.Value = '0';
            comm.Parameters.Add(param);
            //4
            param = comm.CreateParameter();
            param.ParameterName = "vc_Language";
            param.DbType = DbType.String;
            param.Value = vc_Language.Trim();
            comm.Parameters.Add(param);          
            //5
            param = comm.CreateParameter();
            param.ParameterName = "IN_EMPLOYEEMASTERID";
            param.DbType = DbType.Int32;
            param.Value = EmpMasId;
            comm.Parameters.Add(param);
            //6
            param = comm.CreateParameter();
            param.ParameterName = "DT_UPDATED";
            param.DbType = DbType.DateTime;
            param.Value = System.DateTime.Now.Date;
            comm.Parameters.Add(param);
            //7
            param = comm.CreateParameter();
            param.ParameterName = "DT_CRTEATED";
            param.DbType = DbType.DateTime;
            param.Value = System.DateTime.Now.Date;
            comm.Parameters.Add(param);
            //8
            param = comm.CreateParameter();
            param.ParameterName = "CH_STATUS";
            param.DbType = DbType.String;
            param.Value = Status;
            comm.Parameters.Add(param);
            //9
            param = comm.CreateParameter();
            param.ParameterName = "IN_EMPLOYEELANGUAGEID";
            param.DbType = DbType.Int32;
            param.Value = EmpLangId + 1;
            comm.Parameters.Add(param);
            //10
            param = comm.CreateParameter();
            param.ParameterName = "ch_DeleteStatus";
            param.DbType = DbType.String;
            param.Value = "0";
            comm.Parameters.Add(param);
            int iResult = GenericDataAccess.ExecuteNonQuery(comm);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        { throw ex; }

    }
    public static bool DeleteLanguage(int EmpMasId, int EmpLangId, bool vc_read, bool vc_Write, bool vc_Speak, string vc_Language, char Status)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_Update_tblEmpLanguage";
        comm.CommandType = CommandType.StoredProcedure;
        //1
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "vc_read";
        param.DbType = DbType.String;
        if (vc_read)
            param.Value = '1';
        else
            param.Value = '0';
        comm.Parameters.Add(param);
        //2
        param = comm.CreateParameter();
        param.ParameterName = "vc_Write";
        param.DbType = DbType.String;
        if (vc_Write)
            param.Value = '1';
        else
            param.Value = '0';
        comm.Parameters.Add(param);
        //3
        param = comm.CreateParameter();
        param.ParameterName = "vc_Speak";
        param.DbType = DbType.String;
        if (vc_Speak)
            param.Value = '1';
        else
            param.Value = '0';
        comm.Parameters.Add(param);
        //4
        param = comm.CreateParameter();
        param.ParameterName = "vc_Language";
        param.DbType = DbType.String;
        param.Value = vc_Language.Trim();
        comm.Parameters.Add(param);
        //5
        param = comm.CreateParameter();
        param.ParameterName = "IN_EMPLOYEEMASTERID";
        param.DbType = DbType.Int32;
        param.Value = EmpMasId;
        comm.Parameters.Add(param);
        //6
        param = comm.CreateParameter();
        param.ParameterName = "DT_UPDATED";
        param.DbType = DbType.DateTime;
        param.Value = System.DateTime.Now.Date;
        comm.Parameters.Add(param);
        //7
        param = comm.CreateParameter();
        param.ParameterName = "DT_CRTEATED";
        param.DbType = DbType.DateTime;
        param.Value = System.DateTime.Now.Date;
        comm.Parameters.Add(param);
        //8
        param = comm.CreateParameter();
        param.ParameterName = "CH_STATUS";
        param.DbType = DbType.String;
        param.Value = Status;
        comm.Parameters.Add(param);
        //9
        param = comm.CreateParameter();
        param.ParameterName = "IN_EMPLOYEELANGUAGEID";
        param.DbType = DbType.Int32;
        param.Value = EmpLangId + 1;
        comm.Parameters.Add(param);
        //10
        param = comm.CreateParameter();
        param.ParameterName = "ch_DeleteStatus";
        param.DbType = DbType.String;
        param.Value = "1";
        comm.Parameters.Add(param);
        int iResult = GenericDataAccess.ExecuteNonQuery(comm);
        if (iResult > 0)
            return true;
        else
            return false;
    }
    public static bool UpdateLanguage(int EmpMasId, int EmpLangId, bool vc_read, bool vc_Write, bool vc_Speak, string vc_Language, char Status)
    {
        try
        {
           
            DbCommand comm = GenericDataAccess.CreateCommand();
            comm.CommandText = "sp_Update_tblEmpLanguage";
            comm.CommandType = CommandType.StoredProcedure;
            //1
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "vc_read";
            param.DbType = DbType.String;
            if (vc_read)
                param.Value = '1';
            else
                param.Value = '0';
            comm.Parameters.Add(param);
            //2
            param = comm.CreateParameter();
            param.ParameterName = "vc_Write";
            param.DbType = DbType.String;
            if (vc_Write)
                param.Value = '1';
            else
                param.Value = '0';
            comm.Parameters.Add(param);
            //3
            param = comm.CreateParameter();
            param.ParameterName = "vc_Speak";
            param.DbType = DbType.String;
            if (vc_Speak)
                param.Value = '1';
            else
                param.Value = '0';
            comm.Parameters.Add(param);
            //4
            param = comm.CreateParameter();
            param.ParameterName = "vc_Language";
            param.DbType = DbType.String;
            param.Value = vc_Language.Trim();
            comm.Parameters.Add(param);
            //5
            param = comm.CreateParameter();
            param.ParameterName = "IN_EMPLOYEEMASTERID";
            param.DbType = DbType.Int32;
            param.Value = EmpMasId;
            comm.Parameters.Add(param);
            //6
            param = comm.CreateParameter();
            param.ParameterName = "DT_UPDATED";
            param.DbType = DbType.DateTime;
            param.Value = System.DateTime.Now.Date;
            comm.Parameters.Add(param);
            //7
            param = comm.CreateParameter();
            param.ParameterName = "DT_CRTEATED";
            param.DbType = DbType.DateTime;
            param.Value = System.DateTime.Now.Date;
            comm.Parameters.Add(param);
            //8
            param = comm.CreateParameter();
            param.ParameterName = "CH_STATUS";
            param.DbType = DbType.String;
            param.Value = Status;
            comm.Parameters.Add(param);
            //9
            param = comm.CreateParameter();
            param.ParameterName = "IN_EMPLOYEELANGUAGEID";
            param.DbType = DbType.Int32;
            param.Value = EmpLangId + 1;
            comm.Parameters.Add(param);
            //10
            param = comm.CreateParameter();
            param.ParameterName = "ch_DeleteStatus";
            param.DbType = DbType.String;
            param.Value = "0";
            comm.Parameters.Add(param);
            int iResult = GenericDataAccess.ExecuteNonQuery(comm);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        { throw ex; }

    }
    public static int GetNewEmpLanguageId()
    {
        DbCommand ocmd = GenericDataAccess.CreateCommand();
        ocmd.CommandText = "select count(IN_EMPLOYEELANGUAGEID) from tblEmployeeLanguage";
        ocmd.CommandType = CommandType.Text;

        return int.Parse(GenericDataAccess.ExecuteScalar(ocmd));
    }
}
