<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_QC_CheckingList.ascx.cs" Inherits="Module_NewFiber_Controls_Fiber_QC_CheckingList" %>

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
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>
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
                    <span class="titleheading"><strong>Fiber QC Checking List</strong> </span>
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
                                                    <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%#Eval("TRN_TYPE") %>' Visible="false"></asp:Label>
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
                                    <asp:TemplateField HeaderText="MRN&nbsp;Date">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    MRN&nbsp;Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTRN_DATE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
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
                                    <ItemTemplate>
                                        <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTY_DATA") %>' ToolTip='<%#Eval("PARTY_DATA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn&nbsp;Code">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Yarn&nbsp;Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtYARN_CODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnYARN_CODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblYARN_CODE" runat="server" Text='<%#Eval("FIBER_CODE") %>' ToolTip='<%#Eval("FIBER_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn&nbsp;Desc">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Yarn&nbsp;Desc
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtYARN_DESC" Width="120px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnYARN_DESC" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblYARN_DESC" runat="server" Text='<%#Eval("FIBER_DESC") %>' ToolTip='<%#Eval("FIBER_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn&nbsp;Count">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Yarn&nbsp;Count
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtY_COUNT" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnY_COUNT" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblY_COUNT" runat="server" Text='<%#Eval("Y_COUNT") %>' ToolTip='<%#Eval("Y_COUNT") %>'></asp:Label>--%>
                                        <asp:Label ID="lblY_COUNT" runat="server" Text='<%#Eval("INWARD_TYPE") %>' ToolTip='<%#Eval("INWARD_TYPE") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Std&nbsp;Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    QC&nbsp;Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtQC_Value" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSTD_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_Value" runat="server" Text='<%#Eval("QC_VALUE") %>' ToolTip='<%#Eval("QC_VALUE") %>'></asp:Label>
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
                                                    <asp:TextBox ID="txtQ_RESULT" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQ_RESULT" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQ_RESULT" runat="server" Text='<%#Eval("Q_RESULT") %>' ToolTip='<%#Eval("Q_RESULT") %>'></asp:Label>
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
                                                    <asp:TextBox ID="txtQC_CHANGE_RESULT" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQC_CHANGE_RESULT" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQC_CHANGE_RESULT" runat="server" Text='<%#Eval("Q_CHANGE_RESULT") %>'
                                            ToolTip='<%#Eval("Q_CHANGE_RESULT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("QC_CONF_REMARKS") %>' ToolTip='<%#Eval("QC_CONF_REMARKS") %>'></asp:Label>
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

