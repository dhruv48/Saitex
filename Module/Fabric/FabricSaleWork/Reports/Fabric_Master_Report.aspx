<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fabric_Master_Report.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Reports_Fabric_Master_Report" %>



<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body bgcolor="#AFCAE4">
    <form id="form1" runat="server">
              
            <table align="left" class="tContentArial" border = "1" >
               
                <tr>
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="6">
                                    <span style="font-size: 13pt" class="titleheading"><strong>Yarn Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                   Fabric Code
                                </td>
                                <td align="left">
                              <%-- // <asp:DropDownList ID="ddlFabricCode" runat="server"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtYarnCodeRpt" runat="server" Width="150px" 
                                        CssClass="SmallFont TextBox"></asp:TextBox>
                                </td>
                                <td align="left">
                                    Department Code
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDeptCodeRpt" runat="server" Width="150px" 
                                        CssClass="SmallFont TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Fabric Type
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCatCodeRpt" runat="server" Width="150px" 
                                        CssClass="SmallFont TextBox"></asp:TextBox>
                                </td>
                                <td align="left">
                                    Branch Code
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBranchCodeRpt" CssClass="SmallFont TextBox" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                        
                                    
                                    <cc1:oboutbutton ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" 
                                        Text="Get Report" /></cc1:OboutButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
           
    
    </form>
</body>
</html>
