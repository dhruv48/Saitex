<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Sw_Detail_Report.ascx.cs" Inherits="Module_SewingThread_Controls_Sw_Detail_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td" colspan="8">
                        <span class="titleheading"><strong> Sewing Thread Detail Stock Report</strong> </span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                            <td align="right">
                                Year:
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">--%>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                                    Width="160px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                From Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    OnTextChanged="TxtFromDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td align="right">
                                To Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    OnTextChanged="TxtToDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
                            </td>
                 
                  
                    
                   
                </tr>
                <tr>
                  <td class="tdRight">
                        SW Type :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlyarntype" runat="server" CssClass="gCtrTxt"
                            Width="160px" Height="16px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Party :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlpartycode" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Shade Code :
                    </td>
                    <td align="left" valign="top" class="style8">
                        <asp:DropDownList ID="ddlShadeCode" runat="server" CssClass="gCtrTxt" Width="160px">
                        </asp:DropDownList>
                    </td>
                <td style="text-align: right"  >
             Article Code :</td>
           <td >
               <asp:TextBox ID="TxtYarnCode" runat="server" Width="158px"></asp:TextBox>
           </td>
       
                </tr>
                <tr>
                    <td class="tdRight">
                        SW Catagory :
                     </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlyarncat" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                <td colspan = "6">  
                <asp:Panel ID="Panel1" runat="server" BackColor="#99CCFF">
                <asp:RadioButtonList ID="redForQuery" runat="server" Height="16px" 
                        RepeatDirection="Horizontal">                       
                         <asp:ListItem Text="Red" Value="red" Selected ="True"> SW Detail Lotwise Stock Report</asp:ListItem>
                            <asp:ListItem Text="Blue" Value="blue">SW Detail Challanwise Report</asp:ListItem>             
                  </asp:RadioButtonList>       
                    </asp:Panel>
                    </td>
                </tr>
                </table>
                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtFromDate">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtToDate">
                    </cc1:CalendarExtender>
        </td>
    </tr>
</table>
