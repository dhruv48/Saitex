<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialIssue.ascx.cs"
    Inherits="Module_Inventory_Controls_MaterialIssue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
    }
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    .d1
    {
        width: 150px;
    }
    .d2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d3
    {
        width: 100px;
    }
</style>

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
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                    
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
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" colspan="8">
                    <span class="titleheading"><strong>Material Transaction Query Form</strong></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="8"
                        Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="tdRight">
                    Year:
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                        Font-Size="8" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="160px">
                    </asp:DropDownList>
                </td>
                <td class="tdRight">
                    From Date:
                </td>
                <td class="tdLeft">
                    <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                        OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditTxtFromDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtFromDate">
                        </cc1:MaskedEditExtender>
                </td>
                <td class="tdRight">
                    To Date:
                </td>
                <td class="tdLeft">
                    <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                        runat="server" OnTextChanged="TxtToDate_TextChanged1" AutoPostBack="True"></asp:TextBox>
                        
                         <cc1:MaskedEditExtender ID="MaskedEditTxtToDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtToDate">
                        </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Trn Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnType" runat="server" DataTextField="TRN_TYPE" DataValueField="TRN_TYPE"
                        Width="160px" CssClass="gCtrTxt " Font-Size="8">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Item Category:
                </td>
                <td>
                    <asp:DropDownList ID="ddlItemCate" runat="server" CssClass="gCtrTxt " Font-Size="8"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Item Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlItemType" runat="server" CssClass="gCtrTxt " Font-Size="8"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Party :
                </td>
                <td>
                    <asp:DropDownList ID="ddlPartycode" runat="server" DataTextField="PRTY_CODE" DataValueField="PRTY_CODE"
                        Width="160px" CssClass="gCtrTxt" Font-Size="8">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Department:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Location:
                </td>
                 <td>
                    <asp:DropDownList ID="ddllocation" runat="server" CssClass="gCtrTxt " Font-Size="8"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Store:
                </td>
                 <td>
                    <asp:DropDownList ID="ddlstore" runat="server" CssClass="gCtrTxt " Font-Size="8" DataTextField="MST_CODE"
                      DataValueField="MST_NAME"  Width="160px">
                    </asp:DropDownList>
                </td>
                <td class="tdRight">
                    Item:
                </td>
                <td class="tdLeft">
                    <cc2:ComboBox ID="txtICODE" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtICODE_LoadingItems"
                        EmptyText="------------All----------">
                        <HeaderTemplate>
                            <div class="header d1">
                                ITEM CODE</div>
                            <div class="header d2">
                                ITEM DESCRIPTION</div>
                            <div class="header d3">
                                TYPE</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item d1">
                                <%# Eval("ITEM_CODE")%></div>
                            <div class="item d2">
                                <%# Eval("ITEM_DESC") %></div>
                            <div class="item d3">
                                <%# Eval("ITEM_TYPE")%></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                </td>
                <td>
                </td>
               
            </tr>
            <tr>
                <td style="font-size:large;">
                    <b>Total Records : 
                    <asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                </td>
                <td align="right" colspan="3">
                    <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                        <ProgressTemplate>
                            <h3>
                                Loading...
                            </h3>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
                <td>
                    <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click" CssClass="AButton" />
                </td>
                <td colspan="6">
                </td>
                 
            </tr>
            <tr>
                <td class="td tContentArial" colspan="8">
                    <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="350px">
                        <asp:GridView ID="GridLedger" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                            AllowPaging="true" PageSize="14" CellPadding="3" ForeColor="#333333" GridLines="Both"
                            BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" OnPageIndexChanging="GridLedger_PageIndexChanging1" Width="150%">
                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="TRN_DATE" HeaderText="TRAN. DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField DataField="TRN_TYPE" HeaderText="TRAN. TYPE" />
                                <asp:BoundField DataField="TRN_NUMB" HeaderText="TRAN. NUMB" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" />
                                <asp:BoundField DataField="ITEM_DESC" HeaderText="ITEM DESC" />
                                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                <asp:BoundField DataField="TRN_QTY" HeaderText="TRAN QTY" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FINAL_AMOUNT" HeaderText="FINAL AMOUNT" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME"  />
                                <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMBER" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BILL_NUMB" HeaderText="BILL NUMBER" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="LORY_NUMB" HeaderText="LORRY NUMBER" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
                                <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST CODE" />
                                <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                                <asp:BoundField DataField="STORE" HeaderText="STORE" />
                            </Columns>
                           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                        PopupPosition="TopLeft" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                        PopupPosition="TopLeft" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
        </table>
 