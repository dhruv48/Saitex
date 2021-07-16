<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fib_Quality_Shade_Group_Master.ascx.cs" Inherits="Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fib_Quality_Shade_Group_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

    <style type="text/css">
        .tdLeft
        {
            width: -110%;
        }
    </style>
    
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
        width: 130px;
    }
    .c2
    {
        margin-left: 4px;
        width: 500px;
    }
    .c3
    {
        margin-left: 4px;
        width: 550px;
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
    .style1
    {
        height: 207px;
    }
    </style>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 200px;
    }
    .c2
    {
        margin-left: 4px;
        width: 300px;
    }
    .c3
    {
        width: 550px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>

    <style type="text/css">
    .style1
    {
        height: 21px;
        color:White;
    }
</style>

    


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" cellpadding="0" width="70%" cellspacing="0" class="tContentArial">
            <tr>
                <td align="right" class="td" style="text-align: left" valign="top">
                    <table cellpadding="0" cellspacing="0" class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="ss" 
                                    TabIndex="1" />
                            </td>
                            <td id="tdFind" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgfind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgfind_Click" ToolTip="Find" ValidationGroup="M1" Width="48" 
                                    TabIndex="2" />
                            </td>
                            <td id="tdUpdate" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="ss" Width="48"
                                    
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to Update this Form')" 
                                    TabIndex="3" />
                            </td>
                            <td align="left" style="height: 46px" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" 
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to Clear this Form')" 
                                    TabIndex="4" />
                            </td>
                            <td align="left" style="width: 42px; height: 46px">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="43px" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48px" TabIndex="5" />
                            </td>
                            <td width="48">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" 
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to Exit from this Form')" 
                                    TabIndex="6" />
                            </td>
                            <td style="width: 48px">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" TabIndex="7" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <b class="tRowColorAdmin">Quality Shade Group Master</b>
                </td>
            </tr>
            <tr>
                <td align="tdLeft" class="td" style="height: 16px" valign="top">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server">
                        </asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="ss" />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                           
                              
                                            <td class="tdRight" width="15%">
                                                Fiber Code*
                                            </td>
                                            <td width="15%" valign="top">
                                            <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                                    DataTextField="FIBER_CODE" DataValueField="FIBER_DESC" EmptyText="Find Fiber Code" EnableLoadOnDemand="true"
                                                    Height="200px" MenuWidth="700" OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged1"
                                                    TabIndex="1"  Width="125px" Visible="False"  EnableVirtualScrolling="true">
                                                    <HeaderTemplate>
                                                        <div class="header c5">
                                                            Fiber Code</div>
                                                       
                                                        <div class="header c3">
                                                            Fiber Description</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c5">
                                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FIBER_CODE") %>' /></div>
                                                        <div class="item c3">
                                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FIBER_DESC") %>' /></div>
                                                        
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                                <asp:TextBox ID="txtYarnCode" runat="server" CssClass="SmallFont TextBoxNo TextBoxDisplay"
                                                    ReadOnly="True" Width="125" TabIndex="1"></asp:TextBox>
                                               <%-- <asp:DropDownList ID="ddlyarncode" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                 CssClass="SmallFont" TabIndex="4" Width="125" Visible="False" 
                                                 onselectedindexchanged="ddlyarncode_SelectedIndexChanged1"
                                            
                                                    >
                                                    
                                                </asp:DropDownList>--%>
                                        
                                                
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtYarnCode"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM12"></asp:RequiredFieldValidator>
                                            </td>
                                             <td class="tdRight" width="15%">
                                                Yarn&nbsp;Description*
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server"
                                                    ControlToValidate="txtYarnDescription" Display="Dynamic" ErrorMessage="*"
                                                    SetFocusOnError="True" ValidationGroup="YM" ></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="94%">
                                                <asp:TextBox ID="txtYarnDescription" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                                    MaxLength="50" TabIndex="6" Width="99%" ReadOnly="false" 
                                                   AutoPostBack="true" ></asp:TextBox> 
                                                   
                                               
                                            </td>
                                   
                       
                           
                            
                            <td align="right">
                              <%--  *Shade Family Code--%> *Shade Group :
                            </td>
                            <td align="left">
                                <asp:TextBox CssClass=" SmallFont" ID="txtShadeFamilycode" runat="server" AutoPostBack="false"
                                    Width="120px" MaxLength="3"   TabIndex="10"></asp:TextBox>
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        <tr>
                            <td align="right">
                                Remarks :
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox CssClass="SmallFont" Width="100%" ID="txtRemarks" runat="server" 
                                    MaxLength="50" TabIndex="12"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" width="100%">
                    <table width="70%">
                        <tr >
                            <td>
                                <asp:GridView ID="gvShadefamily" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="635px" OnPageIndexChanging="gvShadefamily_PageIndexChanging"
                                    OnRowCommand="gvShadefamily_RowCommand" OnSelectedIndexChanged="gvShadefamily_SelectedIndexChanged"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="FIBER_CODE" HeaderText="Quality Code" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FIBER_DESC" HeaderText="Quality Desc.." ItemStyle-HorizontalAlign="left"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHADE_GROUP" HeaderText="Shade  Group" ItemStyle-HorizontalAlign="center"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remakrs" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#336799" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShadeFamilycode"
            Display="None" ErrorMessage="Please Enter Shade Family Code" ValidationGroup="ss"></asp:RequiredFieldValidator>
       
    </ContentTemplate>
</asp:UpdatePanel>
