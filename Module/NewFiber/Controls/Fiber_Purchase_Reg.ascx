<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Purchase_Reg.ascx.cs" Inherits="Module_Fiber_Controls_Fiber_Purchase_Reg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table align="left" class="tContentArial" width="945px">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            <table width="945px" class=" td tContentArial">
                <tr>
                    <td align="center" class="TableHeader td" colspan="8">
                        <span class="titleheading"><strong>Fiber Purchase Register Detail</strong> </span>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1112" runat="server">
                <ContentTemplate>
                    <table class=" td tContentArial" width ="945px" >
                        <tr>
                            <td align="right">
                                Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="8"
                                    Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Year:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                From&nbsp;date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td align="right">
                                To&nbsp;Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                                    runat="server" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Fiber&nbsp;Name:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlFibercat" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Fiber&nbsp;Category:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlFibertype" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Party:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlpartycode" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td align="tdLeft">
                                <asp:Button ID="btngetdata" runat="server" Text="Get Data" OnClick="btngetdata_Click" CssClass="AButton" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdBackVir" width="45%" colspan = "8">
                                <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                          
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                            
                        </tr>
                    </table>
                    <table width="945px">
                        <tr>
                            <td>
                                <asp:Panel ID="pnl121" runat="server" ScrollBars="Both" Width="945px" 
                                    Height="350px" BorderStyle="None">
                                    <asp:GridView ID="grd_FiberPurchase_query" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="3" BorderStyle="None" CssClass="smallfont"
                                        Font-Size="X-Small" PagerStyle-HorizontalAlign="Left"
                                        Width="150%" 
                                        OnPageIndexChanging="grd_FiberPurchase_query_PageIndexChanging" 
                                       ForeColor="#333333" PageSize="14">
                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                        <Columns>
                                            <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
                                            <asp:BoundField DataField="FIBER_DESC" HeaderText="FIBER DESCRIPTION" />
                                            <asp:BoundField DataField="TRN_DESC" HeaderText="TRNAS. DESCRIPTION" />
                                            <asp:BoundField DataField="MRN" HeaderText="MRN" />
                                            <asp:BoundField DataField="MRN_DATE" HeaderText="MRN DATE" />
                                            <asp:BoundField DataField="GATE_NUMB" HeaderText="GATE NUMBER" />
                                            <asp:BoundField DataField="GATE_DATE" HeaderText="GATE DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="PRTY_CODE" HeaderText="PARTY CODE" />
                                            <asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME" />
                                            <asp:BoundField DataField="TRSP_CODE" HeaderText="TRNSPRT CODE" />
                                            <asp:BoundField DataField="LORY_NUMB" HeaderText="LORY NUMB" />
                                            <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="PRTY NUMB" />
                                            <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMB" />
                                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
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
                    <table>
                        <tr>
                            <td>
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
        </td>
    </tr>
</table>
