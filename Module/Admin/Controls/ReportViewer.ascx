<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportViewer.ascx.cs"
    Inherits="Module_Admin_Controls_ReportViewer" %>
<table class="td tContentArial" width="100%">
    <tr>
        <td align="left" class="td" colspan="3" width="100%">
            <table align="left">
                <tr>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan="3" width="100%">
            <span class="titleheading">Report Creation Wizard</span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" colspan="3" width="100%">
            <asp:Panel ID="pnlWizard" runat="server">
                <asp:Wizard ID="wzReportPrepare" runat="server" ActiveStepIndex="0" Width="98%" OnNextButtonClick="wzReportPrepare_NextButtonClick"
                    OnFinishButtonClick="wzReportPrepare_FinishButtonClick">
                    <WizardSteps>
                        <asp:WizardStep ID="TableViewSelection" runat="server" Title="Step 1">
                            <br />
                            <table style="width: 100%; height: 2px;">
                                <tr>
                                    <td style="font-size: x-small; font-style: italic; color: #008000" width="200px">
                                        Select From Table<br />
                                        <asp:ListBox ID="lstTablesAll" runat="server" Height="150px" Width="198px"></asp:ListBox>
                                    </td>
                                    <td width="50px">
                                        <asp:Button ID="btnSelectTable" runat="server" Text="&gt;&gt;" OnClick="btnSelectTable_Click" />
                                        <br />
                                        <asp:Button ID="btnUnselectTable" runat="server" Text="&lt;&lt;" OnClick="btnUnselectTable_Click" />
                                    </td>
                                    <td style="font-size: x-small; font-style: italic; color: #008000" width="200px">
                                        Selected Tables<br />
                                        <asp:ListBox ID="lstTablesSelected" runat="server" Height="150px" Width="190px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; font-style: italic; color: #008000" colspan="3" width="450px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: x-small; font-style: italic; color: #008000" width="200px">
                                        Select From Views<br />
                                        <asp:ListBox ID="lstViewsAll" runat="server" Height="150px" Width="198px"></asp:ListBox>
                                    </td>
                                    <td width="50px">
                                        <asp:Button ID="btnSelectViews" runat="server" OnClick="btnSelectViews_Click" Text="&gt;&gt;" />
                                        <br />
                                        <asp:Button ID="btnUnselectViews" runat="server" OnClick="btnUnselectViews_Click"
                                            Text="&lt;&lt;" />
                                    </td>
                                    <td style="font-size: x-small; font-style: italic; color: #008000" width="200px">
                                        Selected Views<br />
                                        <asp:ListBox ID="lstViewsSelected" runat="server" Height="150px" Width="198px"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:WizardStep>
                        <asp:WizardStep ID="ColSelection" runat="server" Title="Step 2">
                            <br />
                            <table style="width: 100%; height: 2px;">
                                <tr>
                                    <td style="font-size: x-small; font-style: italic; color: #008000" colspan="3" width="450px">
                                        <asp:DropDownList ID="ddlSelectTableForCol" AutoPostBack="True" AppendDataBoundItems="True"
                                            runat="server" OnSelectedIndexChanged="ddlSelectTableForCol_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="font-size: x-small; font-style: italic; color: #008000"
                                        width="200px">
                                        Select Coloums<br />
                                        <asp:ListBox ID="lstSelectCols" runat="server" Height="150px" Width="198px"></asp:ListBox>
                                    </td>
                                    <td width="50px">
                                        <asp:Button ID="btnSelectCols" runat="server" Text="&gt;&gt;" OnClick="btnSelectCols_Click" />
                                        <br />
                                    </td>
                                    <td colspan="3" align="left" valign="top" style="font-size: x-small; font-style: italic;
                                        color: #008000" width="450px">
                                        <asp:DataList ID="dlSelectedCols" runat="server" OnItemCommand="dlSelectedCols_ItemCommand"
                                            BackColor="#60B7AE" BorderColor="#81F3E2" BorderStyle="Solid" BorderWidth="1px"
                                            ShowFooter="False">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <th class="tdLeft" width="150px">
                                                            Column Name
                                                        </th>
                                                        <th class="tdLeft" width="150px">
                                                            Header Text
                                                        </th>
                                                        <th class="tdLeft" width="150px">
                                                            Format String
                                                        </th>
                                                        <th class="tdLeft" width="50px">
                                                            Show in Report
                                                        </th>
                                                        <th class="tdLeft" width="50px">
                                                            Use in Filter
                                                        </th>
                                                        <th class="tdLeft" width="50px">
                                                            Use in Sorting
                                                        </th>
                                                        <th class="tdLeft" width="50px">
                                                        </th>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="tdLeft" width="150px">
                                                            <asp:Label ID="lbldlColName" runat="server" Text='<%# Bind("COLUMN_NAME") %>'></asp:Label>
                                                        </td>
                                                        <td class="tdLeft" width="150px">
                                                            <asp:TextBox ID="txtdlHeaderName" runat="server" Text='<%# Bind("HEADER_TEXT") %>'></asp:TextBox>
                                                        </td>
                                                        <td class="tdLeft" width="150px">
                                                            <asp:TextBox ID="txtdlFormatString" runat="server" Text='<%# Bind("FORMAT_STRING") %>'></asp:TextBox>
                                                        </td>
                                                        <td class="tdLeft" width="50px">
                                                            <asp:CheckBox ID="chkdlIs_report" runat="server" Checked='<%# Bind("IS_REPORT") %>' />
                                                        </td>
                                                        <td class="tdLeft" width="50px">
                                                            <asp:CheckBox ID="chkdlIs_Filter" runat="server" Checked='<%# Bind("IS_FILTER") %>' />
                                                        </td>
                                                        <td class="tdLeft" width="50px">
                                                            <asp:CheckBox ID="chkdlIs_sort" runat="server" Checked='<%# Bind("IS_SORT") %>' />
                                                        </td>
                                                        <td class="tdLeft" width="80px">
                                                            <asp:LinkButton ID="lbtndlRemove" runat="server" Text="Remove" CommandArgument='<%# Bind("TABLE_NAME") %>'
                                                                CommandName="RemdlRow"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </asp:Panel>
        </td>
    </tr>
</table>
