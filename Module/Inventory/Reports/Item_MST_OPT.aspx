<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/CommonMaster/UserMaster.master" CodeFile="Item_MST_OPT.aspx.cs" Inherits="Module_Inventory_Reports_Item_MST_OPT" Title="Untitled Page" %>



<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<%--<html>
<head>
<title>Item Master Report</title>
    <style type="text/css">


.TableHeader
{
	color: #000000;
	font-weight: bold;
	background-color: #336799;
	font-family: Verdana,arial;
	text-decoration: none;
	font-size: 13px;
	text-align: center;
	height: 19px;
}

.titleheading
{
	font-family: arial;
	font-size: 12px;
	font-weight: bold;
	color: #ffffff;
}
.tContentArial
{
	font-family: Arial;
	font-size: 11px;
}
    </style>
</head>
<body style="background-color:#c1d3fb";>
<form runat=server id="f" >



<asp:ScriptManager ID="scrid1" runat="server"></asp:ScriptManager>
  <asp:UpdatePanel ID="upnl" runat="server">--%> 
   <%--<ContentTemplate> --%>
   
   
            <table align="left" class="tContentArial">
                <tr>
        <td class="td">
            <table>
                
                <tr>
                  
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" 
                            ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
                            CausesValidation="false" OnClick="imgbtnClear_Click" 
                            OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" 
                            TabIndex="54" />
                        <asp:ImageButton ID="imgbtnExit" runat="server" CausesValidation="false" 
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" 
                            OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }" 
                            TabIndex="55" ToolTip="Exit" />
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_help.png" onclick="imgbtnHelp_Click" 
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
                <tr>
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="6">
                                    <span class="titleheading" style="font-size: 13pt"><strong>Item Master report</strong>
                                    </span>
                                </td>
                            </tr>
                             <tr>
        <td  align ="right">
            Select Branch:
        </td>
        <td >
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
               Width="160px" >
            </asp:DropDownList>
        </td>
       
        
        <td align ="right">
            Select Department:
        </td>
                                 <td>
            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
                 Width="160px" >
            </asp:DropDownList>
                                 </td>
                                 <td>
                                     Consume</td>
        <td >
                                    <asp:DropDownList ID="ddlCONSUME" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px"> 
                                        
                
                                    </asp:DropDownList>
        </td>
   </tr>
   <tr>
  
        <td align ="right">
           Item Category:
        </td>
        <td >
            <asp:DropDownList ID="ddlItemCate" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
                 Width="160px" >
            </asp:DropDownList>
        </td>
        <td  align ="right">
          Item Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
               Width="160px" >
            </asp:DropDownList>
        </td>
        <td>
            Is Excisable</td>
        <td >
                                    <asp:DropDownList ID="ddlISEXCISABLE" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px">
                                    </asp:DropDownList>
        </td>
   
    </tr>
                            <tr>
                                <td>
                                    Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlSTATUS" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px" >
                                       
                                        
                                    </asp:DropDownList>
                                </td>   
                                <td align="right">
                                    Approved/Rejected
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px" >
                                       <asp:ListItem Value="1" Selected="True">Approved</asp:ListItem>
                                       <asp:ListItem Value="3">Rejected</asp:ListItem>
                                        <asp:ListItem  Value="0">Pending</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" CssClass="AButton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Content>
            
     <%-- </ContentTemplate>--%>
  <%-- </asp:UpdatePanel>
   
   </form>
</body>
</html>--%>