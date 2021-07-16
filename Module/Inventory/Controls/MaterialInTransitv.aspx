<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialInTransitv.aspx.cs" Inherits="Module_Inventory_Pages_MaterialInTransitv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

      <%--<style type="text/css">
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
    </style>--%>
    <asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>
    
    <table align="left" class=" td tContentArial" width="945px">
        <tr>
            <td class="td" colspan="9">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                ></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                ></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Width="48"  />
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
            <td align="center" class="TableHeader td" colspan="9">
                <span class="titleheading"><strong>Material In Transit Report </strong></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                Transit:
            </td>
            <td>
                <asp:DropDownList ID="ddltrans" runat="server" CssClass="gCtrTxt " Font-Size="9" Width="160px" AutoPostBack="True" >
                    <asp:ListItem Value="1" >Shipment </asp:ListItem>
                    <asp:ListItem Value="2" >Gate In </asp:ListItem>
                   <%-- <asp:ListItem Value="3" >Gate In </asp:ListItem>--%>
                </asp:DropDownList>
            </td>
            <td class="tdRight">
               PO Number :
            </td>
            <td>
                <asp:TextBox ID="txtpo" runat="server" CssClass="gCtrTxt"></asp:TextBox>
            </td>
            <td class="tdRight">
                From date:
            </td>
            <td class="tdLeft">
                <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                    AutoPostBack="True" ></asp:TextBox>
                    
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtFromDate">
                        </cc1:MaskedEditExtender>
            </td>
            <td class="tdRight">
                To Date:
            </td>
            <td class="tdLeft">
                <asp:TextBox ID="TxtToDate" runat="server" AutoPostBack="True" CssClass="SmallFont TextBox UpperCase"  Width="150px"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" TargetControlID="TxtToDate">
                </cc1:MaskedEditExtender>
            </td>
            <td class="tdLeft">
                <asp:Button ID="txtsubmit" runat="server" Text="Search" OnClick="txtsubmit_Click" />
            </td>
        </tr>
        <tr>
            <td class="TdBackVir" colspan="9">
              <asp:GridView ID="gvmaterialintransitv"    runat="server" AllowSorting="True"
                AutoGenerateColumns="False" Width="95%" >
                <Columns>

                     <asp:TemplateField HeaderText="PO&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPO_NUMB" runat="server" ToolTip='<%# Bind("PO_NUMB") %>' Text='<%# Bind("PO_NUMB") %>'
                                CssClass="SmallFont LabelNo"></asp:Label>
                       
                            <asp:Label ID="lblPO_type" runat="server" Text='<%# Bind("PO_TYPE") %>' Visible="false"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                        <ItemStyle  Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>

                  <%--  <asp:BoundField HeaderText="PO&nbsp;No" DataField="PO_NUMB" ReadOnly="true" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>--%>
                     <asp:BoundField HeaderText="PO&nbsp;Date" DataField="po_date" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                     <asp:BoundField HeaderText="PO&nbsp;Nature" DataField="PO_NATURE" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                     <asp:BoundField HeaderText="Party" DataField="PARTY_DATA" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                    <asp:BoundField HeaderText="Delivery&nbsp;Date" DataField="DEL_DATE" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                    <asp:BoundField HeaderText="Shipment&nbsp;Date" DataField="SHIPMENT_DATE" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                     <asp:BoundField HeaderText="Remark" DataField="TRANSIT_REMARK" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                     <asp:BoundField HeaderText="Gate&nbsp;In&nbsp;Date" DataField="GATE_DATE" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewPOTRN" runat="server" Text="View Details" OnClick="lbtnViewPOTRN_Click" 
                                ></asp:LinkButton>
                            <asp:Panel ID="pnlPOTRN" runat="server" Width="520px" BackColor="Beige" BorderWidth="2px"
                                Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="grdPOTRN" runat="server" AutoGenerateColumns="False" Width="500px"
                                    CssClass="SmallFont" Height="140px">
                                    <Columns>
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORD_QTY" HeaderText="Ordered Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BASIC_RATE" HeaderText="Basic Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VALUE" HeaderText="Value">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUOTATION_NO" HeaderText="Quotation No">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEL_DATE" HeaderText="Delivery Date">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COMMENTS" HeaderText="Comments">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle  Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmePOTRN" runat="server" PopupPosition="Left" PopupControlID="pnlPOTRN"
                                TargetControlID="lbtnViewPOTRN">
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
        <tr>
            <td colspan="9">
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
    </table>
    
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

