<%@ Page Language="C#"   MasterPageFile="~/CommonMaster/admin.master"   AutoEventWireup="true" CodeFile="SalesRequestForWaste.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Reports_SalesRequestForWaste" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
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
            margin-left: 4px;
            width: 250px;
        }
        .c3
        {
            margin-left: 4px;
            width: 80px;
        }
        .style1
        {
            width: 5%;
        }
        .style2
        {
            text-align: right;
        }
        .d1
        {
            width: 150px;
        }
        .d2
        {
            margin-left: 4px;
            width: 350px;
        }
        .d3
        {
            width: 80px;
        }
    </style>
    
       
    
    <table cellpadding="3" cellspacing="0" width="100%" class="tContentArial td">
                
                <tr>
                    <td align="left" colspan="8" width="100%" class="td">                       
                       
                        
                          <table class="style1" cellspacing="0" cellpadding="0" border="0" align="left">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                            ToolTip="Help" Width="48" />
                                    </td>
                                    <td width="41">
                                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                            Width="41" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                            Width="41" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                    </td>
                                    
                                </tr>
                            </tbody>
                        </table>
                        
                       
                   
                    </td>
                </tr>
                
             
                
                <tr>
                    <td align="center" class="TableHeader" colspan="8" width="100%">
                        <b class="titleheading">Sale Request Report</b>
                    </td>
                </tr>
                
                <tr>
                    <td style="text-align: right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right">
                        Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="165px" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right">
                        From Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate1" runat="server" AutoPostBack="True" OnTextChanged="txtDate1_TextChanged"
                            Width="165px" CssClass="tdText"></asp:TextBox>
                    </td>
                    <td style="text-align: right">
                        To Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate2" runat="server" AutoPostBack="True" OnTextChanged="txtDate2_TextChanged"
                            Width="165px" CssClass="tdText"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>  
                <td style="text-align: right">CR&nbsp;No</td>  
                <td><asp:TextBox ID="txtInvoiceTo" runat="server" Text="" CssClass="tdText"></asp:TextBox></td>   <td style="text-align: right">To</td>    
             
                <td><asp:TextBox ID="txtInvoiceFrom" runat="server" Text="" CssClass="tdText"></asp:TextBox></td>
                    <td></td>
                <td></td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnView" runat="server" CssClass="AButton"    Text="Report"  
                            Width="75" onclick="btnView_Click" />
                         
                    </td>
                    
                </tr>   
                
                <tr>
                        <td colspan="4" align="center">
                            <asp:Panel ID="pnlCheck" runat="server" BorderColor="White" BorderWidth="1px">
                                <asp:CheckBoxList ID="chkLstInvoiceType" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="4" Font-Size="X-Small">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                    </tr>           
                   
                   
                       
                        <cc1:CalendarExtender ID="TxtIndentDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate1">
                        </cc1:CalendarExtender>
                        
                        <cc1:CalendarExtender ID="TxtIndentDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate2">
                        </cc1:CalendarExtender>
                        
                    
                   
                
            </table>
                                
</asp:Content>
