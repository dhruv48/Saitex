<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="Purchase_Register.aspx.cs" Inherits="Module_Inventory_Reports_Purchase_Register" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
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


body
  {
	padding-top: 0px;
	top: 0px;
	background-color: #FFFFFF;
  }


.td
{
	border-style: ridge;
	border-bottom-width:.5px;
	border-color: #C1D3FB;
}
.titleheading
{
	font-family: arial;
	font-size: 12px;
	font-weight: bold;
	color: #ffffff;
}
.Label
{
	font-family: Arial, Arial, Helvetica, sans-serif;
	font-size: 12px;
	vertical-align: top;
	text-align: left;
}
.LabelNo
{
	font-family: Arial, Arial, Helvetica, sans-serif;
	font-size: 12px;
	text-align: right;
	vertical-align: top;
}
.tContentArial
{
	font-family: Arial;
	font-size: 12px;
}

.tablebox
{
	border: 1px solid #336799;
}

    </style>
<table border="1" cellpadding="3" cellspacing="0" width="60%" class="tContent">
<tr>
            <td width="60%" align="center" class="TableHeader" colspan="2"><b class="titleheading">Material Purchase Register </b> </td>
         </tr>
<tr>
<td width="10%" style="width: 15%">
    Select Month :
    </td>
    <td style="width: 20%">
    <asp:DropDownList ID="ddlMonth" runat="server" Width="160px" CssClass="gCtrTxt" ValidationGroup="M1">
                                <asp:ListItem  Value="" Text="-----All----"></asp:ListItem>
                                <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                <asp:ListItem Value="12" Text="December"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            
    </td>
    <tr>
    <td style="width: 15%">
    Select Year :
    </td>
    <td style="width: 20%">
    <asp:DropDownList ID="ddlYear" runat="server" Width="160px" CssClass="gCtrTxt" >
                            </asp:DropDownList>
    </tr>
    </tr>
    <tr>
    <td style="width:15%">
    Select Transaction Type: 
    </td>
    <td style="width:20%">
    <asp:DropDownList ID="ddlTrntype" runat="server" Width="300px" CssClass="gCtrTxt" > </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td colspan="2" align="center" valign="top" style="height:25px;">
    <asp:RadioButtonList ID="selReport" runat="server"  RepeatColumns="2"
                                                    RepeatDirection="Horizontal" TabIndex="15" Height="11px">
                                                    <asp:ListItem Value="1" Selected="True">MRN Wise Purchase Register</asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="False">Cost Center Wise Purchase Register</asp:ListItem>
                                                </asp:RadioButtonList>
    </td>
    </tr>
    <tr>
                 <td colspan="2" align="center" valign="top" style="height: 25px;">
                       
                <asp:Button ID="btnView" Text="View" runat="server" Width="75" OnClick="btnView_Click"/>
    </tr>
</table>

</asp:Content>

