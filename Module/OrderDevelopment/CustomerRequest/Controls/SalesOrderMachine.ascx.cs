using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Obout.ComboBox;


public partial class Module_OrderDevelopment_Controls_SalesOrderMachine : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string Business_TYPE { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControls();
                BindYarnGrd();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InitialControls()
    {
        try
        {

            tdPrint.Visible = true;
            txtCRFrom.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtCRTo.Text = System.DateTime.Now.ToShortDateString();
            BindBranch();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            txtPartyCode.SelectedIndex = -1;
            ddlArticle.SelectedIndex = -1;
            txtCustNo.Text = string.Empty;
            cmdCustomer.SelectedIndex = -1;
            ddlMachine.SelectedIndex = -1;
            ddlNatureShade.SelectedIndex = -1;
            ddlStatus.SelectedIndex = -1;
            ddlshadecode.SelectedIndex= -1;
            Session["dt"] = null;

            getBusiness();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindBranch()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            ddlBranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--------All---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));

        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }
        catch
        {
            throw;
        }
    }

    protected void ddlArticle_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlArticle.Items.Clear();
                ddlArticle.DataSource = data;
                ddlArticle.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article Loading.\r\nSee error log for detail."));

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM ( SELECT * FROM (SELECT   A.ASS_YARN_CODE YARN_CODE,M.YARN_DESC,M.YARN_TYPE, M.COLOUR,  M.YARN_CODE  || '@' || M.YARN_DESC || '@'|| M.UOM  || '@' || M.TRANSFER_PRICE AS Combined, A.ASS_YARN_DESC  FROM   YRN_MST m, YRN_ASSOCATED_MST a WHERE   M.YARN_CODE = A.YARN_CODE) WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND YARN_code NOT IN (SELECT YARN_CODE FROM ( SELECT * FROM ( SELECT  A.ASS_YARN_CODE   YARN_CODE,M.YARN_DESC,M.YARN_TYPE, M.COLOUR,  M.YARN_CODE  || '@' || M.YARN_DESC || '@'|| M.UOM  || '@' || M.TRANSFER_PRICE AS Combined, A.ASS_YARN_DESC  FROM   YRN_MST m, YRN_ASSOCATED_MST a WHERE   M.YARN_CODE = A.YARN_CODE ) WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT * FROM ( SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST  WHERE YARN_CAT = 'TEXTURISED') WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY YARN_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }
        catch
        {
            throw;
        }
    }


    protected void ddlShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShade(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlshadecode.Items.Clear();
                ddlshadecode.DataSource = data;
                ddlshadecode.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
          //  e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article Loading.\r\nSee error log for detail."));

        }
    }



    protected DataTable GetShade(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   * FROM   (SELECT   DISTINCT ST.SHADE_CODE, S.SHADE_FAMILY_CODE  FROM   OD_CUSTOMER_REQUEST_ST ST, OD_CUSTOMER_REQUEST_MST M,od_shade_family_trn s  WHERE   ST.COMP_CODE = M.COMP_CODE   AND ST.BRANCH_CODE = M.BRANCH_CODE  AND ST.YEAR = M.YEAR  AND ST.ORDER_NO = M.ORDER_NO   AND ST.SHADE_CODE = S.SHADE_CODE) WHERE   SHADE_CODE LIKE :SearchQuery  OR SHADE_FAMILY_CODE LIKE :SearchQuery  ORDER BY   SHADE_CODE) asd WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND SHADE_CODE NOT IN ( SELECT   * FROM   (SELECT   DISTINCT ST.SHADE_CODE, S.SHADE_FAMILY_CODE  FROM   OD_CUSTOMER_REQUEST_ST ST, OD_CUSTOMER_REQUEST_MST M,od_shade_family_trn s  WHERE   ST.COMP_CODE = M.COMP_CODE   AND ST.BRANCH_CODE = M.BRANCH_CODE  AND ST.YEAR = M.YEAR  AND ST.ORDER_NO = M.ORDER_NO   AND ST.SHADE_CODE = S.SHADE_CODE) WHERE   SHADE_CODE LIKE :SearchQuery  OR SHADE_FAMILY_CODE LIKE :SearchQuery  ORDER BY   SHADE_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY SHADE_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }



    protected void ddlORDER_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetOrderNo(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmdCustomer.Items.Clear();
                cmdCustomer.DataSource = data;
                cmdCustomer.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            //  e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article Loading.\r\nSee error log for detail."));

        }
    }


    protected DataTable GetOrderNo(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   *  FROM   (SELECT   DISTINCT M.ORDER_NO  FROM   OD_CUSTOMER_REQUEST_ST ST,  OD_CUSTOMER_REQUEST_MST M, od_shade_family_trn s  WHERE  ST.COMP_CODE = M.COMP_CODE   AND ST.BRANCH_CODE = M.BRANCH_CODE  AND ST.YEAR = M.YEAR  AND ST.ORDER_NO = M.ORDER_NO  AND M.ORDER_NO NOT LIKE '%DE%'  AND ST.SHADE_CODE = S.SHADE_CODE) WHERE   ORDER_NO LIKE :SearchQuery  ORDER BY   ORDER_NO) asd WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND SHADE_CODE NOT IN ( SELECT   * FROM   (SELECT   DISTINCT M.ORDER_NO  FROM   OD_CUSTOMER_REQUEST_ST ST,  OD_CUSTOMER_REQUEST_MST M, od_shade_family_trn s  WHERE  ST.COMP_CODE = M.COMP_CODE   AND ST.BRANCH_CODE = M.BRANCH_CODE  AND ST.YEAR = M.YEAR  AND ST.ORDER_NO = M.ORDER_NO AND M.ORDER_NO NOT LIKE '%DE%'   AND ST.SHADE_CODE = S.SHADE_CODE) WHERE   ORDER_NO LIKE :SearchQuery    ORDER BY   ORDER_NO) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ORDER_NO";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }


    private void BindYarnGrd()
    {
        string strBranch = string.Empty;
        string strParty = string.Empty;
        string strArticle = string.Empty;
        string strShadeCode = string.Empty;
        string DTCRFrom = string.Empty;
        string DTCRTo = string.Empty;
        string strStatus = string.Empty;
        string strCustNo = string.Empty;

        string MACHINE = string.Empty;
        string SHADE_NATURE = string.Empty;
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty && ddlBranch.SelectedIndex > 0)
            {
                strBranch = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                strBranch = string.Empty;
            }

            if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty && txtPartyCode.SelectedIndex > -1)
            {
                strParty = txtPartyCode.SelectedText.Trim();
            }
            else
            {
                strParty = string.Empty;
            }

            if (ddlArticle.SelectedValue.ToString() != null && ddlArticle.SelectedValue.ToString() != string.Empty && ddlArticle.SelectedIndex > -1)
            {
                strArticle = ddlArticle.SelectedText.Trim();
            }
            else
            {
                strArticle = string.Empty;
            }
            if (txtCustNo.Text != null && txtCustNo.Text != string.Empty)
            {
                strCustNo = txtCustNo.Text.ToUpper().Trim();
            }
            else
            {
                strCustNo = string.Empty;
            }
            if (txtshadecode.Text.ToString() != null && txtshadecode.Text.ToString() != string.Empty)
            {
                strShadeCode = txtshadecode.Text.ToString();
            }
            else
            {
                strShadeCode = string.Empty;
            }

            if (ddlBusinessType.SelectedItem.Text.ToString() != null && ddlBusinessType.SelectedItem.Text.ToString() != string.Empty && ddlBusinessType.SelectedItem.Text.ToString() != "--------All---------")
            {
                Business_TYPE = ddlBusinessType.SelectedItem.Text.ToString();
            }
            else
            {
                Business_TYPE = string.Empty;
            }

            if (ddlNatureShade.SelectedItem.Text.ToString() != null && ddlNatureShade.SelectedItem.Text.ToString() != string.Empty )
            {
                SHADE_NATURE = ddlNatureShade.SelectedItem.Text.ToString();
            }
            else
            {
                SHADE_NATURE = string.Empty;
            }

            if (ddlMachine.SelectedValue.ToString() != null && ddlMachine.SelectedValue.ToString() != string.Empty)
            {
                MACHINE = ddlMachine.SelectedText.ToString();
            }
            else
            {
                MACHINE = string.Empty;
            }



            if (txtCRFrom.Text.Trim().ToString() != null && txtCRFrom.Text.Trim().ToString() != string.Empty && txtCRTo.Text.Trim().ToString() != null && txtCRTo.Text.Trim().ToString() != string.Empty)
            {
                DTCRFrom = txtCRFrom.Text.Trim().ToString();
                DTCRTo = txtCRTo.Text.Trim().ToString();
            }
            else
            {
                DTCRFrom = string.Empty;
                DTCRTo = string.Empty;
            }

            if (ddlStatus.SelectedValue.ToString() != null && ddlStatus.SelectedValue.ToString() != string.Empty && ddlStatus.SelectedIndex > 0)
            {
                strStatus = ddlStatus.SelectedValue.ToString();
            }
            else
            {
                strStatus = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetCRForSalesMAchine(oUserLoginDetail.COMP_CODE, strBranch, oUserLoginDetail.DT_STARTDATE.Year, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo, strStatus, strCustNo, Business_TYPE, MACHINE, SHADE_NATURE);

           


            dt.Columns.Add("SHADE_GROUP");
            dt.Columns.Add("SHADE_SEQ");
            for(int y=0;y<dt.Rows.Count;y++ ) 
            {
                DataRow dr = dt.Rows[y];
                if (dr["SHADE_FAMILY"].ToString() == "BEIGE" || dr["SHADE_FAMILY"].ToString() == "GOLD" || dr["SHADE_FAMILY"].ToString() == "DK CREAM" || dr["SHADE_FAMILY"].ToString() == "DK CHIKOO" || dr["SHADE_FAMILY"].ToString() == "DK GOLD" || dr["SHADE_FAMILY"].ToString() == "CREAM" || dr["SHADE_FAMILY"].ToString() == "YELLOW" || dr["SHADE_FAMILY"].ToString() == "LT BEIGE" || dr["SHADE_FAMILY"].ToString() == "LT YELLOW" || dr["SHADE_FAMILY"].ToString() == "IVORY" || dr["SHADE_FAMILY"].ToString() == "CHIKOO" || dr["SHADE_FAMILY"].ToString() == "LT GOLD" || dr["SHADE_FAMILY"].ToString() == "LT CHIKOO" || dr["SHADE_FAMILY"].ToString() == "NEON YELLOW" || dr["SHADE_FAMILY"].ToString() == "LEMMON" || dr["SHADE_FAMILY"].ToString() == "LT .YELLOW")
                {                   
                    dt.Rows[y]["SHADE_GROUP"] = "YELLOW";
                    dt.Rows[y]["SHADE_SEQ"] = "2";
                    dt.AcceptChanges();
                   
                
                }


                else if (dr["SHADE_FAMILY"].ToString() == "ASH GREY" || dr["SHADE_FAMILY"].ToString() == "BLUE GREY" || dr["SHADE_FAMILY"].ToString() == "LT STEEL GREY" || dr["SHADE_FAMILY"].ToString() == "STEEL GREY" || dr["SHADE_FAMILY"].ToString() == "DK GREY" || dr["SHADE_FAMILY"].ToString() == "LT GREY" || dr["SHADE_FAMILY"].ToString() == "SILVER" || dr["SHADE_FAMILY"].ToString() == "GREY")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "GREY";
                    dt.Rows[y]["SHADE_SEQ"] = "4";
                    dt.AcceptChanges();

                }



                else if (dr["SHADE_FAMILY"].ToString() == "WHITE" || dr["SHADE_FAMILY"].ToString() == "BLAECH WHITE" || dr["SHADE_FAMILY"].ToString() == "OFF WHITE" || dr["SHADE_FAMILY"].ToString() == "RAW WHITE" || dr["SHADE_FAMILY"].ToString() == "KORA")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "WHITE";
                    dt.Rows[y]["SHADE_SEQ"] = "1";
                    dt.AcceptChanges();
                
                }
                else if (dr["SHADE_FAMILY"].ToString() == "BLACK" )
                {
                    dt.Rows[y]["SHADE_GROUP"] = "BLACK";
                    dt.Rows[y]["SHADE_SEQ"] = "11";
                    dt.AcceptChanges();

                }

                else if (dr["SHADE_FAMILY"].ToString() == "COFEE" || dr["SHADE_FAMILY"].ToString() == "WINE" || dr["SHADE_FAMILY"].ToString() == "LT COFEE" || dr["SHADE_FAMILY"].ToString() == "LT RUST" || dr["SHADE_FAMILY"].ToString() == "BROWN" || dr["SHADE_FAMILY"].ToString() == "MUSTERD" || dr["SHADE_FAMILY"].ToString() == "RUST" || dr["SHADE_FAMILY"].ToString() == "CHOCO" || dr["SHADE_FAMILY"].ToString() == "LT BROWN" || dr["SHADE_FAMILY"].ToString() == "DK BROWN" || dr["SHADE_FAMILY"].ToString() == "SUNF" || dr["SHADE_FAMILY"].ToString() == "DK WINE" || dr["SHADE_FAMILY"].ToString() == "COPPER" || dr["SHADE_FAMILY"].ToString() == "DK RUST")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "BROWN";
                    dt.Rows[y]["SHADE_SEQ"] = "6";
                    dt.AcceptChanges();

                }

                else if (dr["SHADE_FAMILY"].ToString() == "RAMA" || dr["SHADE_FAMILY"].ToString() == "Y GREEN" || dr["SHADE_FAMILY"].ToString() == "MEHENDI" || dr["SHADE_FAMILY"].ToString() == "GREEN" || dr["SHADE_FAMILY"].ToString() == "DK GREEN" || dr["SHADE_FAMILY"].ToString() == "LT GREEN" || dr["SHADE_FAMILY"].ToString() == "LT RAMA" || dr["SHADE_FAMILY"].ToString() == "KD MEHANDI" || dr["SHADE_FAMILY"].ToString() == "DK RAMA" || dr["SHADE_FAMILY"].ToString() == "P GREEN" || dr["SHADE_FAMILY"].ToString() == "PARROT GREEN" || dr["SHADE_FAMILY"].ToString() == "LT MEHANDI" || dr["SHADE_FAMILY"].ToString() == "BOTTLE GREEN" || dr["SHADE_FAMILY"].ToString() == "OLIVE" || dr["SHADE_FAMILY"].ToString() == "NEON GREEN" || dr["SHADE_FAMILY"].ToString() == "DK PISTA" || dr["SHADE_FAMILY"].ToString() == "PISTA" || dr["SHADE_FAMILY"].ToString() == "LEMON GREEN")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "GREEN";
                    dt.Rows[y]["SHADE_SEQ"] = "5";
                    dt.AcceptChanges();

                }


                else if (dr["SHADE_FAMILY"].ToString() == "MAROON" || dr["SHADE_FAMILY"].ToString() == "MAGENTA" || dr["SHADE_FAMILY"].ToString() == "LT MAROON" || dr["SHADE_FAMILY"].ToString() == "DK MAROON" || dr["SHADE_FAMILY"].ToString() == "BLOOD RED" || dr["SHADE_FAMILY"].ToString() == "DK RED" || dr["SHADE_FAMILY"].ToString() == "LT RED" || dr["SHADE_FAMILY"].ToString() == "BURGENDY" )
                {
                    dt.Rows[y]["SHADE_GROUP"] = "RED";
                    dt.Rows[y]["SHADE_SEQ"] = "4";
                    dt.AcceptChanges();

                }
                else if (dr["SHADE_FAMILY"].ToString() == "DUSTY BLUE" || dr["SHADE_FAMILY"].ToString() == "LT NAVY" || dr["SHADE_FAMILY"].ToString() == "LT BLUE" || dr["SHADE_FAMILY"].ToString() == "PURPLE" || dr["SHADE_FAMILY"].ToString() == "DK NAVY" || dr["SHADE_FAMILY"].ToString() == "VOILET" || dr["SHADE_FAMILY"].ToString() == "NAVY" || dr["SHADE_FAMILY"].ToString() == "DK BLUE" || dr["SHADE_FAMILY"].ToString() == "BLUE" || dr["SHADE_FAMILY"].ToString() == "LT PURPLE" || dr["SHADE_FAMILY"].ToString() == "ROAL BLUE" || dr["SHADE_FAMILY"].ToString() == "DK PURPLE" || dr["SHADE_FAMILY"].ToString() == "LT LAVENDAR" || dr["SHADE_FAMILY"].ToString() == "LAVENDAR" || dr["SHADE_FAMILY"].ToString() == "NEON BLUE")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "BLUE";
                    dt.Rows[y]["SHADE_SEQ"] = "7";
                    dt.AcceptChanges();

                }



                else if (dr["SHADE_FAMILY"].ToString() == "DK ROSE" || dr["SHADE_FAMILY"].ToString() == "DK PINK" || dr["SHADE_FAMILY"].ToString() == "NEON PINK" || dr["SHADE_FAMILY"].ToString() == "NEON RANI" || dr["SHADE_FAMILY"].ToString() == "DK RANI" || dr["SHADE_FAMILY"].ToString() == "LT PINK" || dr["SHADE_FAMILY"].ToString() == "RANI" || dr["SHADE_FAMILY"].ToString() == "PINK" || dr["SHADE_FAMILY"].ToString() == "LT RANI")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "RANI";
                    dt.Rows[y]["SHADE_SEQ"] = "8";
                    dt.AcceptChanges();

                }


                else if (dr["SHADE_FAMILY"].ToString() == "LT GAJRI" || dr["SHADE_FAMILY"].ToString() == "NEON ORANGE" || dr["SHADE_FAMILY"].ToString() == "DK ORANGE" || dr["SHADE_FAMILY"].ToString() == "GAJRI" || dr["SHADE_FAMILY"].ToString() == "ORANGE" || dr["SHADE_FAMILY"].ToString() == "DK GAJRI" || dr["SHADE_FAMILY"].ToString() == "LT ORANDE" || dr["SHADE_FAMILY"].ToString() == "PEACH" || dr["SHADE_FAMILY"].ToString() == "LT PEACH" || dr["SHADE_FAMILY"].ToString() == "DK PEACH" || dr["SHADE_FAMILY"].ToString() == "SINDURY")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "ORANGE";
                    dt.Rows[y]["SHADE_SEQ"] = "3";
                    dt.AcceptChanges();

                }


                else if (dr["SHADE_FAMILY"].ToString() == "SEA GREEN" || dr["SHADE_FAMILY"].ToString() == "SKY BLUE" || dr["SHADE_FAMILY"].ToString() == "LT SEA GREEN" || dr["SHADE_FAMILY"].ToString() == "LT SILVER" || dr["SHADE_FAMILY"].ToString() == "TURQUOISE" || dr["SHADE_FAMILY"].ToString() == "FIROZI" || dr["SHADE_FAMILY"].ToString() == "LT FIOZI")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "SKY BLUE";
                    dt.Rows[y]["SHADE_SEQ"] = "3";
                    dt.AcceptChanges();

                }

             
            
            }

            if (ddlShadeGroup.SelectedValue == "" || ddlShadeGroup.SelectedValue == null || ddlShadeGroup.SelectedValue == string.Empty)
            {
                dt.DefaultView.Sort = "SHADE_SEQ asc";
            }
            else
            {


                DataView dv = new DataView(dt);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dv.RowFilter = "SHADE_GROUP='" + ddlShadeGroup.SelectedValue.ToString() + "'";

                }
               
                 dt = dv.ToTable();
                
                

            }
            Session["dt"] = dt;
            
            
            
            if (dt != null && dt.Rows.Count > 0)
            {
                grdCustomerRequest.DataSource = dt;
                grdCustomerRequest.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
               // CalculateAllData();
                grdCustomerRequest.Visible = true;
            }
            else
            {
                grdCustomerRequest.DataSource = null;
                grdCustomerRequest.DataBind();
                lblTotalRecord.Text = "0";
                grdCustomerRequest.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));

        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();
            BindYarnGrd();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/SALE_MACHINE_PRINT.aspx?BRANCH="+ddlBranch.SelectedValue.ToString();
        URL += "&PRTY_CODE=" + txtPartyCode.SelectedText.ToString();
        URL += "&QUALITY=" + ddlArticle.SelectedValue.ToString();
        URL += "&BUSINESS=" + ddlBusinessType.SelectedValue.ToString();
        URL += "&SHADE_CODE=" + ddlshadecode.SelectedText.ToString();
        URL += "&DTCRFrom=" + txtCRFrom.Text.ToString();
        URL += "&DTCRTo=" + txtCRTo.Text.ToString();
        URL += "&SHADE_GROUP=" + ddlShadeGroup.SelectedValue.ToString();
        URL += "&ORDER_NO=" + cmdCustomer.SelectedValue.ToString();
        URL += "&MACHINE_NO=" + ddlMachine.SelectedValue.ToString();
        URL += "&SHADE_NATURE=" + ddlNatureShade.SelectedValue.ToString();
        Response.Redirect(URL);
        

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindYarnGrd();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtCRTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtCRFrom.Text == null || txtCRFrom.Text == string.Empty)
            {
                CommonFuction.ShowMessage("Please enter From CR Date first..");
            }
            else
            {
                if (DateTime.Parse(txtCRFrom.Text) > DateTime.Parse(txtCRTo.Text))
                {
                    CommonFuction.ShowMessage("Please From CR Date should not be greater than To CR Date..");
                }
                else
                {
                    //BindCRGrid();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request To Date TextBox.\r\nSee error log for detail."));

        }
    }

    protected void grdCustomerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindYarnGrd();

            grdCustomerRequest.PageIndex = e.NewPageIndex;
            grdCustomerRequest.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void CalculateAllData()
    {
        if (grdCustomerRequest.Rows.Count > 0)
        {
            double totalSoQty = 0;
            double totalApprovedQty = 0;
            double totalAdjustedQty = 0;
            double totalInvoiceQty = 0;
            double totalProdutionQty = 0;

            for (int i = 0; i < grdCustomerRequest.Rows.Count; i++)
            {
                double SoQty = 0;
                double ApprovedQty = 0;
                double AdjustedQty = 0;
                double InvoiceQty = 0;
                double ProductionQty = 0;



                Label lblSoQty = grdCustomerRequest.Rows[i].FindControl("lblSoQty") as Label;
                Label lblApprovedQty = grdCustomerRequest.Rows[i].FindControl("lblApprovedQty") as Label;
                Label lblAdjustedQty = grdCustomerRequest.Rows[i].FindControl("lblAdjustedQty") as Label;
                Label lblInvoiceQty = grdCustomerRequest.Rows[i].FindControl("lblInvoiceQty") as Label;
                Label lblProductionQty = grdCustomerRequest.Rows[i].FindControl("lblProductionQty") as Label;

                double.TryParse(lblSoQty.Text, out SoQty);
                double.TryParse(lblApprovedQty.Text, out ApprovedQty);
                double.TryParse(lblAdjustedQty.Text, out AdjustedQty);
                double.TryParse(lblInvoiceQty.Text, out InvoiceQty);
                totalSoQty = totalSoQty + SoQty;
                totalApprovedQty = totalApprovedQty + ApprovedQty;
                totalAdjustedQty = totalAdjustedQty + AdjustedQty;
                totalInvoiceQty = totalInvoiceQty + InvoiceQty;
                totalProdutionQty = totalProdutionQty + ProductionQty;


            }
            ((Label)grdCustomerRequest.FooterRow.FindControl("lblTotalSoQty")).Text = totalSoQty.ToString();
            ((Label)grdCustomerRequest.FooterRow.FindControl("lblTotalApprovedQty")).Text = totalApprovedQty.ToString();
            ((Label)grdCustomerRequest.FooterRow.FindControl("lblTotalAdjustedQty")).Text = totalAdjustedQty.ToString();
            ((Label)grdCustomerRequest.FooterRow.FindControl("lblTotalInvoiceQty")).Text = totalInvoiceQty.ToString();
            ((Label)grdCustomerRequest.FooterRow.FindControl("lblTotalProductionQty")).Text = totalProdutionQty.ToString();



        }
    }

    public void getBusiness()
    {
        try
        {

            ddlBusinessType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);
            ddlBusinessType.DataSource = dtProductionType;
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataBind();
            ddlBusinessType.Items.Insert(0, new ListItem("--------All---------", ""));
            //ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue(PRODUCT_TYPE));
            //ddlBusinessType.Text = PRODUCT_TYPE;
            //ddlProductType.Enabled = false;
        }
        catch
        {
            throw;
        }
    }
    protected void cmdCustomer_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        txtCustNo.Text = cmdCustomer.SelectedText.ToString();
    }
   
    protected void ddlshadecode_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        txtshadecode.Text = ddlshadecode.SelectedText.ToString();
    }
    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        BindMachineCode();
    }


    private void BindMachineCode()
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachine.Items.Clear();
                ddlMachine.DataSource = dt;
                ddlMachine.DataTextField = "MACHINE_CODE";
                ddlMachine.DataValueField = "MACHINE_CAPACITY";
                ddlMachine.DataBind();
                //ddlMachine.Items.Insert(0, new ListItem("------SELECT----", "0"));
            }
        }
        catch
        {
            throw;

        }
    }
}
