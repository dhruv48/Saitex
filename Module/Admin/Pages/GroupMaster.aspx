<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="GroupMaster.aspx.cs" Inherits="Admin_GroupMaster" Title="Group Master" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>


   

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial ">
        <tr>
            <td class="td">
                <table cellpadding="0" cellspacing="0" align="Left">
                    <tr>
                      
                        <td id="tdSave" runat="server">
                            <asp:ImageButton ID="imgbtnSave" runat="server" Width="48"  Height="41" ToolTip="Save"
                                ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" 
                                OnClick="imgbtnSave_Click" BorderWidth="0px" OnClientClick ="(if(!confirm('Are You want to Save Data')){return false ;}" />
                        </td>                      
                        <td id="tdUpdate" runat="server">
                            <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" onclientclick="if (!confirm('Are you want to Update?')) { return false; }"

                                ValidationGroup="M1" BorderWidth="0px">
                            </asp:ImageButton>
                        </td>
                        <td id="tdDelete" runat="server">
                            <asp:ImageButton ID="imgbtnDelete" Width="48"  Height="41" runat="server" ToolTip="Delete"
                                ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click" onclientclick="if (!confirm('Are you sure to delete ?')) { return false; }"
 
                                BorderWidth="0px"></asp:ImageButton>
                        </td >
                        <td id="tdFind" runat="server">
                            <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click" 
                                BorderWidth="0px"></asp:ImageButton>
                        </td>
                        <td id="tdClear" runat="server">
                            <asp:ImageButton ID="imgbtnClear" runat="server"  Height="41" ToolTip="Clear"
                                ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" onclientclick="if (!confirm('Are you want to Clear  ?')) { return false; }"

                                BorderWidth="0px"></asp:ImageButton>
                        </td>
                        <td id="tdPrint" runat="server">
                            <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" onclientclick="if (!confirm('Are you want to Print ?')) { return false; }"

                                BorderWidth="0px"></asp:ImageButton>
                        </td>
                        <td id="tdExit" runat="server">
                            <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" onclientclick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"></asp:ImageButton>
                        </td>
                        <td id="tdHelp" runat="server">
                            <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                        </td>
                   
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                    <tr>
                        <td align="center" class="TableHeader td" colspan="2">
                            <span class="titleheading">Group Master</span>
                        </td>
                    </tr>
                   
                    <tr>
                   <td  align ="left" class ="td" >
                      <span class="Mode">You are in <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                           
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                           ValidationGroup="M1" ShowMessageBox="True" ShowSummary="False" />
                           
                            </td>
                    </tr>
                
                    <tr>
                        <td class="td">
                            <table align="left" width ="100%" >
                                <tr>
                                    <td align="right" valign="top" width="90px">
                                        Group&nbsp; Code :
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtGroupCode" runat="server" CssClass="TextBox  UpperCase" 
                                            Width="150px" TabIndex="1"></asp:TextBox>
                                        <cc2:ComboBox ID="ddlfind" runat="server" DataTextField="GRP_NAME" DataValueField="GRP_CODE"
                                    EnableLoadOnDemand="True" TabIndex="1" OnLoadingItems="ddlfind_LoadingItems"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlfind_SelectedIndexChanged1" AutoValidate="True"
                                    EmptyText="Select Group" Width="152px" AppendDataBoundItems="False" 
                                            MenuWidth="230px" Height="250px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c2">
                                               Name
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("GRP_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("GRP_NAME")%></div>
                                    </ItemTemplate>
                                  <%--  <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>--%>
                                </cc2:ComboBox>
                                        
                                       
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="txtGroupCode" 
                                            ErrorMessage="*Pls Enter Group Code" ValidationGroup="M1" 
                                            Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="90px">
                                        *Group Name :
                                    </td>
                                    <td align="left" valign="top" class="style1">
                                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="TextBox "
                                            ValidationGroup="M1" Width="150px" TabIndex="2" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="txtGroupName" 
                                            ErrorMessage="*Pls Enter Group Name" Display="Dynamic" 
                                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                        </td>
                                    
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="90px">
                                        Remarks :                                     </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" 
                                            Width="300px" TabIndex="3"
                                            Rows="2" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="90px">
                                        Status :
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:CheckBox ID="chk_Status" runat="server" TabIndex="3" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" valign="top">
                            <%# Eval("GRP_NAME")%>
                            <table class="tContentArial" cellspacing="0" cellpadding="0" align="center"
                                border="0">
                                <tbody>
                                  <%--  <%# Container.ItemsCount > 0 ? "1" : "0" %>--%>
                                    <tr>
                                        <td align="center"  class="td">
                                            <cc2:Grid ID="gvGrpMaster" runat="server" AllowPaging="true" PageSize="10" CssClass="tContentArial tablebox"
                                                AllowSorting="true" AutoGenerateColumns="false" BorderWidth="1px" OnSelectedIndexChanged="gvGrpMaster_SelectedIndexChanged"
                                                OnPageIndexChanging="gvGrpMaster_PageIndexChanging" 
                                                onselect="gvGrpMaster_Select" >
                                                <Columns>
                                                    <cc2:Column DataField="GRP_CODE" HeaderText="Group Code" ItemStyle-VerticalAlign="top"
                                                         ItemStyle-HorizontalAlign="left" >
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ></ItemStyle>
                                                    </cc2:Column>
                                                    <cc2:Column DataField="GRP_NAME" HeaderText="Group Name" ItemStyle-VerticalAlign="top"
                                                        ItemStyle-HorizontalAlign="left" >
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ></ItemStyle>
                                                    </cc2:Column>
                                                    <cc2:Column DataField="GRP_DESC" HeaderText="Remarks" ItemStyle-VerticalAlign="top"
                                                  ItemStyle-HorizontalAlign="center" >
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" ></ItemStyle>
                                                    </cc2:Column>
                                                </Columns>
                                            </cc2:Grid>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                      <%--      <%# Container.ItemsLoadedCount %>--%>
                        </td>
                    </tr>
                </table>
   </td>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="Head1">

    <style type="text/css">
   
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
   
        
   
    </style>

</asp:Content>

