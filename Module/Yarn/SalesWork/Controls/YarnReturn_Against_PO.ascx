<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YarnReturn_Against_PO.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_YarnReturn_Against_PO" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        *display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 2px;
    }

    .c1 {
        width: 130px;
    }

    .c2 {
        margin-left: 4px;
        width: 500px;
    }

    .c3 {
        margin-left: 4px;
        width: 250px;
    }

    .c4 {
        margin-left: 4px;
        width: 150px;
    }

    .c5 {
        margin-left: 4px;
        width: 100px;
    }

    .style1 {
        height: 207px;
    }

    .style3 {
        border: .05em ridge #C1D3FB;
        height: 20px;
    }
</style>

<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        *display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 4px;
    }

    .c1 {
        width: 200px;
    }

    .c2 {
        margin-left: 4px;
        width: 300px;
    }

    .c3 {
        width: 200px;
    }

    .d1 {
        width: 180px;
    }

    .d2 {
        margin-left: 4px;
        width: 120px;
    }

    .d3 {
        margin-left: 4px;
        width: 180px;
    }

    .d4 {
        margin-left: 4px;
        width: 120px;
    }
</style>
<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 2px;
    }

    .c1 {
        width: 150px;
    }

    .c2 {
        margin-left: 4px;
        width: 150px;
    }

    .c3 {
        margin-left: 4px;
        width: 150px;
    }

    .c4 {
        width: 150px;
    }

    .c5 {
        margin-left: 4px;
        width: 340px;
    }

    .c6 {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table align="left" class="tContentArial td " width="100%">
    <tr>
        <td class="tContentArial td ">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="ImagePrint" runat="server" OnClick="ImagePrint_Click" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" OnClick="imgbtnClear_Click" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Enabled="false" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" Enabled="false" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImportExcel" runat="server" Height="41" OnClick="ImportExcel_Click" ImageUrl="~/CommonImages/export.png"
                            ToolTip="Import To Excel" Width="48" /></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td">
                        <span class="titleheading"><strong>Yarn Return Against PO</strong></span>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>MRN NUMBER</td>
                    <td>PO NUMBER</td>
                    <td>JOBER PARTY</td>
                    <td>YARN CODE</td>
                    <td>FROM DATE</td>
                    <td>TO DATE</td>
                    <td></td>

                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddltrnno" runat="server" Width="170px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPONumber" runat="server" Width="170px">
                        </asp:DropDownList></td>
                    <td>

                        <asp:DropDownList ID="ddlparty" runat="server" Width="170px">
                        </asp:DropDownList>

                    </td>
                    <td>
                        
                         <asp:DropDownList ID="ddlyarn" runat="server" Width="170px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="TXTDATEFROM" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TXTDATETO" runat="server"></asp:TextBox>

                    </td>

                    <td>
                        <asp:Button ID="BTNRECORD" runat="server" Text="GET RECORD" OnClick="BTNRECORD_Click" BackColor="#235d9b" ForeColor="White" />
                    </td>
                </tr>

            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblrecd" Font-Size="Large" runat="server"></asp:Label>
                        <asp:GridView ID="Get_WO_Detail" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%"  OnPageIndexChanging="Get_WO_Detail_PageIndexChanging" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="CHALLAN NUMB" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_CHALLAN_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("TRN_NUMB") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO NUMB" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PO_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("PO_NUMB") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YARN CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_YARN_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("YARN_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YARN" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_YARN_NAME" runat="server" CssClass=" SmallFont" Text='<%# Eval("YARN_DESC") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PARTY CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PRTY_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRTY_CODE") %>' ToolTip='<%# Eval("PRTY_NAME") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PARTY NAME" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PRTY_NAME" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRTY_NAME") %>' ToolTip='<%# Eval("PRTY_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SHADE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SHADE" runat="server" CssClass=" SmallFont" Text='<%# Eval("SHADE_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_UOM" runat="server" CssClass=" SmallFont" Text='<%# Eval("UOM") %>'  />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DATE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DATE" runat="server" CssClass=" SmallFont" Text='<%# Eval("TRN_DATE","{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Corton" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Corton" runat="server" CssClass=" SmallFont" Text='<%# Eval("CARTON_NO") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lot No" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Lot_No" runat="server" CssClass=" SmallFont" Text='<%# Eval("LOT_NO") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue Qty" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ISSUE_QTY" runat="server" CssClass=" SmallFont" Text='<%# Eval("ISSUE_QTY") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch No" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_BATCHNO" runat="server" CssClass=" SmallFont" Text='<%# Eval("BATCHNO") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_FINAL_RATE" runat="server" CssClass=" SmallFont" Text='<%# Eval("FINAL_RATE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="View Detail">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_trn" Text="View Detail" runat="server"></asp:LinkButton>
                                        <asp:Panel ID="grdTrn" runat="server" Width="470px" BackColor="Beige" BorderWidth="2px"
                                            Height="47px" ScrollBars="Auto">
                                            <asp:GridView ID="Grdadj" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                                                EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                                                Width="100%">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Challan Numb" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_CHALLAN_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("CHALLAN_NUMB") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN NUMB" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_MRN_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("MRN_NUMB") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="btn_trn"
                                            PopupControlID="grdTrn" PopupPosition="Left" PopDelay="10">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>



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
