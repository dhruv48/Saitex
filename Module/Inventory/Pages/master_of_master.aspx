<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="master_of_master.aspx.cs" Inherits="Module_Inventory_Pages_master_of_master" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="Head1">
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
            width: 150px;
        }
        .c2
        {
            margin-left: 4px;
            width: 150px;
        }
        .c3
        {
            margin-left: 4px;
            width: 150px;
        }
        
        
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    
    <table cellpadding="0" cellspacing="0" border="0" align="left">
        <tr>
            <td align="left" valign="top">
                <br />
                <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                    <tr>
                        <td align="center" class ="td">
                            <table cellpadding="0" cellspacing="0" border="0" align="left" width="50%">
                                <tr>
                                    <td id="tdSave" runat="server">
                                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="New" ImageUrl="~/CommonImages/save.jpg"
                                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                                    </td>
                                    <td id="tdFind" runat="server">
                                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                            Width="48" Height="41" OnClick="imgbtnFind_Click" CausesValidation=false></asp:ImageButton>
                                    </td>
                                  
                                    <td id="tdUpdate" runat="server">
                                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click" CausesValidation=false></asp:ImageButton>
                                    </td>
                                    <td id="tdDelete" runat="server">
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                            Width="48" Height="41" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                            Width="48" Height="41" OnClick="imgbtnClear_Click" CausesValidation=false></asp:ImageButton>
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
                        <td align="center" class="TableHeader td">
                            <span class="titleheading">Master Of Master</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class ="td">
                            <span class="Mode">You are in <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                            </span>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
                  <tr><td class ="td">
                  <table class="tContentArial">
  
  
                    <tr>
                        <td align="right" valign="top" width="30%">
                           
                        </td>
                        <td  align="left" valign="top" width="70%">
                            
                           
                            <cc2:ComboBox ID="cmbFind" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                OnLoadingItems="cmbFind_LoadingItems" DataTextField="MST_NAME" DataValueField="MST_NAME"
                                Height="200px" CssClass="SmallFont" EmptyText="Find" OnSelectedIndexChanged="cmbFind_SelectedIndexChanged"
                                MenuWidth="400px" Width="150px">
                                <HeaderTemplate>
                                    <div class="header c1">
                                        Master Name</div>                                  
                                    <div class="header c3">
                                        Description</div>
                                     
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_NAME") %>' /></div>
                                   
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
                          
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="30%">
                            Master Name:
                        </td>
                        <td align="left" valign="top" width="70%"  >
                            <asp:TextBox ID="txtMstName" runat="server" CssClass="TextBox" Width="148px" TabIndex="2"
                                 MaxLength="50" ></asp:TextBox>
                                 
                                 
                                 
                                 
                                   
                            <br />
                            
                            
                        </td>
                    </tr>
                    
             
                    
                    
                    <tr>
                        <td align="right" valign="top" width="30%" valign="middle">
                        <br /> 
                            Master&nbsp;Description:
                        </td>
                        
                        <td>
                        <asp:TextBox ID="txtMstDesc" runat="server" CssClass="TextBox" Width="150px" TabIndex="4"
                                Rows="2" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
  
    
  </table>
  </td></tr>
  
    </table>
    
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
