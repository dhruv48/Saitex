<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Staff_Out_Door_Duty.ascx.cs"
    Inherits="Module_HRMS_Controls_Staff_Out_Door_Duty" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>

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
                    strInputTime = "00:00"
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
                        strInputTime = "00:00"
                    } 
                } else { if (strInputTime.substring(0, 2) <= 23) { if (strInputTime.substring(2, 3) <= 5) { strInputTime = strInputTime.substring(0, 2) + ":" + strInputTime.substring(2, 4); } else { strInputTime = strInputTime.substring(0, 2) + ":" + "00"; } } else { strInputTime = "00:00" } } 
            } else { strInputTime = "00:00" } //alert(strInputTime);
            strReturnValue = strInputTime;
        } return strReturnValue;
    }
    function doClear(theText) {
        if (theText.value == theText.defaultValue) {
            theText.value = ""
        }
    }
    function validate() {
        var str1 = document.getElementById('<%=TxtFromdate.ClientID %>').value;
        var str2 = document.getElementById('<%=TxtToDate.ClientID %>').value;
        var mon1 = parseInt(str1.substring(0, 2), 10);
        var dt1 = parseInt(str1.substring(3, 5), 10);
        var yr1 = parseInt(str1.substring(6, 10), 10);
        var mon2 = parseInt(str2.substring(0, 2), 10);
        var dt2 = parseInt(str2.substring(3, 5), 10);
        var yr2 = parseInt(str2.substring(6, 10), 10);
        var date1 = new Date(yr1, mon1, dt1);
        var date2 = new Date(yr2, mon2, dt2);
        if (date2 < date1) {
            alert("To date must be greater than from date");
            return false;
        }
        else {
            return true;
        }
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
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<ajaxToolkit:CalendarExtender ID="FromDate" TargetControlID="TxtFromdate" Format="dd/MM/yyyy"
    runat="server">
</ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="ToDate" TargetControlID="TxtToDate" Format="dd/MM/yyyy"
    runat="server">
</ajaxToolkit:CalendarExtender>
<table align="left" width="100%" class="tContentArial">
    <tr>
        <td colspan="6">
            <table class="tContentArial" cellspacing="0" width="20%" cellpadding="0" border="0"
                align="left">
                <tbody>
                    <tr>
                        <td id="tdSave" width="48" runat="server">
                            <asp:ImageButton ID="imgbtnInsert" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnInsert_Click"></asp:ImageButton>
                        </td>
                        <td width="48">
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                Width="48" Height="41" OnClientClick="return confirm('Are you sure you want to Print?');"
                                OnClick="imgbtnPrint_Click"></asp:ImageButton>
                        </td>
                        <td width="48">
                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                Width="48" Height="41" OnClientClick="return confirm('Are you sure you want to clear?');"
                                OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td width="48">
                            <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                Width="48" Height="41" OnClientClick="return confirm('Are you sure you want to Exit?');"
                                OnClick="imgbtnExit_Click"></asp:ImageButton>
                        </td>
                        <td width="48">
                            <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                Width="48" Height="41"></asp:ImageButton>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center" class="TableHeader td">
            <span class="titleheading"><b>OUT-DOOR DUTY FORM</b></span>
        </td>
    </tr>
    <tr>
        <td>
            Employee:
        </td>
        <td>
            <obout:ComboBox runat="server" ID="ddlEmployee" EnableVirtualScrolling="true" Width="171px"
                Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="ddlEmployee_LoadingItems"
                AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                MenuWidth="300px">
                <HeaderTemplate>
                    <div class="header c1">
                        Emp Code</div>
                    <div class="header c2">
                        Employee Name</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <%# Eval("EMP_CODE")%></div>
                    <div class="item c2">
                        <%# Eval("EMPLOYEENAME")%></div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </obout:ComboBox>
        </td>
        <td>
            Department:
        </td>
        <td>
            <asp:TextBox ID="TxtDept" runat="server" ReadOnly="true" Width="200px" CssClass="textbox"></asp:TextBox>
        </td>
        <td>
            Designation:
        </td>
        <td align="left" style="width: 20%">
            <asp:TextBox ID="TxtDesig" runat="server" ReadOnly="true" Width="200px" CssClass="textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="100%" colspan="6">
            <table>
                <tr>
                    <td style="width: 20%" align="right">
                        I will be on official duty from:
                    </td>
                    <td style="width: 20%" align="left">
                        <asp:TextBox ID="TxtFromdate" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TxtFromTime" Width="50px" CssClass="textbox" onblur="return MaskTimeFormat(this)"
                            onFocus="doClear(this)" Text="00:00" MaxLength="4" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%" align="right">
                        To
                    </td>
                    <td style="width: 20%" align="left">
                        <asp:TextBox ID="TxtToDate" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TxtToTime" Width="50px" CssClass="textbox" onblur="return MaskTimeFormat(this)"
                            onFocus="doClear(this)" Text="00:00" MaxLength="4" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" align="right">
                        On/From:
                    </td>
                    <td style="width: 20%" align="left">
                        <asp:TextBox ID="TxtOnFrom" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                    <td style="width: 5%" align="right">
                        To
                    </td>
                    <td style="width: 20%" align="left">
                        <asp:TextBox ID="TxtOnTo" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                    <td style="width: 5%" align="right">
                        at(place)
                    </td>
                    <td style="width: 20%" align="left">
                        <asp:TextBox ID="TxtPlace" Width="250px" CssClass="textbox" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="6" class="tdLeft">
            <table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
                <tr>
                    <td width="100%" align="center" class="TableHeader">
                        <b class="titleheading">Out-Door Duty Detail </b>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                        <asp:GridView ID="gvReportDisplayGrid" runat="server" AllowPaging="True" PagerSettings-Position="Bottom"
                            AutoGenerateColumns="False" PagerSettings-Mode="Numeric" CssClass="smallfont"
                            Width="100%" PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging"
                            DataKeyNames="OD_ID" EmptyDataText="There is no record for approval" PageSize="15"
                            Font-Size="X-Small" CaptionAlign="Top" ForeColor="#333333" GridLines="None">
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Apply Date" HeaderStyle-HorizontalAlign="Center" DataField="APPLIEDDATE"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Emp.Code" DataField="Emp_code" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMPNAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Duty From" HeaderStyle-HorizontalAlign="Center" DataField="FROM_DATE"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Duty To" DataField="TO_DATE" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="From" DataField="ON_FROM" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Days" DataField="DAYS" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="To" DataField="ON_TO" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Place" DataField="PLACE" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" DataField="OD_status"
                                    ItemStyle-HorizontalAlign="Center" HtmlEncode="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Request Print" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a target="_blank" href="../Reports/Out_Door_DutyRpt.aspx?EmpCode=<%# Eval("EMP_CODE") %>&OD_ID=<%# Eval("OD_ID") %>">
                                            <b>Print</b></a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                                ForeColor="White" Font-Bold="True" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>