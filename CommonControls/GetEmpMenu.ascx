<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetEmpMenu.ascx.cs" Inherits="CommonControls_GetEmpMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table bgcolor="#AFCAE4" style="border-right: #000099 1px solid; border-top: #000099 1px solid; border-left: #000099 1px solid; border-bottom: #000099 1px solid">
    <tr>
        <td align="left" valign="top" bgcolor="LightSteelBlue">
            <asp:Panel ID="MenuPanel"  runat="server">
                <table border="0" cellpadding="0" cellspacing="1" bgcolor="LightSteelBlue" width="100%" class="toplink">
                    <tr>
                        <td height="26" align="center" width="100%" bgcolor="#29537c" class="heading1">Employee Module</td>
                    </tr>                   
                    <tr>
                      <td valign="top">
                        <asp:TreeView ID="TreeView1"  runat="server"  CssClass="tContentArial" Height="400px">
                       <Nodes>                                         
                         <asp:TreeNode Text="Employee Module" Expanded="false"  ImageUrl="~/CommonImages/addmodule.jpg">
                                               <asp:TreeNode Text="Leave Application" NavigateUrl="~/Module/HRMS/Pages/LeaveApplication.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                              <asp:TreeNode Text="Leave Request" NavigateUrl="~/Module/HRMS/Pages/Employee_Request_for_Leave.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                              <asp:TreeNode Text="Leave Detail" NavigateUrl="~/Module/HRMS/Pages/EmpLeaveDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                              <asp:TreeNode Text="Out-Door Duty" NavigateUrl="~/Module/HRMS/Pages/OutDoorDutyForm.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                               <asp:TreeNode Text="Out-Door Duty Request" NavigateUrl="~/Module/HRMS/Pages/ODApplovalForm.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                <asp:TreeNode Text="Out-Door Detail " NavigateUrl="~/Module/HRMS/Pages/Out_DoorDutyDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                              <asp:TreeNode Text="Optional Holiday" NavigateUrl="~/Module/HRMS/Pages/EmpOptionalLeaveDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                               <asp:TreeNode Text="Investment Declartion" NavigateUrl="~/Module/HRMS/Pages/InvesmentDeclaration1.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                               <asp:TreeNode Text="Loan Application" NavigateUrl="~/Module/HRMS/Pages/EmpApplication_For_Loan.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                <asp:TreeNode Text="Loan Request" NavigateUrl="~/Module/HRMS/Pages/EmpLoanRequest.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                 <asp:TreeNode Text="Loan Detail" NavigateUrl="~/Module/HRMS/Pages/LoanDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                 <asp:TreeNode Text="Appraisal Form" NavigateUrl="~/Module/HRMS/Pages/AppraisalForm.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                 <asp:TreeNode Text="Salary Slip" NavigateUrl="~/Module/HRMS/Pages/SalarySlipView.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                 <asp:TreeNode Text="tESTING" NavigateUrl="~/Module/HRMS/Pages/Default2.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                                 
                       </asp:TreeNode> 
                      </Nodes>
                                    </asp:TreeView>     
                                        </td>
                                    </tr> 
                                               
                </table>
            </asp:Panel>
            <asp:Panel ID="SubMenuPanel" runat="server">
               
            </asp:Panel>
            <asp:Panel ID="CollapsiblePanel" runat="server">
            </asp:Panel>
        </td>
        <td align="left" valign="top" bgcolor="LightSteelBlue">
            <asp:ImageButton ID="imgbtn" runat="server" Width="15px" Height="15px" ImageUrl="~/CommonImages/icon2.jpg"
                OnClick="imgbtn_Click" />
        </td>
    </tr>
</table>
<cc1:CollapsiblePanelExtender ID="cpe" runat="server" CollapseControlID="imgbtn"
    Collapsed="false" ExpandControlID="imgbtn" ExpandDirection="Horizontal" ScrollContents="false"
    SuppressPostBack="True" TargetControlID="MenuPanel">
</cc1:CollapsiblePanelExtender>
<asp:HiddenField ID="HiddenField1" runat="server" />