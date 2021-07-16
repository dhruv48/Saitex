<%@ Page Language="C#" MasterPageFile="~/CommonMaster/UserMaster.master" AutoEventWireup="true" CodeFile="LR_APPROVAL_REPORT.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Reports_LR_APPROVAL_REPORT" Title="Untitled Page" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">



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
                                           <%-- <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="8" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="9"  CausesValidation="false" 
                                                    onclick="imgbtnHelp_Click"/>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
               </tr>
                <tr>
                
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="4">
                                    <span style="font-size: 13pt" class="titleheading"><strong>LR Approval Print Report</strong> </span>
                                </td>
                            </tr>
                            <%--<tr>
                              <td >Business Type : 	</td>
                              <td>
                                 <asp:DropDownList ID="ddlBusinessType"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
                                    CssClass="SmallFont" Width="150px" TabIndex="1"/></td>
                                
                              <td > Report For:</td>
                              <td>  
                               
                                   <asp:DropDownList ID="ddlReportFor"  runat="server" AutoPostBack="true" 
                                    CssClass="SmallFont" Width="150px" TabIndex="2">
                                    
                                    <asp:ListItem>Customer Request For Yarn (Lab Dip)</asp:ListItem>
                                    <asp:ListItem>Lab Dip Submission & Recipe Entry</asp:ListItem>
                                    </asp:DropDownList></td>
                                
                            </tr>--%>
                            <%--<tr>
                             <td colspan="2">Request Type : </td>
                             <td colspan="2"><asp:DropDownList ID="ddlOrderType" runat="server" CssClass="SmallFont" 
                               
                                    AutoPostBack="True" AppendDataBoundItems="true"
                                    Width="150px" TabIndex="2" Visible="false"/>
                                
                                </td>
                            </tr>--%>
                            <tr>
                                
                                <td >
                                    Request No&nbsp; From:</td>
                                <td>
                                        <asp:TextBox ID="txtOrderNoFrom" runat="server" CssClass="TextBox TextBoxDisplay" 
                                             TabIndex="4" ValidationGroup="M1" Width="147px"></asp:TextBox>
                                </td>
                                <td >
                                  Request No&nbsp; To:</td>
                                <td align="left">
                               <asp:TextBox 
                                        ID="txtOrderNoTo" runat="server" CssClass="TextBox TextBoxDisplay" 
                                        TabIndex="5" ValidationGroup="M1" Width="147px"></asp:TextBox>
&nbsp;</td>
                            </tr>
                            <tr>
                            <td colspan="6" align="center">
                                 <cc1:oboutbutton ID="btnGetReport" runat="server" 
                                        Text="Get Report" TabIndex="6" onclick="btnGetReport_Click1" />
                                 
                            </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                           
                        </table>
                        
                    



</asp:Content>

