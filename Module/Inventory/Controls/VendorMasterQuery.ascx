<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VendorMasterQuery.ascx.cs"
    Inherits="Module_Inventory_Controls_VendorMasterQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<style type="text/css">
    .style1
    {
        width: 268435424px;
        margin-top: 0px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel4531" runat="server">
    <ContentTemplate>--%>
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
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
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
                        <span class="titleheading">Vendor Master Query</span>
                    </td>
                </tr>
            </table>
            <table class="tContentArial">
                <tr>
                    <td align="right" width="25%">
                        Party&nbsp;Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPartyName" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                        <cc3:AutoCompleteExtender ID="AutoCompletetxtPartyName" runat="server" ServiceMethod="AutoYarntxtPartyNameL"
                            ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                            CompletionSetCount="1" TargetControlID="txtPartyName" FirstRowSelected="false">
                        </cc3:AutoCompleteExtender>
                    </td>
                 
                    <td align="right" width="10%">
                        Party&nbsp;City:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlprtycity" runat="server" CssClass="tContentArial" Width="165px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Group&nbsp;Code:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlvendorcode" runat="server" CssClass="tContentArial" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Category:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="tContentArial" Width="165px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Pin&nbsp;Code:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:TextBox ID="txtpincode" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        Status:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="tContentArial" Width="160px">
                         <asp:ListItem Selected="True" Value="">All</asp:ListItem>                           
                        <asp:ListItem Value="1">APPROVED</asp:ListItem>
                        <asp:ListItem Value="0">PENDING</asp:ListItem>
                         <asp:ListItem Value="3">REJECTED</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Credit&nbsp;Limit:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:TextBox ID="txtCredit" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                    </td>
                    <td align="right" width="10%">
                        Region:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlregion" runat="server" CssClass="tContentArial" Width="160px">
                            <asp:ListItem Value="">--Select Region--</asp:ListItem>
                            <asp:ListItem>East</asp:ListItem>
                            <asp:ListItem>West</asp:ListItem>
                            <asp:ListItem>North</asp:ListItem>
                            <asp:ListItem>South</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Mobile&nbsp;No:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:TextBox ID="txtmobile" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                    </td>
                    <td align="right" width="10%">
                        Search:
                    </td>
                    <td align="left" valign="top" width="10%">
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" Height="22px" Width="85"
                            OnClick="btngetdata_Click" CssClass="AButton" />
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
                                    <asp:Panel ID="pnlShowHover" runat="server" Width="1100px" ScrollBars="Auto">
                                        <asp:GridView ID="Grid12" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                                            AllowPaging="true" PageSize="20" CellPadding="3" ForeColor="#333333" GridLines="Both"
                                            BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" OnPageIndexChanging="Grid12_PageIndexChanging"
                                            Width="200%" Style="margin-right: 627px">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                            <Columns>
                                                <asp:BoundField DataField="VENDOR_CODE" HeaderText="VENDOR CODE" ></asp:BoundField>
                                                <asp:BoundField DataField="VENDOR_GROUP_CODE" HeaderText="GROUP CODE"></asp:BoundField>
                                                <asp:BoundField DataField="VENDOR_NAME" HeaderText="PARTY NAME"></asp:BoundField>
                                                <asp:BoundField DataField="VENDOR_ADDRESS1" HeaderText="VENDOR ADDRESS1 "></asp:BoundField>
                                                <asp:BoundField DataField="PIN_CODE" HeaderText="PIN CODE"></asp:BoundField>
                                                <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="BOARD_NO_1" HeaderText="BOARD NO"></asp:BoundField>
                                                <asp:BoundField DataField="PERSON1_NAME" HeaderText="PERSON NAME"></asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="E-MAIL"></asp:BoundField>
                                                <asp:BoundField DataField="VENDOR_PAN_NO" HeaderText="PAN No"></asp:BoundField>
                                                <asp:BoundField DataField="BANK_NAME" HeaderText="BANK"></asp:BoundField>
                                                <asp:BoundField DataField="ACCOUNT_NO" HeaderText="ACCOUNT"></asp:BoundField>
                                                <asp:BoundField DataField="NEFT_RTGS_CODE" HeaderText="NEFT/RTGS"></asp:BoundField>
                                                <asp:BoundField DataField="CR_LIMIT" HeaderText="CREDIT LIMIT"></asp:BoundField>
                                                <asp:BoundField DataField="PRODUCT" HeaderText="PRODUCT "></asp:BoundField>
                                                <asp:BoundField DataField="STATE_NAME" HeaderText="STATE NAME "></asp:BoundField>
                                                <asp:BoundField DataField="GSTN_NO" HeaderText="GST_NO "></asp:BoundField>
                                                <asp:BoundField DataField="STATE_CODE" HeaderText="STATE_CODE "></asp:BoundField>
                                                <asp:BoundField DataField="VENDOR_CITY" HeaderText="CITY "></asp:BoundField>
                                                <asp:BoundField DataField="PIN_CODE" HeaderText="PIN "></asp:BoundField>
                                                
                                                <asp:BoundField DataField="APRV_STATUS" HeaderText="STATUS "></asp:BoundField>
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
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>