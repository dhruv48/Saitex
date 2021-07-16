<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Hiring.ascx.cs" Inherits="Module_HRMS_Controls_Hiring" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table align="left" width="100%" class="tContentArial">
    <tr>
        <td>
            <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                     <tbody>
                            <tr>
                                <td align="left" class="td" colspan="3">
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" >
                                        <tbody>
                                            <tr>
                                                <td id="tdSave" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnSave"  runat="server" ValidationGroup="M1" 
                                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" onclick="imgbtnSave_Click"></asp:ImageButton>
                                                </td>                                              
                                                <td id="tdUpdate" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ValidationGroup="M1" 
                                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" 
                                                        onclick="imgbtnUpdate_Click" > </asp:ImageButton>
                                                </td>
                                                 <td id="tdFind" runat="server" valign="top">
                                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                                        Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                                                </td>
                                                 <td id="tdDelete" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnDelete"  runat="server" 
                                                         ImageUrl="~/CommonImages/del6.png"  ToolTip="Delete" 
                                                         onclick="imgbtnDelete_Click" ></asp:ImageButton>
                                                </td>
                                                <td id="tdClear" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnClear"  runat="server" 
                                                        ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
                                                        onclick="imgbtnClear_Click" ></asp:ImageButton>
                                                </td>
                                                <td id="tdPrint" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnPrint"  runat="server" ImageUrl="~/CommonImages/link_print.png" ToolTip="Print" ></asp:ImageButton>
                                                </td>
                                                 <td id="tdExit" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnExit"  runat="server" ToolTip="Exit"
                                                        ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" 
                                                         OnClientClick="javascript:return window.confirm('Are you sure you want to Exit')" 
                                                         onclick="imgbtnExit_Click">
                                                    </asp:ImageButton>
                                                </td>
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" ></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableHeader td" align="center" colspan="3">
                                    <b class="titleheading">Employee Hiring Info</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left" colspan="3">
                                    <span class="Mode">You are in
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ValidationGroup="M1" />                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <table width="100%">                                        
                                        <tr>
                                            <td colspan="9">
                                                <b><i>Hiring Details</i></b>
                                            </td>
                                        </tr>
                                        <tr id="trFind" runat="server" >
                                        <td colspan="2"><b><i>Select :</i></b></td>
                                                <td colspan="3" style="vertical-align:middle ">
                                                    <asp:DropDownList ID="DDLHiringRecord" Width="150px" runat="server" 
                                                        AutoPostBack="True" 
                                                        onselectedindexchanged="DDLHiringRecord_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td colspan="2"> <asp:TextBox ID="TxtHireID" Text="NEW" Visible="false"  runat="server" 
                                                    Width="0px"></asp:TextBox></td>
                                            <td>                                                
                                            </td>
                                            <td style="width:150px;"></td>                                           
                                        </tr>
                                        <tr>
                                            <td>
                                                Location</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLLocation" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Candidate Name</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtCandidatename" runat="server" CssClass="gCtrTxt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Breakup of present status</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLBreakup" runat="server" Width="150px">
                                                    <asp:ListItem Value="0">-----------SELECT-----------</asp:ListItem>
                                                    <asp:ListItem Value="O">Open</asp:ListItem>
                                                    <asp:ListItem Value="SL">Short List</asp:ListItem>
                                                    <asp:ListItem Value="OF">Offered</asp:ListItem>
                                                    <asp:ListItem Value="C">Closed</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Department</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLDepartment" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                DRR</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtDRR" runat="server" CssClass="gCtrTxt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Tentative DOJ</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtDOJ" runat="server" Width="150px" CssClass="gCtrTxt"></asp:TextBox>
                                                 <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="TxtDOJ" runat="server"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Position</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="DDLPosition" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Offering Date</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtOfferDate" runat="server" CssClass="gCtrTxt" Width="150px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="TxtOfferDate" runat="server"></cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                Candidate Joined On</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtCanJoinDate" runat="server" CssClass="gCtrTxt" Width="150px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="TxtCanJoinDate" runat="server"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nature Of Vacancy</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RBNOV" runat="server" RepeatDirection="Horizontal" 
                                                    Width="150px">
                                                    <asp:ListItem Selected="True" Value="N">New</asp:ListItem>
                                                    <asp:ListItem Value="R">Replace</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                TTS(In Days)</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtTTS" runat="server" onKeyPress="return checkNumeric(event)" MaxLength="5" Width="150px" CssClass="gCtrTxt"></asp:TextBox>
                                            </td>
                                            <td>
                                                TTF(In Days)</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td><asp:TextBox ID="TxtTTF" runat="server" onKeyPress="return checkNumeric(event)" MaxLength="5" Width="150px" CssClass="gCtrTxt"></asp:TextBox>
                                                </td>
                                        </tr> 
                                        <tr>
                                           <td>
                                                Replacement</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td colspan="6" style="text-align: left">
                                                <asp:CheckBox ID="ChkReplace" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkReplace_CheckedChanged" />
                                            </td>
                                           
                                        </tr>
                                        <tr id="trOldDetail" runat="server" >
                                            <td>
                                                Name Of Person Left</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtOldPerson" runat="server" ReadOnly="true" Width="150px" CssClass="gCtrTxt"></asp:TextBox>
                                            </td>
                                            <td>
                                                Desigination</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                              <asp:DropDownList ID="DDLOldPDesig" runat="server" Width="150px">
                                                </asp:DropDownList>
                                                </td>
                                                <td>
                                                Date Of Exit</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td><asp:TextBox ID="txtDOE" runat="server" ReadOnly="true" Width="150px" CssClass="gCtrTxt"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtDOE" runat="server"></cc1:CalendarExtender>
                                                </td>
                                        </tr> 
                                         <tr>
                                            <td>
                                                Recruitment Type</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                 <asp:RadioButtonList ID="RBReqType" runat="server" RepeatDirection="Horizontal" 
                                                     Width="150px">
                                                    <asp:ListItem Selected="True" Value="I">Internal</asp:ListItem>
                                                    <asp:ListItem Value="E">External</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                Selection Source</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="DDLSelSource" runat="server" Width="150px" 
                                                      >
                                                     <asp:ListItem Value="0">---------SELECT-----------</asp:ListItem>
                                                     <asp:ListItem Value="A">Advertisement</asp:ListItem>
                                                     <asp:ListItem Value="C">Consultant</asp:ListItem>
                                                     <asp:ListItem Value="ER">Emp Referral</asp:ListItem>
                                                     <asp:ListItem Value="JP">Job Portal</asp:ListItem>
                                                     <asp:ListItem Value="CAM">Campus</asp:ListItem>
                                                     <asp:ListItem Value="W">Walking</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Remarks</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td><asp:TextBox ID="TxtRemarks" runat="server" Width="150px" CssClass="gCtrTxt" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr> 
                                    </table>
                                </td>
                            </tr>                            
                     </tbody>
             </table>
        </td>
    </tr>
</table>
<%-- </ContentTemplate>
</asp:UpdatePanel>--%>