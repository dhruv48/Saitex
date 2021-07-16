<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Holiday.aspx.cs" Inherits="HRMS_Holiday" Title="Holiday List" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
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
        width: 200px;
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

    
    
    <table cellpadding="0" cellspacing="0" border="0" class="tContentArial" id="tblMainTable" runat="server" width="100%">
        <tr>
            <td align="left">
                <table cellpadding="0" cellspacing="0" border="0" width="250" align="left" class="tContentArial">
                    <tr>
                        <td id="tdSave" align="left" Width="48px" runat="server" >
                            <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                Width="48px" Height="40px" ValidationGroup="M1" OnClick="imgbtnSave_Click"></asp:ImageButton>
                        </td>
                        <td id="tdUpdate" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                        </td>
                        <td id="tdDelete" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                Width="48" Height="41" 
                            OnClick="imgbtnDelete_Click"></asp:ImageButton>
                        </td>
                        
                        <td id="tdFind" runat="server" width="48">
                            <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_Find.png"
                                Width="48" Height="41" OnClick="imgbtnFind_Click" TabIndex="34"></asp:ImageButton>&nbsp;
                        </td>
                        <td id="tdClear" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                Width="48" Height="41" 
                                OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td id="tdPrint" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                Width="48" Height="41" OnClick="imgbtnPrint_Click"  ></asp:ImageButton>
                        </td>
                        <td id="tdExist" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                Width="48" Height="41" OnClick="imgbtnExit_Click" ></asp:ImageButton>
                        </td>
                        <td id="tdHelp" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                Width="48" Height="41" OnClick="imgbtnHelp_Click" ></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="0" width="600" class="tContentArial tablebox">
                    <tr>
                        <td align="center" colspan="8" valign="top" class="TableHeader">
                            <span class="titleheading">Holiday Master</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="6" style="width: 597px">
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3" valign="top">
                            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" align="right" valign="top">
                            Year</td>
                        <td width="2%" align="center" valign="top">
                            <b>:</b></td>
                        <td width="58%" align="left" valign="top">
                            <asp:TextBox ID="txtYear" runat="server" Width="50px" CssClass="gCtrTxt" 
                                MaxLength="4" TabIndex="1"></asp:TextBox>
                             &nbsp;&nbsp;
                             <%--<asp:LinkButton ID="lnkFind" runat="server" Text="Find" OnClick="lnkFind_Click"></asp:LinkButton>
                             <br />--%>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtYear"
                                ErrorMessage="Year Can't be Previous Year" MaximumValue="2100" MinimumValue="2010"
                                ValidationGroup="M1"></asp:RangeValidator>
                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtYear"
                                ErrorMessage="Pls enter year" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="40%">
                            *Optional Leave
                        </td>
                        <td align="center" valign="top" width="2%">
                            <b>:</b>
                        </td>
                        <td align="left" valign="top" width="58%">
                            <asp:RadioButtonList ID="radLeaveSelection" runat="server" RepeatColumns="2" 
                                RepeatDirection="Horizontal" TabIndex="2">
                                <asp:ListItem Text="Optional Leave" Value="O" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Compulsory Leave" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radLeaveSelection"
                                ErrorMessage="Pls select leave type" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="40%">
                            *Hoilday Name
                        </td>
                        <td align="center" valign="top" width="2%">
                            <b>:</b>
                        </td>
                        <td align="left" valign="top" width="58%">
                            <asp:TextBox ID="txtHoildayName" runat="server" Width="200px" CssClass="UpperCase"
                                ValidationGroup="M1" MaxLength="100" TabIndex="3"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHoildayName"
                                Display="Dynamic" ErrorMessage="Field Can't be Empty" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHoildayName"
                                Display="Dynamic" ErrorMessage="Pls Enter String" Font-Bold="False" ValidationExpression="^\s*[a-zA-Z ,.\s]+\s*$"
                                ValidationGroup="M1"></asp:RegularExpressionValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHoildayName"
                                ErrorMessage="Pls enter holiday name" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="40%">
                            *Date
                        </td>
                        <td align="center" valign="top" width="2%">
                            <b>:</b>
                        </td>
                        <td align="left" valign="top" width="58%">
                            <asp:TextBox ID="txtHoildayDate" runat="server" Width="80px" CssClass="gCtrTxt" MaxLength="25"
                                ValidationGroup="M1" TabIndex="4"></asp:TextBox>&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHoildayDate"
                                Display="Dynamic" ErrorMessage="Field Can't be Empty" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtHoildayDate"
                                ErrorMessage="Invalid Date" MaximumValue="12/31/2010" MinimumValue="01/01/2010"
                                Type="Date" ValidationGroup="M1"></asp:RangeValidator>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtHoildayDate"
                                PopupButtonID="ImageButton1" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" align="right" valign="top">
                            Status</td>
                        <td width="2%" align="center" valign="top">
                            <b>:</b></td>
                        <td width="73%" align="left" valign="top">
                            <asp:CheckBox ID="chkActive" runat="server" TabIndex="5" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="6" align="left" width="100%" >
                            <br />
                            <br />
                            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                            </b>
                        </td>
                    </tr>--%>
                  <%--  <tr>
                        <td colspan="6" align="center" width="100%">
                            <br />
                            <asp:GridView ID="gvHoliday" runat="server" PagerSettings-Mode="Numeric" PagerSettings-Position="Bottom" PageSize="10"
                                CssClass="tContentArial"  Width="100%" PagerStyle-HorizontalAlign="Left" AutoGenerateColumns="False"
                                AllowPaging="True" OnRowCommand="gvHoliday_RowCommand"  OnPageIndexChanging="gvHoliday_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField HeaderText="HolydayName" DataField="VC_HOLIDAYNAME">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Date" DataField="VC_DATE">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Year" DataField="CH_YEAR">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" Text="Edit"
                                                CommandArgument='<%# Eval("IN_HOLIDAYID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    
    
    <obout:ComboBox runat="server" ID="ddlHoliday" Width="200" Height="150"
	    DataTextField="HLD_NAME" DataValueField="HLD_ID"
	    EnableLoadOnDemand="true" OnLoadingItems="ddlHoliday_LoadingItems" 
        AutoPostBack="True"  onselectedindexchanged="ddlHoliday_SelectedIndexChanged" MenuWidth="200px"
	    >
	    <HeaderTemplate>
	        
	        <div class="header c2">Holiday Name</div>
	        <%--<div class="header c3">Holiday Type</div>
	        <div class="header c3">Holiday Date</div>
	        <div class="header c3">Holiday Year</div>--%>
	    </HeaderTemplate>
	    <ItemTemplate>
	        <div class="item c1"><%# Eval("HLD_NAME")%></div>
	        <%--<div class="item c2"><%# Eval("HLD_Type")%></div>
	        <div class="item c2"><%# Eval("HLD_DATE")%></div>
	        <div class="item c3"><%# Eval("YEAR")%></div>--%>
	    </ItemTemplate>
	    <FooterTemplate>
            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %> out of <%# Container.ItemsCount %>.
        </FooterTemplate>
	</obout:ComboBox>
</asp:Content>
