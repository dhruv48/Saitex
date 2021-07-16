<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EMP_MASTER.ascx.cs" Inherits="Module_HRMS_Controls_EMP_MASTER" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

<script language="javascript" type="text/javascript">
			function func1()
			{
				document.getElementById("imgPhoto").style.display="";
				document.getElementById("imgPhoto").src=document.getElementById("ctl00_cphBody_tPhoto").value;
			}
function doClear(theText)
 {
     if (theText.value == theText.defaultValue) {
         theText.value = ""
     }
 } 
function test()
 {
        var EMPCODE=$('#<%= txtEmployeeCode.ClientID %>').val();        
        var GradeID=$('#<%= ddlGrade.ClientID %>').val();
        $(".viresh").colorbox({width:"95%", height:"95%", iframe:true,href:"EmpCompInfo.aspx?EMP_CODE="+EMPCODE});
        $(".viresh1").colorbox({width:"95%", height:"95%", iframe:true,href:"EmpMedicalDetails.aspx?EMP_CODE="+EMPCODE});
        $(".viresh2").colorbox({width:"95%", height:"95%", iframe:true,href:"EmpLeaveDetails.aspx?EMP_CODE="+EMPCODE+ "&GradeID=" + GradeID });
        $(".viresh3").colorbox({width:"95%", height:"95%", iframe:true,href:"EmpSalaryMaster.aspx?EMP_CODE="+EMPCODE+ "&GradeID=" + GradeID });
        $(".viresh4").colorbox({width:"95%", height:"80%", iframe:true,href:"Employee_Family_Indp.aspx?EMP_CODE="+EMPCODE});
        $(".viresh5").colorbox({width:"95%", height:"95%", iframe:true,href:"Employee_Qualification.aspx?EMP_CODE="+EMPCODE});
        $(".viresh6").colorbox({width:"95%", height:"95%", iframe:true,href:"EMPLOYEE_EXP_DETAIL.aspx?EMP_CODE="+EMPCODE});
        return false ;
}
</script>

<link media="screen" rel="stylesheet" type="text/css" href="../../../StyleSheet/colorbox.css" />

<script type="text/javascript" language="javascript" src="../../../javascript/jquery.min.js"></script>

<script type="text/javascript" language="javascript" src="../../../javascript/jquery.colorbox.js"></script>

<script type="text/javascript" language="javascript">
		$(document).ready(function(){					
			$("#colorbox").appendTo('form'); 
		});		
</script>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table class="TableBox" cellspacing="1" cellpadding="1" width="100%">
    <tr>
        <td align="left" valign="TOP" colspan="7" width="100%">
            <table width="200" align="left" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/save.jpg"
                            Width="48" Height="41px" ValidationGroup="M1" TabIndex="31" OnClick="imgbtnSave_Click">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1" TabIndex="32">
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnDelete_Click" TabIndex="33">
                        </asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_Find.png"
                            Width="48" Height="41" OnClick="imgbtnFind_Click" TabIndex="34"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" OnClick="imgbtnClear_Click" TabIndex="35"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" OnClick="imgbtnPrint_Click" TabIndex="36"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click" TabIndex="37"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="38"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader" valign="top" align="center" colspan="7">
            <span class="titleheading">Employee Personal Details </span>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="7" valign="top">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server"></asp:Label>Mode </span>
            <br />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="7" valign="top">
            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="M1" runat="server"
                ShowMessageBox="True" ShowSummary="False" />
            <br />
        </td>
    </tr>
    <tr>
        <td class="tdRight" style="width: 17%">
            *Employee Code :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtEmployeeCode" TabIndex="1" runat="server" Width="160px" CssClass="SmallFont TextBox"
                MaxLength="25" ReadOnly="false" ValidationGroup="M1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmployeeCode"
                ErrorMessage="Employee code can't be blanks" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
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
        <td class="tdRight" style="width: 15%">
            *Cadder Code:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlCadderCode" runat="server" Width="164px" CssClass="SmallFont TextBox UpperCase"
                ValidationGroup="M1" TabIndex="2">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCadderCode"
                ErrorMessage="*Pls enter cadder code" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight" style="width: 15%">
            *Card No:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtCardNo" TabIndex="3" runat="server" Width="160px" CssClass="SmallFont TextBox"
                TextMode="singleLine" MaxLength="10" ValidationGroup="M1"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCardNo"
                ErrorMessage="*Pls enter card no" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td rowspan="6" class="Imgdiv" valign="top">
            <img id="IMG_USER" runat="server" class="gCtrTxt" src="../../../CommonImages/BlankImage.jpeg"
                height="120" width="130" border="0" />
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            *User Name :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtUserName" TabIndex="4" runat="server" Width="160px" CssClass="SmallFont TextBox"
                MaxLength="50" ValidationGroup="M1" AutoCompleteType="disabled"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUserName"
                ErrorMessage="*Pls enter user name" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            *Password:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtPassword" TabIndex="5" runat="server" Width="160px" CssClass="SmallFont TextBox"
                TextMode="Password" MaxLength="10" AutoCompleteType="disabled" ValidationGroup="M1"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="*Pls enter password " ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            Salutation:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlsalutation" runat="server" CssClass="SmallFont TextBox UpperCase"
                Width="164px" ValidationGroup="M1" TabIndex="6">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            *First Name :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtFirstName" TabIndex="7" runat="server" Width="160px" CssClass="SmallFont TextBox"
                TextMode="SingleLine" MaxLength="100" ValidationGroup="M1"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="*Pls enter first name" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            Middle Name :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtMiddleName" TabIndex="8" runat="server" Width="160px" CssClass="SmallFont TextBox"
                TextMode="SingleLine" MaxLength="100" ValidationGroup="M1"></asp:TextBox>
        </td>
        <td class="tdRight">
            Last Name :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtLastName" runat="server" Width="160px" CssClass="SmallFont TextBox"
                MaxLength="100" ValidationGroup="M1" TabIndex="9"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            *Email-ID :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtEmailId" TabIndex="10" runat="server" Width="160px" ValidationGroup="M1"
                CssClass="SmallFont TextBox" MaxLength="100"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEmailId"
                ErrorMessage="*Email Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailId"
                ErrorMessage="*Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ValidationGroup="M1" Display="None"></asp:RegularExpressionValidator>
        </td>
        <td class="tdRight">
            Dear:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtDear" TabIndex="120" runat="server" Width="160px" ValidationGroup="M1"
                CssClass="SmallFont TextBox" MaxLength="100"></asp:TextBox>
        </td>
        <td class="tdRight">
            Gender :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="radGender" runat="server" CssClass="SmallFont TextBox UpperCase"
                Width="164px" TabIndex="11">
                <asp:ListItem Value="MALE">MALE</asp:ListItem>
                <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Nationality :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLNationality" runat="server" Width="164px" CssClass="SmallFont TextBox UpperCase"
                TabIndex="12">
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            *Date Of Birth :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtDateOfBirth" runat="server" Width="160px" CssClass="SmallFont TextBox UpperCase"
                MaxLength="10" ValidationGroup="M1" TabIndex="13"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDateOfBirth"
                ErrorMessage="*DOB Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
            <cc1:CalendarExtender ID="a" runat="server" Format="dd/MM/yyyy" PopupPosition="Right"
                TargetControlID="txtDateOfBirth">
            </cc1:CalendarExtender>
        </td>
        <td class="tdRight">
            *F/H Name :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtFatherName" TabIndex="14" runat="server" Width="160px" CssClass="SmallFont TextBox"
                TextMode="singleLine" MaxLength="150" ValidationGroup="M1" Rows="100"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFatherName"
                ErrorMessage="*Father / Husband Name Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Relation :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLRelation" CssClass="SmallFont TextBox UpperCase" runat="server"
                Width="164px" TabIndex="15">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="DDLRelation"
                ErrorMessage="*Father / Husband Name Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            Marital Status :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLMarital_Status" CssClass="SmallFont TextBox" runat="server"
                Width="164px" TabIndex="16">
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            Religion :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLReligion" runat="server" Width="164px" CssClass="SmallFont TextBox"
                TabIndex="17">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            *Grade :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlGrade" runat="server" Width="164px" CssClass="SmallFont TextBox"
                AppendDataBoundItems="True" ValidationGroup="M1" TabIndex="18">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlGrade"
                ErrorMessage="*Grade Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            *Shift :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlShiftName" runat="server" Width="164px" CssClass="SmallFont TextBox"
                AppendDataBoundItems="True" ValidationGroup="M1" TabIndex="19">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlShiftName"
                ErrorMessage="*Shift Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            UploadImage :
        </td>
        <td class="tdLeft">
          <asp:FileUpload ID="imgUpload" runat="server" onchange="func1();" /> 
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Weeekly Off:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlWeeeklyOff" runat="server" CssClass="SmallFont TextBox"
                Width="164px">
                <asp:ListItem Value="0">----------Select-----------</asp:ListItem>
                <asp:ListItem>Sunday</asp:ListItem>
                <asp:ListItem>Monday</asp:ListItem>
                <asp:ListItem>Tuesday</asp:ListItem>
                <asp:ListItem>Wednesday</asp:ListItem>
                <asp:ListItem>Thursday</asp:ListItem>
                <asp:ListItem>Friday</asp:ListItem>
                <asp:ListItem>Saturday</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            Marriage Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtMarriageDate" runat="server" CssClass="SmallFont TextBox" MaxLength="25"
                ReadOnly="false" TabIndex="1" ValidationGroup="M1" Width="160px"></asp:TextBox>
            <cc1:CalendarExtender ID="CE7" runat="server" Format="dd/MM/yyyy" PopupPosition="Right"
                TargetControlID="txtMarriageDate">
            </cc1:CalendarExtender>
        </td>
        <td class="tdRight">
            *Branch Name:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="SmallFont TextBox"
                AppendDataBoundItems="True" TabIndex="21" ValidationGroup="M1" Width="164px">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranchName"
                Display="None" ErrorMessage="*Pls enter branch name" ValidationGroup="M1"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            *Joining Date :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtJoiningDate" TabIndex="25" runat="server" Width="160px" CssClass="SmallFont TextBox"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Joining Date Required"
                ControlToValidate="txtJoiningDate" ValidationGroup="M1" Display="None">
            </asp:RequiredFieldValidator>
            <cc1:CalendarExtender ID="CE6" runat="server" PopupPosition="Right" Format="dd/MM/yyyy"
                TargetControlID="txtJoiningDate">
            </cc1:CalendarExtender>
        </td>
        <td class="tdRight">
            *Department:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="SmallFont TextBox"
                Width="164px" ValidationGroup="M1" AppendDataBoundItems="True" TabIndex="22">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlDepartment"
                ErrorMessage="*Pls enter department" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            Employee Type:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLEMP_Type" runat="server" Width="164px" CssClass="SmallFont TextBox"
                TabIndex="30">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Confirmation Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtConfirmation" runat="server" Width="160px" CssClass="SmallFont TextBox"
                MaxLength="10" ValidationGroup="M1" TabIndex="26"></asp:TextBox>
            <cc1:CalendarExtender ID="CE5" runat="server" PopupPosition="Right" TargetControlID="txtConfirmation"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
        </td>
        <td class="tdRight">
            Last Increment Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtLastIncrement" TabIndex="27" runat="server" Width="160px" CssClass="SmallFont TextBox"
                MaxLength="10"></asp:TextBox>
            <cc1:CalendarExtender ID="CE2" runat="server" PopupPosition="Right" TargetControlID="txtLastIncrement"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
        </td>
        <td class="tdRight">
            Last Promotion Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtLastPromotion" TabIndex="28" runat="server" Width="160px" CssClass="SmallFont TextBox"
                MaxLength="10"></asp:TextBox><br />
            <cc1:CalendarExtender ID="CE1" PopupPosition="Left" runat="server" TargetControlID="txtLastPromotion"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            * Designation:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="ddlDesignation" runat="server" Width="164px" CssClass="SmallFont TextBox"
                ValidationGroup="M1" AppendDataBoundItems="True" TabIndex="29">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlDesignation"
                ErrorMessage="*Designation Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            * Level:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLLevel" runat="server" Width="164px" CssClass="SmallFont TextBox"
                ValidationGroup="M1" AppendDataBoundItems="True" TabIndex="29" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqFieldLevel" runat="server" ControlToValidate="DDLLevel"
                ErrorMessage="*Level Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td class="tdRight">
            * Position:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLPosition" runat="server" Width="164px" CssClass="SmallFont TextBox"
                ValidationGroup="M1" AppendDataBoundItems="True" TabIndex="29" AutoPostBack="true"
                OnSelectedIndexChanged="DDLPosition_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqFieldPosition" runat="server" ControlToValidate="DDLPosition"
                ErrorMessage="*Position Required" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Payment Mode :
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLPayment_Mode" runat="server" Width="164px" CssClass="SmallFont TextBox"
                TabIndex="31">
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            Skills :
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="txtSkill" TabIndex="24" onFocus="doClear(this)" CssClass="SmallFont TextBox"
                Text="(Comma Separated Up to 10)" runat="server" Width="160px" TextMode="MultiLine"
                MaxLength="250" Rows="2" ValidationGroup="M1"></asp:TextBox>
        </td>
        <td class="tdRight">
            Report To(Position):
        </td>
        <td class="tdLeft">
            <asp:Panel ID="PanelReportTo1" runat="server" ScrollBars="None" Height="60px" Width="164px">
                <asp:ListBox ID="LstReportTo1" runat="server" Width="164px" SelectionMode="Multiple"
                    Height="60px" CssClass="SmallFont TextBox" TabIndex="33"></asp:ListBox>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Allow PF:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLIsPF" Width="164px" CssClass="SmallFont TextBox" runat="server">
                <asp:ListItem Value="N">N/A</asp:ListItem>
                <asp:ListItem Value="A">Actual</asp:ListItem>
                <asp:ListItem Value="B">Base Limit</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            Allow ESI:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLIsESI" Width="164px" CssClass="SmallFont TextBox" runat="server">
                <asp:ListItem Value="N">N/A</asp:ListItem>
                <asp:ListItem Value="A">Actual</asp:ListItem>
                <asp:ListItem Value="B">Base Limit</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            *Status:
        </td>
        <td class="tdLeft">
            <asp:DropDownList ID="DDLStatus" Width="164px" CssClass="SmallFont TextBox" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="DDLStatus_SelectedIndexChanged">
                <asp:ListItem Value="A">ACTIVE</asp:ListItem>
                <asp:ListItem Value="T">TERMINATE</asp:ListItem>
                <asp:ListItem Value="L">LEFT</asp:ListItem>
                <asp:ListItem Value="S">SUSPENDED</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr runat="server" visible="false" id="TrEmpStatus">
        <td class="tdRight">
            Leaving Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtLeavingDate" CssClass="SmallFont TextBox" Width="160px" runat="server"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtLeavingDate"
                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
                ErrorTooltipEnabled="True">
            </cc1:MaskedEditExtender>
        </td>
        <td class="tdRight">
            Termination Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtTerminationDate" CssClass="SmallFont TextBox" Width="160px" runat="server"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="TxtTerminationDate"
                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
                ErrorTooltipEnabled="True">
            </cc1:MaskedEditExtender>
        </td>
        <td class="tdRight">
            Suspending Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtSuspendingDate" CssClass="SmallFont TextBox" Width="160px" runat="server"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtSuspendingDate"
                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
                ErrorTooltipEnabled="True">
            </cc1:MaskedEditExtender>
        </td>
    </tr>
    <tr>
        <td id="tdEmpInfo" colspan="6" align="center" runat="server" visible="false">
            <fieldset>
                <asp:LinkButton class='viresh' ID="IMGBtnCompanyInfo" OnClientClick="javascript:return test();"
                    runat="server">CompanyInfo</asp:LinkButton>| |
                <asp:LinkButton ID="ImgBtnMedicalDetail" class='viresh1' OnClientClick="javascript:return test();"
                    runat="server">MedicalDetail</asp:LinkButton>| |
                <asp:LinkButton ID="ImgBtnLeaveDetail" class='viresh2' OnClientClick="javascript:return test();"
                    runat="server">Leave Assign.Detail</asp:LinkButton>| |
                <asp:LinkButton ID="ImgBtnSalaryDetail" class='viresh3' OnClientClick="javascript:return test();"
                    runat="server">Salary Detail</asp:LinkButton>| |
                <asp:LinkButton ID="ImgBtnFamilyDetail" class='viresh4' OnClientClick="javascript:return test();"
                    runat="server">FamilyDetail</asp:LinkButton>| |
                <asp:LinkButton ID="ImgBtnQualification" class='viresh5' OnClientClick="javascript:return test();"
                    runat="server">Qualification</asp:LinkButton>| |
                <asp:LinkButton ID="LinkButton1" class='viresh6' OnClientClick="javascript:return test();"
                    runat="server">Experience Record</asp:LinkButton>| |
            </fieldset>
        </td>
    </tr>
</table>
<%--  </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlEmployee" EventName="SelectedIndexChanged">
        </asp:AsyncPostBackTrigger>
    </Triggers>
</asp:UpdatePanel>--%>