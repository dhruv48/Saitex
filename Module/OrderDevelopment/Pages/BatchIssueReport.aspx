<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BatchIssueReport.aspx.cs" Inherits="Module_OrderDevelopment_Pages_BatchIssueReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table class="td tContentArial" width="95%">
            <tr>
                <td class="td tContentArial">
                    <table>
                        <tr>
                           
                           
                  <td id="tdPrint" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnPrint_Click1" />
                            </td>
                 <td id="tdCLEAR" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>

                           
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading"><b>Batch Issue Report</b></span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                       
                        <tr>
                            <td align="right" valign="top">
                                From Date : &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TxtFdate" runat="server" CssClass="TextBox" TabIndex="5"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                To Date:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TxtTdate" runat="server" CssClass="TextBox" TabIndex="4"></asp:TextBox>
                            </td>
                            <td align="right">
                                Machine:
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlmachine" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    TabIndex="3" Width="120px" >
                                   
                                </asp:DropDownList>
                                <asp:Button ID="btnview" runat="server" VISIBLE="false" CssClass="SmallFont" OnClick="btnview_Click"
                                    Text="Show" />
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            
            <tr>
               <%-- <td class="td" width="100%" valign="top" align="center">
                   
                         <b style="font-size:larger;">Total Records :
                       <asp:Label ID="lblTotalRecord" runat="server" Text=""></asp:Label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Refresh Grid Data" Width="100px"
                        CssClass="SmallFont" />  </td>--%>
                
            </tr>
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                        <span class="titleheading"><b>
                        
                            
                            
                      
                        </b></span>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="TxtFdate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="TxtTdate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="TxtTdate" PromptCharacter="_">
        </cc1:MaskedEditExtender>

</asp:Content>

