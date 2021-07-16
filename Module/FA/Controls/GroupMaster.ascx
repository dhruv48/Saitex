<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupMaster.ascx.cs" Inherits="FA_Controls_GroupMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>--%>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
    }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 170px;
    }
    .TableHeader
    {
        color: #000000;
        font-weight: bold;
        background-color: #336799;
        font-family: Verdana,arial;
        text-decoration: none;
        font-size: 13px;
        text-align: center;
        height: 19px;
    }
    body
    {
        padding-top: 0px;
        top: 0px;
        background-color: #FFFFFF;
    }
    .td
    {
        border-style: ridge;
        border-bottom-width: .5px;
        border-color: #C1D3FB;
    }
    .titleheading
    {
        font-family: arial;
        font-size: 12px;
        font-weight: bold;
        color: #ffffff;
    }
    .Label
    {
        font-family: Arial, Arial, Helvetica, sans-serif;
        font-size: 12px;
        vertical-align: top;
        text-align: left;
    }
    .LabelNo
    {
        font-family: Arial, Arial, Helvetica, sans-serif;
        font-size: 12px;
        text-align: right;
        vertical-align: top;
    }
    .tContentArial
    {
        font-family: Arial;
        font-size: 12px;
    }
    .tablebox
    {
        border: 1px solid #336799;
    }
    .style1
    {
        color: #000000;
        font-weight: bold;
        background-color: #336799;
        font-family: Verdana,arial;
        text-decoration: none;
        font-size: 13px;
        text-align: center;
        height: 19px;
        width: 525px;
    }
    .style3
    {
        height: 23px;
        width: 425px;
    }
    .style4
    {
        width: 425px;
    }
    .style6
    {
        font-family: Arial;
        font-size: 12px;
        width: 96px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table>
            <tr>
                <td class="td ">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click" ToolTip="Delete" Width="48" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Group Master</span>
                </td>
            </tr>
            <tr>
                <td class="td" align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td class="td ">
                    <table align="left" class="tContentArial">
                        <tr>
                            <td>
                                Group Code
                            </td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="txtGroupcode" runat="server" ValidationGroup="M1" Width="200px"
                                    CssClass="TextBox TextBoxDisplay" ReadOnly="true" TabIndex="1"></asp:TextBox>
                                <cc2:ComboBox ID="ddlfind" runat="server" DataTextField="GRP_NAME" DataValueField="GRP_CODE"
                                    EnableLoadOnDemand="True" TabIndex="2" OnLoadingItems="ddlfind_LoadingItems"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlfind_SelectedIndexChanged1" AutoValidate="True"
                                    EmptyText="Select Group" Width="203" AppendDataBoundItems="False" MenuWidth="250px"
                                    Height="250px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c4">
                                            Name
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("GRP_CODE")%></div>
                                        <div class="item c4">
                                            <%# Eval("GRP_NAME")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:LinkButton ID="lnkbtnGroupCode" runat="server" ForeColor="BlueViolet" Font-Bold="true"
                                    Font-Italic="true" OnClick="lnkbtnGroupCode_Click" TabIndex="3">View Group Tree</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Group Name
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtGroupname" runat="server" ValidationGroup="M1" Width="200px"
                                    CssClass="gCtrTxt UpperCase" MaxLength="50" TabIndex="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupname"
                                    ErrorMessage="Pls Enter Group Name" ValidationGroup="M1" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Parent Code
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlprntcode" runat="server" AppendDataBoundItems="true" Width="205px"
                                    TabIndex="5" DataValueField="GRP_CODE" DataTextField="GRP_NAME" CssClass="SmallFont">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFParentCode" runat="server" ControlToValidate="ddlprntcode"
                                    Display="None" ErrorMessage="Please Select Parent Code" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesc" runat="server" TabIndex="6" TextMode="MultiLine" Width="200px"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td class="tContentArial">
                                <asp:CheckBox ID="chkActive" runat="server" TabIndex="7"></asp:CheckBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
              
            <td>
              <table width="100%">
              <tr>
               <td valign="top">
                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="Grid1_PageIndexChanging"
                            PagerStyle-HorizontalAlign="Left" PageSize="50" OnSelect="Grid1_Select" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="GRP_CODE" HeaderText="Group Code" />
                                <asp:BoundField DataField="GRP_NAME" HeaderText="Group Name" />
                                <asp:BoundField DataField="PARENT_NAME" HeaderText="Parent Name" />
                                <asp:BoundField DataField="GRP_DESC" HeaderText="Description" />
                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                
                                 </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </table> 
                   
                    </td>
             
        </tr>
        </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>
--%>