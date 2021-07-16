<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="UpdateSalarySlip.aspx.cs" Inherits="Module_HRMS_Pages_UpdateSalarySlip"
    Title="Update Salary Slip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table border="1" cellpadding="0" cellspacing="0" width="750" class="tContentArial"
        align="left">
        <tr>
            <td valign="top" align="left">
                <table border="1" cellpadding="0" cellspacing="0" width="750" class="tContentArial">
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                            <asp:Label ID="lblTotalTest" runat="server" Visible="false"></asp:Label>
                       
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="tContentArial">
                                <tr>
                                    <td align="center" width="100%" style="height: 25px;" colspan="3">
                                        <asp:Label ID="lblComapanyName" runat="server" CssClass="reportHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="100%" style="height: 25px;" colspan="3">
                                        <asp:Label ID="lblPaySlipMonth_Year" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="left" style="white-space: nowrap;">
                                        <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblEnrolNumber" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblJoining" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="white-space: nowrap;">
                                        <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblPF" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblPayMode" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblBankName" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
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
                        <td width="100%" align="left">
                            <asp:Label ID="lblCurrentMonthCalculation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
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
                                                    <asp:GridView ID="gvBasicCalculation" Enabled="false" runat="server" ShowFooter="false"
                                                        ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" GridLines="None"
                                                        CssClass="tContentArial">
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
                                                                        Width="70px" CssClass="tContentArial" OnTextChanged="txtSalaryAmount_TextChanged"
                                                                        AutoPostBack="true"></asp:TextBox>
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
                                                    <asp:GridView ID="gvDeductionCalculation_Loans" Enabled="false" runat="server" ShowFooter="false"
                                                        ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" GridLines="None"
                                                        CssClass="tContentArial">
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
                                                                        Width="70px" CssClass="tContentArial" OnTextChanged="txtSalaryAmount_Loans_TextChanged"
                                                                        AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="33%" align="left" valign="top">
                                        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="tContentArial">
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:GridView ID="gvDeductionCalculation" Enabled="false" runat="server" ShowFooter="false"
                                                        ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" GridLines="None"
                                                        CssClass="tContentArial">
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
                                                                        Width="70px" CssClass="tContentArial" OnTextChanged="txtSalaryAmount_Deduction_TextChanged"
                                                                        AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="33%" align="left" valign="top" id="tdTotalAdditional" runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tContentArial"
                                            height="30px">
                                            <tr>
                                                <td width="100px" align="left" valign="middle">
                                                    
                                                </td>
                                                <td width="100px" align="right" valign="middle">
                                                    <asp:Label ID="lblTotalSubHeadAmount" runat="server"></asp:Label>
                                                    <asp:Label ID="lblTotaladditionSalay" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                <td width="80px">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="33%" align="left" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                                            <tr>
                                                <td width="15%" align="left" valign="top">
                                                   
                                                </td>
                                                <td width="2%" align="center" valign="top">
                                                    <b></b>
                                                </td>
                                                <td width="15%" align="left" valign="top">
                                                    <asp:Label ID="lblLoadTotal" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="33%" align="right" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                                            <tr>
                                                <td width="15%" align="left" valign="top">
                                                                                                        
                                                </td>
                                                <td width="2%" align="center" valign="top">
                                                    <b></b>
                                                </td>
                                                <td width="15%" align="left" valign="top">
                                                    <asp:Label ID="lblOtherDeduction" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lblActualTotal" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="750">
                    <tr>
                        <td align="left" height="40px" valign="bottom">
                            <b>Net Salary :
                                <asp:Label ID="lblNetSalary" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" height="40px" valign="middle">
                            <b>Salary in Words :
                                <asp:Label ID="lblFiguretoWord" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="675">
                            <br />
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" align="left">
                                <tr>
                                    <td width="15%" valign="top" align="left">
                                        Prepared By
                                    </td>
                                    <td width="2%" valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td width="83%" valign="top" align="left">
                                        <asp:Label ID="lblPreparedBy" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" valign="top" align="left">
                                        Prepared Date
                                    </td>
                                    <td width="2%" valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td width="83%" valign="top" align="left">
                                        <asp:Label ID="lblPreparedDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">
                            <br />
                            Want to Edit the Feild : &nbsp;&nbsp
                            <asp:CheckBox ID="chkActiveGrid" runat="server" OnCheckedChanged="chkActiveGrid_CheckedChanged"
                                AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td align="Center" valign="middle">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="75px" CssClass="AButton"
                                Visible="false" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
