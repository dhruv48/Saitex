<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true"
    CodeFile="WasteStockQuery.aspx.cs" Inherits="Module_Waste_Queries_WasteStockQuery"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <script language="javascript" type="text/javascript">

        function ShowOther(OtherID) {
            window.showModalDialog('ReceiptQ.aspx?OtherID=' + OtherID, 'dialogHeight:600px;dialogWidth:600px;')
            $(".viresh").colorbox({ width: "90%", height: "90%", iframe: true, href: "ReceiptQ.aspx?BCODE=" + BCODE + "&DCODE=" + DCODE + "&YEAR=" + YEAR });
            return false;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function OpenReceiptDetails(BCODE, ICODE, TTYPE) {

            var YEAR = $("#<%=ddlYear.ClientID%> option:selected").text();
            var SDATE = $('#<%= txtDate1.ClientID %>').val();
            var EDATE = $('#<%= txtDate2.ClientID%>').val();
            window.showModalDialog("ReceiptQ.aspx?BCODE=" + BCODE + "&ICODE=" + ICODE + "&YEAR=" + YEAR + "&TRANS_TYPE=" + TTYPE + "&SDATE=" + SDATE + "&EDATE=" + EDATE, null, "dialogheight:600px; dialogWidth:850px; center:yes");
            // $(".viresh").colorbox({width:"90%", height:"90%", iframe:true,href:"ReceiptQ.aspx?BCODE="+BCODE+"&DCODE="+DCODE+"&YEAR="+YEAR+"&SDATE="+SDATE+"&EDATE="+EDATE});
        }

        function OpenIssueDetails(BCODE, ICODE, TTYPE) {
            var YEAR = $("#<%=ddlYear.ClientID%> option:selected").text();
            //  var YEAR = $('#<%= ddlYear.ClientID %>').val();
            var SDATE = $('#<%= txtDate1.ClientID %>').val();
            var EDATE = $('#<%= txtDate2.ClientID %>').val();

            window.showModalDialog("IssueQ.aspx?BCODE=" + BCODE + "&ICODE=" + ICODE + "&YEAR=" + YEAR + "&TRANS_TYPE=" + TTYPE + "&SDATE=" + SDATE + "&EDATE=" + EDATE, null, "dialogheight:600px; dialogWidth:850px; center:yes");
            //$(".viresh").colorbox({width:"90%", height:"90%", iframe:true,href:"ReceiptQ.aspx?BCODE="+BCODE+"&DCODE="+DCODE+"&YEAR="+YEAR+"&SDATE="+SDATE+"&EDATE="+EDATE});
        }
    </script>

    <link media="screen" rel="stylesheet" type="text/css" href="../../../StyleSheet/colorbox.css" />

    <script type="text/javascript" language="javascript" src="../../../javascript/jquery.min.js"></script>

    <script type="text/javascript" language="javascript" src="../../../javascript/jquery.colorbox.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $("#colorbox").appendTo('form');
        });		
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

    <script language="javascript" type="text/javascript">
        function ShowModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).show();
        }

        function HideModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).hide();
        }        
    </script>

    <cc1:ModalPopupExtender runat="server" PopupControlID="PanLoad" ID="ModalProgress"
        TargetControlID="PanLoad" BackgroundCssClass="modalBackground" BehaviorID="pload">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="PanLoad" runat="server" CssClass="modalPopup" Height="15px" Width="220px">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePane2l">
            <ProgressTemplate>
                <img src="../../../CommonImages/Icons/loading.gif" alt="" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePane2l" runat="server">
        <ContentTemplate>
            <table cellpadding="3" cellspacing="0" width="100%" class="tContentArial td">
                <tr>
                    <td align="left" colspan="8" width="100%" class="td">
                        <table class="style1" cellspacing="0" cellpadding="0" border="0" align="left">
                            <tbody>
                                <tr>
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
                        <b class="titleheading">Material Stock Query</b>
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
                    <td style="text-align: right">
                        Item :
                    </td>
                    <td>
                        <cc2:ComboBox ID="txtItemCode" runat="server" CssClass="smallfont" Width="151px"
                            AutoPostBack="true" EnableLoadOnDemand="True" DataTextField="ITEM_CODE" DataValueField="ITEM_CODE"
                            MenuWidth="650px" EnableVirtualScrolling="true" OpenOnFocus="true" Visible="true"
                            Height="200px" EmptyText="Find Item" OnLoadingItems="txtItemCode_LoadingItems"
                            OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header d1">
                                    CODE</div>
                                <div class="header d2">
                                    DESCRIPTION</div>
                                <div class="header d3">
                                    Category</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item d1">
                                    <%# Eval("ITEM_CODE")%></div>
                                <div class="item d2">
                                    <%# Eval("ITEM_DESC") %></div>
                                <div class="item d3">
                                    <%# Eval("CAT_CODE")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                 
                    <td style="text-align: right">
                        Category:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="tContentArial" Width="165px"
                            OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" TabIndex="5" AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chkBalance" runat="server" Text="Balance > 0" />
                        <asp:Button ID="btnView" runat="server" CssClass="AButton" OnClick="btnView_Click"
                            Text="View" OnClientClick="ShowModalPopup('pload');" Width="75" />
                    </td>
                       <td style="text-align: right">
                      <%--  Item Type:--%>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlItemType" runat="server" CssClass="tContentArial" Width="165px"
                            OnSelectedIndexChanged="ddlItemtype_SelectedIndexChanged" TabIndex="5" AutoPostBack="False" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" class="TdBackVir">
                        <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" class="td" align="center">
                        <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="None" Width="945px">
                            <asp:GridView ID="gvReportDisplayGrid" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="3" CssClass="smallfont" EmptyDataText="No Record Found"
                                Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging"
                                PagerStyle-HorizontalAlign="Left" PagerStyle-CssClass="Pager" PageSize="20" Width="100%">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%"
                                    Height="100%" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BRANCH_CODE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("YEAR")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCode" runat="server" Font-Bold="true" Text='<%#Eval("ITEM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="true" HorizontalAlign="Left" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDOJ" runat="server" Font-Bold="true" Text='<%#Eval("ITEM_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM" runat="server" Text='<%#Eval("UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Qty.">
                                        <ItemStyle BackColor="#99FF66" Font-Bold="true" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpnBal" runat="server" Text='<%#Eval("OPBAL_STOK","{0:N3}").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipt Qty.">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlReceiptQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                                NavigateUrl='<%# "javascript:OpenReceiptDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("ITEM_CODE") + "&#39;,&#39;" + "R" + "&#39;);" %>'
                                                Text='<%#Eval("RECPT_QTY","{0:N3}").ToString() %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#99FF66" HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Qty.">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlIssueQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                                NavigateUrl='<%# "javascript:OpenIssueDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("ITEM_CODE") + "&#39;,&#39;" + "I" + "&#39;);" %>'
                                                Text='<%#Eval("ISSUE_QTY","{0:N3}").ToString() %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#99FF66" HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Qty.">
                                        <ItemStyle BackColor="#99FF66" Font-Bold="true" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingBal" runat="server" Text='<%#Eval("CLOSING_QTY","{0:N3}").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Value">
                                        <ItemStyle BackColor="#99CCFF" Font-Bold="true" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpnBal" runat="server" Text='<%#Eval("OPBAL_VALUE","{0:N3}").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipt Value">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlReceiptQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                                NavigateUrl='<%# "javascript:OpenReceiptDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("ITEM_CODE") + "&#39;,&#39;" + "R" + "&#39;);" %>'
                                                Text='<%#Eval("RECPT_VALUE","{0:N3}").ToString() %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#99CCFF" HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Value">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlIssueQty" runat="server" Font-Bold="true" ForeColor="Brown"
                                                NavigateUrl='<%# "javascript:OpenIssueDetails("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("ITEM_CODE") + "&#39;,&#39;" + "I" + "&#39;);" %>'
                                                Text='<%#Eval("ISSUE_VALUE","{0:N3}").ToString() %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#99CCFF" HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Value">
                                        <ItemStyle BackColor="#99CCFF" Font-Bold="true" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingValue" runat="server" Text='<%#Eval("CLOSING_VALUE","{0:N3}").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Receipt Date">
                                        <ItemStyle BackColor="#CCCCCC" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastreceipt" runat="server" Text='<%#Eval("LRDATE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
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
                    <td>
                        <cc1:CalendarExtender ID="TxtIndentDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate1">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="TxtIndentDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate2">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
