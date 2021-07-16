<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddWOBOM.aspx.cs" Inherits="Module_WorkOrder_Pages_AddWOBOM" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generate BOM</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    
    function BindYRNSPIN_BOM(COST,TextBoxCOST)
    {          
//        window.opener.document.getElementById(TextBoxCOST).value=COST; 
        window.opener.document.forms[0].submit();      
        window.close();
    } 
    </script>

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
                            <strong class="titleheading">Supply Quality</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Base&nbsp;Product&nbsp;Type</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Base&nbsp;Article&nbsp;Code</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Base&nbsp;Shade&nbsp;Code</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>
                                    <%--  <td align="left" class="tdLeft SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Basis</b></span>
                                    </td>--%>
                                    <td align="left" class="tdRight SmallFont" valign="top" width="14%">
                                        <span class="titleheading"><b>Required&nbsp;Qty</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="16%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
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
                                        <asp:DropDownList ID="txtBOMArticleCode" runat="server" AppendDataBoundItems="True"
                                            CssClass="SmallFont TextBoxDisplay" TabIndex="16" Width="150px">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvArt" runat="server" ControlToValidate="txtBOMArticleCode"
                                            Display="None" ErrorMessage="Please Select Artical Code" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="BA"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        <asp:DropDownList ID="ddlshadeCode" runat="server"
                                            TabIndex="12" CssClass="SmallFont " Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top" width="14%">
                                        <asp:DropDownList ID="ddlBOMUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                            TabIndex="16" Width="98%">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvUOM" runat="server" ControlToValidate="ddlBOMUOM"
                                            Display="None" ErrorMessage="Please Select UOM" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="BA"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" valign="top" width="14%">
                                        <asp:TextBox ID="txtBOMRequiredQty" runat="server" CssClass="SmallFont TextBoxNo"
                                            MaxLength="6" TabIndex="17" Width="98%"></asp:TextBox>
                                        <br />
                                        <asp:RangeValidator ID="RangeValidator17" runat="server" ControlToValidate="txtBOMRequiredQty"
                                            Display="None" ErrorMessage="Please Enter  Value Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                            MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="BA"></asp:RangeValidator>
                                    </td>
                                    <td align="left" valign="top" width="16%">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Save"
                                            ValidationGroup="BA"  CssClass="SmallFont" Width="60px"/>
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click" Text="Cancel"  CssClass="SmallFont" Width="60px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="6" valign="top" width="100%">
                                        <asp:GridView ID="grdBOM" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBOMArticleDetail_RowCommand"
                                            ShowFooter="True" Width="98%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" Width="25px" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Base&nbsp;Product&nbsp;Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMProductType" runat="server" Text='<%# Bind("BASE_ARTICLE_TYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base&nbsp;Article&nbsp;Code">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_ARTICLE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Base&nbsp;Article&nbsp;Desc">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMArticleDesc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_ARTICLE_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base&nbsp;Shade&nbsp;Code">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBASE_SHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_SHADE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Required&nbsp;Qty">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMRequiredQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnkBOMEdit" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMEdit" TabIndex="12" Text="Edit"  CssClass="SmallFont" Width="50px"/>
                                                        <asp:Button ID="lnkBOMDelete" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                                            TabIndex="12" Text="Delete"  CssClass="SmallFont" Width="50px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top" width="100%">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1"  CssClass="SmallFont" Width="100px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
