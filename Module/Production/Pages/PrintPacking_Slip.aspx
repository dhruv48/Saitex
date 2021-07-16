<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CommonMaster/admin.master" CodeFile="PrintPacking_Slip.aspx.cs" Inherits="Module_Production_Pages_PrintPacking_Slip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
 <asp:UpdatePanel ID="upnl" runat="server">
   <ContentTemplate>  
    <asp:Panel ID="pnlItemMst" runat="server">
        <table>
            <tr>
             <td class="td">
                    <table align="left">
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">Packing Slip</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                <tr id="trRange" runat="server">
                <td>
                CARTON NO. :
                </td>
                <td>      
            <asp:TextBox ID="txtCartonNo" runat="server"  Width="125px" CssClass="SmallFont TextBoxNo"></asp:TextBox>    
            </td>
            <td>
               
            </td>
            <td>
            <asp:Button ID="btnCartonNo" runat="server" Text="Print Slip" Width="70px" 
                    OnClick="btnCartonNo_Click" CssClass="AButton" /></td>        
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
