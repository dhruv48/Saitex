<%@ Control Language="C#" AutoEventWireup="true" CodeFile="User_Navigation.ascx.cs"
    Inherits="Module_Admin_Controls_User_Navigation" %>
    <%--<%@ Register Src="~/CommonControls/LOV/UserLOV.ascx" TagName="UserLOV" TagPrefix="uc1" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 800px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
    .c6
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<table align="left" class=" td tContentArial" width="945px">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click" />
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
            <table width="945px">
                <tr>
                    <td align="center" class="TableHeader td" colspan="13">
                        <span class="titleheading"><strong>User Navigation Query</strong></span>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1113" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="right">
                                User Code:
                            </td>
                            <td>
                            <%--<uc1:UserLOV ID="UserLOV1" runat="server" />--%>
                                <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="True" width = "250px"
                                   onselectedindexchanged="ddlUser_SelectedIndexChanged">
                                     
                            </asp:DropDownList>
                            </td>
                           
                            <td align="center">
                                <asp:Button ID="btnGetReport" runat="server" Text="Get Data" OnClick="btnGetReport_Click1" />
                            </td>
                        </tr>
                      
                        <tr>
                            <td class="TdBackVir" width="945px" colspan="3">
                                <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                            <b>
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server">
                                        <ProgressTemplate>
                                            Loading...
                                                  
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                                </table>
                                         <table width="945px"> 
                                         <tr>
                                         <td> 
                                         <asp:Panel ID="pnl121" runat="server" ScrollBars="Both" Width="945px" 
                                    Height="350px" BorderStyle="None">
                                            <asp:GridView ID="grd_UserNavigation" runat="server" 
                                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                                                BorderStyle="Ridge" CellPadding="3" CssClass="smallfont" Font-Size="X-Small" 
                                                ForeColor="#333333" OnPageIndexChanging="grd_UserNavigation_PageIndexChanging" 
                                                PagerStyle-HorizontalAlign="Left" PageSize="10" Width="135%" >
                                                 
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                                    Width="100%" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                                <Columns>
                                                    <asp:BoundField DataField="USER_CODE" HeaderText="User Code" />
                                                    <asp:BoundField DataField="USER_NAME" HeaderText="User Name" />
                                                    <%--<asp:BoundField DataField="USER_TYPE" HeaderText="User Type" />--%>
                                                    <%--<asp:BoundField DataField="USER_ID" HeaderText="User ID" />--%>
                                                    <%--<asp:BoundField DataField="USER_LOG_ID" HeaderText="User LogID" />--%>
                                                    <%--<asp:BoundField DataField="MDL_ID" HeaderText="MDL ID" />--%>
                                                    <asp:BoundField DataField="MDL_NAME" HeaderText="MDL Name" />
                                                    <asp:BoundField DataField="MODULE_DISP_ODR" HeaderText="Module Disp Odr" />
                                                    <%--<asp:BoundField DataField="POSTED_LENGTHMODULE" 
                                                        HeaderText="Posted LengthModule" />--%>
                                                    <%--<asp:BoundField DataField="POSTED_LENGTHCHILDMODULE" 
                                                        HeaderText="Posted_LENGTHCHILDMODULE" />--%>
                                                   <%-- <asp:BoundField DataField="CHILD_MDL_ID" HeaderText="CHILD_MDL_ID" />--%>
                                                    <%--<asp:BoundField DataField="CHILD_MDL_DISP_ODR" 
                                                        HeaderText="CHILD_MDL_DISP_ODR" />--%>
                                                    <asp:BoundField DataField="NAV_ID" HeaderText="QuickID" />
                                                    <asp:BoundField DataField="NAV_NAME" HeaderText="Nav Name" />
                                                    <asp:BoundField DataField="DISP_ODR" HeaderText="Disp Odr" />
                                                    <asp:BoundField DataField="NAV_URL" HeaderText="Nav url" />
                                                    <asp:BoundField DataField="ACTIVEDES" HeaderText="Status" />
                                                    <%--<asp:BoundField DataField="POSTED_LENGTH" HeaderText="POSTED_LENGTH" />--%>
                                                    <asp:BoundField DataField="CREATE_RHT" HeaderText="Create rht" />
                                                    <asp:BoundField DataField="MDFY_RHT" HeaderText="Mdfy rht" />
                                                    <asp:BoundField DataField="DEL_RHT" HeaderText="Del rht" />
                                                    <asp:BoundField DataField="VIEW_RHT" HeaderText="View rht" />
                                                    <asp:BoundField DataField="TAB_ID" HeaderText="TabID" />
                                                    <asp:BoundField />
                                                </Columns>
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                      </asp:GridView>
                                      </asp:Panel>
                                      </td>
                                      </tr>
                                      </table>
                                      
                            </td>
                           <%--<td align="right" valign="top" cssclass="Label">--%>
                                
                        
                    
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
