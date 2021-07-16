<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="ProductionDoffReport.aspx.cs" Inherits="Module_Production_Report_ProductionDoffReport" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; 
        *display:inline;
        overflow:hidden;
        white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
       <%--<asp:UpdatePanel ID="UpdatePanel8971" runat="server">
    <ContentTemplate>--%>
        <table width="100%" class ="td tContentArial">
            <tr>
                <td width="100%">
                    <table >
                        <tr>
                        <td >
                                &nbsp;</td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin ">
                                <span class="titleheading">Production Doff Report Form</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                        <tr>
                            <td align="right" class="style1">
                                Select&nbsp;Branch:
                            </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="style2">
                                Dept.: </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                <td align="right" class="style2">
                                Doff&nbsp;Id:</td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlBatchNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Lot&nbsp;No:</td>
                            <td >
                                <asp:DropDownList ID="ddlLotNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                              <td>
                                   Machine</td>
                                <td>
                                    <cc1:ComboBox ID="ddlMacCode" runat="server" CssClass="SmallFont"
                            EmptyText="select Machine" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                            Height="200px" MenuWidth="650px" OnLoadingItems="ddlMacCode_LoadingItems" 
                            Width="160px">
                            <HeaderTemplate>
                                <div class="header c3">
                                    Mac Code</div>
                                <div class="header c2">
                                    Mac Group</div>
                                <div class="header c3 ">
                                    Mac Segement</div>
                                <div class="header c3">
                                    Mac Type</div>
                                <div class="header c3 ">
                                    Mac Section</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                                </div>
                                <div class="item c3 ">
                                    <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("MACHINE_TYPE") %>' />
                                </div>
                                <div class="item c3 ">
                                    <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("MACHINE_SEC") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox></td>
                            <td align="right" class="style1">
                                </td>
                            <td >
                            </td>    
                           
                        </tr>
                        <caption>
                            <br />
                            <tr>
                                <td align="right">
                                    From&nbsp;Date: </td>
                                <td>
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="145px" CssClass="SmallFont"   ></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged"--%>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                    
                                </td>
                                <td>
                                    To&nbsp;Date:</td>
                                <td>
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"
                           ></asp:TextBox>  <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged"--%>
               <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                  
                                
                                </td>
                                <td>
                                    Lot&nbsp;Type:</td>
                                <td>
            <asp:DropDownList Width="160px" TabIndex="2"
                ID="ddlLotType" runat="server"  AppendDataBoundItems="True">
                <asp:ListItem id="select0" Value="" Text="------------Select------------"></asp:ListItem>
                <asp:ListItem id="Domastic" Text="Domastic"></asp:ListItem>
                <asp:ListItem id="Export" Text="Export"></asp:ListItem>
            </asp:DropDownList>
                                </td>
                                <td>
                                    Merge&nbsp;No:</td>
                                <td>
                                <asp:DropDownList ID="ddlMergeNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                </td>
                                <td>Prod.&nbsp;Id:</td>
                                <td
                                </td>
                                <asp:DropDownList ID="ddlProdPocsIdNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                <td align="right">
                                    Order No:</td>
                                <td>
                                <asp:DropDownList ID="ddlOrderNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                
                                    </td>
                                    <td align="right" class="style2">
                                Finish&nbsp;Type:</td>
                            <td >
            <asp:DropDownList Width="150px" TabIndex="2" 
                ID="ddlFinishType" runat="server" AppendDataBoundItems="True">
                <asp:ListItem id="select1" Value="" Text="----------Select-----------"></asp:ListItem>
                <asp:ListItem id="Soft" Text="Soft"></asp:ListItem>
                <asp:ListItem id="Hard" Text="Hard"></asp:ListItem>
            </asp:DropDownList>
                            </td>
                              
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                <td
                                </td>
                                    &nbsp;<td>
                                        &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                                <tr>
                                   
                                    <td colspan="10" width="50%">
                                        <asp:UpdateProgress ID="UpdateProgress9587" runat="server">
                                            <ProgressTemplate>
                                                <h3>
                                                    Loading...</h3>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        
                                        <asp:Panel ID="Panel1" runat="server" BackColor="#99CCFF">
                            <asp:RadioButtonList ID="redForQuery" runat="server" Height="16px" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Red" Value="red" Selected="True">Production Summary</asp:ListItem>
                                <asp:ListItem  Text="Green" Value="green">Lot Wise Production Summary</asp:ListItem>
                                <asp:ListItem  Text="Blue" Value="blue">Lot Wise Doff Production Summary</asp:ListItem>
                                <asp:ListItem Text="Yellow" Value="yellow">Machine Wise Production Summary</asp:ListItem>
                                 <asp:ListItem Text="Dark" Value="dark">Machine Wise Production Details</asp:ListItem>
                                <asp:ListItem Text="Sky" Value="sky">All Production Details</asp:ListItem>
                                 
                                
                            </asp:RadioButtonList>
                        </asp:Panel>
                                    </td>
                                </tr>
                            </caption>
                        </caption>
                    </table>
                 
                </td>
            </tr>
        </table>
    <%--</ContentTemplate>--%>                         
</asp:Content>
