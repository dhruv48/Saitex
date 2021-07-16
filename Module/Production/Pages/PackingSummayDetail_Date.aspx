<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="PackingSummayDetail_Date.aspx.cs" Inherits="Module_Production_Pages_PackingSummayDetail_Date" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
                            </td><td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 



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
                    <span class="titleheading">Packing Summary Form</span>
                </td>
            </tr>
            <tr>
                <td class="td">
        <table>
                <tr id="trRange" runat="server">
                    <td>
                     Date From
                    </td>
                    <td>
              <asp:TextBox ID="txtPackingDateFrom" runat="server" TabIndex="2" ValidationGroup="M1" Width="145px"
                    CssClass="TextBox SmallFont"></asp:TextBox>
  <cc4:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPackingDateFrom"> </cc4:CalendarExtender>                
                    </td>
                    <td>
                    Date To
                    </td>
                    <td>
         <asp:TextBox ID="txtPackingDateTo" runat="server" TabIndex="2" ValidationGroup="M1" Width="145px"
                    CssClass="TextBox SmallFont"></asp:TextBox>
  <cc4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPackingDateTo"> </cc4:CalendarExtender>           
                    </td>
                </tr>
            </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" BackColor="#99CCFF">
                            <asp:RadioButtonList ID="redForQuery" runat="server" Height="16px" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Red" Value="red" Selected="True">Yarn Wise Packing Summary</asp:ListItem>
                                <asp:ListItem  Text="Green" Value="green">Lot Wise Packing Summary</asp:ListItem>
                                 <asp:ListItem  Text="Blue" Value="blue">Lot Wise Packing Details</asp:ListItem>
                               
                                 
                                
                            </asp:RadioButtonList>
                        </asp:Panel>
    </asp:Panel>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="imgBtnExportExcel" />
    
    </Triggers>
   </asp:UpdatePanel>
</asp:Content>