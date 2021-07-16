<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DEPT_BUDGET.ascx.cs" Inherits="Module_HRMS_Controls_DEPT_BUDGET" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="BUDGET_LOV.ascx" TagName="BUDGET_LOV" TagPrefix="uc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<style type="text/css">
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial" cellpadding="0" cellspacing="0">
            <tr>
                <td class="td">
                    <table>
                        <td id="tdSave" runat="server">
                            <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" OnClientClick="if (!confirm('Are you want to save?')) { return false; }" />
                        </td>
                        <td id="tdUpdate" runat="server">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click" OnClientClick="if (!confirm('Are you want to Update ?')) { return false; }">
                            </asp:ImageButton>
                        </td>
                        <td id="tdDelete" runat="server">
                            <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                Width="48" Height="41" OnClick="imgbtnDelete_Click" OnClientClick="if (!confirm('Are you want to Delete Record ?')) { return false; }">
                            </asp:ImageButton>
                        </td>
                        <td id="tdFind" runat="server">
                            <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                        </td>
                        <td id="tdClear" runat="server">
                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                Width="48" Height="41" OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to clear?')) { return false; }">
                            </asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                Width="48" Height="41" OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print ?')) { return false; }">
                            </asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                Width="48" Height="41" OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }">
                            </asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                Width="48" Height="41"></asp:ImageButton>
                        </td>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="tRowColorAdmin td">
                    <span class="titleheading">Department Budget</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <td>
                            <tr>
                                <td align="right" valign="top">
                                    Year :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="TextBoxNo" Width="50px" TabIndex="1"
                                        Rows="2" MaxLength="50" AutoPostBack="True" ValidationGroup="M1" OnTextChanged="txtYear_TextChanged"></asp:TextBox>
                                    <uc1:BUDGET_LOV ID="BUDGET_LOV1" runat="server" />
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtYear"
                                        Display="None" ErrorMessage="Year Can't be Previous Year" MaximumValue="2100"
                                        MinimumValue="2010" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Department Name :
                                </td>
                                <td align="left" valign="top">
                                    <cc2:OboutDropDownList ID="ddlDeptCode" runat="server" Width="160px" TabIndex="2">
                                    </cc2:OboutDropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                        ErrorMessage="*Select Department Code" ControlToValidate="ddlDeptCode" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Budget Amount :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtBdgAmt" runat="server" CssClass="TextBoxNo" Width="50px" TabIndex="3"
                                        Rows="2" MaxLength="50" AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                        ErrorMessage="*Pls Enter Budget Amount." ControlToValidate="txtBdgAmt" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <%--<asp:RangeValidator ID="RangeValidator2" runat="server" 
                            ControlToValidate="txtBdgAmt" ErrorMessage="Enter Numeric Value" Type="Integer"></asp:RangeValidator>--%>
                            </tr>
                        </td>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <td align="left">
                            <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AutoPostBackOnSelect="true"
                                AllowFiltering="True" PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select">
                                <Columns>
                                    <cc2:Column DataField="DEPT_BUDGET_CODE" HeaderText="Dept Budget Code" Visible="false">
                                    </cc2:Column>
                                    <cc2:Column DataField="DEPT_CODE" HeaderText="Department Code" Visible="false">
                                    </cc2:Column>
                                    <cc2:Column DataField="DEPT_NAME" HeaderText="Department Name">
                                    </cc2:Column>
                                    <cc2:Column DataField="YEAR" HeaderText="Year">
                                    </cc2:Column>
                                    <cc2:Column DataField="BUDGET_AMT" HeaderText="Budget Amount" Align="Left">
                                    </cc2:Column>
                                </Columns>
                            </cc2:Grid>
                        </td>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>