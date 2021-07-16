<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryDetails_OPT.aspx.cs"
    Inherits="HRMS_SalaryDetails_OPT" Title="Salary Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Details</title>
     <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" BackColor="#336799"  Height="600px" width="600">
                <table width="595" align="left" border="1" class="tContentArial">
                    <tr>
                        <td align="center" width="600">
                            <table width="600" align="left" border="0" class="tContentArial">
                                <tr>
                                    <td align="center" colspan="3">
                                        <span class="titleheading" style="font-size: 13pt">Salary Grade</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <br />
                                        <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                        </strong>
                                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError">
                                        </asp:Label><strong> </strong>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="15%" valign="top">
                                        <span class="titleheading12">Grade</span></td>
                                    <td align="left" width="2%" valign="top">
                                    </td>
                                    <td align="left" width="83%" valign="top">
                                        <asp:DropDownList ID="ddlGrade" runat="server" Width="200px" CssClass="gCtrTxt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <br /><br />
                                        <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" />
                                        <br />
                                        <br />
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
