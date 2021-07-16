using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Module_Production_Pages_Warping_issu_4Prod : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string yarn_code;
    private string shadecode;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (Request.QueryString["yarn_code"] != null)
            {
                string yarn_code = Request.QueryString["yarn_code"].ToString();

            }
            if (Request.QueryString["shadecode"] != null)
            {
                string shadecode = Request.QueryString["shadecode"].ToString();
            }

            InitialControls();
        }
    }

    private void InitialControls()
    {
        txtbranch.ReadOnly = true;
        txtshade_code.ReadOnly = true;
        txtyrn_code.ReadOnly = true;
        BindDataInGrid();
        grd_issu_pa_warp.Visible = true;
    }

    private void BindDataInGrid()
    {

        DataTable dt = new DataTable();
        dt = SaitexBL.Interface.Method.WARP_ENTRY.BindIssuRefDataInGrid(oUserLoginDetail.CH_BRANCHCODE, yarn_code, shadecode);
        if (dt != null && dt.Rows.Count > 0)
        {
            grd_issu_pa_warp.DataSource = dt;
            grd_issu_pa_warp.DataBind();
            grd_issu_pa_warp.Visible = true;
        }
        else
        {
            grd_issu_pa_warp.DataSource = null;
            grd_issu_pa_warp.DataBind();
            grd_issu_pa_warp.Visible = true;
        }
    }
}
