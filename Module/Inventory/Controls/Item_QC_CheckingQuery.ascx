<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_CheckingQuery.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_CheckingQuery" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
    .d1
    {
        width: 80px;
    }
    .d2
    {
        margin-left: 4px;
        width: 350px;
    }
    .d3
    {
        width: 80px;
    }
</style>
<table width="990px" class="td tContentArial">
    <tr>
        <td>
            <table class="tContentArial">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" valign="top" class="tRowColorAdmin td">
                        <span class="titleheading">Item QC Checking Query</span>
                    </td>
                </tr>
            </table>
            <table class="tContentArial">
                <tr>
                    <td align="right" width="10%">
                        Item:
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="ddlItemCode" runat="server" CssClass="SmallFont" AutoPostBack="true"
                            TabIndex="2" Width="100px" EnableLoadOnDemand="True" DataTextField="ITEM_DESC"
                            DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true" OpenOnFocus="true"
                            Visible="true" Height="200px" EmptyTextSelect="Select Item" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                            OnLoadingItems="ddlItemCode_LoadingItems">
                            <HeaderTemplate>
                                <div class="header d1">
                                    CODE</div>
                                <div class="header d2">
                                    DESCRIPTION</div>
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
                    <td align="right" width="10%">
                        Std Type:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList runat="server" ID="ddlStdType" Width="100px" CssClass="tContentArial"
                            TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlStdType_SelectedIndexChanged" />
                    </td>
                    <td align="right" width="10%">
                        Status:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="tContentArial" Width="100px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                            <asp:ListItem Value="0">Pending</asp:ListItem>
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        QC Result:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList runat="server" ID="ddlQCResult" Width="100px" CssClass="tContentArial"
                            TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlQCResult_SelectedIndexChanged">
                            <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Fail" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Approved Result:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList runat="server" ID="ddlMQCResult" Width="100px" CssClass="tContentArial"
                            TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlMQCResult_SelectedIndexChanged">
                            <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Fail" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="left" valign="top" width="50%" class="Label">
                        <b>Total Records :</b>
                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                    </td>
                    <td align="left" width="50%" class="Label" valign="top">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <h3>
                                    Loading...</h3>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="pnlShowHover" runat="server" Width="990px" ScrollBars="Auto">
                                        <asp:GridView ID="Grid12" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                                            AllowPaging="true" PageSize="20"  ForeColor="#333333" GridLines="Both"
                                            BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" OnPageIndexChanging="Grid12_PageIndexChanging"
                                            Width="99%" Style="margin-right: 627px">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQC_NUMB" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_NUMB")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC&nbsp;YEAR" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQC_Year" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="QC_DATE" HeaderText="QC&nbsp;Date" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TRN_YEAR" HeaderText="MRN&nbsp;Year" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="TRN_NUMB" HeaderText="MRN&nbsp;No. " ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="TRN_DATE" HeaderText="MRN&nbsp;Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="PARTY_DATA" HeaderText="Party" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item&nbsp;Code" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="MAX_VALUE" HeaderText="Max&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="MIN_VALUE" HeaderText="Min&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="QC_VALUE" HeaderText="QC&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="STD_TYPE" HeaderText="Std&nbsp;Type" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="QC_Result" HeaderText="Result" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="QC_Approved_Result" HeaderText="Approved&nbsp;Result"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="QC_REMARKS" HeaderText="Remarks" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                            </Columns>
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
