<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SalarySlip.aspx.cs" Inherits="Module_Admin_Pages_SalarySlip" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
<script type="text/javascript">
function NewWindow() {
document.forms[0].target = "_blank";
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table class="tContentArial" width="100%">
    <tr>
             <td width="850" align="left" class="TableHeader td">
                <table border="0"  width="850"  >
                    <tr>
                        <td align="center"  width="850" >
                            <span class="titleheading"><b>View Salary</b></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="850" align="left" class="td">
                <table border="0" cellpadding="3" cellspacing="0" width="850" class="tContentArial">
                    <tr>
                        <td colspan="8" align="center" valign="top" >
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                        </td>
                    </tr>
                    <tr><td width="10%" align="right" valign="top">
                            Employee
                        </td>
                        <td width="2%" align="left" valign="top">
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLEmployee" runat="server" Width="160px" CssClass="gCtrTxt">
                            </asp:DropDownList>                        
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="right" valign="top">
                            Branch
                        </td>
                        <td width="2%" align="left" valign="top">
                        </td>
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" CssClass="gCtrTxt">
                            </asp:DropDownList>
                        </td>
                        <td width="3%" align="left" valign="top">
                        </td>
                        <td width="10%" align="right" valign="top">
                            Department
                        </td>
                        <td width="2%" align="right" valign="top">
                        </td>
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" CssClass="gCtrTxt">
                            </asp:DropDownList>
                        </td>
                        <td width="13%" align="right" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="right" valign="top">
                            Month
                        </td>
                        <td width="2%" align="left" valign="top">
                        </td>
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="gCtrTxt" AutoPostBack="True">
                                <asp:ListItem Value="" Text="------Select-----"></asp:ListItem>
                                <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                <asp:ListItem Value="12" Text="December"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonth"
                                ErrorMessage="Pls select month" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td width="3%" align="left" valign="top">
                        </td>
                        <td width="10%" align="right" valign="top">
                            Year
                        </td>
                        <td width="2%" align="right" valign="top">
                        </td>
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="90px" CssClass="gCtrTxt">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                ErrorMessage="Pls select year" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td width="13%" align="right" valign="top">
                        </td>
                    </tr>
                    <tr>
                    <td colspan="8" align="center" valign="top" style="height: 25px;">
                         <br /><br />
                <asp:Button ID="btnPrint" Text="Print" runat="server" Width="75" OnClientClick="NewWindow();"  CssClass="AButton"  onclick="btnPrint_Click" />               
                    <br /><br />
                    </td>
                    </tr>
                </table>
            </td>
        </tr>       
       
</table>
</asp:Content>

