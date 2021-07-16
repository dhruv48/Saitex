<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/UserMaster.master" CodeFile="Yarn_Mst_Opt.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_Yarn_Mst_Opt" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table align="left" class="tContentArial" border = "1" >
               <tr>
                <td align="left" class="td" valign="top">
                                    <table>
                                        <tr>
                                            
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }"
                                                    ToolTip="Clear" TabIndex="54" CausesValidation="false"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="55" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="56"  CausesValidation="false" 
                                                    onclick="imgbtnHelp_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
               </tr>
                <tr>
                
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="4">
                                    <span style="font-size: 13pt" class="titleheading"><strong>Yarn Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    YarnCode
                                </td>
                                <td align="left">
                                    <%--<asp:TextBox ID="txtYarnCodeRpt" runat="server" Width="150px" 
                                        CssClass="SmallFont TextBox"></asp:TextBox>--%>
                                            <cc2:ComboBox ID="ddlyarncode" runat="server"  CssClass="SmallFont"
                                                    DataTextField="Combined" DataValueField="YARN_CODE" EmptyText="Find Yarn Code" EnableLoadOnDemand="true"
                                                    Height="200px" MenuWidth="400" OnLoadingItems="ddlyarncode_LoadingItems" 
                                                    TabIndex="2"  Width="150px"  >
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            YARN CODE</div>
                                                        <div class="header c2">
                                                            YARN TYPE</div>
                                                        <div class="header c2">
                                                            YARN Description</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("YARN_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("Combined") %>' /></div>
                                                        
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                      
                                </td>
                                <td align="left">
                                    Department Code
                                </td>
                                <td>
                                        <asp:DropDownList ID="ddlDepartment" runat="server"  Font-Size="8" CssClass="SmallFont"
                                    Width="150px">
                                </asp:DropDownList>
                                </td>
                                <td>
                                    End Use</td>
                                <td align="left">
                                   <%-- <asp:TextBox ID="txtDeptCodeRpt" runat="server" Width="150px" 
                                        CssClass="SmallFont TextBox"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlENDUSE" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Category Code
                                </td>
                                <td align="left">
                                   <%-- <asp:TextBox ID="txtCatCodeRpt" runat="server" Width="150px" 
                                        CssClass="SmallFont TextBox"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlYarnCat" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="3" Width="150">
                                                </asp:DropDownList>
                                </td>
                                <td align="left">
                                    Branch Code
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                    </td>
                                <td>
                                    Location
                                </td>
                                <td align="left">
                                    <%--<asp:TextBox ID="txtBranchCodeRpt" CssClass="SmallFont TextBox" runat="server" Width="150px"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlLOCATION" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    RGB</td>
                                <td align="left">
                                <asp:DropDownList ID="ddlRGB" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    Yarn Shade</td>
                                <td>
                                <asp:DropDownList ID="ddlYARNSHADE" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Store</td>
                                <td align="left">
                                <asp:DropDownList ID="ddlSTORE" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Coating</td>
                                <td align="left">
                                <asp:DropDownList ID="ddlCOATING" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    Is Excisable</td>
                                <td>
                                <asp:DropDownList ID="ddlISEXCISABLE" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td align="left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                        
                                    
                                    <cc1:oboutbutton ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" 
                                        Text="Get Report" /></cc1:OboutButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                                
</asp:Content>
