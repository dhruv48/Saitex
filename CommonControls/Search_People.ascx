<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search_People.ascx.cs"
    Inherits="CommonControls_Search_People" %>
<style type="text/css">
    .ItemStyle
    {
        background-color: #EFF3FB;
        font-size: x-small;
    }
    .HeaderStyle
    {
        background-color: #507CD1;
        font-weight: bold;
        font-size: x-small;
        color: White;
    }
</style>
<table class="td tContentArial" width="98%">
    <tr>
        <td align="Left" class="td" colspan="4">
            <table align="left">
                <tr>
                    <td id="td1" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Width="48" OnClick="imgbtnFind_Click" />
                    </td>
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
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan="4">
            <span class="titleheading">Search People</span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="4">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" Text="Find" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="right">
            Emp Name :
        </td>
        <td>
            <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
        </td>
        <td align="right">
            Department :
        </td>
        <td>
            <asp:DropDownList ID="ddldept" runat="server" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE"
                Width="128px" CssClass="tContentArial" AppendDataBoundItems="True">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="4">
            <asp:Label ID="lblRecordFound" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="td tContentArial" >
            <asp:Panel ID="pnlShowHover" runat="server" Width="100%" BackColor="Beige" BorderWidth="2px"
                Height="540px" ScrollBars="Auto">
                <asp:DataList ID="dlSearchEmp" runat="server" Font-Size="X-Small" ForeColor="#333333"
                    GridLines="Both" Width="98%" RepeatColumns="2" OnItemDataBound="dlSearchEmp_ItemDataBound">
                    <ItemTemplate>
                        <table >
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    Emp Code
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lbldlEmpCode" runat="server" CssClass="SmallFont" Font-Size="X-Small"
                                        Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                                </td>
                                <td colspan="4" rowspan="7">
                                    <asp:Image ID="imgEmp" runat="server" Height="133px" Width="180px" 
                                        ImageUrl="~/CommonImages/logo.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    Emp Name
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EMP_NAME") %>' Font-Size="X-Small"
                                        CssClass="SmallFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    Sex
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblGender" runat="server" CssClass="SmallFont" Font-Size="X-Small"
                                        Text='<%# Eval("GENDER") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    DOB
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblDOB" runat="server" CssClass="SmallFont" Font-Size="X-Small" Text='<%# Eval("DOB", "{0:d}") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    DOJ
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblDOJ" runat="server" CssClass="SmallFont" Font-Size="X-Small" Text='<%# Eval("JOIN_DT", "{0:d}") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    Designation
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblDesig" runat="server" CssClass="SmallFont" Font-Size="X-Small"
                                        Text='<%# Eval("DESIG_NAME") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    Email Id
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblEmail" runat="server" CssClass="SmallFont" Font-Size="X-Small"
                                        Text='<%# Eval("EMAIL_ID") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdRight HeaderStyle">
                                    Department
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblDept" runat="server" CssClass="SmallFont" Font-Size="X-Small" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                                </td>
                                <th class="tdRight HeaderStyle">
                                    Position
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblPosition" runat="server" CssClass="SmallFont" Font-Size="X-Small"
                                        Text='<%# Eval("POSITION_NAME") %>'></asp:Label>
                                </td>
                                <th class="tdRight HeaderStyle">
                                    Blood Group
                                </th>
                                <td class="tdLeft ItemStyle">
                                    <asp:Label ID="lblBloodGroup" runat="server" CssClass="SmallFont" Font-Size="X-Small"
                                        Text='<%# Eval("EMP_BLD_GRP") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle BackColor="#EFF3FB" Font-Size="X-Small" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="X-Small" ForeColor="White" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingItemStyle BackColor="White" />
                </asp:DataList>
                <%--<asp:GridView ID="grdSearchEmployee" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="true"
                    HeaderStyle-Wrap="true" PageSize="15" Font-Size="X-Small" OnPageIndexChanging="grdSearchEmployee_PageIndexChanging"
                    OnSorting="grdSearchEmployee_Sorting">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="EMP_CODE" HeaderText="Emp Code" />
                        <asp:BoundField DataField="CARD_NO" HeaderText="Card No" />
                        <asp:BoundField DataField="EMP_NAME" HeaderText="Emp Name" />
                             <asp:BoundField DataField="M_NAME" HeaderText="Middle Name" />
                        <asp:BoundField DataField="L_NAME" HeaderText="Last Name" />
                        <asp:BoundField DataField="GENDER" HeaderText="Sex" />
                        <asp:BoundField DataField="DOB" DataFormatString="{0:d}" HeaderText="Date Of Birth" />
                        <asp:BoundField DataField="F_H_NAME" HeaderText="Father/ Husband Name" />
                        <asp:BoundField DataField="RELATIONSHIP" HeaderText="Relationship" />
                        <asp:BoundField DataField="JOIN_DT" DataFormatString="{0:d}" HeaderText="date Of Joining" />
                        <asp:BoundField DataField="DESIG_NAME" HeaderText="Designation" />
                        <asp:BoundField DataField="EMAIL_ID" HeaderText="Email Id" />
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />
                        <asp:BoundField DataField="POSITION_NAME" HeaderText="Position" />
                        <asp:BoundField DataField="EMP_BLD_GRP" HeaderText="Blood Group" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>--%>
            </asp:Panel>
        </td>
    </tr>
</table>
