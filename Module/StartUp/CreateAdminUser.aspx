<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/UserMaster.master" CodeFile="CreateAdminUser.aspx.cs" Inherits="Module_StartUp_CreateAdminUser" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="Obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
     <style type ="text/css" >
        .c1
        {
            width: 80px;
        }
        .c2
        {
            margin-left: 4px;
            width: 100px;
        }
        .c3
        {
            margin-left: 4px;
            width: 100px;
        } 
	
	</style>  
	<asp:ScriptManager ID="src" runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
       
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
                                       
                                    </td>
                                  
                                    <td id="tdFind" width="48" runat="server">
                                        </td>
                                    <td width="48" id="tdClear" runat="server">
                                         </td>
                                    <td width="48" id="tdprint" runat="server">
                                        </td>
                                    <td width="48" id="tdExit" runat="server">
                                       </td>
                                <td width="48" id="tdHelp" runat="server">                              
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
                                        AppendDataBoundItems="True" AutoPostBack="True" MenuWidth="200px" Enabled="false">
                                        <asp:ListItem Value="SA">SUPER ADMIN</asp:ListItem>                                       
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                        ErrorMessage="Please select the User Type" ControlToValidate="radUserType" ValidationGroup="M1"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            
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
                                   PageSize="3">
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
                    </ContentTemplate>
                    </asp:UpdatePanel>
    
</asp:Content>
