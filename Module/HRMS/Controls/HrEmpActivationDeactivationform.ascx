<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HrEmpActivationDeactivationform.ascx.cs"
    Inherits="Module_HRMS_Controls_HrEmpActivationDeactivationform" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Src="../../../CommonControls/LOV/DepartmentLOV.ascx" TagName="DepartmentLOV"
    TagPrefix="uc1" %>
   
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
       
         <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtformdate"
            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
            ErrorTooltipEnabled="True">
        </cc4:MaskedEditExtender>
         <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtTodate"
            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
            ErrorTooltipEnabled="True">
        </cc4:MaskedEditExtender>
<table class="td tContent" width="950px">
    <tr>
        <td align="left " class="style1" colspan="1">
            <table>
                <tr>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Height="41" Width="48" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan="1">
            <span class="titleheading">Employee Active / Deactive Form </span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="style1" colspan="1">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <table class="td tContent" width="950px">
                <tr>
                <td class="TdLeft">
                        Branch :
                    </td>
                    <td class="TdLeft">
                        <asp:DropDownList ID="ddlbranchcode" runat="server" DataTextField="BRANCH_CODE" DataValueField="BRANCH_NAME"
                            Width="150px"  CssClass="SmallFont TextBox" >
                        </asp:DropDownList>
                    </td>                    
                   
                    <td class="TdRight">
                        Department :
                    </td>
                    <td class="TdLeft">
                        <uc1:DepartmentLOV ID="ddlDepartment" CssClass="SmallFont TextBox" runat="server" width="128px" />
                    </td>
                    <td class="TdRight">
                        Designation:
                    </td>
                    <td class="TdLeft">
                        <asp:DropDownList ID="ddldesig" runat="server" DataTextField="DESIG_NAME" DataValueField="DESIG_CODE"
                            Width="150px"  CssClass="SmallFont TextBox" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TdRight">
                        Joining From :
                    </td>
                    <td class="TdLeft">
                        <asp:TextBox ID="txtformdate" CssClass="SmallFont TextBox" runat="server"  Width="148px" ></asp:TextBox>
                    </td>                   
                   <td class="TdRight">
                       Joining To:
                    </td>
                    <td class="TdLeft" >
                        <asp:TextBox ID="txtTodate" CssClass="SmallFont TextBox" runat="server"  Width="148px" ></asp:TextBox>
                    </td>  
                    <td class="TdRight">
                        Status:
                    </td>
                    <td class="TdLeft">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="SmallFont TextBox" Width="150px" >
                            <asp:ListItem>---Select-----</asp:ListItem>
                            <asp:ListItem Value="0">Active</asp:ListItem>
                            <asp:ListItem Value="1">Deactive</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>                   
                    <td class="TdRight">
                        Employee :
                    </td>
                    <td  class="TdLeft" >
                        <asp:DropDownList ID="ddlemployee" runat="server" DataTextField="EMP_NAME" DataValueField="EMPLOYEENAME"
                            Width="150px" AutoPostBack="True" CssClass="SmallFont TextBox" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td> 
                    <td></td>
                    <td>
                        <asp:Button ID="CmdView" runat="server" CssClass="SmallFont TextBox" 
                            Text="View Record" onclick="CmdView_Click" /></td>                 
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td  >Employee Record :- </td>
    </tr>
    <tr>
        <td>
            <table  width="100%">
                <tr style=" background:#507CD1;font-size:x-small;">
                    <td style="width:6%">EMP CODE</td>
                    <td style="width:16%">EMPLOYEE NAME</td>
                    <td style="width:15%">FATHER NAME</td>
                    <td style="width:12%">COMPANY</td>
                    <td style="width:12%">JOIN DT</td>
                    <td style="width:12%">DESIGNATION</td>
                    <td style="width:12%">DEPARTMENT</td>
                    <td >STATUS</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td >
            <asp:Panel ID="pnlShowHover" runat="server" BackColor="Beige" BorderWidth="2px" Height="240px"
                ScrollBars="Vertical" Width="950px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="tContentArial"
                    Width="100%" CellPadding="4" ForeColor="#333333" ShowHeader="false"  GridLines="None" HeaderStyle-Wrap="true"
                    Font-Size="X-Small" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="950px" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="EMP_CODE" HeaderText="EMP CODE " />
                        <asp:BoundField DataField="EMPLOYEENAME" HeaderText="EMPLOYEENAME" />
                        <asp:BoundField DataField="FATHERNAME" HeaderText="FATHER NAME" />
                        <asp:BoundField DataField="COMPANY" HeaderText="COMPANY" />
                        <asp:BoundField DataField="JOIN_DT" HeaderText="JOIN DT" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" />
                        <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkStatus" runat="server" Text='<%# Bind("EMP_STATUS") %>' CssClass="SmallFont"
                                    Width="100px" Height="16px" CommandName="RecordDelete" CommandArgument='<%# Eval("EMP_CODE") %>'></asp:LinkButton>
                                <cc4:ConfirmButtonExtender ID="ConfirmBtExt1" runat="server" TargetControlID="LinkStatus"
                                    ConfirmText="Sir ! Are You want to change Status ?">
                                </cc4:ConfirmButtonExtender>                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="LblCode" runat="server" Value='<%# Eval("DEL_STATUS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                   
                    <HeaderStyle BackColor="#507CD1"  Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>    
</table>
</ContentTemplate>
</asp:UpdatePanel>