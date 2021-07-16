<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRApproval4YarnSpning.aspx.cs" Inherits="Module_OrderDevelopment_Reports_CRApproval4YarnSpning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table class="tContentArial">
<tr>
<td>
<table align="left">
                <tbody>
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
                </tbody>
            </table>
            </td>
            </tr>
            <tr>
            <td class="TableHeader" class="td" align="center" width="100%">
            <b class="titleheading">Print Sales Contract Approval Report</b>
           </td>
            </tr>
            <tr>
            <td>
            <table>
            <tr>
            <td class="tdRight">
            Order&nbsp;No:
            </td>
            <td class="tdLeft">
            <asp:TextBox  ID="txtOrderno" CssClass="textboxno SmallFont" runat="server" Width="150px"></asp:TextBox>
            </td >
            <td class="tdRight SmallFont" >
            Business&nbsp;Type:
            </td>
            <td>
            <asp:TextBox ID="txtBusinessType" runat="server" CssClass="textboxno SmallFont" Width="150px"></asp:TextBox>
            </td>
            <td class="tdLeft SmallFont" >
            Artical&nbsp;No:
            </td>
            <td class="tdRight" >
           <asp:TextBox ID="txtArticleNo" runat="server" CssClass="textboxno TextBox SmallFont" Width="150px"></asp:TextBox>
            </td>
            
            </tr>
            <tr>
            <td class="tdLeft SmallFont">
              BRANCH:
            </td>
            <td class="tdRight" >
           <asp:DropDownList runat="server" id="ddlBranch"  CssClass="SmallFont TextBox UpperCase BoldFont" Width="150px"></asp:DropDownList>
            </td>
            <td class="tdLeft SmallFont">
            PRODUCT&nbsp;TYPE:
            </td>
            <td>
            <asp:DropDownList ID="ddlProductType" CssClass="SmallFont TextBox UpperCase BoldFont"
                            runat="server" Width="150px">
                        </asp:DropDownList>
            </td>
            </tr>
            </table>
            </td>
            </tr>
  </table>  
</asp:Content>

