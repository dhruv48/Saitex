<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LotDetail.aspx.cs" Inherits="Module_PlanningAndScheduling_Pages_LotDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="Stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    
    <title>Lot Details</title>
    
    <script language="javascript" type="text/javascript">
    function BindYRNSPIN_BOM(COST, TextBoxCOST) {
            //window.opener.document.getElementById(TextBoxCOST).value=COST; 
            window.opener.document.forms[0].submit();
            window.close();

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
        .style1
        {
            width: 14%;
        }
    </style>
</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server" width="100%">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top" width="100%">
                            <strong class="titleheading">Lot Detail</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <table width="98%">
                                <tr bgcolor="#006699">
                        </td>
                        <td align="left" class="tdLeft SmallFont" valign="top" id="tdLotIdHeads" runat="server"
                            width="14%">
                            <span class="titleheading"><b>LOT ID</b></span>
                        </td>
                        <td align="left" class="tdLeft SmallFont" valign="top" width="14%" id="tdPA_No" runat="server">
                            <span class="titleheading"><b>PA_NO</b></span>
                        </td>
                        <td align="left" class="tdLeft SmallFont" valign="top" width="14%" id="tdProductTypeHeads"
                            runat="server">
                            <span class="titleheading"><b>PRODUCT TYPE</b></span>
                        </td>
                        <td align="left" class="tdLeft SmallFont" id="tdArticalCodeHeads" runat="server"
                            width="14%">
                            <span class="titleheading"><b>ARTICAL CODE</b></span>
                        </td>
                        <td align="left" class="tdLeft SmallFont" valign="top" id="tdShadeCodeHeads" runat="server"
                            width="14%">
                            <span class="titleheading"><b>SHADE CODE</b></span>
                        </td>
                        <td align="left" class="tdRight SmallFont" valign="top" width="14%" id="tdOrdQtyHeads"
                            runat="server">
                            <span class="titleheading"><b>ORD QTY</b></span>
                        </td>
                        <td align="left" class="tdRight SmallFont" valign="top" width="14%" id="tdLotQtyHeads"
                            runat="server">
                            <span class="titleheading"><b>LOT QTY</b></span>
                        </td>
                        <td align="left" class="tdRight SmallFont" valign="top" width="14%" id="tdRemQtyHeads"
                            runat="server">
                            <span class="titleheading"><b>REM QTY</b></span>
                        </td>
                        <td align="left" class="tdLeft SmallFont" valign="top" width="16%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" id="tdLotDet" runat="server" width="14%">
                            <asp:TextBox ID="txtLotId" runat="server" CssClass="SmallFont TextBoxDisplay" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" id="tdPANODet" runat="server" width="14%">
                            <asp:TextBox ID="txtPANO" runat="server" CssClass="SmallFont TextBoxDisplay" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" id="tdProductTypeDet" runat="server" width="14%">
                            <asp:TextBox ID="txtProdType" runat="server" CssClass="SmallFont TextBoxDisplay"
                                ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" id="td1" runat="server" width="14%">
                            <asp:TextBox ID="txtArticalCode" runat="server" CssClass="SmallFont TextBoxDisplay"
                                ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" id="tdShadeCode" runat="server" width="14%">
                            <asp:TextBox ID="txtShadeCode" runat="server" CssClass="SmallFont TextBoxDisplay"
                                ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" class="tdLeft SmallFont" valign="top" id="td2" runat="server" width="14%">
                            <asp:TextBox ID="txtOrdQty" runat="server" CssClass="SmallFont TextBoxDisplay" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" id="tdLotQty" runat="server" width="14%">
                            <asp:TextBox ID="txtLotQty" runat="server" CssClass="SmallFont TextBoxDisplay" OnTextChanged="txtLotQty_TextChanged">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" id="tdRemQTY" runat="server" width="14%">
                            <asp:TextBox ID="txtRemQty" runat="server" CssClass="SmallFont TextBoxDisplay" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="left" valign="top" width="24%">
                            <asp:Button ID="BtnLOTSave" runat="server" Text="Save" ValidationGroup="BA" Width="40px"
                                CssClass="SmallFont" OnClick="BtnLOTSave_Click" />
                            <asp:Button ID="BtnLOTCancel" runat="server" CssClass="SmallFont" Text="Cancel" Width="40px"
                                OnClick="BtnLOTCancel_Click" />
                        </td>
                    </tr>
                    <%--  <tr>
  <td id="Td3" align="left" valign="top" runat="server" class="style1">
     <asp:TextBox ID="txtArticleCode" runat="server" CssClass="SmallFont TextBoxDisplay"
                                            Width="100%"></asp:TextBox>--%>
                    <%--</td>--%>
                    <%--  <td align="left" valign="top" colspan="8" style="width: 38%">
                                      <asp:TextBox ID="txtArticleDesc" runat="server" Width="130%" 
                                            CssClass="SmallFont TextBoxDisplay"></asp:TextBox>
                                    </td>--%>
                    <%--  </tr>--%>
                    <tr>
                        <td id="Td4" align="left" valign="top" runat="server" class="style1">
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
                        <td align="right" valign="top" width="14%">
                            &nbsp;
                        </td>
                        <td align="left" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="9" valign="top" width="100%">
                            <asp:GridView ID="grdLOT" runat="server" AllowSorting="true" AutoGenerateColumns="false"
                                BorderWidth="1px" CssClass="SmallFont" Font-Bold="false" ShowFooter="true" Width="98%"
                                OnRowCommand="grdLOT_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="Top" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <%-- <asp:Label ID="lblLOTUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>--%>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="25px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductType" runat="server" Text='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PA NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPANO" runat="server" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Artical Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArticalCode" runat="server" Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shade Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LOT QTY">
                                        <%--  <HeaderStyle HorizontalAlign="Right" />--%>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLotQty" runat="server" Text='<%# Bind("LOT_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditLotValue" Text='<%# Bind("LOT_QTY") %>' CssClass="SmallFont"
                                                runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REM QTY" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRem_Qty" runat="server" Text='<%# Bind("REM_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemTemplate>
                                            <asp:Button ID="lnkLOTEdit" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                CommandName="LOTEdit" TabIndex="12" Text="Edit" />
                                            <asp:Button ID="lnkLOTDelete" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                CommandName="LOTDelete" OnClientClick="return confirm('Are you Sure want to delete this LOT Detail?');"
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
                </td> </tr>
                <tr>
                    <td align="center" class="td" valign="top" width="100%">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="SmallFont" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="M1" CssClass="SmallFont"
                            OnClick="btnClose_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="td" valign="top" width="100%">
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update Flag" CssClass="SmallFont"
                            OnClick="btnUpdate_Click" />
                    </td>
                </tr>
                </tr> 
                </table>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
