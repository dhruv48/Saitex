<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkOrder_Issue.ascx.cs" Inherits="Module_WorkOrder_Controls_WorkOrder_Issue" %>
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
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImportExcel" runat="server" Height="41" OnClick="imgBtnExportExcel_Click" ImageUrl="~/CommonImages/export.png"
                            ToolTip="Import To Excel" Width="48" /></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td">
                        <span class="titleheading"><strong>Work Order Issue </strong></span>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>TRN TYPE</td>
                    <td>WO NUMBER</td>
                    <td>JOBER PARTY</td>
                    <td>QUALITY NAME</td>
                    <td>FROM DATE</td>
                    <td>TO DATE</td>
                    <td></td>

                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlTrnType" runat="server" DataTextField="TRN_TYPE" DataValueField="TRN_TYPE"
                            Width="160px" CssClass="gCtrTxt " Font-Size="8">
                        </asp:DropDownList>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddwo" runat="server" Width="170px" >
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddprty" runat="server" Width="170px" >
                        </asp:DropDownList></td>
                    <td>
                        <cc2:ComboBox ID="ddlYarn" runat="server" AutoPostBack="True" CssClass="smallfont"
                            DataTextField="YARN_CODE" DataValueField="YARN_CODE" EnableLoadOnDemand="true"
                            MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems" EnableVirtualScrolling="true"
                            OpenOnFocus="true" TabIndex="9" Visible="true" Height="200px" EmptyText="---------All--------">
                            <HeaderTemplate>
                                <div class="header c4">
                                    YARN CODE
                                </div>
                                <div class="header c5">
                                    YARN DESCRIPTION
                                </div>
                                <div class="header c6">
                                    TYPE
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <%# Eval("YARN_CODE") %>
                                </div>
                                <div class="item c5">
                                    <%# Eval("YARN_DESC") %>
                                </div>
                                <div class="item c6">
                                    <%# Eval("YARN_TYPE")%>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                        <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
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
                            Width="100%" OnPageIndexChanging="Get_WO_Detail_PageIndexChanging"
                            >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="ISSUE NUMB" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_TRN_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("TRN_NUMB") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO NUMB" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_WO_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("WO_NUMB") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QUALITY NAME" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_QUALITY_NAME" runat="server" CssClass=" SmallFont" Text='<%# Eval("QUALITY") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JOBER PARTY" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_JOBER_PARTY" runat="server" CssClass=" SmallFont" Text='<%# Eval("JOBER_PARTY") %>' />
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
                                        <asp:Label ID="lbl_UOM" runat="server" CssClass=" SmallFont" Text='<%# Eval("UOM") %>' ToolTip='<%# Eval("PRTY_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WORK CAT" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOB_WORK_CAT" runat="server" CssClass=" SmallFont" Text='<%# Eval("FORM_TYPE") %>' />
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
                                        <asp:Label ID="lbl_Corton" runat="server" CssClass=" SmallFont" Text='<%# Eval("CARTONS") %>' />
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
                                 <asp:TemplateField HeaderText="Nt. Weight" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_WEIGHT" runat="server" CssClass=" SmallFont" Text='<%# Eval("TRN_QTY") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <%--<asp:TemplateField HeaderText="Issue">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_trn" Text="View Detail" runat="server"></asp:LinkButton>
                                        <asp:Panel ID="grdTrn" runat="server" Width="470px" BackColor="Beige" BorderWidth="2px"
                                            Height="47px" ScrollBars="Auto">
                                            <asp:GridView ID="Grd_Wo_trn" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                                                EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                                                Width="100%" OnPageIndexChanging="Grd_Wo_trn_PageIndexChanging">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Wo&nbsp;Numb" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblwo_numb" runat="server" CssClass=" SmallFont" Text='<%# Eval("WO_NUMB") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARTICLE_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ARTICLE_CODE") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="BASE_SHADE_CODE" HeaderText="Base&nbsp;Shade&nbsp;Code" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTY" HeaderText="Qty" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTY_ISS" HeaderText="Qty&nbsp;Issue" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="lnkbtn"
                                            PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
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
            <table width="100%">
                <tr>
                    <td align="center" valign="top" id="tdtran_id" runat="server" class="tContentArial td ">
                        <strong>Transcation Detail </strong>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
