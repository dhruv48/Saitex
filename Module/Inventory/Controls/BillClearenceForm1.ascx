<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BillClearenceForm1.ascx.cs" Inherits="Module_Inventory_Controls_BillClearenceForm1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 50px;
    }
    .c2
    {
        margin-left: 5px;
        width: 280px;
    }
    .c3
    {
        margin-left: 5px;
        width: 90px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" 
                            Visible="False" />
                    </td>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <%--<td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                    </td>--%>
                    <td id="tdFind" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">&nbsp;Bill Clearence</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr id="trTotalRecord" runat="server">
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"
                CssClass="Label"></asp:Label></b>
        </td>
    </tr>
    <tr id="trgrid" runat="server">
        <td align="left" class="td">
            <asp:GridView ID="grdBillClearence" CssClass="SmallFont" runat="server" AllowSorting="True"
                AutoGenerateColumns="False" OnRowDataBound="grdBillClearence_RowDataBound" Width="100%">
                <Columns>
                 <asp:TemplateField HeaderText="Bill&nbsp;Type" >
                        <ItemTemplate>
                            <asp:Label ID="lblBillTypeName" runat="server" Text='<%# Bind("BILL_TYPE_NAME") %>' CssClass="Label smallfont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill&nbsp;Type"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBillType" runat="server" Text='<%# Bind("BILL_TYPE") %>' CssClass="Label smallfont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="BILL_NUMB" HeaderText="Bill Number">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                            />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Bill&nbsp;Date">
                        <ItemTemplate>
                            <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BILL_DATE","{0:dd/MM/yyyy}") %>' CssClass="Label smallfont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party&nbsp;Code">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyCode" runat="server" HtmlEncode="false" Text='<%# Bind("PRTY_CODE") %>'
                                CssClass="Label smallfont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PRTY_NAME" HeaderText="Party&nbsp;Name">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkConfirmed" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill&nbsp;Amount">
                        <ItemTemplate>
                           
                            <asp:Label ID="txtBillDate" runat="server" ReadOnly="true" Text='<%# Bind("BILL_AMNT") %>'
                                CssClass="  SmallFont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clr&nbsp;By">
                        <ItemTemplate>
                            <asp:Label ID="txtClrBy" runat="server" ReadOnly="true" Text='<%# Bind("BILL_CLR_USER") %>'
                                CssClass="  SmallFont" ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clr&nbsp;Date">
                        <ItemTemplate>
                            <asp:Label ID="txtClrDate" runat="server" ReadOnly="true" Text='<%# Bind("BILL_CLR_DATE","{0:dd/MM/yyyy}") %>'
                                CssClass="  SmallFont"  Width="95%"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pur&nbsp;Clr&nbsp;By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPurClrBy" runat="server" ReadOnly="true" Text='<%# Bind("CONF_BY") %>'
                                CssClass="TextBox TextBoxDisplay SmallFont" Width="95%"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pur&nbsp;Clr&nbsp;Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPurClrDate" runat="server" ReadOnly="true" Text='<%# Bind("CONF_DATE","{0:dd/MM/yyyy}") %>'
                                CssClass="TextBox TextBoxDisplay SmallFont"  Width="95%"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PurChase&nbsp;Comment">
                        <ItemTemplate>
                            <asp:TextBox ID="txtComment" runat="server"  
                                CssClass="TextBox  SmallFont"  Width="95%"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Show&nbsp;Details">
                        <ItemTemplate>
                            <asp:Panel ID="pnlView" runat="server">
                                <asp:LinkButton ID="lbtnView" runat="server" Text="View Detail" CssClass="Label"
                                    ></asp:LinkButton>
                                <asp:Label ID="lblCombinedid" runat="server" Text="Label" Visible ="false"  ToolTip='<%# Eval("BillID") %>'></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="pnlShowHover" runat="server"  BackColor="Beige" 
                                 ScrollBars="Auto">
                                <asp:GridView ID="grdTrndetails" runat="server" Width="450px" CssClass="SmallFont"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="YEAR" HeaderText="Year">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BILL_TYPE" HeaderText="Type">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BILL_NUMB" HeaderText="Numb">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TDATE" HeaderText="Trn Date">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUMB">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TRN_AMT" HeaderText="TRN AMT">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="pnlView"
                                PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
                            </cc1:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>