<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetFabricPODisTax.aspx.cs" Inherits="Module_Inventory_Pages_GetFabricPODisTax" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get discount and taxes</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
    <%--  <link rel="stylesheet" type="text/css" href="../../../StyleSheet/ModalPopup.css" />
--%>

    <script language="javascript" type="text/javascript">
    function BindRate(Amount,TextBoxId)
    {          
        window.opener.document.getElementById(TextBoxId).value=Amount; 
        window.opener.document.forms[0].submit();      
        window.close();
    } 
    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Get Rate Component for
                                <asp:Label ID="Label1" runat="server"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <asp:Label ID="lblErrormsg" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdRight">
                            <table>
                                <tr>
                                    <td class="TableHeader">
                                        <span class="titleheading">Select Component</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Rate(%)</span>
                                    </td>
                                    <td class="TableHeader">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc3:OboutDropDownList ID="ddlRateComponent" runat="server" AppendDataBoundItems="True"
                                            MenuWidth="200px" Width="120px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </cc3:OboutDropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="SmallFont TextBox" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <cc3:OboutButton ID="btnSaveRateCompo" runat="server" OnClick="btnSaveRateCompo_Click"
                                            Text="Save">
                                        </cc3:OboutButton>
                                        <cc3:OboutButton ID="btnCancelRateCompo" runat="server" OnClick="btnCancelRateCompo_Click"
                                            Text="Cancel">
                                        </cc3:OboutButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            Base Rate :
                            <asp:TextBox ID="txtBaseRate" runat="server" CssClass="SmallFont TextBoxDisplay TextBox"
                                ReadOnly="true" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            Final Rate :
                            <asp:TextBox ID="txtFinalAmount" runat="server" CssClass="SmallFont TextBoxDisplay TextBox"
                                ReadOnly="true" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" ScrollBars="Vertical"
                                Height="150px">
                                <asp:GridView ID="grdRate" runat="server" AutoGenerateColumns="False" OnRowCommand="grdRate_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Component Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComponentName" runat="server" CssClass="SmallFont Label" Text='<%# Bind("COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate (%)">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="txtRate" runat="server" CssClass="SmallFont Label" AutoPostBack="True"
                                                    MaxLength="3" Text='<%# Bind("Rate") %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" CommandName="editRate"
                                                    CommandArgument='<%# Bind("UniqueId") %>' Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="delRate"
                                                    CommandArgument='<%# Bind("UniqueId") %>' Text="Remove"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
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
