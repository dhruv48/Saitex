<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="GridDispayofSalary.aspx.cs" Inherits="Module_HRMS_Pages_GridDispayofSalary"
    Title="View Salary Slip" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="850" align="left" class="tContentArial">
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
                <asp:Button ID="btnView" Text="View" runat="server" Width="75" OnClick="btnView_Click"
                    CssClass="AButton" />                   
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td width="100%" align="left" >
                
                <table border="0" cellpadding="0" cellspacing="0" width="100%" align="left" class="tContentArial">
                    <tr>
                        <td width="100%" align="left">
                            <b>Total Record : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="850" align="left" class="td" >
                <table border="0" cellpadding="0" cellspacing="0" width="850"  align="left" class="tContentArial">
                    
                    <tr>
                        <td width="850" align="left">
                            <asp:GridView ID="gvSalaryDisplay" AutoGenerateColumns="false" AllowPaging="true"
                                GridLines="Both" PagerSettings-Mode="Numeric" PagerSettings-Position="Bottom"
                                PagerStyle-HorizontalAlign="Left" PageSize="20" runat="server" ShowFooter="false"
                                ShowHeader="true" OnRowCommand="gvSalaryDisplay_RowCommand" 
                                onpageindexchanging="gvSalaryDisplay_PageIndexChanging">
                                <HeaderStyle HorizontalAlign="Center" Width="850" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Month" DataField="SAL_MONTH" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Year" DataField="SAL_YEAR" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Employee Code" DataField="EMP_CODE" ItemStyle-Width="80"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Paid Day" DataField="PAID_DAY" ItemStyle-Width="80" ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Total Amount" DataField="ERN_AMT" ItemStyle-Width="100"
                                        ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Total Loan" DataField="LOAN_AMT" ItemStyle-Width="100"
                                        ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Total Deduction" DataField="DEDCT_AMT" ItemStyle-Width="100"
                                        ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Net Salary" DataField="NET_SAL" ItemStyle-Width="100"
                                        ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Status" DataField="LOCK_LV" ItemStyle-Width="75" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="View" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" Text="VIEW" Font-Size="11PX" Font-Bold="true" runat="server"
                                                CommandArgument='<%# Eval("SAL_SLIP_MST_ID") %>' CommandName="SalarySlipEdit">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary Print" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="./PrintSalarySlip.aspx?SalaryId=<%# Eval("SAL_SLIP_MST_ID") %>"><b>Print</b></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</asp:Content>
