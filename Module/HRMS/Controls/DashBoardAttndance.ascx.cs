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
using System.Text;
using System.Collections.Generic;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_DashBoardAttndance : System.Web.UI.UserControl
{
    private static DataSet DS = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindMonths();
    }
    protected void GVMonths_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label MonthId = (Label)e.Row.FindControl("lblMonthId");
                GridView GVDate = (GridView)e.Row.FindControl("GVDate");             

                if (DS.Tables["AttnDate"].Rows != null && DS.Tables["AttnDate"].Rows.Count > 0)
                {
                    DataView dv = new DataView(DS.Tables["AttnDate"]);
                    dv.RowFilter = "MNO='" + MonthId.Text.ToString().Trim() + "'";
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            GVDate.DataSource = dv;
                            GVDate.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
           ErrHandler.WriteError(ex.Message);
        }
    }
    private void BindMonths()
    {
        try
        {
            DS = SaitexBL.Interface.Method.HR_ATTN_TRN.Attn_Record(2011, "C00001", "PKL001");  
            GVMonths.DataSource = DS.Tables["MONTHS"];
            GVMonths .DataBind();            
        }
        catch (Exception ex)
        {            
            ErrHandler.WriteError(ex.Message);
        }
    }   

}
