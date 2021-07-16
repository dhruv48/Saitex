<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeaveStatusDetails.ascx.cs" Inherits="Module_HRMS_Controls_Leave_Status_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="right" class=" tContentArial" width="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" class="TableHeader td">
                                <span class="titleheading"><strong>Leave Status Details</strong> </span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border-style: ridge; border-color: White;">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Emp Name
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlEmpName" runat="server" Width="140px" 
                                    CssClass="SmallFont BoldFont UPPERCASE" >
                                    
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Leave Type
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlLeaveType" runat="server" Width="140px" 
                                    CssClass="SmallFont BoldFont UPPERCASE" >
                                    
                                    <asp:ListItem Value="1">CASUAL LEAVE</asp:ListItem>
                                    <asp:ListItem Value="2">EL</asp:ListItem>
                                    <asp:ListItem Value="3">SICK LEAVE</asp:ListItem>
                                    <asp:ListItem Value="4">ML</asp:ListItem>
                                    <asp:ListItem Value="7">LEAVE WITHOUT PAY</asp:ListItem>
                                    <asp:ListItem Value="5">COMPENSATORY OFF</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>
                             <td align="right" valign="top" width="15%">
                                Leave Status
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlLeaveStatus" runat="server" Width="140px" 
                                    CssClass="SmallFont BoldFont UPPERCASE" >
                                    
                                    <asp:ListItem Value="A">APPROVED</asp:ListItem>
                                    <asp:ListItem Value="R">REJECT</asp:ListItem>
                                    <asp:ListItem Value="P">PENDING</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>
                            <tr>
                             <td align="right" valign="top" width="15%">
                                From Date
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="DTCRFromDate" runat="server" OnTextChanged="DTCRFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                             <td align="right" valign="top" width="15%">
                               To Date
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="DTCRToDate" runat="server" OnTextChanged="DTCRToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                            
                            </tr>
                            <tr>
                                <td align="left" valign="top" width="15%">
                                    <%--<asp:DropDownList ID="ddlUserName" runat="server" Width="140px" 
                                    CssClass="SmallFont BoldFont"> 
                                     
                                   
                                </asp:DropDownList>--%>
                                </td>
                                <td align="right" valign="top" width="10%">
                                    <asp:Button ID="btngetrecord" runat="server" Height="22px" 
                                        OnClick="btngetrecord_Click1" Text="Get Record" Width="85px" />
                                </td>
                            </tr>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="left" width="50%">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                            <td align="left" valign="top" width="50%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <asp:GridView ID="grLeave_Status_Details" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Ridge" 
                            CellPadding="3" CssClass="smallfont" EmptyDataText="No Record Found" 
                            Font-Size="X-Small" ForeColor="#333333" 
                            OnPageIndexChanging="grLeave_Status_Details_PageIndexChanging" 
                            PagerStyle-HorizontalAlign="Left" PageSize="20" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="EMP_CODE" HeaderText="Emp Name" />
                                <asp:BoundField DataField="LV_TYPE" HeaderText="Leave Type" />
                                <asp:BoundField DataField="LV_STATUS" HeaderText="Leave Status" />
                                <asp:BoundField DataField="LV_TO_DATE" HeaderText="To Date" />
                                <asp:BoundField DataField="LV_FROM_DATE" HeaderText="From Date" />
                                <asp:BoundField DataField="LV_PURPOSE" HeaderText="purpose" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </table>
                     
                </td>
            </tr>
        </table>
       <table>
                        <tr>
                            <td>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                                    TargetControlID="DTCRFromDate">
                                </cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                                    TargetControlID="DTCRToDate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                     </ContentTemplate>
           
</asp:UpdatePanel>
