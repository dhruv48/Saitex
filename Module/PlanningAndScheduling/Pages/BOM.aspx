<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BOM.aspx.cs" Inherits="Module_PlanningAndScheduling_Pages_BOM" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViewBOM</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">

        function BindYRNSPIN_BOM(COST, TextBoxCOST) {
            //window.opener.document.getElementById(TextBoxCOST).value=COST; 
            window.opener.document.forms[0].submit();
            window.close();
        }

        function Calculation(val) {
            document.getElementById('txtTotalBOM').value = (parseFloat(document.getElementById('txtSale').value) + parseFloat(document.getElementById('txtFreight').value) + parseFloat(document.getElementById('txtCommission').value) + parseFloat(document.getElementById('txtIncentives').value) + parseFloat(document.getElementById('txtExMill').value) + parseFloat(document.getElementById('txtBrokerage').value)).toFixed(3);
        }
     
    </script>

    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
        .header
        {
            margin-left: 2px;
        }
        .c1
        {
            width: 120px;
        }
        .c2
        {
            margin-left: 4px;
            width: 400px;
        }
    </style>
</head>
<body bgcolor="#afcae4" width="100%">
    <form id="form1" style="background-color: #afcae4" runat="server" width="100%">
    <div width="100%">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top" width="100%">
                            <strong class="titleheading">BOM</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top" id="tdWarpHeads" runat="server"
                                        width="14%">
                                        <span class="titleheading"><b>Warp/Weft</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Product Type</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Article Code</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" id="tdShadeCode" runat="server" valign="top" width="14%">
                                        <span class="titleheading"><b>Shade Code</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>
                                    <%--  <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Basis</b></span>
                                    </td>--%>
                                    <td align="left" class="tdRight SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Required Qty</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="16%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" width="14%" id="tdWarpDet" runat="server">
                                        <asp:DropDownList ID="ddlW_Side" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="SmallFont" OnSelectedIndexChanged="ddlBOMProductType_SelectedIndexChanged"
                                            TabIndex="16" Width="98%">
                                            <asp:ListItem Text="NA" Value="NA"></asp:ListItem>
                                            <asp:ListItem Text="WARP" Value="WARP"></asp:ListItem>
                                            <asp:ListItem Text="WEFT" Value="WEFT"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        <asp:DropDownList ID="ddlBOMProductType" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" CssClass="SmallFont" OnSelectedIndexChanged="ddlBOMProductType_SelectedIndexChanged"
                                            TabIndex="16" Width="98%">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvproduct" runat="server" ControlToValidate="ddlBOMProductType"
                                            Display="None" ErrorMessage="Please Select Product Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="BA"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        <cc2:ComboBox ID="cmbArticle" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            EnableLoadOnDemand="True" DataTextField="CODE" DataValueField="Combined" MenuWidth="650"
                                            OnLoadingItems="cmbArticle_LoadingItems" OnSelectedIndexChanged="cmbArticle_SelectedIndexChanged"
                                            EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11" Visible="true"
                                            Height="200px">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    CODE</div>
                                                <div class="header c2">
                                                    DESCRIPTION</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("CODE") %></div>
                                                <div class="item c2">
                                                    <%# Eval("YARNDESC") %></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                        <%--  <asp:RequiredFieldValidator ID="rfvArt" runat="server" ControlToValidate="txtBOMArticleCode"
                                            Display="None" ErrorMessage="Please Select Artical Code" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="BA"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td align="left" id="tdshadeDropDown" valign="top" width="14%" runat="server">
                                        <asp:DropDownList ID="ddlShadeCode" runat="server" AppendDataBoundItems="True" CssClass="SmallFont "
                                            TabIndex="16" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top" width="5%">
                                        <%--<asp:DropDownList ID="ddlBOMUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                            TabIndex="16" Width="98%">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtUom" runat="server" CssClass="SmallFont TextBoxDisplay" ReadOnly="True"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvUOM" runat="server" ControlToValidate="ddlBOMUOM"
                                            Display="None" ErrorMessage="Please Select UOM" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="BA"></asp:RequiredFieldValidator>--%>
                                        <br />
                                    </td>
                                    <%--   <td align="left" valign="top" width="14%">
                                        <asp:DropDownList ID="ddlBOMBasis" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                            TabIndex="16" Width="98%">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvBasis" runat="server" ControlToValidate="ddlBOMBasis"
                                            Display="None" ErrorMessage="Please Select Basis" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="BA"></asp:RequiredFieldValidator>
                                    </td>--%>
                                    <td align="right" valign="top" width="14%">
                                        <asp:TextBox ID="txtBOMRequiredQty" runat="server" CssClass="SmallFont TextBoxNo"
                                            MaxLength="6" TabIndex="17" Width="98%"></asp:TextBox>
                                        <br />
                                        <asp:RangeValidator ID="RangeValidator17" runat="server" ControlToValidate="txtBOMRequiredQty"
                                            Display="None" ErrorMessage="Please Enter  Value Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                            MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="BA"></asp:RangeValidator>
                                    </td>
                                    <td align="left" valign="top" width="24%">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Save"
                                            ValidationGroup="BA" CssClass="SmallFont" Width="40px" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click"
                                            Text="Cancel" CssClass="SmallFont" Width="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" width="14%" runat="server">
                                        <asp:TextBox ID="txtArticleCode" runat="server" CssClass="SmallFont TextBoxDisplay"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" colspan="6" style="width: 28%">
                                        <asp:TextBox ID="txtArticleDesc" runat="server" Width="100%" CssClass="SmallFont TextBoxDisplay"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" width="14%" runat="server">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        &nbsp;
                                    </td>
                                    <td align="right" valign="top" width="14%">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="7" valign="top" width="100%">
                                        <asp:GridView ID="grdBOM" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBOMArticleDetail_RowCommand"
                                            ShowFooter="True" Width="98%" OnRowDataBound="grdBOM_RowDataBound" OnRowEditing="grdBOM_RowEditing"
                                             onrowcancelingedit="grdBOM_RowCancelingEdit" 
                                            onrowupdated="grdBOM_RowUpdated" onrowupdating="grdBOM_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Warp/Weft">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMW_SIDE" runat="server" Text='<%# Bind("W_SIDE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ProductType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMProductType" runat="server" Text='<%# Bind("BASE_ARTICAL_TYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Code">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_ARTICAL_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shade Code">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_SHADE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Right">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value Qty" HeaderStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditValue" Text='<%# Bind("VALUE_QTY") %>' CssClass="SmallFont" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("VALUE_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Required Qty" HeaderStyle-HorizontalAlign="Right">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMRequiredQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditReqValue" Text='<%# Bind("REQ_QTY") %>' CssClass="SmallFont" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:CommandField ShowEditButton="true" ButtonType="Button"/>
                                           
                                                <asp:TemplateField HeaderText="" Visible ="false"  >
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnkBOMEdit" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                                                        <asp:Button ID="lnkBOMDelete" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMDelete" Enabled="False" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                                            TabIndex="12" Text="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="HeaderStyle " ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top" width="100%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="SmallFont" />
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" ValidationGroup="M1"
                                CssClass="SmallFont" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="td" valign="top" width="100%">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Flag" CssClass="SmallFont"
                                OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
