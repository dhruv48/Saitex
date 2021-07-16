<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Slips_Report.aspx.cs" Inherits="Module_Production_Pages_Slips_Report" Title="Untitled Page" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
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
        width: 120px;
    }
    .c2
    {
        margin-left: 10px;
        width: 70px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        width: 190px;
    }
    .c5
    {
        margin-left: 4px;
        width: 320px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<table align="left" class="tContentArial" border = "1" >
               <tr>
                <td align="left" class="td" valign="top">
                                    <table>
                                        <tr>
                                            
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }"
                                                    ToolTip="Clear" TabIndex="7" CausesValidation="false"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="8" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="9"  CausesValidation="false" 
                                                    onclick="imgbtnHelp_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
               </tr>
                <tr>
                
                    <td align="center" class="td" width="100%">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="4">
                                    <span style="font-size: 13pt" class="titleheading"><strong>Slip Reports</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="7%">
                                    <asp:Label ID="lblPaNo" runat="server" Text="Select Job Card No:"></asp:Label>
                                </td>
                                <td class="tdLeft" width="7%">
                                    <cc2:ComboBox ID="cmbJobCard" runat="server" TabIndex="1" Width="200 px" MenuWidth="200"
                                        AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                        Height="200px" EnableVirtualScrolling="true" OnLoadingItems="cmbJobCard_LoadingItems"
                                         EmptyText="Select Job Card" 
                                        onselectedindexchanged="cmbJobCard_SelectedIndexChanged">
                                        <HeaderTemplate>
                                            
                                             <div class="header c2">
                                                Job Card No
                                            </div>
                                             <div class="header c2">
                                               Batch No
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <div class="item c2">
                                                <%# Eval("BATCH_CODE")%></div>
                                             
                                             <div class="item c2">
                                                <%# Eval("BATCH_ISSUE_NO")%></div>
                                             </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                           </tr>
                               
                               
                           <tr>
                           <td width="40%" align="right"><asp:Label ID="Label3" runat="server" Text="Party :"></asp:Label></td>
                           <td width="60%" align="left">
                                <asp:TextBox ID="txtParty" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="100%"
                                ReadOnly="true" runat="server"></asp:TextBox>
                           </td>
                           </tr>
                               
                           <tr>
                           <td width="40%" align="right"><asp:Label ID="Label1" runat="server" Text="Quality :"></asp:Label></td>
                           <td width="60%" align="left">
                                <asp:TextBox ID="txtDisQuality" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="100%"
                                ReadOnly="true" runat="server"></asp:TextBox>
                           </td>
                           </tr>
                           
                           
                           <tr>
                           <td width="40%" align="right"><asp:Label ID="Label2" runat="server" Text="Shade :"></asp:Label></td>
                           <td width="60%" align="left">
                                <asp:TextBox ID="txtShade" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="50%"
                                ReadOnly="true" runat="server"></asp:TextBox>
                           </td>
                           </tr>
                           
                           <tr>
                           <td width="40%" align="right"><asp:Label ID="Label4" runat="server" Text="Party Ref. No :"></asp:Label></td>
                           <td width="60%" align="left">
                                <asp:TextBox ID="txtRefNo" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="50%"
                                ReadOnly="true" runat="server"></asp:TextBox>
                           </td>
                           </tr>
                           
                           
                           <tr>
                           <td width="40%" align="right"><asp:Label ID="Label5" runat="server" Text="No of Prints :"></asp:Label></td>
                           <td width="60%" align="left">
                                <asp:TextBox ID="txtNoOfSlip" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="50%"
                                 runat="server"></asp:TextBox>
                           </td>
                           </tr>
                           
                           
                         
                                        
                                        
                          <tr>
                          
                           <td align="center" colspan="2">
                                 <cc3:OboutButton ID="btnGetReport" runat="server" 
                                        Text="Get Report" TabIndex="6" onclick="btnGetReport_Click"  />
                                        
                                 <cc3:OboutButton ID="btnCancel" runat="server" 
                                        Text="Cancel" TabIndex="7" onclick="btnCancel_Click"  />
                           </td>
                           </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>

</ContentTemplate></asp:UpdatePanel>

</asp:Content>

