<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SalarySlipView.aspx.cs" Inherits="Module_HRMS_Pages_SalarySlipView" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table border="0" align="left" cellpadding="0" cellspacing="0" width="675" class="tContentArial">
        <tr>
            <td width="100%" align="left">
                <table border="1" cellpadding="0" cellspacing="0" width="675" class="tContentArial">
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="tContentArial">
                                <tr>
                                    <td align="center" width="100%" style="height: 10px;" colspan="3">
                                        <asp:Label ID="lblComapanyName" runat="server" CssClass="reportHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="100%" style="height: 10px;" colspan="3">
                                        <asp:Label ID="lblPaySlipMonth_Year" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100%" valign="top">
                                        <table border="0" cellpadding="3" cellspacing="0" width="100%" class="tContentArial">
                                            <tr>
                                                <td align="left" width="15%" valign="top">
                                                    Employee Name
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    Employee Code
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblEnrolNumber" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    Joining Date
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblJoining" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="15%" valign="top">
                                                    Father Name
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    Branch
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblBranchName" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    Department
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="15%" valign="top">
                                                    Designation
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    Pay Mode
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblPayMode" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    Bank Name
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblBankName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="15%" valign="top">
                                                    A/C No.
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    PF No.
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                    <asp:Label ID="lblPF" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                </td>
                                                <td align="center" width="2%" valign="top">
                                                </td>
                                                <td align="left" width="15%" valign="top">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" valign="top">
                            <table border="1" cellpadding="0" cellspacing="0" width="100%" align="center" class="tContentArial">
                                <tr>
                                    <td width="33%" align="left" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                                            <tr>
                                                <td colspan="2" align="right" width="170">
                                                    Earnings
                                                </td>
                                                <td align="right">
                                                    Arrears&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="33%" align="Center" valign="top">
                                        Loans
                                    </td>
                                    <td width="33%" align="Center" valign="top">
                                        Other Deduction
                                    </td>
                                </tr>
                                <tr>
                                    <td width="33%" align="left" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:GridView ID="gvBasicCalculation" runat="server" ShowFooter="false" ShowHeader="false"
                                                        AutoGenerateColumns="false" AllowPaging="false" GridLines="None" 
                                                        CssClass="tContentArial10" onrowdatabound="gvBasicCalculation_RowDataBound">
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SalaryName" HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="" ItemStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSalaryName" runat="server" Text='<%#Eval("SalaryName") %>'></asp:Label>
                                                                    <asp:Label ID="lblSalaryId" runat="server" Text='<%#Eval("SalaryId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblSalaryFieldName" runat="server" Text='<%#Eval("FieldName") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Earning" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    
                                                                    <asp:TextBox ID="txtSalaryAmount" Text='<%#Eval("SalaryAmount") %>' runat="server"
                                                                        Width="70px" CssClass="tContentArial10" 
                                                                        ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td valign="top">
                                                </td>
                                            </tr>
                                            <%--<tr>
                                    <td valign="top" width="100px">
                                        Employer's Epf
                                    </td>
                                    <td valign="top" width="70px" align="right">
                                        <asp:TextBox ID="lblEmployerEpf" runat="server" Width="70px" CssClass="tContentArial"
                                            Enabled="False"></asp:TextBox></td>
                                    <td valign="top" width="90px" align="right">
                                    </td>
                                </tr>--%>
                                        </table>
                                    </td>
                                    <td width="33%" align="left" valign="top">
                                        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:GridView ID="gvDeductionCalculation_Loans" runat="server" ShowFooter="false"
                                                        ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" GridLines="None"
                                                        CssClass="tContentArial10">
                                                        <HeaderStyle Font-Bold="false" />
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SalaryName" HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="" ItemStyle-Width="150px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSalaryName" runat="server" Text='<%#Eval("SalaryName") %>'></asp:Label>
                                                                    <asp:Label ID="lblSalaryId" runat="server" Text='<%#Eval("SalaryId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblSalaryFieldName" runat="server" Text='<%#Eval("FieldName") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Earning" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSalaryAmount" Text='<%#Eval("SalaryAmount") %>' runat="server"
                                                                        Width="70px" CssClass="tContentArial10" 
                                                                        ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial"
                                                        height="15px">
                                                        <tr>
                                                            <td width="30%" align="left" valign="middle">
                                                                &nbsp;&nbsp;Total :
                                                            </td>
                                                            <td width="70%" align="right" valign="middle">
                                                                <asp:Label ID="lblLoadTotal" runat="server"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="33%" align="left" valign="top">
                                        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:GridView ID="gvDeductionCalculation" runat="server" ShowFooter="false" ShowHeader="false"
                                                        AutoGenerateColumns="false" AllowPaging="false" GridLines="None" CssClass="tContentArial10">
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SalaryName" HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />--%>
                                                            <asp:TemplateField HeaderText="" ItemStyle-Width="150px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSalaryName" runat="server" Text='<%#Eval("SalaryName") %>'></asp:Label>
                                                                    <asp:Label ID="lblSalaryId" runat="server" Text='<%#Eval("SalaryId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblSalaryFieldName" runat="server" Text='<%#Eval("FieldName") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Earning" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSalaryAmount" Text='<%#Eval("SalaryAmount") %>' runat="server"
                                                                        Width="70px" CssClass="tContentArial10" 
                                                                        ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial"
                                                        height="15px">
                                                        <tr>
                                                            <td width="30%" align="left" valign="middle">
                                                                &nbsp;&nbsp;Total :
                                                            </td>
                                                            <td width="70%" align="right" valign="middle">
                                                                <asp:Label ID="lblOtherDeduction" runat="server"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="33%" align="left" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial"
                                            height="15px">
                                            <tr>
                                                <td width="100px" align="left" valign="middle">
                                                    &nbsp;&nbsp;Total :
                                                </td>
                                                <td width="70px" align="right" valign="middle">
                                                    <asp:Label ID="lblTotaladditionSalay" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td width="80px">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="33%" align="left" valign="top">
                                    </td>
                                    <td width="33%" align="right" valign="middle">
                                        <asp:Label ID="lblActualTotal" runat="server"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" align="left">
               <br />
               <table border="1" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                    <tr>
                        <td align="left" width="100%" valign="top">
                            <asp:Label ID="lblCurrentMonthCalculation" runat="server"></asp:Label>        
                        </td>
                    </tr>
                    </table>
                
            </td>
        </tr>
        <tr>
            <td align="left" width="100%" valign="top">
                <br />
                <table border="1" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                    <tr>
                        <td width="300" align="left">
                            <asp:Label ID="lblLeaveDetail" runat="server"></asp:Label>
                        </td>
                        <td width="300" align="left" style="left: 10px;">
                            <asp:Label ID="lblTotalLeaveDetail" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" align="left">
                <br />
                <table border="0" cellpadding="0" cellspacing="0" width="675">
                    <tr>
                        <td align="left" height="15px" valign="bottom">
                            <b>Net Salary :
                                <asp:Label ID="lblNetSalary" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" height="15px" valign="middle">
                            <b>Salary in Words :
                                <asp:Label ID="lblFiguretoWord" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <%--<tr>
                            <td align="left" valign="middle">
                                Want to Edit the Feild : &nbsp;&nbsp
                                <asp:CheckBox ID="chkActiveGrid" runat="server" OnCheckedChanged="chkActiveGrid_CheckedChanged"
                                    AutoPostBack="True" />
                            </td>
                        </tr>--%>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

