<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Holiday_Master.ascx.cs" Inherits="Module_HRMS_Controls_Holiday_Master" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
     
     	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
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
                        <td id="tdFind" align="left" Width="48px" runat="server">
                            <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_Find.png"
                                Width="48" Height="41" OnClick="imgbtnFind_Click" TabIndex="34"></asp:ImageButton>
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
                            <asp:TextBox ID="txtYear" runat="server" Width="50px" CssClass="gCtrTxt"      MaxLength="4" TabIndex="1"></asp:TextBox>
                            <asp:DropDownList ID="ddlHoliday" CssClass="gCtrTxt" AutoPostBack="true" 
                                Width="180px"  runat="server" 
                                onselectedindexchanged="ddlHoliday_SelectedIndexChanged">
                            </asp:DropDownList>
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
                            <asp:TextBox ID="txtHoildayDate" runat="server" Width="150px" CssClass="gCtrTxt" MaxLength="25"
                                ValidationGroup="M1" TabIndex="4"></asp:TextBox>&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHoildayDate"
                                Display="Dynamic" ErrorMessage="Field Can't be Empty" ValidationGroup="M1"></asp:RequiredFieldValidator>                           
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHoildayDate"
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
             <tr>
                <td colspan="3">
                     <table >
                <tr>
                    <td align="left" valign="top" class="td" colspan="2">
                        <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False"
                            PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select"> 
                            <Columns>
                                <cc2:Column DataField="HLD_I" HeaderText="Holiday CODE" visible="false" Width="100px">
                                </cc2:Column>
                                <cc2:Column DataField="HLD_NAME" HeaderText="Holiday Name" Width="200px">
                                </cc2:Column>
                                <cc2:Column DataField="YEAR" HeaderText="Year" Width="150px">
                                </cc2:Column>
                                   <cc2:Column DataField="HLD_DATE" HeaderText="Holiday Date" Width="150px">
                                </cc2:Column>
                                <cc2:Column DataField="OPT_LV" HeaderText="Optional Leave" Width="150px">
                                </cc2:Column>
                       
                            </Columns>
                        </cc2:Grid>
                        </td>
                </tr>
               
                </table>   
                </td>
             </tr>
                 
    </table>
        </td>
        </tr>
        </table>
        </ContentTemplate>                   
                </asp:UpdatePanel>