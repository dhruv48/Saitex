<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Gate_Entry_detail.ascx.cs"
    Inherits="Module_GateEntry_Controls_Gate_Entry_detail" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial td " width="100%">
            <tr>
                <td>
                    <table class="tContentArial td ">
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
                               <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                              ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" class="TableHeader td">
                                <span class="titleheading"><strong>Gate Entry Detail </strong></span>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <table width="100%" class="tContentArial td ">
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    Branch:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:DropDownList ID="ddlbranch" runat="server" Width="140px" CssClass="SmallFont BoldFont UPPERCASE">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    Trn Type:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:DropDownList ID="ddltrntype" runat="server" Width="140px" CssClass="SmallFont BoldFont UPPERCASE">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    Party Code:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                        DataTextField="PRTY_CODE" DataValueField="Address" EmptyText="Select Party" EnableVirtualScrolling="true"
                                        Width="140px" MenuWidth="350px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code</div>
                                            <div class="header c2">
                                                Name</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                            <div class="item c2">
                                                <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    Gate From
                                </td>
                                <td align="left" valign="top" width="10%" class="style8">
                                    <asp:TextBox ID="txtgateno_from" runat="server" Width="140px" Height="13px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    Gate To:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtgateno_to" Height="13px" Width="140px" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    Doc No:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtdocno" Width="140px" Height="13px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    Date From:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtentrydate_from" Width="140px" Height="13px" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtentrydate_from"
                                        PopupPosition="TopLeft" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    Date To:
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtentrydate_to" Width="140px" Height="13px" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtentrydate_to" Format="dd/MM/yyyy"
                                        PopupPosition="TopLeft">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:Button ID="btngetrecord" runat="server" Text="Get Record" Width="85px" 
                                        OnClick="btngetrecord_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table width="100%" class="tContentArial td ">
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
                        <tr>
                            <td>
                                <asp:GridView ID="grdgateentrydetail" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" AllowSorting="True" CellPadding="3" BorderStyle="Ridge"
                                    CssClass="smallfont" EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333"
                                    PagerStyle-HorizontalAlign="Left" Width="100%" OnPageIndexChanging="grdgateentrydetail_PageIndexChanging">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                    <Columns>
                                        <asp:BoundField DataField="BRANCH_CODE" HeaderText="Branch Code" Visible="false" />
                                        <asp:BoundField DataField="TRN_TYPE" HeaderText="Trn type" Visible="false" />
                                        <asp:BoundField DataField="GATE_NUMB" HeaderText="Gate No" />
                                        <asp:BoundField DataField="GATE_DATE" HeaderText="Gate Date" />
                                        <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="LblVendor" ToolTip='<%#Eval("PRTY_CODE") %>' Text='<%#Eval("PRTY_NAME") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="4%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DOC_NO" HeaderText="Doc No" />
                                        <asp:BoundField DataField="DOC_DATE" HeaderText="Doc Date" />
                                        <asp:BoundField DataField="DOC_AMOUNT" HeaderText="Doc Amount" />
                                        <asp:BoundField DataField="LORRY_NO" HeaderText="Lorry No" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                        <asp:BoundField DataField="NO_OF_ITEMS" HeaderText="NO Of Unit" />
                                         <asp:BoundField DataField="MATERIAL_DTL" HeaderText="Item Details" />
                                          <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers><asp:PostBackTrigger ControlID="imgBtnExportExcel" /></Triggers>
</asp:UpdatePanel>