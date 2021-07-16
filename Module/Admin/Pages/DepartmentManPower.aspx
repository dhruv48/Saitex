<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="DepartmentManPower.aspx.cs" Inherits="Module_Admin_Pages_DepartmentManPower"
    Title="Department Man Power" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
    <%@ register assembly="obout_Grid_NET" namespace="Obout.Grid" tagprefix="cc2" %>
    <style type="text/css">
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
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                <tr>
                    <td valign="top" align="left" class="td">
                        <table cellspacing="0" cellpadding="0" align="left">
                            <tbody>
                                <tr>
                                    <td id="tdSave" valign="top" align="center" runat="server">
                                        <asp:ImageButton ID="imgbtnSave" TabIndex="9" OnClick="imgbtnSave_Click" runat="server"
                                            ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                        </asp:ImageButton>
                                    </td>
                                    <td id="tdUpdate" valign="top" align="center" runat="server">
                                        <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                            ToolTip="Update" Height="41" Width="48" ValidationGroup="M1" OnClick="imgbtnUpdate_Click">
                                        </asp:ImageButton>
                                    </td>
                                    <td id="tdDelete" valign="top" align="center" runat="server">
                                        <asp:ImageButton ID="imgbtnDelete" TabIndex="9" runat="server" ImageUrl="~/CommonImages/del6.png"
                                            ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1" OnClick="imgbtnDelete_Click">
                                        </asp:ImageButton>
                                    </td>
                                    <td valign="top" id="tdFind" runat="server" align="center">
                                        <asp:ImageButton ID="imgbtnFind" TabIndex="9" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                            ToolTip="Find" Height="41" Width="48" OnClick="imgbtnFind_Click"></asp:ImageButton>
                                    </td>
                                    <td valign="top" align="center">
                                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                            ToolTip="Print" Height="41" Width="48" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                    </td>
                                    <td valign="top" align="center">
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                            ToolTip="Clear" Height="41" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                                    </td>
                                    <td valign="top" align="center">
                                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                            ToolTip="Exit" Height="41" Width="48" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                    </td>
                                    <td valign="top" align="center">
                                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader" align="center">
                        <b class="titleheading">Department Man Power</b>
                    </td>
                </tr>
                <tr>
                    <td class="td" valign="top" align="left">
                        <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
                        </asp:Label>&nbsp;Mode</span>
                    </td>
                </tr>
                <tr>
                    <td class="td ">
                        <table>
                            <tr>
                                <td align="right" valign="top">
                                    *Department&nbsp;
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <cc2:OboutDropDownList ID="DDLdepartment" runat="server" CssClass="SmallFont">
                                    </cc2:OboutDropDownList>
                                    <obout:ComboBox ID="cmbfind" runat="server" EmptyText="Find" MenuWidth="450px" AutoPostBack="True"
                                        OnLoadingItems="cmbfind_LoadingItems" EnableLoadOnDemand="True" Height="250px"
                                        OnSelectedIndexChanged="cmbfind_SelectedIndexChanged">
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("YEAR")%></div>
                                            <div class="item c2">
                                                <%# Eval("DEPT_NAME")%></div>
                                            <div class="item c3">
                                                <%# Eval("DESIG_NAME")%></div>
                                            <div class="item c4">
                                                <%# Eval("NO_OF_PERSON")%></div>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Year
                                            </div>
                                            <div class="header c2">
                                                Department</div>
                                            <div class="header c3 ">
                                                Designation
                                            </div>
                                            <div class="header c4 ">
                                                NO_OF_PERSON
                                            </div>
                                        </HeaderTemplate>
                                    </obout:ComboBox>
                                </td>
                                <td align="right" valign="top">
                                    *Desigination
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <cc2:OboutDropDownList ID="DDLDesination" runat="server" TabIndex="4" CssClass="SmallFont"
                                        Height="250px" MenuWidth="200px">
                                    </cc2:OboutDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    *No.Of Man Power&nbsp;
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtNoOfMan" runat="server" CssClass="SmallFont TextBoxNo" TabIndex="2"
                                        Width="147px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top">
                                    *Year
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtCurrentYear" runat="server" CssClass="SmallFont TextBoxNo" TabIndex="3"
                                        Width="147px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td">
                        <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="False"
                            PageSize="5" AutoGenerateColumns="False" AutoPostBackOnSelect="True" OnSelect="Grid1_Select">
                            <Columns>
                                <cc2:Column DataField="YEAR" Align="Left" HeaderText="Year" Width="100px">
                                </cc2:Column>
                                <cc2:Column DataField="DEPT_NAME" Align="Left" HeaderText="Dept Name" Width="100px">
                                </cc2:Column>
                                <cc2:Column DataField="DESIG_NAME" Align="Left" HeaderText="Desig Name" Width="150px">
                                </cc2:Column>
                                <cc2:Column DataField="NO_OF_PERSON" Align="Left" HeaderText="No. 0f Person" Width="130px">
                                </cc2:Column>
                            </Columns>
                        </cc2:Grid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
