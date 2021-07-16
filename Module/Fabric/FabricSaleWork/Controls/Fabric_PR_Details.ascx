<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fabric_PR_Details.ascx.cs"
    Inherits="Module_Fabric_FabricSaleWork_Controls_Fabric_PR_Details" %>
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
<table>
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="Print" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48" OnClick="Print_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="Clear" runat="server" ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear"
                            Height="41" Width="48" OnClick="Clear_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="Exit" ToolTip="Exit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            Height="41" Width="48" OnClick="Exit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class=" td tContentArial" width ="945px"  >
    <tr>
        <td align="center" colspan="8" class="TableHeader td">
            <span class="titleheading"><strong>Purchase Register Detail</strong></span>
        </td>
    </tr>
    <tr>
        <td align="right">
            Branch:
        </td>
        <td>
            <asp:DropDownList ID="ddl_branch" runat="server" Font-Size="8" AutoPostBack="true"
                Width="160px" CssClass="gCtrText" OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Year:
        </td>
        <td>
            <asp:DropDownList ID="ddl_year" runat="server" Font-Size="8" AutoPostBack="true"
                Width="160px" CssClass="gCtrtext" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="tdRight" align="right">
            From date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
        </td>
        <td class="tdRight" align="right">
            To Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                runat="server" OnTextChanged="TxtToDate_TextChanged1" AutoPostBack="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdRight" align="right">
            Fabric:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlFabrCode" runat="server" CssClass="gCtrTxt" Font-Size="8"
                Width="160px">
            </asp:DropDownList>
        </td>
        <td align="right">
            Fabric Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlFabrType" runat="server" CssClass="gCtrTxt " Font-Size="8"
                Width="160px">
            </asp:DropDownList>
        </td>
        <td align="right">
            Party:
        </td>
        <td>
            <asp:DropDownList ID="ddlpartycode" runat="server" DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"
                Width="160px" AutoPostBack="True" CssClass="tContentArial">
            </asp:DropDownList>
        </td>
        <td align="right">
            Department:
        </td>
        <td>
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt" Font-Size="8"
                Width="160px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="GetReport" Text="GetReport" runat="server" OnClick="GetReport_Click" />
        </td>
    </tr>
     <tr>
        <td colspan="1" align="right">
            Total Record:
        </td>
        <td>
            <asp:Label ID="lbl_TotalRecord" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
  <tr></tr>
  <tr></tr>
        <td colspan="8">
            <asp:Panel ID="Pnl_PrDetail" runat="server" ScrollBars="Both" Height="100%" Width="100%">
                <asp:GridView ID="grd_PrDetail" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%"
                    CellPadding="8" CellSpacing="8" ForeColor="#333333" HeaderStyle-Wrap="true"
                    Font-Size="X-Small" AllowPaging="true" OnPageIndexChanging="grd_PrDetail_PageIndexChanging"
                    OnSelectedIndexChanged="grd_PrDetail_SelectedIndexChanged" AllowSorting="true"
                    PageSize="14">
                    <FooterStyle BackColor="#507CD1" Font-Bold="true" ForeColor="White" Width="100%" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Size="Medium" Font-Bold="true" />
                    <Columns>
                        <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
                        <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                        <asp:BoundField DataField="TRN_DESC" HeaderText="TRN DESC" />
                        <asp:BoundField DataField="MRN" HeaderText="MRN" />
                        <asp:BoundField DataField="MRN_DATE" HeaderText="MRN DATE" />
                        <asp:BoundField DataField="FABR_CODE" HeaderText="FABRIC CODE" />
                        <asp:BoundField DataField="FABR_DESC" HeaderText="FABRIC DESCRIPTION" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="MRN_QTY" HeaderText="MRN QTY" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMB" />
                        <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST CENTER CODE" />
                        <asp:BoundField DataField="GATE_DATE" HeaderText="GATE DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="PRTY_CODE" HeaderText="PARTY CODE" />
                        <asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME" />
                        <asp:BoundField DataField="TRNSPRT_CODE" HeaderText="TRNSPRT CODE" />
                        <asp:BoundField DataField="LORY_NUMB" HeaderText="LORY NUMB" />
                        <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="PRTY CH NUMB" />
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
