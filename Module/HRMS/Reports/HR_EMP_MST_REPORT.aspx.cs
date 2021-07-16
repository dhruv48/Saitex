using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.IO;

public partial class Module_HRMS_Reports_HR_EMP_MST_REPORT : System.Web.UI.Page
{
    private string SearchQuery = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["Search"] != null && Request.QueryString["Search"] != "")
            {
                SearchQuery = Request.QueryString["Search"].ToString().Trim();
            }
            DataSet  DS = GetData();
            GetReport(DS, "Employee_Master_Report.rpt");
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataSet ds, string rptname)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"" + rptname));
            rDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
    private DataSet GetData()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = SaitexBL.Interface.Method.HR_EMP_MST.Load_Employee_Detail_For_Print(SearchQuery);

            //for (int index = 0; index < ds.Tables[0].Rows.Count; index++)
            //{
            //    if (ds.Tables[0].Rows[index]["image_path"].ToString() != "")
            //    {
            //        string s = this.Server.MapPath(
            //                    ds.Tables[0].Rows[index]["image_path"].ToString());

            //        if (File.Exists(s))
            //        {
            //            LoadImage(ds.Tables[0].Rows[index], "image_stream", s);
            //        }
            //        else
            //        {
            //            LoadImage(ds.Tables[0].Rows[index], "image_stream",
            //                      "DefaultPicturePath");
            //        }
            //    }
            //    else
            //    {
            //        LoadImage(ds.Tables[0].Rows[index], "image_stream",
            //                  "DefaultPicturePath");
            //    }
            //}
            return ds;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
    private void LoadImage(DataRow objDataRow, string strImageField, string FilePath)
    {
        try
        {
            FileStream fs = new FileStream(FilePath,
                       System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] Image = new byte[fs.Length];
            fs.Read(Image, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            objDataRow[strImageField] = Image;
        }
        catch (Exception ex)
        {
            Response.Write("<font color=red>" + ex.Message + "</font>");
        }
    }
}
