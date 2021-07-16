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
using System.Xml.Linq;

public partial class CommonControls_AprovalPendings : System.Web.UI.UserControl
{
    private static DataTable dtNavId = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            {
                if (!IsPostBack)
                {
                    Initilagepage();
                    CheckUserNavRight();
                }
            }
        }

    }
    private void Initilagepage()
    {
        try
        {
            CreateDatatableforNavId();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    private void CheckUserNavRight()
    {
        try
        {
            string UserCode = oUserLoginDetail.UserCode.ToString();

            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = oUserLoginDetail.UserCode;
            DataTable dtUser_Nav_Rgt = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightByUserCode(oUserAccessRight);
            if (dtUser_Nav_Rgt != null && dtUser_Nav_Rgt.Rows.Count > 0)
            {

                for (int i = 0; i < dtNavId.Rows.Count; i++)
                {

                    DataView dv = new DataView(dtUser_Nav_Rgt);
                    string filter = string.Empty;
                    filter += "NAV_ID=" + int.Parse(dtNavId.Rows[i]["NavId"].ToString());
                    filter += " AND NAV_NAME='" + dtNavId.Rows[i]["NAVNAME"].ToString() + "'";

                    dv.RowFilter = filter;
                    if (dv != null && dv.Count > 0)
                    {

                        if (dv[0]["NAV_NAME"].ToString() == "Material Indent Approval")
                        {
                            SaitexDM.Common.DataModel.TX_ITEM_IND_MST oTX_ITEM_IND_MST = new SaitexDM.Common.DataModel.TX_ITEM_IND_MST();
                            oTX_ITEM_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oTX_ITEM_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oTX_ITEM_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oTX_ITEM_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                            DataTable dtIndentApproval = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetIndentDataForApproval(oTX_ITEM_IND_MST);
                            if (dtIndentApproval != null && dtIndentApproval.Rows.Count > 0)
                            {
                                lblIndentApproval.Text = "Material Indent Pending" + "<B>(" + dtIndentApproval.Rows.Count.ToString() + ")</B>";
                                lblIndentApproval.Visible = true;
                                lnkIndentApproval.Visible = true;
                            }
                            else
                            {

                                lblIndentApproval.Visible = false;
                                lnkIndentApproval.Visible = false;
                            }
                        }

                        if (dv[0]["NAV_NAME"].ToString() == "PO Approval")
                        {

                            SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
                            oTX_ITEM_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                            DataTable dtPoApproval = SaitexBL.Interface.Method.Material_Purchase_Order.GetPODataForApproval(oTX_ITEM_PU_MST);
                            if (dtPoApproval != null && dtPoApproval.Rows.Count > 0)
                            {
                                lblPoApproval.Text = "Material PO Pending" + "<B>(" + dtPoApproval.Rows.Count.ToString() + ")</B>";
                                lnkPoApproval.Visible = true;
                                lblPoApproval.Visible = true;


                            }
                            else
                            {
                                lblPoApproval.Visible = false;
                                lnkPoApproval.Visible = false;


                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "(MRN)Transaction Approval")
                        {
                            DataTable dtTransactionalApproval = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "'RMS01', 'RMS02', 'RMS03', 'RMS04', 'RMS05','RMS11', 'RMS06', 'RMS30', 'GRM01','IMS05'");
                            var dtReceiveApproval = new DataView(dtTransactionalApproval);
                            //dtReceiveApproval.RowFilter = "TRN_TYPE='RMS01,RMS02,RMS03,RMS04,RMS05,RMS11,RMS06,RMS30,GRM01'";
                            if (dtReceiveApproval != null && dtReceiveApproval.Count > 0)
                            {
                                lblTransactional.Text = "Material Receipt Approval Pending" + "<B>(" + dtReceiveApproval.Count.ToString() + ")</B>";
                                lblTransactional.Visible = true;
                                lnkTransactional.Visible = true;

                            }
                            else
                            {
                                lblTransactional.Visible = false;
                                lnkTransactional.Visible = false;
                            }
                        }

                        if (dv[0]["NAV_NAME"].ToString() == "(MRN)Transaction Approval")
                        {
                            DataTable dtTransactionalApproval = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "'IMS06','IMS07','IMS01','IMS02','IMS03','IMS04','IMS11','GIM01'");
                            var dtIsuueApproval = new DataView(dtTransactionalApproval);
                           // dtIsuueApproval.RowFilter = "TRN_TYPE='IMS06,IMS07,IMS01,IMS02,IMS03,IMS04,IMS05,IMS11,GIM01'";
                            if (dtIsuueApproval != null && dtIsuueApproval.Count > 0)
                            {
                                lblIssueApproval.Text = "Material Issue Approval Pending" + "<B>(" + dtIsuueApproval.Count.ToString() + ")</B>";
                                lblIssueApproval.Visible = true;
                                lnkIssueApproval.Visible = true;

                            }
                            else
                            {
                                lblIssueApproval.Visible = false;
                                lnkIssueApproval.Visible = false;
                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "Indents Pending For PO")
                        {
                            var OBJ = new SaitexDM.Common.DataModel.TX_ITEM_IND_MST();
                            OBJ.BRANCH_CODE=oUserLoginDetail.CH_BRANCHCODE;
                            OBJ.COMP_CODE=oUserLoginDetail.COMP_CODE;
                            OBJ.YEAR=oUserLoginDetail.DT_STARTDATE.Year;
                            OBJ.DEPT_CODE=oUserLoginDetail.VC_DEPARTMENTCODE;                           
                            DataTable dtPendingIndent = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetPendingIndentForPO(OBJ);
                            var dvPendingIndent = new DataView(dtPendingIndent);
                            if (dvPendingIndent != null && dvPendingIndent.Count > 0)
                            {
                                lblIndentPendingForPO.Text = "Material Indents Pending For PO" + "<B>(" + dvPendingIndent.Count.ToString() + ")</B>";
                                lblIndentPendingForPO.Visible = true;
                                lnkIndentPendingForPO.Visible = true;
                            }
                            else
                            {
                                lblIndentPendingForPO.Visible = false;
                                lnkIndentPendingForPO.Visible = false;
                            }
                        }

                        if (dv[0]["NAV_NAME"].ToString() == "Indent Approval")
                        {
                            DataTable dtYarnIndentApproval = SaitexBL.Interface.Method.YRN_INT_MST.GetIndentDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                            if (dtYarnIndentApproval != null && dtYarnIndentApproval.Rows.Count > 0)
                            {
                                lblYarnIndent.Text = "Yarn Indent Approval Pending" + "<B>(" + dtYarnIndentApproval.Rows.Count.ToString() + ")</B>";
                                lblYarnIndent.Visible = true;
                                lnkYarnIndent.Visible = true;

                            }
                            else
                            {
                                lblYarnIndent.Visible = false;
                                lnkYarnIndent.Visible = false;
                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "Po Approval")
                        {
                            DataTable dtYarnPOApproval = SaitexBL.Interface.Method.YRN_PU_MST.GetPODataForApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year );
                            if (dtYarnPOApproval != null && dtYarnPOApproval.Rows.Count > 0)
                            {
                                lblYarnPOIndent.Text = "Yarn PO Approval Pending" + "<B>(" + dtYarnPOApproval.Rows.Count.ToString() + ")</B>";
                                lblYarnPOIndent.Visible = true;
                                lnkYarnPOIndent.Visible = true;

                            }
                            else
                            {

                                lblYarnPOIndent.Visible = false;
                                lnkYarnPOIndent.Visible = false;

                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "(MRN) Transactional Approval")
                        {
                            DataTable dtYarnTransactional = SaitexBL.Interface.Method.YRN_IR_MST.GetReceiptDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "YARN");
                            if (dtYarnTransactional != null && dtYarnTransactional.Rows.Count > 0)
                            {
                                lblYarnTransactional.Text = "Yarn Transactinal Approval Pending" + "<B>(" + dtYarnTransactional.Rows.Count.ToString() + ")</B>";
                                lblYarnTransactional.Visible = true;
                                lnkYarnTransactional.Visible = true;

                            }
                            else
                            {

                                lblYarnTransactional.Visible = false;
                                lnkYarnTransactional.Visible = false;

                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "Indent Aproval")
                        {
                            DataTable dtFabricIndenty = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.GetIndentDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                            if (dtFabricIndenty != null && dtFabricIndenty.Rows.Count > 0)
                            {
                                lblFabricIndentApproval.Text = "Fabric Indent Approval Pending" + "<B>(" + dtFabricIndenty.Rows.Count.ToString() + ")</B>";
                                lblFabricIndentApproval.Visible = true;
                                lnkFabricIndentApproval.Visible = true;

                            }
                            else
                            {
                                lblFabricIndentApproval.Visible = false;
                                lnkFabricIndentApproval.Visible = false;

                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "PO Approval")
                        {
                            DataTable dtFabricPOApproval = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetPODataForApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                            if (dtFabricPOApproval != null && dtFabricPOApproval.Rows.Count > 0)
                            {
                                lblFabricPOApproval.Text = "Fabric PO Approval Pending" + "<B>(" + dtFabricPOApproval.Rows.Count.ToString() + ")</B>";
                                lblFabricPOApproval.Visible = true;
                                lnkFabricPOApproval.Visible = true;

                            }
                            else
                            {

                                lblFabricPOApproval.Visible = false;
                                lnkFabricPOApproval.Visible = false;

                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "(MRN)Transaction Approval")
                        {
                            DataTable dtFabricTransactionalApproval = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetReceiptDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                            if (dtFabricTransactionalApproval != null && dtFabricTransactionalApproval.Rows.Count > 0)
                            {
                                lblFabricTransactionApproval.Text = "Fabric Transactional Approval Pending" + "<B>(" + dtFabricTransactionalApproval.Rows.Count.ToString() + ")</B>";
                                lblFabricTransactionApproval.Visible = true;
                                lnkFabricTransactionApproval.Visible = true;

                            }
                            else
                            {

                                lblFabricTransactionApproval.Visible = false;
                                lnkFabricTransactionApproval.Visible = false;

                            }
                       
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "Poy Indent Approval")
                        {
                            DataTable dtFiberIndenty = SaitexBL.Interface.Method.FIBER_IND_MST.GetIndentDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                            if (dtFiberIndenty != null && dtFiberIndenty.Rows.Count > 0)
                            {
                                lblFiberIndentApproval.Text = "Poy Indent Approval Pending" + "<B>(" + dtFiberIndenty.Rows.Count.ToString() + ")</B>";
                                lblFiberIndentApproval.Visible = true;
                                lnkFiberIndentApproval.Visible = true;

                            }
                            else
                            {
                                lblFiberIndentApproval.Visible = false;
                                lnkFiberIndentApproval.Visible = false;

                            }
                        }

                        if (dv[0]["NAV_NAME"].ToString() == "Poy Po Approval")
                        {
                            DataTable dtFiberPOApproval = SaitexBL.Interface.Method.FIBER_PU_MST.GetApprovedIndentDataForPO(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
                            if (dtFiberPOApproval != null && dtFiberPOApproval.Rows.Count > 0)
                            {
                                lblApprovedIndentPendingForPO.Text = "Poy Indent Pending For Po" + "<B>(" + dtFiberPOApproval.Rows.Count.ToString() + ")</B>";
                                lblApprovedIndentPendingForPO.Visible = true;
                                lnkApprovedIndentPendingForPO.Visible = true;

                            }
                            else
                            {

                                lblFiberPOApproval.Visible = false;
                                lnkFiberPOApproval.Visible = false;

                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "Poy Po Approval")
                        {
                            DataTable dtFiberPOApproval = SaitexBL.Interface.Method.TX_FIBER_PO_CREDIT.GetDataForApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
                            if (dtFiberPOApproval != null && dtFiberPOApproval.Rows.Count > 0)
                            {
                                lblFiberPOApproval.Text = "Poy PO Approval Pending" + "<B>(" + dtFiberPOApproval.Rows.Count.ToString() + ")</B>";
                                lblFiberPOApproval.Visible = true;
                                lnkFiberPOApproval.Visible = true;

                            }
                            else
                            {

                                lblFiberPOApproval.Visible = false;
                                lnkFiberPOApproval.Visible = false;

                            }
                        }
                        if (dv[0]["NAV_NAME"].ToString() == "Transaction Approval")
                        {
                            DataTable dtFiberTransactionalApproval = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetReceiptDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                            if (dtFiberTransactionalApproval != null && dtFiberTransactionalApproval.Rows.Count > 0)
                            {
                                lblFiberTransactionApproval.Text = "Poy Transactional Approval Pending" + "<B>(" + dtFiberTransactionalApproval.Rows.Count.ToString() + ")</B>";
                                lblFiberTransactionApproval.Visible = true;
                                lnkFiberTransactionApproval.Visible = true;

                            }
                            else
                            {

                                lblFiberTransactionApproval.Visible = false;
                                lnkFiberTransactionApproval.Visible = false;

                            }

                        }
                    }
                }

            }
            else
            {

                lblmsg.Text = "No Pending For Approval";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    private void CreateDatatableforNavId()
    {
        try
        {
            #          region Material
            dtNavId = new DataTable();
            dtNavId.Columns.Add("id", typeof(int));
            dtNavId.Columns.Add("NavId", typeof(int));
            dtNavId.Columns.Add("UserCode", typeof(string));
            dtNavId.Columns.Add("NAVNAME", typeof(string));
            DataRow row1;
            row1 = dtNavId.NewRow();
            row1["id"] = 1;
            row1["NavId"] = 12;  // Static Nav id for Indent Approval for Material
            row1["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row1["NAVNAME"] = "Material Indent Approval"; //Static Nav Name for Indent Approval for Material
            dtNavId.Rows.Add(row1);

            DataRow row2;
            row2 = dtNavId.NewRow();
            row2["id"] = 2;
            row2["NavId"] = 129;// Static Nav id for PO Approval for Material
            row2["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row2["NAVNAME"] = "PO Approval";  //Static Nav Name for PO Approval for Material
            dtNavId.Rows.Add(row2);

            DataRow row3;
            row3 = dtNavId.NewRow();
            row3["id"] = 3;
            row3["NavId"] = 130;// Static Nav id for Transaction Approval for Material
            row3["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row3["NAVNAME"] = "(MRN)Transaction Approval";  //Static Nav Name for Transaction Approval for Material
            dtNavId.Rows.Add(row3);
           
            DataRow row13;
            row13 = dtNavId.NewRow();
            row13["id"] = 13;
            row13["NavId"] = 718;// Static Nav id for Pending Indent for PO
            row13["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row13["NAVNAME"] = "Indents Pending For PO";  //Static Nav Name for Transaction Approval for Material
            dtNavId.Rows.Add(row13);


            #endregion
            #   region Yarn

            DataRow row4;
            row4 = dtNavId.NewRow();
            row4["id"] = 4;
            row4["NavId"] = 108;  // Static Nav id for Indent Approval for Yarn
            row4["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row4["NAVNAME"] = "Indent Approval"; //Static Nav Name for Indent Approval for Yarn
            dtNavId.Rows.Add(row4);

            DataRow row5;
            row5 = dtNavId.NewRow();
            row5["id"] = 5;
            row5["NavId"] = 137;// Static Nav id for PO Approval for Yarn
            row5["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row5["NAVNAME"] = "Po Approval";  //Static Nav Name for PO Approval for Yarn
            dtNavId.Rows.Add(row5);

            DataRow row6;
            row6 = dtNavId.NewRow();
            row6["id"] = 6;
            row6["NavId"] = 138;// Static Nav id for Transaction Approval for Yarn
            row6["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row6["NAVNAME"] = "(MRN) Transactional Approval";  //Static Nav Name for Transaction Approval for Yarn
            dtNavId.Rows.Add(row6);
            #endregion
            #   region  Fabric

            DataRow row7;
            row7 = dtNavId.NewRow();
            row7["id"] = 7;
            row7["NavId"] = 122;  // Static Nav id for Indent Approval for Fabric
            row7["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row7["NAVNAME"] = "Indent Aproval"; //Static Nav Name for Indent Approval for Fabric
            dtNavId.Rows.Add(row7);

            DataRow row8;
            row8 = dtNavId.NewRow();
            row8["id"] = 8;
            row8["NavId"] = 154;// Static Nav id for PO Approval for Fabric
            row8["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row8["NAVNAME"] = "PO Approval";  //Static Nav Name for PO Approval for Fabric
            dtNavId.Rows.Add(row8);

            DataRow row9;
            row9 = dtNavId.NewRow();
            row9["id"] = 9;
            row9["NavId"] = 166;// Static Nav id for Transaction Approval for Fabric
            row9["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row9["NAVNAME"] = "(MRN)Transaction Approval";  //Static Nav Name for Transaction Approval for Fabric
            dtNavId.Rows.Add(row9);
            #endregion
            #   region  Fiber

            DataRow row10;
            row10 = dtNavId.NewRow();
            row10["id"] = 10;
            row10["NavId"] = 320;  // Static Nav id for Indent Approval for Fiber
            row10["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row10["NAVNAME"] = "Poy Indent Approval"; //Static Nav Name for Indent Approval for Fiber
            dtNavId.Rows.Add(row10);

            DataRow row11;
            row11 = dtNavId.NewRow();
            row11["id"] = 11;
            row11["NavId"] = 565;// Static Nav id for PO Approval for Fiber
            row11["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row11["NAVNAME"] = "Poy Po Approval";  //Static Nav Name for PO Approval for Fiber
            dtNavId.Rows.Add(row11);

            DataRow row12;
            row12 = dtNavId.NewRow();
            row12["id"] = 12;
            row12["NavId"] = 570;// Static Nav id for Transaction Approval for Fiber
            row12["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row12["NAVNAME"] = "Transaction Approval";  //Static Nav Name for Transaction Approval for Fiber
            dtNavId.Rows.Add(row12);

            
            
            #endregion
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }






}
