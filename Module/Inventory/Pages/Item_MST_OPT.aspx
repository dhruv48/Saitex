<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Item_MST_OPT.aspx.cs" Inherits="Inventory_Item_MST_OPT" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Master Option</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
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
            width: 80px;
        }
        .c2
        {
            margin-left: 4px;
            width: 100px;
        }
        .c3
        {
            margin-left: 4px;
            width: 100px;
        }
    </style>
</head>
<body style="background-color: #afcae4;">
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" BackColor="#afcae4" runat="server">
            <table align="left" class="tContentArial">
                <tr>
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="2">
                                    <span style="font-size: 13pt" class="titleheading"><strong>Item Master report</strong>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Category Code
                                </td>
                                <td align="left">
                                        
                                    <cc1:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" MenuWidth="250px"
                                        Width="150px" Height="180px" EmptyText="Select Item Category..." EnableLoadOnDemand="true"
                                        OnLoadingItems="ddlItemCategory_LoadingItems" TabIndex="2" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Department Code
                                </td>
                                <td align="left">
                                    <cc1:ComboBox runat="server" ID="ddlDepartment" CssClass="SmallFont" MenuWidth="250px"
                                        Width="150px" Height="180px" EmptyText="Select Department..." EnableLoadOnDemand="true"
                                        TabIndex="2" onloadingitems="ddlDepartment_LoadingItems" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
