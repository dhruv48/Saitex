<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_CheckingList.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_CheckingList" %>
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td>
            <table class=" td tContentArial">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                    </td>
                </tr>
            </table>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><strong>Item QC Checking List</strong> </span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 100%;">
                        <asp:GridView ID="grdITEMMasterQuery" runat="server" AutoGenerateColumns="False"
                            Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge"
                            CssClass="smallfont" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            OnPageIndexChanging="grUserMasterQuery_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="QC&nbsp;Year">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Year
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtYEAR" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnYEAR" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblYEAR" runat="server" Text='<%#Eval("YEAR") %>' ToolTip='<%#Eval("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC&nbsp;No">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;No
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_NUMB" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_NUMB" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_NUMB" runat="server" Text='<%#Eval("QC_NUMB") %>' ToolTip='<%#Eval("QC_NUMB") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC&nbsp;DATE">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_DATE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_DATE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_DATE" runat="server" Text='<%#Eval("QC_DATE") %>' ToolTip='<%#Eval("QC_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRN&nbsp;Year">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    MRN&nbsp;Year
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTRN_YEAR" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTRN_YEAR" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRN_YEAR" runat="server" Text='<%#Eval("TRN_YEAR") %>' ToolTip='<%#Eval("TRN_YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRN&nbsp;No">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    MRN&nbsp;No
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTRN_NUMB" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTRN_NUMB" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRN_NUMB" runat="server" Text='<%#Eval("TRN_NUMB") %>' ToolTip='<%#Eval("TRN_NUMB") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prepared&nbsp;Date">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    MRN&nbsp;Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTRN_DATE" Width="120px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTRN_DATE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRN_DATE" runat="server" Text='<%#Eval("TRN_DATE") %>' ToolTip='<%#Eval("TRN_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party">
                                    <%-- <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Party
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtPARTY_DATA" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnPARTY_DATA" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPARTY_DATA" runat="server" Text='<%#Eval("PARTY_DATA") %>' ToolTip='<%#Eval("PARTY_DATA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item&nbsp;Code">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Item&nbsp;Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtITEM_CODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnITEM_CODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblITEM_CODE" runat="server" Text='<%#Eval("ITEM_CODE") %>' ToolTip='<%#Eval("ITEM_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item&nbsp;Description">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Item&nbsp;Description
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtITEM_DESC" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnITEM_DESC" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblITEM_DESC" runat="server" Text='<%#Eval("ITEM_DESC") %>' ToolTip='<%#Eval("ITEM_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Std&nbsp;Type">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Std&nbsp;Type
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtSTD_TYPE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSTD_TYPE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTD_TYPE" runat="server" Text='<%#Eval("STD_TYPE") %>' ToolTip='<%#Eval("STD_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max&nbsp;Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Max&nbsp;Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtMAX_VALUE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnMAX_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMAX_VALUE" runat="server" Text='<%#Eval("MAX_VALUE") %>' ToolTip='<%#Eval("MAX_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min&nbsp;Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Min&nbsp;Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtMIN_VALUE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnMIN_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMIN_VALUE" runat="server" Text='<%#Eval("MIN_VALUE") %>' ToolTip='<%#Eval("MIN_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC&nbsp;Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_VALUE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_VALUE" runat="server" Text='<%#Eval("QC_VALUE") %>' ToolTip='<%#Eval("QC_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <%-- <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Remarks
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_REMARKS" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_REMARKS" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_REMARKS" runat="server" Text='<%#Eval("QC_REMARKS") %>' ToolTip='<%#Eval("QC_REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Result">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Result
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_Result" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_Result" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_Result" runat="server" Text='<%#Eval("QC_Result") %>' ToolTip='<%#Eval("QC_Result") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved&nbsp;Result">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Approved&nbsp;Result
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_Approved_Result" Width="50px" Height="10px" runat="server"
                                                        Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_Approved_Result" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_Approved_Result" runat="server" Text='<%#Eval("QC_Approved_Result") %>'
                                            ToolTip='<%#Eval("QC_Approved_Result") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Status
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtSTATUS" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSTATUS" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%#Eval("STATUS") %>' ToolTip='<%#Eval("STATUS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC&nbsp;Prepared&nbsp;By">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Prepared&nbsp;By
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_DONE_BY" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_DONE_BY" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_DONE_BY" runat="server" Text='<%#Eval("QC_DONE_BY") %>' ToolTip='<%#Eval("QC_DONE_BY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC&nbsp;Approved&nbsp;DATE">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Approved&nbsp;Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_CONF_DATE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_CONF_DATE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_CONF_DATE" runat="server" Text='<%#Eval("QC_CONF_DATE") %>'
                                            ToolTip='<%#Eval("QC_CONF_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC&nbsp;Approved&nbsp;By">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Approved&nbsp;By
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_Approved_By" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_Approved_By" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_Approved_By" runat="server" Text='<%#Eval("QC_Approved_By") %>'
                                            ToolTip='<%#Eval("QC_Approved_By") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </td>
    </tr>
</table>
