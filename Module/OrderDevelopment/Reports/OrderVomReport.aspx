<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderVomReport.aspx.cs" Inherits="Module_OrderDevelopment_Reports_OrderVomReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="left" class=" td tContentArial" >
    <tr>
        <td class="td" colspan="4">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            <table >
                <tr>
                    <td align="center" class="TableHeader td" colspan="4">
                        <span class="titleheading"><strong>Yarn Bom Report</strong> </span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        PI No.:
                    </td>
                    <td>
                      
                        <%--<asp:DropDownList ID="ddlBaseArtyCode" runat="server" Width="150px">
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txtPiNo" runat="server">All </asp:TextBox>
                      
                    </td>
                    <td class="tdRight">
                     Base Article Code :
                        <%--<asp:DropDownList ID="ddlBaseArtyCode" runat="server" Width="150px">
                        </asp:DropDownList>--%>
                     </td>
                    <td class="tdLeft">
                        
                        <asp:TextBox ID="txtBaseArtCode" runat="server">All</asp:TextBox>
                        
                    </td>
                  
                </tr>
              
                </table>
        </td>
    </tr>
</table>
</asp:Content>

