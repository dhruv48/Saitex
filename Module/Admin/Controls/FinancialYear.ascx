<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FinancialYear.ascx.cs"
    Inherits="Admin_UserControls_FinancialYear" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>


<style type="text/css">
    .style1
    {
        height: 46px;
    }
</style>


<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial" width = "90%">
            <tr>
                <td class="td">
                    <table align="left">
                        <tr>
                            <td valign="top" align="center" >
                                <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server" ValidationGroup="FA"
                                    ToolTip="Save" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click" onclientclick="if (!confirm('Are you want to Save ?')) { return false; }" />

                                
                            </td>
                            <td valign="top" align="center" >
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" onclientclick="if (!confirm('Are you want to print ?')) { return false; }" />

                            </td>
                            <td valign="top" align="center" >
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" onclientclick="if (!confirm('Are you want to Clear ?')) { return false; }" />

                            </td>
                            <td valign="top" align="center" >
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" onclientclick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }" />

                            </td>
                            <td valign="top" align="center" >
                                <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Financial Year Master</b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server">
                        </asp:Label>
                        Mode </span>
                </td>
            </tr>
           <%-- <tr>
                <td align="center" valign="top" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td align="left" valign="top" class="td">
                    <table>
                        <tr>
                            <td align="right" valign="top">
                                Financial Year Code :
                            </td>
                            <td align="left" valign="top" colspan="3">
                                <asp:TextBox ID="txtFinancialYearCode" runat="server" MaxLength="10" ValidationGroup="M1"
                                    Width="165px" CssClass="TextBox UpperCase"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rcd" runat="server" ControlToValidate="txtFinancialYearCode"
                                    ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFinancialYearCode"
                                    Display="None" ErrorMessage="Please Enter Year Code" SetFocusOnError="True" ValidationGroup="FA"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="150px">
                                Selected Company :
                            </td>
                            <td align="left" valign="top" width="200px">
                                <cc2:OboutDropDownList ID="ddlcompany" runat="server" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"
                                    AppendDataBoundItems="True" AutoPostBack="True" Height="100px" 
                                    MenuWidth="255px" Width="170px">
                                </cc2:OboutDropDownList>
                                <asp:RequiredFieldValidator ID="rc" runat="server" ControlToValidate="ddlcompany"
                                    ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcompany"
                                    Display="None" ErrorMessage="Please Select Company" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="FA"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        <td style="text-align: right" width="150px">Selected Branch :
                            </td><td>
                                <cc2:OboutDropDownList  ID="ddlbranch" runat="server" Height="100px" 
                                    MenuWidth="255px" Width="170px">
                                    <asp:ListItem>------Select-------</asp:ListItem>
                                </cc2:OboutDropDownList >
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlbranch"
                                    Display="None" ErrorMessage="Please Select Branch " InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="FA" Width="165px"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                                </cc1:ValidatorCalloutExtender>
                                </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="150px">
                                Start Date :
                            </td>
                            <td align="left" valign="top" width="200px">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="165px" 
                                    ValidationGroup="M1" AutoPostBack="True" 
                                    ontextchanged="txtStartDate_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="r1" runat="server" ControlToValidate="txtStartDate"
                                    ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartDate"
                                    Display="None" ErrorMessage="Please Select Start Date" SetFocusOnError="True"
                                    ValidationGroup="FA" Width="1px"></asp:RequiredFieldValidator>
                            </td>
                        <td style="text-align: right" width="150px">End Date :
                            </td><td>
                                <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="M1" Width="165px" 
                                    style="text-align: left" Enabled="False" CssClass="TextBoxDisplay "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="re" runat="server" ControlToValidate="txtEndDate"
                                    ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEndDate"
                                    Display="None" ErrorMessage="Please  Select Start Date.." 
                                    SetFocusOnError="True" ValidationGroup="FA" Width="165px"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator5">
                                </cc1:ValidatorCalloutExtender>
                                </td>
                        </tr>
                         <tr>
                            <td align="right" valign="top" >
                                Financial YearDescription :
                            </td>
                            <td align="left" valign="top"  colspan="3">
                                <asp:TextBox ID="txtFinancialDesc" runat="server"
                                    ValidationGroup="M1" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Status :
                            </td>
                            <td align="left" valign="top" colspan="3">
                                <asp:CheckBox ID="chkStatus" runat="server" ValidationGroup="M1" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="right" valign="top" colspan="2">
                            </td>
                        </tr>--%>
                    </table>
                    <cc1:CalendarExtender ID="c1" runat="server" TargetControlID="txtStartDate" 
                     Format="dd/MM/yyyy"    PopupPosition="TopLeft">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                      Format="dd/MM/yyyy"  TargetControlID="txtEndDate" PopupPosition="TopLeft">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                        MaskType="Date" PromptCharacter="_" TargetControlID="txtStartDate">
                    </cc1:MaskedEditExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                        MaskType="Date" PromptCharacter="_" TargetControlID="txtEndDate">
                    </cc1:MaskedEditExtender>
                </td>
                <tr><td class ="td">
                    <cc2:Grid ID="grdFinancialYear" runat="server" AutoGenerateColumns="False" 
                            OnRowCommand="grdFinancialYear_RowCommand" Width="100%" >
                          
                            <Columns>
                                <cc2:Column DataField="COMP_NAME" HeaderText="Company" Width = "190px" 
                                    Wrap="True" />
                                <cc2:Column DataField="BRANCH_NAME" HeaderText="Branch" Width = "130px" 
                                    Wrap="True"  />
                                <cc2:Column DataField="FIN_YEAR_CODE" HeaderText="Code" Width = "85px" 
                                    Wrap="True"  />
                                <cc2:Column DataField="FIN_DESC" HeaderText="Description" Width = "130px" 
                                    Wrap="True"  />
                                <cc2:Column DataField="START_DATE" HeaderText="Start Date"  
                                    dataformatstring="{0:MMMM d, yyyy}"   Width = "110px" Wrap="True" />
                                <cc2:Column DataField="END_DATE" HeaderText="End Date" 
                                    dataformatstring="{0:MMMM d, yyyy}" Width = "110px" Wrap="True" />
                                <cc2:Column DataField="STATUS" HeaderText="Status" Width = "75px" Wrap="True" />
                              
                            </Columns>
                        </cc2:Grid></td></tr>
            </tr>
        </table>
   <%-- </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnSave" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>--%>
