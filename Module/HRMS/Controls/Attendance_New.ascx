<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Attendance_New.ascx.cs"
    Inherits="Module_HRMS_Controls_Attendance_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
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
    .modalPopup
    {
        background-color: #696969;
        filter: alpha(opacity=40);
        opacity: 0.7;
        xindex: -1;
    }
</style>

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

    //
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
    prm.add_beginRequest(BeginRequestHandler);
    // Raised after an asynchronous postback is finished and control has been returned to the browser.
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.show();
        }
    }

    function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }
</script>

<link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/dialog.css" />
<link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/pager.css" />
<link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/grid.css" />
<asp:UpdateProgress ID="UpdateProgress" runat="server">
    <ProgressTemplate>
        <asp:Image ID="Image1" ImageUrl="~/CommonImages/waiting.gif" AlternateText="Processing"
            runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
    PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" BehaviorID="pload" />
<asp:UpdatePanel ID="pnlData" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table align="left" width="100%" class="td tContent">
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
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td>
                                Attendance Year:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLYear" runat="server" CssClass="TextBox SmallFont" Width="130px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Attendance Month:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLMonth" runat="server" CssClass="TextBox SmallFont" Width="130px"
                                    AutoPostBack="True" OnSelectedIndexChanged="DDLMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="--------SELECT-------"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLBranch" Width="130px" CssClass="TextBox SmallFont" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Department:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLDepartment" Width="130px" CssClass="TextBox SmallFont" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLDesigination" Width="130px" runat="server" CssClass="TextBox SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Shift:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLShift" DataValueField="SFT_ID" Width="130px" CssClass="TextBox SmallFont"
                                    DataTextField="SFT_NAME" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Employee:
                            </td>
                            <td>
                                <obout:ComboBox runat="server" ID="ddlEmployee" EnableVirtualScrolling="true" Width="130px"
                                    Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                                    DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="ddlEmployee_LoadingItems"
                                    AutoPostBack="True" MenuWidth="300px">
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
                                <asp:Button ID="CmdView" runat="server" CssClass="TextBox SmallFont" Text="View Record"
                                    OnClick="CmdView_Click" />
                            </td>
                            <td>
                                <asp:Button ID="CmdCalCulate" runat="server" CssClass="TextBox SmallFont" Text="Attn.Process"
                                    OnClick="CmdCalCulate_Click" />
                            </td>
                        </tr>
                        <tr style="font-weight: bold">
                            <td>
                                Shift InTime:
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="LblInTime" CssClass="TextBox" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Shift OutTime:
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="LblOutTime" CssClass="TextBox" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <div id="dlg" class="dialog" style="width: 100%">
                        <div class="header" style="cursor: default">
                            <div class="outer">
                                <div class="inner">
                                    <div class="content">
                                        <h5>
                                            EMPLOYEE RECORD</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="body">
                            <div class="outer">
                                <div class="inner">
                                    <div class="content">
                                        <asp:Panel CssClass="smallfont" Width="100%" ID="pnlCust" runat="server">
                                            <asp:UpdatePanel ID="pnlUpdate" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView Width="100%" CssClass="grid" AllowPaging="True" ID="gvAttendanceRegister"
                                                        AutoGenerateColumns="False" runat="server" ShowHeader="False" OnRowCreated="gvAttendanceRegister_RowCreated"
                                                        OnRowDataBound="gvAttendanceRegister_RowDataBound" OnPageIndexChanging="gvAttendanceRegister_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Panel CssClass="row" ID="pnlCustomer" runat="server">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 5%">
                                                                                    <asp:Image ID="imgCollapsible" CssClass="first" ImageUrl="~/CommonImages/img/plus.png"
                                                                                        Style="margin-right: 5px;" runat="server" />
                                                                                </td>
                                                                                <td style="width: 10%">
                                                                                    <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Bind("EMP_CODE") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%">
                                                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%">
                                                                                    <asp:Label ID="LblDesign" runat="server" Text='<%# Bind("DESIG_NAME") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%">
                                                                                    <asp:Label ID="LblDept" runat="server" Text='<%# Bind("DEPT_NAME") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%">
                                                                                    <asp:Label ID="LblCardNO" runat="server" Visible="false" Text='<%# Bind("CARD_NO") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%">
                                                                                    <asp:Label ID="lblSftiiId" runat="server" ToolTip='<%# Bind("SFT_ID") %>' Text='<%# Bind("SFT_NAME") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:SqlDataSource ID="sqlDsOrders" runat="server" ConnectionString="<%$ ConnectionStrings:csTextile %>"
                                                                        SelectCommand="SELECT  * FROM V_ATTN_SUMMARY_NEW WHERE LTRIM(RTRIM(MON))=:AMONTH AND LTRIM(RTRIM(AYEAR))=:AYEAR AND LTRIM(RTRIM(EMP_CODE))=:EMP_CODE "
                                                                        ProviderName="<%$ ConnectionStrings:csTextile.ProviderName %>">
                                                                        <SelectParameters>
                                                                            <asp:Parameter Name="EMP_CODE" />
                                                                            <asp:Parameter Name="AMONTH" Type="String" DefaultValue="01" />
                                                                            <asp:Parameter Name="AYEAR" Type="String" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                    <asp:Panel Style="margin-left: 20px; margin-right: 20px" ID="pnlOrders" runat="server">
                                                                        <asp:UpdatePanel ID="pnlUpdateGrid" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:GridView AutoGenerateColumns="False" CssClass="grid" ID="gvATTN" DataSourceID="sqlDsOrders"
                                                                                    runat="server">
                                                                                    <RowStyle CssClass="row" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Attendance Summary">
                                                                                            <ItemStyle HorizontalAlign="Left" Width="35px" />
                                                                                            <ItemTemplate>
                                                                                                <table>
                                                                                                    <tr style="font-weight: bold">
                                                                                                        <td>
                                                                                                            <asp:CheckBox ID="ChkMark" runat="server" AutoPostBack="True" OnCheckedChanged="ChkMark_CheckedChanged" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            01
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            02
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            03
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            04
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            05
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            06
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            07
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            08
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            09
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            10
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            11
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            12
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            13
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            14
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            15
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            In Time
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN1" Text='<% #Bind("D1_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN2" Text='<% #Bind("D2_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN3" Text='<% #Bind("D3_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN4" Text='<% #Bind("D4_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN5" Text='<% #Bind("D5_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN6" Text='<% #Bind("D6_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN7" Text='<% #Bind("D7_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN8" Text='<% #Bind("D8_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN9" Text='<% #Bind("D9_I") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN10" Text='<% #Bind("D10_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN11" Text='<% #Bind("D11_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN12" Text='<% #Bind("D12_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN13" Text='<% #Bind("D13_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN14" Text='<% #Bind("D14_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN15" Text='<% #Bind("D15_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            OutTime
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT1" Text='<% #Bind("D1_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT2" Text='<% #Bind("D2_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT3" Text='<% #Bind("D3_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT4" Text='<% #Bind("D4_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT5" Text='<% #Bind("D5_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT6" Text='<% #Bind("D6_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT7" Text='<% #Bind("D7_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT8" Text='<% #Bind("D8_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT9" Text='<% #Bind("D9_O") %>' CssClass="TextBox SmallFont" onblur="return MaskTimeFormat(this)"
                                                                                                                runat="server" Width="35px" TabIndex="1" MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT10" Text='<% #Bind("D10_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT11" Text='<% #Bind("D11_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT12" Text='<% #Bind("D12_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT13" Text='<% #Bind("D13_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT14" Text='<% #Bind("D14_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT15" Text='<% #Bind("D15_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="font-weight: bold">
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            16
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            17
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            18
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            19
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            20
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            21
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            22
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            23
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            24
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            25
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            26
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            27
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            28
                                                                                                        </td>
                                                                                                        <td id="td29" runat="server">
                                                                                                            29
                                                                                                        </td>
                                                                                                        <td id="td30" runat="server">
                                                                                                            30
                                                                                                        </td>
                                                                                                        <td id="td31" runat="server">
                                                                                                            31
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            In Time
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN16" Text='<% #Bind("D16_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN17" Text='<% #Bind("D17_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN18" Text='<% #Bind("D18_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN19" Text='<% #Bind("D19_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN20" Text='<% #Bind("D20_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN21" Text='<% #Bind("D21_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN22" Text='<% #Bind("D22_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN23" Text='<% #Bind("D23_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN24" Text='<% #Bind("D24_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN25" Text='<% #Bind("D25_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN26" Text='<% #Bind("D26_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN27" Text='<% #Bind("D27_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TIN28" Text='<% #Bind("D28_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td id="tdIN29" runat="server">
                                                                                                            <asp:TextBox ID="TIN29" Text='<% #Bind("D29_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td id="tdIN30" runat="server">
                                                                                                            <asp:TextBox ID="TIN30" Text='<% #Bind("D30_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td id="tdIN31" runat="server">
                                                                                                            <asp:TextBox ID="TIN31" Text='<% #Bind("D31_I") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            OutTime
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT16" Text='<% #Bind("D16_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT17" Text='<% #Bind("D17_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT18" Text='<% #Bind("D18_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT19" Text='<% #Bind("D19_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT20" Text='<% #Bind("D20_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT21" Text='<% #Bind("D21_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT22" Text='<% #Bind("D22_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT23" Text='<% #Bind("D23_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT24" Text='<% #Bind("D24_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT25" Text='<% #Bind("D25_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT26" Text='<% #Bind("D26_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT27" Text='<% #Bind("D27_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TOUT28" Text='<% #Bind("D28_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td id="tdOUT29" runat="server">
                                                                                                            <asp:TextBox ID="TOUT29" Text='<% #Bind("D29_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td id="tdOUT30" runat="server">
                                                                                                            <asp:TextBox ID="TOUT30" Text='<% #Bind("D30_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td id="tdOUT31" runat="server">
                                                                                                            <asp:TextBox ID="TOUT31" Text='<% #Bind("D31_O") %>' CssClass="TextBox SmallFont"
                                                                                                                onblur="return MaskTimeFormat(this)" runat="server" Width="35px" TabIndex="1"
                                                                                                                MaxLength="5"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                </asp:GridView>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </asp:Panel>
                                                                    <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlOrders"
                                                                        CollapsedSize="0" Collapsed="True" ExpandControlID="pnlCustomer" CollapseControlID="pnlCustomer"
                                                                        AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgCollapsible"
                                                                        ExpandedImage="~/CommonImages/img/minus.png" CollapsedImage="~/CommonImages/img/plus.png"
                                                                        ExpandDirection="Vertical" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="row" />
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="footer">
                            <div class="outer">
                                <div class="inner">
                                    <div class="content">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>