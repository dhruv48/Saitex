<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="UserMaster.aspx.cs" Inherits="Admin_UserMaster" Title="Create User" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="Obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
   
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
            width: 80px;
        }
        .c2
        {
            margin-left: 4px;
            width: 120px;
        }
        .c3
        {
            margin-left: 4px;
            width: 100px;
        }
        .c4
        {
            margin-left: 4px;
            width: 100px;
        }
    </style>
<%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
            <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                <tr>
                    <td class="td" align="left">
                        <table class="tContentArial" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td id="tdSave" width="48" runat="server">
                                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                                          onclientclick="if (!confirm('Are you Want to Save?')) { return false; }"  ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                                    </td>
                                    <td id="tdUpdate" width="48" runat="server">
                                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                          onclientclick="if (!confirm('Are you want to Update?')) { return false; }"  ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                                        </asp:ImageButton>
                                    </td>
                                    <%--  <td id="tdDelete" width="48" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" Visible="false" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" Width="48" Height="41" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')">
                                </asp:ImageButton>
                            </td>--%>
                                    <td id="tdFind" width="48" runat="server">
                                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                                              onclientclick="if (!confirm('Are you Want to find ?')) { return false; }" ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41"></asp:ImageButton>
                                    </td>
                                    <td width="48" id="tdClear" runat="server">
                                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                                             onclientclick="if (!confirm('Are you Want to Clear ?')) { return false; }" ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41"></asp:ImageButton>
                                    </td>
                                    <td width="48" id="tdprint" runat="server">
                                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                                             onclientclick="if (!confirm('Are you Want to Print From This Form ?')) { return false; }" ImageUrl="~/CommonImages/link_print.png" Width="48" Height="41"></asp:ImageButton>
                                    </td>
                                    <td width="48" id="tdExit" runat="server">
                                        <asp:ImageButton ID="imgbtnExit"  runat="server" ToolTip="Exit"
                                           
                                            onclientclick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }" 
                                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" 
                                            onclick="imgbtnExit_Click">
                                        </asp:ImageButton>
                                    </td>
                                <td width="48" id="tdHelp" runat="server">
                               <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help"
                                                ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41"></asp:ImageButton>
                               
                               </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader td" align="center">
                        <span class="titleheading">User Master</span>
                    </td>
                </tr>
                <tr >
                    <td class="td" >
                        <table class="tContentArial" cellspacing="0" cellpadding="0" width = "100%">
                             <tr >
                <td class="td" colspan ="6">
                    <span class="Mode">You are in <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                    <asp:Label ID="Label1" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="Label2" runat="server" CssClass="UserError"></asp:Label><strong>
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="M1" />
                    </strong>
                </td>
            </tr>
                           
                            <tr>
                                <td id="ValidationSummary2" align="center" runat="server" valign="top" visible="false"
                                    colspan="6">
                                    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="csslblMessage"></asp:Label>
                                    <asp:Label ID="lblErrorMessage" runat="server" Visible="false" CssClass="UserError"></asp:Label>
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="50%" ValidationGroup="M1"
                                        ShowMessageBox="True"></asp:ValidationSummary>--%>
                                </td>
                            </tr>
                            <tr>
                                
                                <td>
                                </td>
                                <td id="tdComboBoxID" align="Left" valign="top" runat="server">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="Right"  valign="top">
                                    *User Type
                                </td>
                                <td align="center" valign="top">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td align="left" valign="top" style="margin-left: 40px" colspan ="4">
                                  
                                    <asp:DropDownList ID="radUserType" runat="server" Width="155px" CssClass="tContentArial"
                                        AppendDataBoundItems="True" AutoPostBack="True" MenuWidth="200px">
                                        <asp:ListItem Value="0">------select--------</asp:ListItem>
                                        <asp:ListItem Value="AN">ADMIN</asp:ListItem>
                                        <asp:ListItem Value="EE">EMPLOYEE</asp:ListItem>
                                        <asp:ListItem Value="AT">FINANCIAL ACCOUNTING</asp:ListItem>
                                        <asp:ListItem Value="ST">STOCK</asp:ListItem>
                                        <asp:ListItem Value="PT">PARTY</asp:ListItem>
                                        <asp:ListItem Value="VD">VENDOR</asp:ListItem>
                                        <asp:ListItem Value="TP">TRANSPORTER</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                        ErrorMessage="Please select the User Type" ControlToValidate="radUserType" ValidationGroup="M1"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc2:ComboBox ID="ddlItemCode" runat="server" AutoPostBack="True" 
                                        CssClass="SmallFont" DataTextField="USER_CODE" DataValueField="USER_ID" 
                                        EmptyText="Find User" EnableLoadOnDemand="true" Height="200px" 
                                        MenuWidth="450px" OnLoadingItems="ddlItemCode_LoadingItems" 
                                        OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" TabIndex="1" 
                                        Width="156px">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                USER_ID</div>
                                            <div class="header c2">
                                                USER_CODE</div>
                                            <div class="header c3">
                                                USER_NAME</div>
                                            
                                            <div class="header c3">
                                                Type</div>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("USER_ID") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("USER_CODE") %>' />
                                            </div>
                                            <div class="item c3">
                                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("USER_NAME") %>' />
                                            </div>
                                            <div class="item c4">
                                                <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("USER_TYPE") %>' />
                                            </div>
                                            
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %> 
                                            out of <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="Right" valign="top">
                                    *User Code 
                                </td>
                    </td>
                               <td align="Center" valign="top">
                         <b>&nbsp;:&nbsp;</b>
                    </td>
                           <td>
                   <asp:TextBox ID="txtUserCode" runat="server" CssClass="TextBox  UpperCase SmallFont" 
                       MaxLength="10" TabIndex="2" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                            ErrorMessage="Please enter the User Code" ControlToValidate="txtUserCode" ValidationGroup="M1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                  <td align="Right"  valign="top">
                        *User Name
                    </td>
                    <td align="center" valign="top">
                         <b>&nbsp;:&nbsp;</b>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox SmallFont" Width="150px"
                            TabIndex="3" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" runat="server"
                            ErrorMessage="Please enter the User Name" ControlToValidate="txtUserName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
               
               
                </tr>
               
                <tr>
                    <td id="tdLoginId1" runat="server" align="Right"  valign="top">
                        *Login Id
                    </td>
                    <td id="tdLoginId2" runat="server" align="center" valign="top">
                        <b>&nbsp;:&nbsp;</b>
                    </td>
                    <td id="tdLoginId3" runat="server" align="left" valign="top">
                        <asp:TextBox ID="txtLoginId" runat="server" CssClass="TextBox" Width="150px" TabIndex="4"
                            MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                            ErrorMessage="Please enter the Login Id" ControlToValidate="txtLoginId" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="Right"  valign="top">
                        *Password
                    </td>
                    <td align="center" valign="top">
                        <b>&nbsp;:&nbsp;</b>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPassword" runat="server"  CssClass="TextBox" Width="150px" TabIndex="5"
                            TextMode="Password" MaxLength="10"></asp:TextBox>
                        <%--<asp:TextBox ID="txtPassword" runat="server" CssClass="TextBox" Width="150px" TabIndex="5"
                            TextMode="Password" MaxLength="10"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
                            ErrorMessage="Please enter the Password" ControlToValidate="txtPassword" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
               
                </tr>
              
                <tr>
                    <td align="Right"  valign="top">
                        &nbsp; Remarks
                    </td>
                    <td align="center" valign="top">
                        <b>&nbsp;:&nbsp;</b>
                    </td>
                    <td align="left" valign="top" style="width: 86%; height: 40px" colspan ="4">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" 
                            Width="517px" TextMode="MultiLine"
                            TabIndex="6" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="Right"  valign="top">
                        &nbsp; Status
                    </td>
                    <td align="Center"  valign="top">
                        <b>&nbsp;:&nbsp;</b>
                    </td>
                    <td align="left" valign="top" colspan ="4">
                        <asp:CheckBox ID="chk_Status" runat="server" TabIndex="7" />
                    </td>
                </tr>
            </table>
            </td> </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td align="left">
                                <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" 
                                    AllowFiltering="True" AutoGenerateColumns="False" AutoPostBackOnSelect="True" 
                                    OnSelect="Grid1_Select" PageSize="3">
                                    <Columns>
                                        <cc2:Column DataField="USER_ID" HeaderText="User Id" Visible="false" 
                                            Width="10px" Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="USER_TYPE" HeaderText="User Type" Width="140px" 
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="USER_CODE" HeaderText="User Code" Width="110px" 
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="USER_NAME" HeaderText="User Name" Width="110px" 
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="USER_LOG_ID" HeaderText="Login Id" Width="110px" 
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="USER_REMARKS" HeaderText="Remarks" Width="140px" 
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="STATUS" HeaderText="Status" Width="100px" Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="USER_PASS" HeaderText="User Pass" Visible="false" 
                                            Width="100px" Wrap="True">
                                        </cc2:Column>
                                    </Columns>
                                </cc2:Grid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
