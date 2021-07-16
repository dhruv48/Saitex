<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="InvoiceReportCRPrint.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_InvoiceReportCRPrint"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:Panel ID="pnlItemMst" runat="server">
    
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" ><ContentTemplate>--%>
         <table>
            <tr>
                <td class="td">
                    <table align="left">
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" Width="48" OnClick="imgbtnPrint_Click1"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Height="41" Width="48" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">Invoice Report Print</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr id="trRange" runat="server">
                           <%-- <td>
                                Invoice Type:
                            </td>--%>
                            <td>
                                <asp:DropDownList Width="140px" ID="ddlInvoiceType" Visible="true" 
                                    runat="server" onselectedindexchanged="ddlInvoiceType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Text="Sale Work" Value="SALEWORK"></asp:ListItem>
                                    <asp:ListItem Text="Job Work" Value="JOBWORK"></asp:ListItem>
                                    <asp:ListItem Text="FROM STOCK" Value="FROM STOCK"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                From:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" Width="140px" runat="server" 
                                    CssClass="SmallFont TextBoxNo" AutoPostBack="true" ontextchanged="txtFrom_TextChanged">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtFrom"
                                    Display="Dynamic" ErrorMessage="Numeric value required" MaximumValue="9999999"
                                    MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom"
                                    Display="Dynamic" ErrorMessage="Provede starting No." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" Width="140px" runat="server" CssClass="SmallFont TextBoxNo"  Enabled="false" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo"
                                    Display="Dynamic" ErrorMessage="Provede ending No." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtTo"
                                    Display="Dynamic" ErrorMessage="Numeric value required" MaximumValue="9999999"
                                    MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        
                         <tr>
                        <td colspan="4" align="center">
                            <asp:Panel ID="pnlCheck" runat="server" BorderColor="White" BorderWidth="1px">
                                <asp:CheckBoxList ID="chkLstInvoiceType" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="4" Font-Size="X-Small" AutoPostBack="true">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                    </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
       <%-- 
        </ContentTemplate></asp:UpdatePanel>--%>
    </asp:Panel>
</asp:Content>
