<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Attendance.ascx.cs" Inherits="Module_HRMS_Controls_Attendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    //created by viresh//
    function MaskTimeFormat(fldnm) {
        var strTimeValue = fldnm.value;
        fldnm.value = GiveCorrectTimeFormat(strTimeValue);
    }
    function GiveCorrectTimeFormat(strInputTime) {
        var strReturnValue = "";
        if (strInputTime.length <= 5) {
            strInputTime = strInputTime.replace(":", "");
            if (strInputTime.length == 1) {
                strInputTime = "0" + strInputTime + ":" + "00";
            }
            else if (strInputTime.length == 2) {
                if (strInputTime <= 23) {
                    strInputTime = strInputTime + ":" + "00";
                }
                else if (strInputTime <= 59) {
                    strInputTime = "00" + ":" + strInputTime;
                }
                else {
                    strInputTime = ""
                }
            }
            else if (strInputTime.length == 3) {
                if (strInputTime < 959) {
                    if (strInputTime.substring(1, 2) <= 5) {
                        strInputTime = "0" + strInputTime.substring(0, 1) + ":" + strInputTime.substring(1, 3);
                    }
                    else {
                        strInputTime = "0" + strInputTime.substring(0, 1) + ":" + "00";
                    }
                }
                else {
                    strInputTime = "0" + strInputTime.substring(0, 1) + ":" + "00";
                }
            }
            else if (strInputTime.length == 4) {
                if (strInputTime < 2359) {
                    if (strInputTime.substring(0, 2) <= 23) {
                        if (strInputTime.substring(2, 3) <= 5) {
                            strInputTime = strInputTime.substring(0, 2) + ":" + strInputTime.substring(2, 4);
                        } else {
                            strInputTime = strInputTime.substring(0, 2) + ":" + "00";
                        }
                    }

                    else {
                        strInputTime = ""
                    }
                } else { if (strInputTime.substring(0, 2) <= 23) { if (strInputTime.substring(2, 3) <= 5) { strInputTime = strInputTime.substring(0, 2) + ":" + strInputTime.substring(2, 4); } else { strInputTime = strInputTime.substring(0, 2) + ":" + "00"; } } else { strInputTime = "" } }
            } else { strInputTime = "" } //alert(strInputTime);
            strReturnValue = strInputTime;
        } return strReturnValue;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <cc1:CalendarExtender ID="CalExDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
        </cc1:CalendarExtender>
        <table align="left" width="100%" class="tContentArial">
            <tr>
                <td colspan="2" class="td">
                    <table class="tContent">
                        <tr>
                            <td id="tdSave" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click1" TabIndex="4" ToolTip="Find" ValidationGroup="M1"
                                    Width="48" />
                            </td>
                            <td id="tdClear" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')"
                                    TabIndex="8" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" TabIndex="9" ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdExit" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" TabIndex="10" ToolTip="Exit" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    TabIndex="11" ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="top" class="tRowColorAdmin td">
                    <span class="titleheading">Attendance Register</span>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="td">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Filter Record By:</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    Branch:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLBranch" Width="150px" CssClass="TextBox SmallFont" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Department:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLDepartment" Width="150px" CssClass="TextBox SmallFont" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Employee:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLEmployee" Width="150px" CssClass="TextBox SmallFont" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Designation:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLDesigination" Width="150px" CssClass="TextBox SmallFont"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Shift:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLShift" DataValueField="SFT_ID" Width="150px" CssClass="TextBox SmallFont"
                                        DataTextField="SFT_NAME" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Attendance Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" Width="150px" AutoPostBack="true" CssClass="TextBox SmallFont"
                                        OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Shift InTime:
                                </td>
                                <td>
                                    <b>
                                        <asp:Label ID="LblInTime" CssClass="SmallFont" runat="server" Text=""></asp:Label></b>
                                </td>
                                <td>
                                    Shift OutTime:
                                </td>
                                <td>
                                    <b>
                                        <asp:Label ID="LblOutTime" CssClass="SmallFont" runat="server" Text=""></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="CmdViewRecord" CssClass="TextBox" runat="server" Text="View Record"
                                        OnClick="CmdViewRecord_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <b>
                        <asp:Label ID="lblTotal" Text="Total Record :" runat="server"></asp:Label><asp:Label
                            ID="lblTotalRecord" runat="server"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset>
                        <legend>Attendance Record:</legend>
                        <table width="100%" class="smallfont">
                            <tr style="color: White; background-color:AFCAE4; ">
                                <td style="width: 11%">
                                    Employee Code
                                </td>
                                <td style="width: 17%">
                                    Employee Name
                                </td>
                                <td style="width: 14%">
                                    Designation
                                </td>
                                <td style="width: 14%">
                                    Department
                                </td>
                                <td style="width: 6%">
                                    In Time
                                </td>
                                <td style="width: 6%">
                                    Out Time
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="gridPanel" runat="server" ScrollBars="Vertical" Width="100%" Height="450px">
                            <asp:GridView ID="gvAttendanceRegister" runat="server" AutoGenerateColumns="False"
                                AllowSorting="True" Font-Size="X-Small" CellPadding="4" ShowHeader="false" GridLines="None"
                                Width="100%" ForeColor="#333333" CssClass="smallfont" OnRowCommand="gvAttendanceRegister_RowCommand"
                                OnRowDataBound="gvAttendanceRegister_RowDataBound" EmptyDataText="No Record Found..">
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee Code" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Bind("EMP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDesign" runat="server" Text='<%# Bind("DESIG_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDept" runat="server" Text='<%# Bind("DEPT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Time">
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtInTime" Text='<% #Bind("In_Time") %>' CssClass="TextBox SmallFont"
                                                onblur="return MaskTimeFormat(this)" runat="server" Width="50px" TabIndex="1"
                                                MaxLength="4" ToolTip="Time Format(09:30)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out Time">
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOutTime" Text='<% #Bind("Out_Time") %>' CssClass="TextBox SmallFont"
                                                runat="server" onblur="return MaskTimeFormat(this)" TabIndex="1" Width="50px"
                                                MaxLength="4" ToolTip="Time Format(18:00)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LblCardNo" runat="server" Text='<%# Eval("CARD_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" Font-Size="Medium" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                                    ForeColor="White" Font-Bold="True" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                            </asp:GridView>
                        </asp:Panel>
                    </fieldset>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
