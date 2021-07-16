<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="FabricRecievingQuery.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricRecievingQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1;
            display: inline;
            overflow: hidden;
            white-space: nowrap;
        }
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
    <table class="td tContentArial">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="Clear" runat="server" ImageUrl="~/CommonImages/clear.jpg" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="Exit" runat="server" 
                                            ImageUrl="~/CommonImages/link_exit.png" onclick="Exit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="15" class="TableHeader td tdinset">
                <span class="titleheading">Fabric Recieving Query form</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                FABRIC CODE:
            </td>
            <td>
            <asp:DropDownList ID="ddlFabric" runat="server"   Width="150px" CssClass="SmallFont TextBox"></asp:DropDownList>
                <%--<cc2:ComboBox ID="ddlFabric1" runat="server" AutoPostBack="true" DataTextField="FABR_CODE"
                    DataValueField="FABR_CODE" CssClass="SmallFont" EnableLoadOnDemand="true" MenuWidth="660"
                    OnLoadingItems="Fabric_Lov_Items" EnableVirtualScrolling="true" OpenOnFocus="true"
                    Visible="true" Height="660px" EmptyText="-----All">
                    <HeaderTemplate>
                        <div class="header c1">
                            FABR_CODE
                        </div>
                        <div class="header c2">
                            FABR_DESC
                        </div>
                        <div class="header c3">
                            FABR_TYPE
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item c4">
                            <%#Eval("FABR_CODE") %>
                        </div>
                        <div class="item c5">
                            <%#Eval("FABR_DESC") %>
                        </div>
                        <div class="item c6">
                            <%#Eval("FABR_TYPE") %>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%#Container.ItemsCount>0?"1":"0" %>-<%#Container.ItemsLoadedCount %>
                        out of
                        <%#Container.ItemsCount %>
                    </FooterTemplate>
                </cc2:ComboBox>--%>
            </td>
            <td align="right">
                PARTY:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlParty" runat="server" Width="150px" CssClass="SmallFont TextBox">
                </asp:DropDownList>
            </td>
            <td align="right">
                FROM TRN. NO.:
            </td>
            <td align="left">
                <asp:TextBox ID="txtfromtrnno" runat="server" Width="75px" CssClass="TextBox SmallFont"></asp:TextBox>
            </td>
            <td align="right">
                TO TRN. NO.:
            </td>
            <td align="right">
                <asp:TextBox ID="txttotrnno" runat="server" Width="75px" CssClass="SmallFont TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                FROM TRN.DATE:
            </td>
            <td align="left">
                <asp:TextBox ID="txtfrmdate" runat="server" Width="75px" CssClass="SmallFont TextBox"></asp:TextBox>
            </td>
            <td align="right">
                TO TRN. DATE:
            </td>
            <td align="left">
                <asp:TextBox ID="txttodate" runat="server" Width="75px" CssClass="SmallFont TextBox"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnGetRecord" runat="server" Text="GetRecord" 
                    onclick="btnGetRecord_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="8" class=" td tContentArial">
                <b>TOTAL RECORD : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td " colspan="15">
                <asp:Panel ID="pnlShowHover" runat="server" Width="950px" Height="350px" ScrollBars="Auto">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Ridge"
                        AllowSorting="True" CellPadding="3" CssClass="smallfont" EmptyDataText="No Record Found"
                        Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" Width="150%"
                        BorderColor="#CCCCCC" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="14"
                        AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" BackColor="White"
                        BorderWidth="1px">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
                            <asp:TemplateField HeaderText="COMPANY" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompCode1" Text='<%#Eval("COMP_CODE") %>' ToolTip='<%#Eval("COMP_NAME") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BRANCH" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" Text='<%#Eval("BRANCH_CODE") %>' ToolTip='<%#Eval("BRANCH_NAME") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FABRIC" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblfabric" Text='<%#Eval("FABR_CODE") %>' ToolTip='<%#Eval("FABR_DESC") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARTY CODE" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblParty" Text='<%#Eval("PRTY_CODE") %>' ToolTip='<%#Eval("PRTY_NAME") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                            <asp:BoundField DataField="TRN_Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="TRN DATE" />
                            <asp:TemplateField HeaderText="TRN NUMB" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrnnumb" Text='<%#Eval("TRN_NUMB") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TRN_QTY" DataFormatString="{0:0.00}" HeaderText="TRN QTY"
                                ItemStyle-HorizontalAlign="Right" ControlStyle-ForeColor="#3399FF" ControlStyle-BackColor="Blue">
                                <ControlStyle BackColor="Blue" ForeColor="#3399FF" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRN_QTY_ADJ" HeaderText="TRN QTY ADJ" />
                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                            <asp:BoundField DataField="BASIC_RATE" HeaderText="BASIC RATE" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#0099CC">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FINAL_RATE" DataFormatString="{0:0.00}" HeaderText="FINAL RATE"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#669999">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAC_CODE" HeaderText="MAC CODE" />
                            <%-- <asp:BoundField DataField="PI_NO" HeaderText="PI NO" />--%>
                            <asp:TemplateField HeaderText="PI NO" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrnpino" Text='<%#Eval("PI_NO") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO OF UNIT" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM OF UNIT" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WEIGHT_OF_UNIT" DataFormatString="{0:0.00}" HeaderText="WEIGHT OF UNIT"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LR_NUMB" HeaderText="LR NUMB" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="SubTran">
                                <ItemTemplate>
                                    <asp:Label ID="LblFinalRate" Text="View Sub Tran" runat="server" ForeColor="#3333FF"></asp:Label>
                                    <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                        BorderWidth="5px" HorizontalAlign="Left">
                                        <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="LOT_NO" HeaderText="LOT NO." HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GRADE" HeaderText="GRADE" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TRN_QTY" HeaderText="QTY" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO. UNIT" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="WEIGHT OF UNIT" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DATE_OF_MFG" HeaderText="DATE OF MANUFACTURE" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MATERIAL_STATUS" HeaderText="STATUS" HeaderStyle-Font-Size="8">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="SmallFont" />
                                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="LblFinalRate"
                                        PopupPosition="Left">
                                    </cc1:HoverMenuExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
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
        <td colspan="15">
        <cc1:CalendarExtender ID="cc1Extender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="Mc1Extender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrmdate" PromptCharacter="-"></cc1:MaskedEditExtender>
        <cc1:CalendarExtender ID="cc2Ectender" Format="dd/MM/yyyy" runat="server" TargetControlID="txttodate"></cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="Mc2Extender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate" PromptCharacter="-"></cc1:MaskedEditExtender>
        </td>
        </tr>
    </table>
</asp:Content>
