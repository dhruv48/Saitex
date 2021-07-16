<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetUserMenu.ascx.cs" Inherits="Admin_UserControls_GetUserMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table bgcolor="#AFCAE4" style="border-right: #000099 1px solid; border-top: #000099 1px solid;
    border-left: #000099 1px solid; border-bottom: #000099 1px solid">
    <tr>
        <td align="left" valign="top" bgcolor="LightSteelBlue">
            <asp:Panel ID="MenuPanel" runat="server">
                <table border="0" cellpadding="0" cellspacing="1" bgcolor="LightSteelBlue" width="100%"
                    class="toplink">
                    <tr>
                        <td height="26" align="center" width="100%" bgcolor="#29537c" class="heading1">
                            User Modules
                        </td>
                    </tr>
                </table>
                <table id="tblHideEmpMenu" runat="server">
                    <tr>
                        <td>
                            <table id="tblMainLinkc" runat="server" cellpadding="1" cellspacing="1">
                                <tr height="2px">
                                    <td height="2px">
                                        <asp:Image ID="Image1" ImageUrl="~/CommonImages/linebar.jpg" Width="100%" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" Height="20px" Width="20px" ToolTip="Employee Module"
                                            runat="server" Text="Employee Module" ImageUrl="~/CommonImages/addmodule.jpg" />
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Employee Module" Height="15px"
                                            Font-Size="9pt" CssClass="LeftMenu_Main_Image" Font-Bold="false" ToolTip="Employee Module"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlChildDetail" runat="server">
                                <table>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton3" Height="12px" Width="12px" ToolTip="Leave Application"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/LeaveApplication.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton3" runat="server" Text="  Leave Application" PostBackUrl="~/Module/HRMS/Pages/LeaveApplication.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Leave Application"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton4" Height="12px" Width="12px" ToolTip="Leave Request"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/Employee_Request_for_Leave.aspx"
                                                ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton4" runat="server" Text="  Leave Request" PostBackUrl="~/Module/HRMS/Pages/Employee_Request_for_Leave.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Leave Request"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton5" Height="12px" Width="12px" ToolTip="Leave Detail"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/EmpLeaveDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton5" runat="server" Text="  Leave Detail" PostBackUrl="~/Module/HRMS/Pages/EmpLeaveDetail.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Leave Detail"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton6" Height="12px" Width="12px" ToolTip="Out-Door Duty"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/OutDoorDutyForm.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton6" runat="server" Text="  Out-Door Duty" PostBackUrl="~/Module/HRMS/Pages/OutDoorDutyForm.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Out-Door Duty"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton7" Height="12px" Width="12px" ToolTip="Out-Door Duty Request"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/ODApplovalForm.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton7" runat="server" Text="  Out-Door Duty Request" PostBackUrl="~/Module/HRMS/Pages/ODApplovalForm.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Out-Door Duty Request"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton8" Height="12px" Width="12px" ToolTip="Out-Door Detail"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/Out_DoorDutyDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton8" runat="server" Text="  Out-Door Detail" PostBackUrl="~/Module/HRMS/Pages/Out_DoorDutyDetail.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Out-Door Detail"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton9" Height="12px" Width="12px" ToolTip="Optional Holiday"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/EmpOptionalLeaveDetail.aspx"
                                                ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton9" runat="server" Text="  Optional Holiday" PostBackUrl="~/Module/HRMS/Pages/EmpOptionalLeaveDetail.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Optional Holiday"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton10" Height="12px" Width="12px" ToolTip="Investment Declaration"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/InvesmentDeclaration1.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton10" runat="server" Text="  Investment Declaration"
                                                PostBackUrl="~/Module/HRMS/Pages/InvesmentDeclaration1.aspx" Height="20px" Font-Names="ARIAL"
                                                Font-Size="8pt" CssClass="LeftMenu_Child_Image" Font-Bold="false" ToolTip="Investment Declaration"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton11" Height="12px" Width="12px" ToolTip="Loan Application"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/EmpApplication_For_Loan.aspx"
                                                ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton11" runat="server" Text="  Loan Application" PostBackUrl="~/Module/HRMS/Pages/EmpApplication_For_Loan.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Loan Application"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton12" Height="12px" Width="12px" ToolTip="Loan Request"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/EmpLoanRequest.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton12" runat="server" Text="  Loan Request" PostBackUrl="~/Module/HRMS/Pages/EmpLoanRequest.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Loan Request"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton13" Height="12px" Width="12px" ToolTip="Loan Detail"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/LoanDetail.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton13" runat="server" Text="  Loan Detail" PostBackUrl="~/Module/HRMS/Pages/LoanDetail.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Loan Detail"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton14" Height="12px" Width="12px" ToolTip="Appraisal Form"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/AppraisalForm.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton14" runat="server" Text="  Appraisal Form" PostBackUrl="~/Module/HRMS/Pages/AppraisalForm.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Appraisal Form"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton15" Height="12px" Width="12px" ToolTip="Leave Application"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/SalarySlipView.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton15" runat="server" Text="  Salary Slip" PostBackUrl="~/Module/HRMS/Pages/SalarySlipView.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Salary Slip"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton16" Height="12px" Width="12px" ToolTip="Hiring Request"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/HiringReq.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton16" runat="server" Text="Hiring Request" PostBackUrl="~/Module/HRMS/Pages/HiringReq.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Salary Slip"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton17" Height="12px" Width="12px" ToolTip="Hiring Request"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/HiringReqApp.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton17" runat="server" Text="Hiring Approval" PostBackUrl="~/Module/HRMS/Pages/HiringReqApp.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Salary Slip"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton2" Height="12px" Width="12px" ToolTip="Change Password"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/ChangePassword.aspx" ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton2" runat="server" Text="  Change Password" PostBackUrl="~/Module/HRMS/Pages/ChangePassword.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Change Password"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="22px">
                                            <asp:ImageButton ID="ImageButton18" Height="12px" Width="12px" ToolTip="Advance Request"
                                                runat="server" PostBackUrl="~/Module/HRMS/Pages/Employee_Advance_Request.aspx"
                                                ImageUrl="~/CommonImages/b1.jpg" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton18" runat="server" Text="Advance Request" PostBackUrl="~/Module/HRMS/Pages/Employee_Advance_Request.aspx"
                                                Height="20px" Font-Names="ARIAL" Font-Size="8pt" CssClass="LeftMenu_Child_Image"
                                                Font-Bold="false" ToolTip="Advance Request"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <cc1:CollapsiblePanelExtender ID="cpe2" runat="server" CollapseControlID="tblMainLinkc"
                    Collapsed="true" ExpandControlID="tblMainLinkc" ExpandDirection="Vertical" ScrollContents="false"
                    SuppressPostBack="True" TargetControlID="pnlChildDetail">
                </cc1:CollapsiblePanelExtender>
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
