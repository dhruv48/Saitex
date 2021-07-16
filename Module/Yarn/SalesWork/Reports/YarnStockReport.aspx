<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YarnStockReport.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_YarnStockReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

    <script language="javascript" type="text/javascript">
        function OpenReceiptDetails(BCODE, YCODE, TTYPE,YEAR) {
            //var YEAR = $('#<%= ddlYear.ClientID %>').val();
            var SDATE = $('#<%= TxtFromDate.ClientID %>').val();
            var EDATE = $('#<%= TxtToDate.ClientID%>').val();
            window.showModalDialog("RequestY.aspx?BCODE=" + BCODE + "&YCODE=" + YCODE + "&YEAR=" + YEAR + "&TRANS_TYPE=" + TTYPE + "&SDATE=" + SDATE + "&EDATE=" + EDATE, null, "dialogheight:600px; dialogWidth:800px; center:yes");
            // $(".viresh").colorbox({width:"90%", height:"90%", iframe:true,href:"ReceiptQ.aspx?BCODE="+BCODE+"&DCODE="+DCODE+"&YEAR="+YEAR+"&SDATE="+SDATE+"&EDATE="+EDATE});
        }

        function OpenIssueDetails(BCODE, YCODE, TTYPE,YEAR) {
            //var YEAR = $('#<%= ddlYear.ClientID %>').val();
            var SDATE = $('#<%= TxtFromDate.ClientID %>').val();
            var EDATE = $('#<%= TxtToDate.ClientID %>').val();

            window.showModalDialog("IssueY.aspx?BCODE=" + BCODE + "&YCODE=" + YCODE + "&YEAR=" + YEAR + "&TRANS_TYPE=" + TTYPE + "&SDATE=" + SDATE + "&EDATE=" + EDATE, null, "dialogheight:600px; dialogWidth:800px; center:yes");
            //$(".viresh").colorbox({width:"90%", height:"90%", iframe:true,href:"ReceiptQ.aspx?BCODE="+BCODE+"&DCODE="+DCODE+"&YEAR="+YEAR+"&SDATE="+SDATE+"&EDATE="+EDATE});
        }
    </script>

    
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
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
    .c6
    {
        margin-left: 4px;
        width: 80px;
    }
</style>

  <%--<asp:UpdatePanel ID="uppnl" runat="server">--%>
    <ContentTemplate>
<table align="left" class=" td tContentArial" width="945px">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" colspan="8">
            <span class="titleheading"><strong>YARN STOCK REPORT </strong></span>
        </td>
    </tr>
    <tr>
        <td align="right">
            Branch:
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px" AutoPostBack="True" 
                onselectedindexchanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
          <td style="text-align: right"  >
                        Year:
                    </td>
                    <td  >
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="165px" 
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
        <td class="tdRight">
            From date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                OnTextChanged="TxtFromDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
        </td>
        <td class="tdRight">
            To Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                OnTextChanged="TxtToDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Yarn:
        </td>
        <td class="tdLeft">
            <cc2:ComboBox ID="ddlYarn" runat="server" CssClass="SmallFont" EmptyText="------------All----------"
                EnableLoadOnDemand="True" Height="200px" MenuWidth="800px" OnLoadingItems="ddlYarn_LoadingItems"
                Width="161px">
                <HeaderTemplate>
                    <div class="header c2">
                        Yarn Code</div>
                    <div class="header c4">
                        Description</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c2">
                        <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                    </div>
                    <div class="item c4">
                        <asp:Literal ID="Container3" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
        </td>
        <td align="right">
            Yarn Category:
        </td>
        <td>
            <asp:DropDownList ID="ddlYarnCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px" DataTextField="YARN_CAT" DataValueField="YARN_CAT">
            </asp:DropDownList>
        </td>
        <td align="right">
            Yarn Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlYarnType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px">
            </asp:DropDownList>
        </td>
        <td align="right">
            Location:
        </td>
        <td>
            <asp:DropDownList ID="ddllocation" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px">
            </asp:DropDownList>
        </td>
        <tr>
        <td align="right">
            Store:
        </td>
        <td>
            <asp:DropDownList ID="ddlstore" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px">
            </asp:DropDownList>
        </td>
          <td align="center" >
          Party:  
        </td>
        <td>
        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"
                            EmptyText="Select Vendor" 
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="400px" Height="150px" 
                                   >
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
        </td>
        </tr>
        <td align="center" colspan="2">
            <asp:Button ID="btnGetReport" runat="server" Text="Get Report" 
                OnClick="btnGetReport_Click1" Visible="False" />
        </td>
    </tr>
    <tr>
        <td class="TdBackVir" colspan="8">
          <%--  <b>TotTotal Records : &nbsp;&nbsp;</b>--%><asp:Label ID="lblTotalRecord" 
                runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td tContentArial" colspan="8">
            <asp:Panel ID="pnlShowHover" runat="server" Height="350px" ScrollBars="Auto" Width="945px">
                <asp:GridView ID="GridLedger" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="3"  EmptyDataText="No Record Found"
                     ForeColor="#333333" PagerStyle-HorizontalAlign="Left" PageSize="25"
                     OnPageIndexChanging="GridLedger_PageIndexChanging1" Visible="False" Font-Size = "7">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns >
                        <asp:TemplateField HeaderText="SR.NO." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BRANCH ">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BRANCH_NAME")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YEAR">
                            <ItemTemplate>
                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("YEAR")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YEAR CODE">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" runat="server" Font-Bold="true" Text='<%#Eval("YARN_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" YARN DESCRIPTION">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" runat="server" Font-Bold="true" Text='<%#Eval("YARN_DESC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" runat="server" Text='<%#Eval("UOM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OPENING QTY.">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99FF66" Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblOpnBal" runat="server" Text='<%#Eval("OPBAL_STOK","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RECEIPT QTY.">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlReceiptQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                    NavigateUrl='<%# "javascript:OpenReceiptDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("YARN_CODE") + "&#39;,&#39;" + "R" + "&#39;,&#39;" + Eval("YEAR") + "&#39;);" %>'
                                    Text='<%#Eval("RECPT_QTY","{0:N3}").ToString() %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle BackColor="#99FF66" HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ISSUE QTY.">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlIssueQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                    NavigateUrl='<%# "javascript:OpenIssueDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("YARN_CODE") + "&#39;,&#39;" + "I" + "&#39;,&#39;" + Eval("YEAR") + "&#39;);" %>'
                                    Text='<%#Eval("ISSUE_QTY","{0:N3}").ToString() %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle BackColor="#99FF66" HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BALENCE QTY.">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99FF66" Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblClosingBal" runat="server" Text='<%#Eval("CLOSING_QTY","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OPENING VALUE">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99CCFF" Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblOpnBal" runat="server" Text='<%#Eval("OPBAL_VALUE","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RECEIPT VALUE">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlReceiptQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                    NavigateUrl='<%# "javascript:OpenReceiptDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("YARN_CODE") + "&#39;,&#39;" + "R" + "&#39;,&#39;" + Eval("YEAR") + "&#39;);" %>'
                                    Text='<%#Eval("RECPT_VALUE","{0:N3}").ToString() %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle BackColor="#99CCFF" HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ISSUE QTY.">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlIssueQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                    NavigateUrl='<%# "javascript:OpenIssueDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("YARN_CODE") + "&#39;,&#39;" + "I" + "&#39;,&#39;" + Eval("YEAR") + "&#39;);" %>'
                                    Text='<%#Eval("ISSUE_VALUE","{0:N3}").ToString() %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle BackColor="#99CCFF" HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CLOSING VALUE">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99CCFF" Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblClosingValue" runat="server" Text='<%#Eval("CLOSING_VALUE","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LAST RECEIPT DATE">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lbllastreceipt" runat="server" Text='<%#Eval("LRDATE")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                TargetControlID="TxtFromDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                TargetControlID="TxtToDate">
            </cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
    
  
  <td align="right">      
        </td>
        <td>
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt "  Visible ="false"
                Font-Size="9" Width="160px">
            </asp:DropDownList>
        </td></tr>
</table>
   </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>

