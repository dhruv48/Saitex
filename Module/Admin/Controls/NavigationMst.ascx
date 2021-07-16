<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavigationMst.ascx.cs" Inherits="Module_Admin_Controls_NavigationMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Src="AddChildModule.ascx" TagName="AddChildModule" TagPrefix="uc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
</style>

<script language="javascript" type="text/javascript">
    function func1() {
        document.getElementById("imgPhoto").style.display = "";
        document.getElementById("imgPhoto").src = document.getElementById("ctl00_cphBody_NavigationMaster1_tPhoto").value;
    }
</script>
<%--
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table style="text-align: left" class="tContentArial" cellspacing="0" cellpadding="0">
                <tr>
                    
                    
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click" 
                            OnClientClick="if (!confirm('Are you sure Want To Clear ?')) { return false; }" 
                            TabIndex="1">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" 
                            OnClientClick="if (!confirm('Are you want to print?')) { return false; }" 
                            TabIndex="2">
                        </asp:ImageButton>
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click" 
                            OnClientClick="if (!confirm('Are you sure to Exit?')) { return false; }" 
                            TabIndex="3">
                        </asp:ImageButton>
                    </td>
                    <td style="width: 48px">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="4"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="tRowColorAdmin"> Navigation Master Query</b>
        </td>
    </tr>
    <tr>
        <td class="td" valign="top" align="left" style="height: 16px">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server">
                </asp:Label></span>
        </td>
    </tr>
    <tr>
        <td valign="top" align="center">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
            </strong>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
            </strong>
        </td>
    </tr>
    
    <tr>
        <td align="left" class="td" width="100%">
            <table>
                <tr style="font-weight: bold">
                    <td valign="top" align="right">
                        *Parent Module Name <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:DropDownList ID="ddlParenModuleName" TabIndex="5" runat="server" Width="255px"
                            AutoPostBack="true" AppendDataBoundItems="True" 
                            CssClass="SmallFont" 
                            onselectedindexchanged="ddlParenModuleName_SelectedIndexChanged" >
                        </asp:DropDownList>
                       
                    </td>
                
                
                    <td valign="top" align="right">
                        *Child Module Name <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:DropDownList ID="ddlChildModuleName" TabIndex="6" runat="server" Width="255px"
                            AutoPostBack="True" 
                            CssClass="SmallFont">
                        </asp:DropDownList>
                        <strong></strong>
                    </td>
                </tr>
                <tr>
                
                <td>
                <asp:Button ID="btnGetReport" runat="server" Text="SEARCH" 
                        OnClick="btnGetReport_Click" TabIndex="7" />
                </td>
                </tr>
                
                
                
               
                <tr>
                    <td valign="top" align="right" colspan="3">
                        <asp:GridView ID="gvNavigation" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true"
                            AutoGenerateColumns="false" 
                            OnPageIndexChanging="gvNavigation_PageIndexChanging1"
                            Width="100%" CellPadding="1" CellSpacing="2" TabIndex="7">
                            <RowStyle Font-Names="Times New Roman" Font-Size="Small" />
                            <Columns>
                                
                                <asp:BoundField DataField="MDL_NAME" HeaderText="Parent Module Name" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHILD_MDL_NAME" HeaderText="Child Module Name" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NAV_NAME" HeaderText="Navigation Name" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NAV_URL" HeaderText="Navigation URL" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TAB_NAME" HeaderText="Tab Name" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="DISP_ODR" HeaderText="Display Order" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                              
                            </Columns>
                            <PagerStyle HorizontalAlign="left" />
                            <HeaderStyle Font-Bold="True" Font-Names="Times New Roman" Font-Size="Medium" 
                                BackColor="#3366FF" ForeColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
                
            </table>
        </td>
    </tr>
</table>
 </ContentTemplate>
</asp:UpdatePanel>