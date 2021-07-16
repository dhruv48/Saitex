<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/UserMaster.master" CodeFile="CreateGroup.aspx.cs" Inherits="Module_StartUp_CreateGroup" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content  ContentPlaceHolderID="cphBody" runat="server">
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
    
    
    <br /><br /><br />
    
    
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
                           
                        </td>
                        <td id="tdDelete" runat="server">
                           
                        </td >
                        <td id="tdFind" runat="server">
                           
                        </td>
                        <td id="tdClear" runat="server">
                           
                        </td>
                        <td id="tdPrint" runat="server">
                           
                        </td>
                        <td id="tdExit" runat="server">
                            </td>
                        <td id="tdHelp" runat="server">
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
                                
                                    <tr>
                                        <td align="center"  class="td">
                                            <cc2:Grid ID="gvGrpMaster" runat="server" AllowPaging="true" PageSize="10" CssClass="tContentArial tablebox"
                                                AllowSorting="true" AutoGenerateColumns="false" BorderWidth="1px" 
                                                OnPageIndexChanging="gvGrpMaster_PageIndexChanging" 
                                                >
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
    <br /><br /><br /> <br /><br /><br /> <br /><br /><br /><br />
</asp:Content>