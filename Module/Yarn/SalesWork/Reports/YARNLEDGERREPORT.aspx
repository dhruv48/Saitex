<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YARNLEDGERREPORT.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_YARN_LEDGER_REPORT" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
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
    .c4
    {
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 100px;
    }
</style>

<%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtFromDate"   Mask="99/99/9999" MessageValidatorTip="true"    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"  MaskType="Date"   InputDirection="LeftToRight"
    ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtToDate"   Mask="99/99/9999" MessageValidatorTip="true"    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"  MaskType="Date"   InputDirection="LeftToRight" 
     ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
--%>
<asp:UpdatePanel ID="uppnl" runat="server">
<ContentTemplate>

                                      
<table align="left" class=" td tContentArial" width="945px">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" 
                            ImageUrl="~/CommonImages/link_print.png" 
                            ToolTip="Print" onclick="imgbtnPrint_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" 
                            ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" onclick="imgbtnClear_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_exit.png" 
                            ToolTip="Exit" Width="48" onclick="imgbtnExit_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" colspan="8">
            <span class="titleheading"><strong>YARN LEDGER REPORT</strong></span>
        </td>
    </tr>
    <tr>
        <td align="right">
            Branch:
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px" 
                onselectedindexchanged="ddlBranch_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </td>
        <td align="right">
            Year:
        </td>
        <td>
           <%--<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">--%>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px" AutoPostBack="True" 
                onselectedindexchanged="ddlYear_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
           From Date:
        </td>
        <td>
              <asp:TextBox ID="TxtFromDate" runat="server" 
                CssClass="SmallFont TextBox UpperCase" ontextchanged="TxtFromDate_TextChanged" 
                Width="150px" AutoPostBack="True"></asp:TextBox>
        </td>
        <td align="right">
            To Date:
        </td>
        <td>
           <asp:TextBox ID="TxtToDate" runat="server" 
                CssClass="SmallFont TextBox UpperCase" ontextchanged="TxtToDate_TextChanged" 
                Width="150px" AutoPostBack="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
           Yarn:</td>
        <td class="tdLeft">
          
  <cc2:combobox ID="ddlYarn" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="YARN_CODE" DataValueField="YARN_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems" 
                                    EnableVirtualScrolling="true" OpenOnFocus="true" 
                TabIndex="9" Visible="true"
                                    Height="200px" EmptyText="---------All--------">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            YARN CODE</div>
                                        <div class="header c5">
                                            YARN DESCRIPTION</div>
                                        <div class="header c6">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("YARN_CODE") %></div>
                                        <div class="item c5">
                                            <%# Eval("YARN_DESC") %></div>
                                        <div class="item c6">
                                            <%# Eval("YARN_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:combobox>
         
        </td>
        <td class="tdRight">
            Yarn Category:</td>
        <td class="tdLeft">
         <asp:DropDownList ID="ddlYarnCate" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px" DataTextField="YARN_CAT" 
                DataValueField="YARN_CAT">
            </asp:DropDownList>
         
        </td>
        <td class="tdRight">
            Yarn Type:</td>
        <td class="tdLeft">
          <asp:DropDownList ID="ddlYarnType" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">
            </asp:DropDownList>
           
        </td>
       
    </tr>
 
  
    <tr>
        <td colspan="8">
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                PopupPosition="TopLeft" TargetControlID="TxtFromDate">
</cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                PopupPosition="TopLeft" TargetControlID="TxtToDate"></cc1:CalendarExtender>
        </td>
    </tr>
</table>


</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

