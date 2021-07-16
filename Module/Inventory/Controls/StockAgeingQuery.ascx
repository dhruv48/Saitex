<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockAgeingQuery.ascx.cs"
    Inherits="Module_Inventory_Controls_StockAgeingQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
</style>

<script type="text/javascript" language="javascript">
    // Added By Rajesh for Printing Directly from Form (05 Jan 2012)
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        if (prtContent != null) {
            var WinPrint = window.open('', '', 'center=1,width=800,height=600,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    }    
</script>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                             <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                    <td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Material Stock Ageing Query Form</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlFilter" runat="server">
                        <table>
                            <tr>
                                <td align="right" style="width: 7%;">
                                    *Day 1 :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:TextBox ID="txtDay1" runat="server" Width="50px" TabIndex="1" CssClass="TextBoxNo"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 7%;">
                                    *Day 2 :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:TextBox ID="txtDay2" runat="server" Width="50px" TabIndex="2" CssClass="TextBoxNo"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 7%;">
                                    *Day 3 :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:TextBox ID="txtDay3" runat="server" Width="50px" TabIndex="3" CssClass="TextBoxNo"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 7.50%;">
                                    *Branch :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 7.50%;">
                                    Item Type :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:DropDownList ID="ddlItemType" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 7.50%;">
                                    Category Code :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:DropDownList ID="ddlCatCode" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlCatCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            <td align="right" style="width: 7.50%;">
                                    Location :
                                </td>
                                <td align="center" style="width: 7%;">
                                    <asp:DropDownList ID="ddllocation" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlCatCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 7.50%;">
                                    Store :
                                </td>
                                <td align="left" style="width: 7%;">
                                    <asp:DropDownList ID="ddlstore" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlCatCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
           
            <tr>
            
                <td >
                    <asp:Button ID="btnPrint" runat="Server" Text="Print Direct" TabIndex="7"  CssClass="AButton" />
                   &nbsp;&nbsp;&nbsp;  <span style="font-family:Times New Roman;font-size:large;">Total Record =  <asp:Label ID="lblTotalRecord" runat="server"></asp:Label></span>
                </td>
                
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="350px" Width="100%">
                        <div id="divPrint">
                            <asp:GridView ID="grdStockAgeing" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                ShowFooter="true" CellPadding="4" ForeColor="#333333" BorderStyle="Ridge" GridLines="Both"
                                OnRowDataBound="grdStockAgeing_RowDataBound" Font-Size="X-Small" Width="100%">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch" />
                                    <asp:BoundField DataField="LOCATION" HeaderText="Location" />
                                    <asp:BoundField DataField="STORE" HeaderText="Store" />
                                    <asp:BoundField DataField="ITEM_code" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description" HeaderStyle-Width="225px" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 1" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay1" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld1day" runat="server" Text='<%# Eval("d1day", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 2" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay2" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld2day" runat="server" Text='<%# Eval("d2day", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 3" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay3" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld3day" runat="server" Text='<%# Eval("d3day", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 4" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay4" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld4day" runat="server" Text='<%# Eval("d4Say", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Total Qty" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrTotQty" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTQTY" runat="server" Text='<%# Eval("TQTY", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 1 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay1Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld1dayval" runat="server" Text='<%# Eval("d1dayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 2 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay2Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld2dayval" runat="server" Text='<%# Eval("d2dayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 3 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay3Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld3dayval" runat="server" Text='<%# Eval("d3dayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 4 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay4Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld4sayval" runat="server" Text='<%# Eval("d4sayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Total Qty Value"
                                        FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrTotQtyValue" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltqtyval" runat="server" Text='<%# Eval("tqtyval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>