<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionEntry4YS.ascx.cs" Inherits="Module_Production_Queries_ProductionEntry4YS" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<style type="text/css">
    .style1
    {
        border: .05em ridge #C1D3FB;
        width: 720px;
    }
    .style2
    {
        width: 84px;
    }
</style>

<table class="td" align="center"  >
<tr >
<td valign="top" class="style1" align="left" >
<table align="left" class="tContentArial">
                <tr>
                <td align="left">
                <table align="left">
                <tr>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48" onclick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                    </tr>
                    </table>
                    </td>
                </tr>

        <tr>
        <td class="TableHeader td" align="center" colspan="6" >
            <span class="titleheading">
                Production Entry Query </span>
        </td>
    </tr>
<tr>
        <td align="left" valign="top" class="td" >
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
    <td>
    <table>
    <tr>
        <td align="right">
            PA No :
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderNo" runat="server" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right" width="17%">
            Process Code:
        </td>
        <td>
            <asp:DropDownList ID="ddlprocessno" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlprocessno_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Machine Code :
        </td>
        <td>
            <asp:DropDownList ID="ddlMachin" runat="server" DataTextField="MACHINE_CODE" DataValueField="MACHINE_CODE"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlMachin_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            Department :
        </td>
        <td>
            <asp:DropDownList ID="ddldept" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Lot No :
        </td>
        <td>
            <asp:DropDownList ID="ddllotno" runat="server" DataTextField="LOT_NUMBER" DataValueField="LOT_NUMBER"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddllotno_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Dyed Lot No :
        </td>
        <td>
            <asp:DropDownList ID="ddldyedlotno" runat="server" DataTextField="DYED_LOT_NO" DataValueField="DYED_LOT_NO"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddldyedlotno_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            Shift Name :
        </td>
        <td>
            <asp:DropDownList ID="ddlshift" runat="server" DataTextField="SFT_NAME" DataValueField="SFT_ID"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlshift_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            From Date :
        </td>
        <td>
            <asp:TextBox ID="txtformdate" runat="server" AutoPostBack="True" Width="126px" OnTextChanged="txtformdate_TextChanged"></asp:TextBox>
            <cc4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtformdate"
                PopupPosition="TopLeft">
            </cc4:CalendarExtender>
        </td>
        <td align="right">
            To Date :
        </td>
        <td>
            <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" Width="126px" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>
            <cc4:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodate"
                PopupPosition="TopLeft">
            </cc4:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td align="right" id="tdPI_Type" runat="server">
            PI Type :
        </td>
        <td colspan="5">
            <asp:DropDownList ID="ddlPItype" runat="server" DataTextField="PI_TYPE" DataValueField="PI_TYPE"
                Visible="false"  AutoPostBack="True" CssClass="tContentArial">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="6"  class="TdBackVir">
            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
        </td>
    </tr>
    
    </table>
    </td>
    </tr>
   
    </table>
    </td>
    </tr>
</table>
<table align="center" >
<tr>
    <td >
    <asp:Panel ID="pnlShowHover" runat="server" Width="712px"  BackColor="Beige" BorderWidth="2px"
                            ScrollBars="Auto" >
                            <asp:GridView  Width="712px" ID="GridProductEntry" runat="server" 
                                AutoGenerateColumns="False" AllowPaging="True"
                                CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-Wrap="true"
                                Font-Size="X-Small" OnPageIndexChanging="GridProductEntry_PageIndexChanging"
                                PageSize="15" >
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="YEAR" HeaderText="YEAR " />
                                    <asp:BoundField DataField="ORDER_NO" HeaderText="PA NO" />
                                    <asp:BoundField DataField="ARTICLE_CODE" HeaderText="ARTICLE CODE" />
                                    <asp:BoundField DataField="LOT_NUMBER" HeaderText="LOT NUMBER" />
                                    <asp:BoundField DataField="DYED_LOT_NO" HeaderText="DYED LOT NO" />
                                    <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY " Visible="false" />
                                    <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH" Visible="false" />
                                    <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
                                    <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                                    <asp:BoundField DataField="PROS_CODE" HeaderText="PROS CODE" />
                                    <asp:BoundField DataField="SFT_NAME" HeaderText="SFT NAME" />
                                    <asp:BoundField DataField="PROS_DESC" HeaderText="PROS DESC" />
                                    <asp:BoundField DataField="MAIN_PROCESS" HeaderText="MAIN PROCESS" />
                                    <asp:BoundField DataField="MACHINE_GROUP" HeaderText="MACHINE GROUP" />
                                    <asp:BoundField DataField="PROS_ID_NO" HeaderText="PROS ID_NO" 
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE CODE" />
                                    <asp:BoundField DataField="TRN_DATE" HeaderText="TRN DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField DataField="SFT_ID" HeaderText="SFT ID" />
                                    <asp:BoundField DataField="LOAD_DATE" HeaderText="LOAD DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField DataField="UNLOAD_DATE" HeaderText="UNLOAD DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField DataField="OPERATOR" HeaderText="OPERATOR" />
                                    <asp:BoundField DataField="SUPERVISOR" HeaderText="SUPERVISOR" />
                                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" Visible="false" />
                                    <asp:BoundField DataField="DEL_STATUS" HeaderText="DEL STATUS" Visible="false" />
                                    <asp:BoundField DataField="TDATE" HeaderText="TDATE" DataFormatString="{0:dd-MM-yyyy}"
                                        Visible="false" />
                                    <asp:BoundField DataField="TUSER" HeaderText="TUSER" Visible="false" />
                                    <asp:BoundField DataField="LOT_QTY" HeaderText="LOT QTY" DataFormatString="{0:0.00}"
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOAD_QTY" HeaderText="LOAD QTY" DataFormatString="{0:0.00}"
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOAD_NO_OF_UNIT" HeaderText="LOAD NO OF UNIT" 
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOAD_UOM_OF_UNIT" HeaderText="LOAD UOM OF UNIT" 
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOAD_WEIGHT_OF_UNIT" HeaderText="LOAD WEIGHT OF UNIT"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNLOAD_QTY" HeaderText="UNLOAD QTY" 
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNLOAD_NO_OF_UNIT" HeaderText="UNLOAD NO OF UNIT" 
                                        ItemStyle-HorizontalAlign="Right" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNLOAD_UOM_OF_UNIT" HeaderText="UNLOAD UOM OF UNIT" />
                                    <asp:BoundField DataField="FR_LOCATION" HeaderText="FR LOCATION" />
                                    <asp:BoundField DataField="TO_LOCATION" HeaderText="TO LOCATION" />
                                    <asp:BoundField DataField="FR_BATCH_NO" HeaderText="FR BATCH_NO" />
                                    <asp:BoundField DataField="TO_BATCH_NO" HeaderText="TO BATCH_NO" />
                                    <asp:BoundField DataField="FR_PROS_CODE" HeaderText="FR PROS CODE" />
                                    <asp:BoundField DataField="NO_OF_DRUM" HeaderText="NO OF DRUM" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SPEED_OF_DRUM" HeaderText="SPEED OF DRUM" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PATTERN_NO" HeaderText="PATTERN NO" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EFFICIENCY" HeaderText="EFFICIENCY" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WET_PACKAGES_REDRYING" HeaderText="WET PACKAGES REDRYING"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LENGTH_IN_METERS" HeaderText="LENGTH IN METERS" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REJECTIONS_OF_UNIT" HeaderText="REJECTIONS OF UNIT" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WEIGHT_OF_REJECTION" HeaderText="WEIGHT OF REJECTION"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PEEL_OF" HeaderText="PEEL OF" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </asp:Panel>
    </td>
    </tr>
</table>