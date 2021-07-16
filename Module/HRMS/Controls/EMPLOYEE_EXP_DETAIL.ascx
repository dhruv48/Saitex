<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EMPLOYEE_EXP_DETAIL.ascx.cs"
    Inherits="Module_HRMS_Controls_EMPLOYEE_EXP_DETAIL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%" style="border-style: ridge; border-width: 1px;">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="M1" Width="48" OnClientClick="if (!confirm('Are you want to Save record ?')) { return false; }" />
                    </td>
                    <td id="tdUpdate" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Find" Width="48" />
                    </td>
                    <td id="tdFind" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Width="48" />
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="if (!confirm('Are you want to Clear this record ?')) { return false; }" />
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClientClick="if (!confirm('Are you want to Print record ?')) { return false; }" />
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" OnClientClick="javascript:return window.confirm('Are you sure you want to Exit from this Form')"
                            ToolTip="Exit" Width="48" />
                    </td>
                    <td style="width: 48px">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" valign="top" style="border-width: 2px; border-style: inset; border-color: #A0A0A4;
                        font-size: 14px; border-color: #f0f2f1; font-family: Arial;">
                        Employee Experience Detail
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-width: 1px; border-color: #F0F2F1;">
                <tr>
                    <td align="right" valign="top" width="25%">
                        Employee Code
                    </td>
                    <td align="left" valign="top" width="75%">
                        <%--   <asp:DropDownList ID="ddlempcode" Width="135px" runat="server" OnSelectedIndexChanged="ddlempcode_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txtempcode" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtempName" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-width: 1px; border-color: #F0F2F1;">
                <tr>
                    <td align="right" valign="top" width="25%">
                        Company Name
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtcompname" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%">
                        Company Level
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtcomplevel" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-width: 1px; border-color: #F0F2F1;">
                <tr>
                    <td align="right" valign="top" width="25%">
                        Reference By
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtreferby" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%">
                        From Date
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtfrom_dt" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-width: 1px; border-color: #F0F2F1;">
                <tr>
                    <td align="right" valign="top" width="25%">
                        To Date
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtTo_dt" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%">
                        CTC
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtctc" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-width: 1px; border-color: #F0F2F1;">
                <tr>
                    <td align="right" valign="top" width="25%">
                        Department
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtdepartment" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%">
                        Designation
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtdesignation" runat="server"></asp:TextBox>
                    </td>
                    <td width="30%">
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%">
                        Company Location
                    </td>
                    <td align="left" valign="top" width="75%">
                        <asp:TextBox ID="txtcomplocation" Width="430px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%">
                        Leaving Reason
                    </td>
                    <td align="left" valign="top" width="75%">
                        <asp:TextBox ID="txtleavingreason" Width="430px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-style: ridge; border-width: 1px;">
                <asp:GridView ID="EmpExpDataDind" AutoGenerateColumns="false" runat="server" Width="100%"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="ddlempcode" runat="server" CssClass="Label" Text='<%# Bind("EMP_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <asp:Label ID="txtcompname" runat="server" CssClass="Label" Text='<%# Bind("OLD_COMPANY") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refer By">
                            <ItemTemplate>
                                <asp:Label ID="txtreferby" runat="server" CssClass="Label" Text='<%# Bind("REFER_BY") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Level">
                            <ItemTemplate>
                                <asp:Label ID="txtcomplevel" runat="server" CssClass="Label" Text='<%# Bind("COMPANY_LEVEL") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="designation">
                            <ItemTemplate>
                                <asp:Label ID="txtdesignation" runat="server" CssClass="Label" Text='<%# Bind("DESIGNATION") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="txtdepartment" runat="server" CssClass="Label" Text='<%# Bind("DEPARTMENT") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="txtcomplocation" runat="server" CssClass="Label" Text='<%# Bind("C_LOCATION") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From">
                            <ItemTemplate>
                                <asp:Label ID="txtfrom_dt" runat="server" CssClass="Label" Text='<%# Bind("FROM_DATE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To">
                            <ItemTemplate>
                                <asp:Label ID="txtTo_dt" runat="server" CssClass="Label" Text='<%# Bind("T_DATE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CTC">
                            <ItemTemplate>
                                <asp:Label ID="txtctc" runat="server" CssClass="Label" Text='<%# Bind("LAST_CTC") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="txtleavingreason" runat="server" CssClass="Label" Text='<%# Bind("LEAVING_REASON") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </table>
        </td>
    </tr>
</table>
<cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="txtfrom_dt"
    runat="server">
</cc1:CalendarExtender>
<cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="txtTo_dt"
    runat="server">
</cc1:CalendarExtender>
