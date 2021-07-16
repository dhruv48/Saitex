<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="Itchs_Custom_Master.aspx.cs" Inherits="Module_Fiber_Pages_Itchs_Custom_Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
  <asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>

<table cellpadding="0" cellspacing="0" border="0" width="100%" align="left">
    <tr>
        <td align="left" width="100%" valign="top" >
            <br />
            <table cellpadding="0" cellspacing="0" border="0" width="60%" align="left" class="tContentArial">
           <%-- tContentArial tablebox--%>
                <tr>
                    <td align="center" colspan="3" class ="td">
                        <table cellpadding="0" cellspacing="0" border="0" align="left" width="50%">
                            <tr>
                                <td id="tdSave" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="New" ImageUrl="~/CommonImages/save.jpg"
                                        Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                                </td>
                                <td id="tdUpdate" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                        Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                                </td>
                                <td id="tdDelete" runat="server">
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                        Width="48" Height="41" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                                </td>
                                <td id="tdFind" runat="server">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                        Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                        Width="48" Height="41" OnClick="imgbtnClear_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                        Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                </td>
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
                    </td>
                </tr>
                <tr>
                    <td align="center" class="TableHeader" colspan="3">
                        <asp:Label ID="lblFormHeading" CssClass="titleheading" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3" valign="top">
                        <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" colspan="3">
                        <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="M1" />
                    </td>
                </tr>
                <tr>
                
                <td class ="td">
                
                <table align="center" colspan="3" class="tContentArial">
                
                  <tr>
                    <td align="right" valign="top" width="30%">
                        <%--Master Name:--%>
                    </td>
                    <td align="left" valign="top" width="70%">
                        <cc2:ComboBox ID="cmbFind" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="cmbFind_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            Height="200px" CssClass="SmallFont" EmptyText="Find Code" OnSelectedIndexChanged="cmbFind_SelectedIndexChanged"
                            MenuWidth="400px" Width="150px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Master Name</div>
                                <div class="header c2">
                                    Master Code</div>
                                <div class="header c3">
                                    Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_NAME") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                            ErrorMessage="*Select Master Name" ControlToValidate="ddlMasterName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </cc1:AutoCompleteExtender>
                          </cc1:AutoCompleteExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="30%">
                        ITCHS Code:
                    </td>
                    <td align="left" valign="top" width="70%">
                        <asp:TextBox ID="txtMstCode" runat="server" CssClass="TextBox UpperCase" Width="148px"
                            TabIndex="2" MaxLength="50"></asp:TextBox>
                      
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                            ErrorMessage="* Enter Master Code" ControlToValidate="txtMstCode" ValidationGroup="M1"></asp:RequiredFieldValidator>
                       
                    </td>
                </tr>
                                <tr>
                    <td align="right" valign="top" width="30%">
                   
                        Desc: 
                    </td>
                    <td align="left" valign="top" width="70%">
                    <asp:DropDownList ID="ddlpallettype" runat="server" Width="150px" CssClass="TextBox UpperCase" Visible=false>
                     <asp:ListItem>Wooden</asp:ListItem>
                     <asp:ListItem>Plastic</asp:ListItem>
                    </asp:DropDownList>
                        <asp:TextBox ID="txtMstDesc" runat="server" CssClass="TextBox UpperCase" Width="250px"
                            TabIndex="3" Rows="1"  TextMode="SingleLine" MaxLength="50" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                            ErrorMessage="* Enter Master Description" ControlToValidate="txtMstDesc" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <%--   added by Nishant rai at 26/07/2013--%>
                    
                    <tr>
                        <td align="right" valign="top" width="30%">
                          <asp:Label ID="lblCodePrefix" runat="server" Text="Code&nbsp;Prefix:" Visible=false></asp:Label>  
                        </td>
                        <td align="left" valign="top" width="70%"  >
                            <asp:TextBox ID="txtCodePrefix" runat="server" CssClass="TextBox UpperCase" Width="148px"  TabIndex="3"
                                MaxLength="2" Visible=false ></asp:TextBox>
                            <br />
                            
                        </td>
                    </tr>
                    
                    <%--   added by Nishant rai at 26/07/2013--%>
  
                
                </table>
                
                </td>
                </tr>
              
                
            </table>
        </td>
    </tr>
</table>
 </ContentTemplate>
    </asp:UpdatePanel>
                                
</asp:Content>
