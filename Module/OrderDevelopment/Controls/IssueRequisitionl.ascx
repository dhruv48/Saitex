<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssueRequisitionl.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_IssueRequisitionl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
    
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
    }
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
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 250px;
    }
    .c6
    {
        margin-left: 4px;
        width: 100px;
    }
   
</style>
<table class="td  tContentArial">
    <tr>
        <td class="td  tContentArial" colspan="8">
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" Width="48" onclick="imgbtnNew_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" Width="48" onclick="imgbtnUpdate_Click" />
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                            ToolTip="Delete" Width="48" onclick="imgbtnDelete_Click" />
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Width="48" onclick="imgbtnFind_Click" />
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" onclick="imgbtnClear_Click" />
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" onclick="imgbtnExit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td align="center" valign="top" class="td" colspan="8">
            <span class="titleheading">Issue Requisition</span></td>
    </tr>
    <tr>
        <td colspan="8">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            Issu Req NO :
        </td>
        <td>
            <asp:TextBox ID="txtReqNo" runat="server" CssClass="TextBox"></asp:TextBox>      
        
        </td>
        <td colspan ="6"> 
            <cc2:ComboBox ID="ddlfind" runat="server"
                                    EnableLoadOnDemand="True"  
                                    AutoPostBack="True"  AutoValidate="True"
                                    EmptyText="Find" Width="165px" 
                AppendDataBoundItems="False" MenuWidth="550px"
                                    Height="250px" onloadingitems="ddlfind_LoadingItems" 
                onselectedindexchanged="ddlfind_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                          Issue Req NO.
                                        </div>
                                        <div class="header c4">
                                            Issue Req Date.
                                        </div>
                                         <div class="header c4">
                                           Lot No.
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("ISSUE_REQ_NO")%></div>
                                        <div class="item c5">
                                            <%# Eval("ISSUE_REQ_DATE")%></div>
                                            <div class="item c6">
                                            <%# Eval("LOT_NUMBER")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox></td>
    </tr>
    <tr>
        <td style="text-align: right">
            Issue Req Date :
        </td>
        <td>
            <asp:TextBox ID="txtReqDate" runat="server" CssClass="TextBox"></asp:TextBox>
        </td>
        <td style="text-align: right">
            LotNo:
        </td>
        <td>
            <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextBoxNo" 
                ></asp:TextBox>
        </td>
        <td style="text-align: right">
            Product type :
        </td>
        <td>
            <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" CssClass="tContentArial"
                OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td style="text-align: right">
            PA NO .:
        </td>
        <td>
            <asp:DropDownList ID="ddlPiNo" runat="server" CssClass="tContentArial" 
                OnSelectedIndexChanged="ddlPiNo_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            Remark :
        </td>
        <td colspan="7">
            <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <asp:GridView ID="grdBOM" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" ShowFooter="True" 
                Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Warp/Weft">
                        <ItemTemplate>
                            <asp:Label ID="txtBOMW_SIDE" runat="server" Text='<%# Bind("W_SIDE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductType">
                        <ItemTemplate>
                            <asp:Label ID="txtBaseArticalType" runat="server" Text='<%# Bind("BASE_ARTICAL_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Article Code">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_ARTICAL_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shade Code">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="txtShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BASE_SHADE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
              
                    <asp:TemplateField HeaderText="Required Qty" HeaderStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOMRequiredQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expected Qty" HeaderStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:TextBox ID="TxtExpectQty" runat="server" CssClass="TextBox" Width ="50px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="RowStyle SmallFont" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <HeaderStyle BackColor="#336699" CssClass="HeaderStyle " ForeColor="White" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <cc1:CalendarExtender ID="calRd" Format="dd/MM/yyyy" TargetControlID="txtReqDate"
                runat="server">
            </cc1:CalendarExtender>
        </td>
    </tr>
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>
--%>