<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Packing.aspx.cs" Inherits="Module_OrderDevelopment_Pages_Packing" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generate PACK</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    
    function BindYRNSPIN_PACK(COST,TextBoxCOST)
    {          
//        window.opener.document.getElementById(TextBoxCOST).value=COST; 
        window.opener.document.forms[0].submit();      
        window.close();
    } 
     
    function Calculation(val)
    {                                                                
     document.getElementById('txtTotalPACK').value=(parseFloat(document.getElementById('txtSale').value)+parseFloat(document.getElementById('txtFreight').value)+parseFloat(document.getElementById('txtCommission').value)+parseFloat(document.getElementById('txtIncentives').value)+parseFloat(document.getElementById('txtExMill').value)+parseFloat(document.getElementById('txtBrokerage').value)).toFixed(3);
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
                            <strong class="titleheading">Packing Details</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="25%">
                                        <span class="titleheading"><b>Packing Code</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="25%">
                                        <span class="titleheading"><b>Quantity</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="25%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" width="25%">
                                        <asp:DropDownList ID="ddlPACK_CODE" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="SmallFont" OnSelectedIndexChanged="ddlPACK_CODE_SelectedIndexChanged"
                                            TabIndex="16" Width="98%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top" width="25%">
                                        <asp:TextBox ID="txtPACKQty" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                            TabIndex="17" Width="98%"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top" width="25%">
                                        <asp:Button ID="BtnPACKSave" runat="server" OnClick="BtnPACKSave_Click" Text="Save"
                                            ValidationGroup="BA" />
                                        <asp:Button ID="BtnPACKCancel" runat="server" OnClick="BtnPACKCancel_Click" Text="Cancel" />
                                        <asp:RangeValidator ID="RangeValidator17" runat="server" 
                                            ControlToValidate="txtPACKQty" Display="None" 
                                            ErrorMessage="Please Enter  Value Quantity in Numeric &amp; Precision Should be 7 and Scale 2   " 
                                            MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="BA"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" valign="top">
                                        <asp:TextBox ID="lblPACKING_DESC" runat="server" 
                                            CssClass="SmallFont TextBoxDisplay" ReadOnly="true" Width="98%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" valign="top" width="100%">
                                        <asp:GridView ID="grdPACK" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdPACKArticleDetail_RowCommand"
                                            ShowFooter="True" Width="98%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPACKUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Packing Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdPACK_CODE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PCK_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Packing Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdPACK_DESC" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PCK_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrdPACK_QTY" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PCK_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnkPACKEdit" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="PACKEdit" TabIndex="12" Text="Edit" />
                                                        <asp:Button ID="lnkPACKDelete" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="PACKDelete" OnClientClick="return confirm('Are you Sure want to delete this PACK Detail?');"
                                                            TabIndex="12" Text="Delete" />
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
                                ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
