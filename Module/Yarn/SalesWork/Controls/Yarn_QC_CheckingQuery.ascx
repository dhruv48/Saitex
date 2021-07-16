<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_QC_CheckingQuery.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_QC_CheckingQuery" %>
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
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
    .d1
    {
        width: 150px;
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
    .Smallfont
    {
        font-size: 8pt;
    }
</style>
<table width="100%" class="td tContentArial">
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
                        <span class="titleheading">Yarn QC Checking Query</span>
                    </td>
                </tr>
            </table>
            <table class="tContentArial">
                <tr>
                    <td align="right" width="10%">
                        MRN No:
                    </td>
                    <td>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="combined"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                            Width="180px" Height="200px" MenuWidth="200px" TabIndex="1" EmptyText="Select MRN">
                            <HeaderTemplate>
                                <div class="header c1">
                                    MRN #</div>
                                <div class="header c1">
                                    Year</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container5" Text='<%# Eval("YEAR") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:HiddenField ID="hdTRN_TYPE" runat="server" />
                        <asp:HiddenField ID="hdTRN_NUMB" runat="server" />
                        <asp:HiddenField ID="hdYEAR" runat="server" />
                    </td>
                    <td align="right" width="10%">
                        Yarn :
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="YARN_CODE" DataValueField="YARN_DESC" EmptyText="Find Yarn Code"
                            EnableLoadOnDemand="true" EnableVirtualScrolling="true" Height="200px" MenuWidth="500"
                            OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged"
                            TabIndex="2" Width="180px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    YARN CODE</div>
                                <div class="header c2">
                                    YARN Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("YARN_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_DESC") %>' /></div>
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
                        <asp:DropDownList runat="server" ID="ddlStdType" Width="160px" CssClass="SmallFont"
                            TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlStdType_SelectedIndexChanged" />
                    </td>
                    <td align="right" width="10%">
                        Status:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="SmallFont" Width="160px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                            <asp:ListItem Value="0">Pending</asp:ListItem>
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Value="3">REJECTED</asp:ListItem>
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
                    <td align="left">
                        <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto">
                            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                                AllowSorting="True" AutoGenerateColumns="False" Width="99%" OnPageIndexChanging="gvMaterialReceiptApproval_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.&nbsp;No." HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRN&nbsp;Year" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("TRN_YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRN&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTRN_NUMB" runat="server" Text='<%#Eval("TRN_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TRN_DATE" HeaderText="MRN&nbsp;Date" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Party" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTY_DATA") %>'></asp:Label>
                                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%#Eval("TRN_TYPE") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yarn&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYARN_CODE" runat="server" Text='<%#Eval("YARN_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn&nbsp;Description" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="Y_COUNT" HeaderText="Yarn&nbsp;Count" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Std&nbsp;Type" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTD_TYPE" runat="server" Text='<%#Eval("STD_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QC_VALUE" HeaderText="QC&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:BoundField DataField="MAX_VALUE" HeaderText="Max&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:BoundField DataField="MIN_VALUE" HeaderText="Min&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Result" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQC_Result" runat="server" Text='<%#Eval("Q_Result") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved&nbsp;Result">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQC_CHANGE_RESULT" runat="server" Text='<%#Eval("Q_CHANGE_RESULT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTATUS" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" Width="100%" />
                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
