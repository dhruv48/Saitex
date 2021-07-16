using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{
    //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];
    DataTable dtPODetail = new DataTable();
    bool result = false;

    public AutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string[] GetVendorCategoryList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "Select * from TX_VENDCATEGORY_MST Where vc_VENDORCATEGORYCODE like :prefixText and CH_DELETESTATUS=0";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["vc_VENDORCATEGORYCODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetVendorList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "Select PRTY_CODE from TX_VENDOR_MST Where  ltrim(rtrim(PRTY_CODE)) like :prefixText and DEL_STATUS = 0";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRTY_CODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetPOVendorList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select * from(Select distinct v.PRTY_CODE,IN_POTYPECODE,PRTY_STATE,PRTY_ADD1,PRTY_ADD2,PRTY_NAME,PRTY_ADD1 ||',  '||nvl( PRTY_ADD2,' ') ||',  '|| nvl(PRTY_STATE,' ') address from TX_VENDOR_MST v  right outer join tx_item_pu_mst p  on V.PRTY_CODE=P.PRTY_CODE where  v.CH_DELETESTATUS=0) a where IN_POTYPECODE='PUM' and ltrim(rtrim(PRTY_CODE)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRTY_CODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetPOVendorListCash(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select * from(Select distinct v.PRTY_CODE,IN_POTYPECODE,PRTY_STATE,PRTY_ADD1,PRTY_ADD2,PRTY_NAME,PRTY_ADD1 ||',  '||nvl( PRTY_ADD2,' ') ||',  '|| nvl(PRTY_STATE,' ') address from TX_VENDOR_MST v  right outer join tx_item_pu_mst p  on V.PRTY_CODE=P.PRTY_CODE where  v.CH_DELETESTATUS=0) a where IN_POTYPECODE='PUC' and ltrim(rtrim(PRTY_CODE)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRTY_CODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetCompanyName(string prefixText, int count)
    {

        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select comp_Code from tblCompanyMaster where CH_DELETESTATUS=0 and ltrim(rtrim(comp_Code)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["comp_Code"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetCompanyName1(string prefixText, int count)
    {

        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select VC_COMPANYNAME from tblCompanyMaster where CH_DELETESTATUS=0 and ltrim(rtrim(VC_COMPANYNAME)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_COMPANYNAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetBranchMasterList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strDup = "Select CH_BRANCHCODE from TBLBRANCHMASTER Where CH_BRANCHCODE like :prefixText";
        OracleCommand cmd = new OracleCommand(strDup, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["CH_BRANCHCODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetBranchMasterName(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strDup = "Select VC_BRANCHNAME from TBLBRANCHMASTER Where VC_BRANCHNAME like :prefixText";
        OracleCommand cmd = new OracleCommand(strDup, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_BRANCHNAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetDepartmentMasterList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strDup = "Select VC_DEPARTMENTCODE from TBLDEPARTMENTMASTER Where VC_DEPARTMENTCODE like :prefixText";
        OracleCommand cmd = new OracleCommand(strDup, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_DEPARTMENTCODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetDepartmentName(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strDup = "Select VC_DEPARTMENTNAME from TBLDEPARTMENTMASTER Where VC_DEPARTMENTNAME like :prefixText";
        OracleCommand cmd = new OracleCommand(strDup, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_DEPARTMENTNAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetDepartmentName1(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strDup = "Select VC_DEPARTMENTNAME from TBLDEPARTMENTMASTER Where VC_DEPARTMENTNAME like :prefixText";
        OracleCommand cmd = new OracleCommand(strDup, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_DEPARTMENTNAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetUserMaster(string prefixText, int count)
    {

        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select vc_UserCode from tblUserMaster where ltrim(rtrim(vc_UserCode)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["vc_UserCode"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetBranchNameByCompany(string prefixText, int count, string contextKey)
    {

        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select ch_BranchCode from tblBranchMaster where Comp_Code=:Comp_Code and ltrim(rtrim(ch_BranchCode)) like :prefixText and CH_DELETESTATUS=0";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        param = new OracleParameter(":Comp_Code", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = contextKey;
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["ch_BranchCode"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetBranchCode(string prefixText, int count, string contextKey)
    {

        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select ch_BranchCode from tblBranchMaster where ltrim(rtrim(ch_BranchCode)) like :prefixText and CH_DELETESTATUS=0";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["ch_BranchCode"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetGroupName(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select VC_GROUPNAME from tblGroupMaster where ltrim(rtrim(VC_GROUPNAME)) like :prefixText and CH_DELETESTATUS=0";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_GROUPNAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetCurrency(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select VC_CURRENCYNAME from TX_CURRENCYMASTER where ltrim(rtrim(VC_CURRENCYNAME)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_CURRENCYNAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetItemCategoryNameListTT(string prefixText, int count)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "select VC_CATEGORYNAME from tx_item_category_mst where ltrim(rtrim(VC_CATEGORYNAME)) like :prefixText and CH_DELETESTATUS=0";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = prefixText + "%";
            cmd.Parameters.Add(param);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["VC_CATEGORYNAME"].ToString(), i);
                i++;
            }

            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetUnitMasterNameList(string prefixText, int count)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "select VC_UNITNAME from tx_unit_mst where ltrim(rtrim(VC_UNITNAME)) like :prefixText and CH_DELETESTATUS=0";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = prefixText + "%";
            cmd.Parameters.Add(param);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["VC_UNITNAME"].ToString(), i);
                i++;
            }

            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetCurrencyMaster(string prefixText, int count)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "select VC_CURRENCYNAME from TX_CURRENCYMASTER where ltrim(rtrim(VC_CURRENCYNAME)) like :prefixText";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = prefixText + "%";
            cmd.Parameters.Add(param);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["VC_CURRENCYNAME"].ToString(), i);
                i++;
            }

            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetVendorNameList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "Select PRTY_NAME from TX_VENDOR_MST Where  ltrim(rtrim(PRTY_NAME)) like :prefixText and CH_DELETESTATUS=0";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRTY_NAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetItemForPO(string prefixText, int count)
    {
        try
        {
            if (count == 0)
            {
                count = 10;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "select ITEM_CODE From tx_item_indent_trn where nvl(APPR_QTY,0)-nvl(PUR_ADJ_QTY,0)>0 and ltrim(rtrim(ITEM_CODE)) like :prefixText";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = prefixText + "%";
            cmd.Parameters.Add(param);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["ITEM_CODE"].ToString(), i);
                i++;
            }

            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetMasterName(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select MST_NAME from TX_MASTER_MST where ltrim(rtrim(MST_NAME)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["MST_NAME"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetMasterCode(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select MST_CODE from TX_MASTER_TRN where ltrim(rtrim(MST_CODE)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 50);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["MST_CODE"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetDesignationName(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select VC_DESIGNATIONCODE from TBLDESIGNATIONMASTER where ltrim(rtrim(VC_DESIGNATIONCODE)) like :prefixText and ch_deletestatus=:ch_deletestatus";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 50);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        param = new OracleParameter(":ch_deletestatus", OracleType.Char, 1);
        param.Direction = ParameterDirection.Input;
        param.Value = '0';
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["VC_DESIGNATIONCODE"].ToString(), i);
            i++;
        }

        return items;
    }

    // Item For Material receipt against po
    [WebMethod]
    public string[] GetItemForRecPO(string prefixText, int count, string contextKey)
    {
        try
        {
            if (count == 0)
            {
                count = 10;
            }
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "SELECT pt.ITEM_CODE FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and pt.PO_NUMB=:PO_NUMB and ltrim(rtrim(pt.ITEM_CODE)) like :prefixText";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = prefixText + "%";
            cmd.Parameters.Add(param);

            param = new OracleParameter(":PO_NUMB", OracleType.Number);
            param.Direction = ParameterDirection.Input;
            param.Value = int.Parse(contextKey);
            cmd.Parameters.Add(param);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["ITEM_CODE"].ToString(), i);
                i++;
            }

            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string[] GetPONUMB(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select PO_NUMB from TX_MASTER_TRN where IN_POTYPECODE='PUM' and ltrim(rtrim(MST_CODE)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 50);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PO_NUMB"].ToString(), i);
            i++;
        }

        return items;
    }

    [WebMethod]
    public string[] GetPONUMBCash(string prefixText, int count)
    {
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        string strSQL = "select PO_NUMB from TX_MASTER_TRN where IN_POTYPECODE='PUC' and ltrim(rtrim(MST_CODE)) like :prefixText";
        OracleCommand cmd = new OracleCommand(strSQL, con);

        OracleParameter param = new OracleParameter(":prefixText", OracleType.VarChar, 50);
        param.Direction = ParameterDirection.Input;
        param.Value = prefixText + "%";
        cmd.Parameters.Add(param);

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PO_NUMB"].ToString(), i);
            i++;
        }

        return items;
    }

    /// <summary>
    /// Code for yarn code
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [WebMethod]
    public string[] GetYarnData(string prefixText, int count)
    {

        if (count == 0)
        {
            count = 10;
        }

        string whereClause = " WHERE Yarn_Code like :SearchQuery or Yarn_desc like :SearchQuery ";
        string sortExpression = " ORDER BY Yarn_Code";

        string commandText = "SELECT * from ( SELECT Yarn_Code,Yarn_desc,YARN_CAT,YARN_TYPE,YARN_MAKE FROM YRN_MST WHERE ROWNUM <= 10 ) asd ";

        string sPO = "";

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", prefixText + '%', sPO);

        List<string> items = new List<string>(dt.Rows.Count);

        foreach (DataRow dr in dt.Rows)
        {
            items.Add(dr["Yarn_Code"].ToString() + "|" + dr["Yarn_desc"].ToString() + "|" + dr["YARN_CAT"].ToString());

        }

        return items.ToArray();
    }

    #region code by bharat on 3 jan 2012 for navigation

    [WebMethod]
    public string[] GetNavByChildModule(string prefixText, int count, string contextKey)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string[] code = contextKey.Split('@');
            int mdlId = int.Parse(code[0].ToString());
            int ChildmdlId = int.Parse(code[1].ToString());

            string strSQL = "select NAV_ID, NAV_NAME from (select * from V_CM_NAV_MST where MDL_ID=:MDL_ID and CHILD_MDL_ID=:CHILD_MDL_ID) where upper(NAV_NAME) like :prefixText or upper(NAV_URL) like :prefixText";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            cmd.Parameters.AddWithValue(":prefixText", "%" + prefixText.ToUpper() + "%");
            cmd.Parameters.AddWithValue(":MDL_ID", mdlId);
            cmd.Parameters.AddWithValue(":CHILD_MDL_ID", ChildmdlId);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                //items.SetValue(dr["NAV_NAME"].ToString() + "-" + dr["NAV_ID"].ToString(), i);
                items.SetValue(dr["NAV_ID"].ToString() + "-" + dr["NAV_NAME"].ToString(), i);
                i++;
            }

            return items;
        }
        catch
        {
            throw;
        }
    }

    #endregion

    [WebMethod]
    public bool confirmCustomerRequest(DataTable dtPODetail)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];
            int iResult = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
            if (iResult > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        catch
        {
            throw;
        }
        // Add your operation implementation here

    }

    [WebMethod]
    public bool ClosedCustomerRequest()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];
            if (HttpContext.Current.Session["dtPODetailClosed"] != null)
            {
                dtPODetail = HttpContext.Current.Session["dtPODetailClosed"] as DataTable;
                int iResult = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail); // Rajesh
                if (iResult > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;

        }
        catch
        {
            throw;

        }
        // Add your operation implementation here

    }



    [WebMethod]
    public bool UpdateSalesRevise()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];
            if (HttpContext.Current.Session["dtDetailRevised"] != null)
            {
                var dtDetailRevised = HttpContext.Current.Session["dtDetailRevised"] as DataTable;
                int iResult = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateSalesRevise(oUserLoginDetail.UserCode, dtDetailRevised); // Rajesh
                if (iResult > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;

        }
        catch
        {
            throw;

        }     

    }



    [WebMethod]
    public DataTable GetCRBySearchFilterUnApprovedOnly(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty,string partyYarn)
    {
        try
        {
            DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetCRBySearchFilterUnApprovedOnly(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty,partyYarn);

            return result;

        }
        catch
        {
            throw;

        }
        // Add your operation implementation here

    }

    [WebMethod]
    public DataTable GetCRBySearchFilterApprUnClose(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty, string PartyGray)
    {
        try
        {
            DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetCRBySearchFilterApprUnClose(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty, PartyGray);

            return result;

        }
        catch
        {
            throw;

        }
        // Add your operation implementation here

    }
   

    [WebMethod]
    public DataTable GetCRBySearchFilterApprClose(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty)
    {
        try
        {
            DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetCRBySearchFilterApprClose(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty);

            return result;

        }
        catch
        {
            throw;

        }
        // Add your operation implementation here

    }

    [WebMethod]
    public DataTable GetCRBySearchFilterAll(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty)
    {
        try
        {
            DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetCRBySearchFilterAll(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty);

            return result;

        }
        catch
        {
            throw;

        }
        // Add your operation implementation here

    }

    [WebMethod]
    public DataTable GetCRBySearchFilterUnApprovedFabriconly(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string transPrice, string SalePrice, string UOM, string CRQty)
    {
        try
        {
            DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetCRBySearchFilterUnApprovedFabriconly(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, transPrice, SalePrice, UOM, CRQty);

            return result;

        }
        catch
        {
            throw;

        }
        // Add your operation implementation here

    }

    #region Added By Rajesh Sahu 29 Feb 2012 (For [Marketing Department] Employee's in Customer Request For SW LabDip)

    [WebMethod]
    public string[] GetAgentForCRSWLabDip(string prefixText, int count, string contextKey)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string[] code = contextKey.Split('@');
            string Comp = code[0].ToString();
            string Branch = code[1].ToString();

            string strSQL = "SELECT EMP_CODE, EMPLOYEENAME FROM (SELECT DISTINCT  EMP_CODE,  F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME FROM HR_EMP_MST WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND DEPT_CODE = :DEPT_CODE) WHERE UPPER (EMP_CODE) LIKE :prefixText  OR UPPER (EMPLOYEENAME) LIKE :prefixText ";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            cmd.Parameters.AddWithValue(":prefixText", prefixText.ToUpper() + "%");
            cmd.Parameters.AddWithValue(":COMP_CODE", Comp);
            cmd.Parameters.AddWithValue(":BRANCH_CODE", Branch);
            cmd.Parameters.AddWithValue(":DEPT_CODE", "D00005");    // For Marketing Department Employees

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                //items.SetValue(dr["NAV_NAME"].ToString() + "-" + dr["NAV_ID"].ToString(), i);
                items.SetValue(dr["EMP_CODE"].ToString() + "-" + dr["EMPLOYEENAME"].ToString(), i);
                i++;
            }

            return items;
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Added By Rajesh Sahu 02 March 2012 (For Shade Code

    [WebMethod]
    public string[] GetShadeCodeThroughShadeFamily(string prefixText, int count, string contextKey)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string[] code = contextKey.Split('@');
            string Comp = code[0].ToString();
            string SHADE_FAMILY_CODE = code[1].ToString();
            string PRODUCT_TYPE = code[2].ToString();

            string strSQL = " SELECT SHADE_CODE FROM (SELECT SHADE_CODE FROM OD_SHADE_FAMILY_TRN WHERE COMP_CODE = :COMP_CODE AND SHADE_FAMILY_CODE = :SHADE_FAMILY_CODE AND PRODUCT_TYPE = :PRODUCT_TYPE AND DEL_STATUS = '0') WHERE UPPER (SHADE_CODE) LIKE :prefixText ";
            OracleCommand cmd = new OracleCommand(strSQL, con);

            cmd.Parameters.AddWithValue(":prefixText", prefixText.ToUpper() + "%");
            cmd.Parameters.AddWithValue(":COMP_CODE", Comp);
            cmd.Parameters.AddWithValue(":SHADE_FAMILY_CODE", SHADE_FAMILY_CODE);
            cmd.Parameters.AddWithValue(":PRODUCT_TYPE", PRODUCT_TYPE);    // For Marketing Department Employees

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                //items.SetValue(dr["NAV_NAME"].ToString() + "-" + dr["NAV_ID"].ToString(), i);
                items.SetValue(dr["SHADE_CODE"].ToString(), i);
                i++;
            }

            return items;
        }
        catch
        {
            throw;
        }
    }

    #endregion




    [WebMethod]
    public DataTable GetVendorListForTesting()
    {
       
        OracleConnection con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();
        string strSQL = "Select * from TX_VENDOR_MST Where  DEL_STATUS = 0";
        OracleCommand cmd = new OracleCommand(strSQL, con);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);



        return dt;
    }



    public DataTable GetCRBySearchFilterApprUnClose_Yarn(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty, string PartyGray)
    {
        try
        {
            DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterApprUnClose_Yarn(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty, PartyGray);

            return result;

        }
        catch
        {
            throw;

        }
    }
}

