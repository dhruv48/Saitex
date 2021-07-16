<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Designation.ascx.cs" Inherits="Admin_UserControls_Designation" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 180px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left" class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" OnClick="imgbtnDelete_Click"></asp:ImageButton>
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
        <td align="center" class ="TableHeader td">
            <span class="titleheading">Designation Master</span>
        </td>
    </tr>
    
    
    <tr>
        <td align="left" valign="top" class ="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    
  
   
    
    <tr>
        <td class ="td">
            <table>
                <tr>
                    <td align="right" valign="top">
                        Designation Code :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtDesigCode" runat="server" CssClass=" TextBox" 
                            Width="130px" TabIndex="1"
                            Rows="2" MaxLength="50"></asp:TextBox>
                        <obout:ComboBox runat="server" ID="ddlDesig" Width="200" Height="150" DataTextField="DESIG_NAME"
                            DataValueField="DESIG_CODE" EnableLoadOnDemand="true" AutoPostBack="True" MenuWidth="250px"
                            OnLoadingItems="ddlDesig_LoadingItems" 
                            OnSelectedIndexChanged="ddlDesig_SelectedIndexChanged1" EmptyText="Find">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Desigination Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("DESIG_NAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </obout:ComboBox>
                    </td>
                </tr>
                 <tr>
                    <td align="right" valign="top">
                        Designation Name :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtDesigName" runat="server" CssClass="SmallFont TextBox" 
                            Width="130px" TabIndex="2"
                            Rows="2" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td align="right" valign="top">
                        Senior Designation :
                    </td>
                    <td align="left" valign="top">
                        <cc2:OboutDropDownList ID="ddlDesignation" runat="server" Width="132px" 
                            TabIndex="2" MenuWidth="250px" Height="200px">
                        </cc2:OboutDropDownList>
                    </td>
                </tr>
                   <tr>
                    <td align="right" valign="top">
                        Remarks :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="gCtrTxt" Width="300px" TabIndex="4"
                            Rows="2" MaxLength="50" TextMode="MultiLine" AutoPostBack="True" Height="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  
  
  
  
   <%-- <tr>
        <td >
            <table>
               
            </table>
        </td>
    </tr>
    <tr>
        <td >
            <table>
              
            </table>
        </td>
    </tr>
    <tr>
        <td >
            <table>
             
            </table>
        </td>
    </tr>
   --%>
  
   
    
</table>
