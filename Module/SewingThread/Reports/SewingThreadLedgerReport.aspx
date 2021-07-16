<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SewingThreadLedgerReport.aspx.cs" Inherits="Module_SewingThread_Reports_SewingThreadLedgerReport" Title="Untitled Page" %>
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
        width: 500px;
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

<%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtFromDate"   Mask="99/99/9999" MessageValidatorTip="true"    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"  MaskType="Date"   InputDirection="LeftToRight"
    ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtToDate"   Mask="99/99/9999" MessageValidatorTip="true"    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"  MaskType="Date"   InputDirection="LeftToRight" 
     ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
--%>
                                      
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
            <span class="titleheading"><strong>Sewing Thread Ledger Report</strong></span>
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
           Sewing Thread:</td>
        <td class="tdLeft">
            <cc2:ComboBox ID="ddlYarn" runat="server" CssClass="SmallFont" 
                EmptyText="------------All----------" EnableLoadOnDemand="True" Height="200px" 
                MenuWidth="800px" OnLoadingItems="ddlYarn_LoadingItems" Width="161px">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Sw Code</div>
                             
                                <div class="header c4">
                                    Description</div>
                            </HeaderTemplate>
            
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                </div>
                          
                                <div class="item c4">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                                </div>
                                
                               
                            </ItemTemplate>
            
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
            
                        </cc2:ComboBox>
        </td>
        <td class="tdRight">
            SW Category:</td>
        <td class="tdLeft">
         <asp:DropDownList ID="ddlYarnCate" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px" DataTextField="YARN_CAT" 
                DataValueField="YARN_CAT">
            </asp:DropDownList>
         
        </td>
        <td class="tdRight">
          SW Type:</td>
        <td class="tdLeft">
          <asp:DropDownList ID="ddlYarnType" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">
            </asp:DropDownList>
           
        </td>
        <td align="center" colspan="2">
            <asp:Button ID="btnGetReport" runat="server" 
                Text="Get Report" onclick="btnGetReport_Click1" Visible="False" />
        </td>
    </tr>
    <tr>
        <td class="TdBackVir" colspan="8">
      <%-- <b>Total Records : &nbsp;&nbsp;</b>--%><asp:Label ID="lblTotalRecord" 
                runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td tContentArial" colspan="8">
            <asp:Panel ID="pnlShowHover" runat="server" Height="350px" ScrollBars="Auto" 
                Width="945px">
                <asp:GridView ID="GridLedger" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                    Font-Size="X-Small" ForeColor="#333333" GridLines="None" 
                    HeaderStyle-Wrap="true" onpageindexchanging="GridLedger_PageIndexChanging" 
                    PageSize="14" Width="250%" Visible="False">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH NAME" />
                        <asp:BoundField DataField="YARN_CODE" HeaderText="SW CODE" />
                        <asp:BoundField DataField="YARN_DESC" HeaderText="SW DESCRIPTION" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="USER_NAME" HeaderText="USER NAME" />
                        <asp:BoundField DataField="TRN_TYPE" HeaderText="TRAN TYPE" />
                        <asp:BoundField DataField="TRN_DATE" DataFormatString="{0:dd-MM-yyyy}" 
                            HeaderText="TRAN DATE" />
                        <asp:BoundField DataField="TRN_QTY" HeaderStyle-HorizontalAlign="Right" 
                            HeaderText="TRAN QTY" ItemStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FINAL_RATE" DataFormatString="{0:0.00}" 
                            HeaderStyle-HorizontalAlign="Right" HeaderText="FINAL RATE" 
                            ItemStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
                        <asp:BoundField DataField="PRTY_CH_NUMB" HeaderStyle-HorizontalAlign="Right" 
                            HeaderText="PARTY CHALAN NUMBER" ItemStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME" />
                        <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE QTY" />
                        <asp:BoundField DataField="RECEIVE_QTY" HeaderStyle-HorizontalAlign="Right" 
                            HeaderText="RECEIVE QTY" ItemStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                        <asp:BoundField DataField="COMP_ADD" HeaderText="COMPANY ADDRESS" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </asp:Panel>
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
</asp:Content>


