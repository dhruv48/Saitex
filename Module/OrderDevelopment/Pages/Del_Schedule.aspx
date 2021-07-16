<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Del_Schedule.aspx.cs" Inherits="Module_OrderDevelopment_Pages_Del_Schedule" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get Delivery Schedule</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    window.onload = function()
    {
        window.opener.document.body.disabled=true;  
        window.opener.document.body.readonly=true;    
    }
    
    window.onunload = function()
    {
        window.opener.document.body.disabled=false;
        window.opener.document.body.readonly=true;  
    }

    function BindDelShedule(QTY,TextBoxOrderQty,DelDate ,TextBoxDelDate )
    {          
        window.opener.document.getElementById(TextBoxOrderQty).value=QTY; 
        window.opener.document.getElementById(TextBoxDelDate).value=DelDate; 
        window.opener.document.forms[0].submit();      
        window.close();        
    }
    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="SmallFont">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">DelIvery Schedule</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <asp:Label ID="lblErrormsg" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        Final Delivery Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFinalDelDate" runat="server" CssClass="SmallFont TextBoxDisplay TextBoxNo"
                                            ReadOnly="true" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Total Delivery Quantity :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalQuantity" runat="server" CssClass="SmallFont TextBoxDisplay TextBoxNo"
                                            ReadOnly="true" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdRight">
                            <table>
                                <tr>
                                    <%--   <td class="TableHeader">
                                        <span class="titleheading">Delivery Address</span>
                                    </td>--%>
                                    <td class="TableHeader">
                                        <span class="titleheading">Delivery Date</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Delivery Qty</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Delivery Remarks</span>
                                    </td>
                                    <td class="TableHeader">
                                    </td>
                                </tr>
                                <tr>
                                    <%-- <td>
                                        <asp:TextBox ID="txtDelAddress" runat="server" CssClass="SmallFont TextBox" Width="250px"></asp:TextBox>
                                    </td>--%>
                                    <td>
                                        <asp:TextBox ID="txtDelDate" runat="server" CssClass="SmallFont TextBox" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDelQuantity" runat="server" CssClass="SmallFont TextBoxNo" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDelRemarks" runat="server" CssClass="SmallFont TextBoxNo" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSaveDelRow" runat="server" OnClick="btnSaveDelRow_Click" Text="Save">
                                        </asp:Button>
                                        <asp:Button ID="btnCancelDelRow" runat="server" OnClick="btnCancelDelRow_Click" Text="Cancel">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Panel ID="pnlDelSchedule" runat="server" BackColor="#afcae4" ScrollBars="Vertical"
                                Height="150px">
                                <asp:GridView ID="grdDelSchedule" runat="server" AutoGenerateColumns="False" OnRowCommand="grdDelSchedule_RowCommand">
                                    <Columns>
                                        <%-- <asp:TemplateField HeaderText="Delivery Address">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDelAdd" runat="server" CssClass="SmallFont Label" Text='<%# Bind("DEL_ADDRESS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Delivery Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDelDate" runat="server" CssClass="SmallFont Label" Text='<%# Bind("DEL_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDelQty" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("DEL_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDelRemarks" runat="server" CssClass="SmallFont Label" Text='<%# Bind("DEL_REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" CommandName="editDEL"
                                                    CommandArgument='<%# Bind("UNIQUE_ID") %>' Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="delDEL"
                                                    CommandArgument='<%# Bind("UNIQUE_ID") %>' Text="Remove"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                                <cc1:CalendarExtender ID="ceDeldate" runat="server" TargetControlID="txtDelDate"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1" />
                            <asp:Label ID="PI_TYPE" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="ARTICAL_CODE" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
