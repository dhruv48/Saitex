<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/UserMaster.master"  CodeFile="CreateCompany.aspx.cs" Inherits="Module_StartUp_CreateCompany" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
<style type="text/css">
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="tContentArial">
    <tr>
        <td align="left" class="td">
            <table cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server" width="48">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="M1" Width="48" OnClientClick="if (!confirm('Are you want to Save record ?')) { return false; }" />
                    </td>
                    <td id="tdUpdate" runat="server" width="48">
                   
                    </td>
                    <td id="tdFind" runat="server" width="48">
                       
                    </td>
                    <td width="48">
                         </td>
                    <td width="48">
                         </td>
                    <td width="48">
                        
                    </td>
                    <td style="width: 48px">
                       
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <span class="titleheading">Company Master</span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"> </asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td align="left" class="td" colspan="4">
                        <table>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    *Company Code :
                                </td>
                                <td align="left" valign="top" colspan="1" width="280px">
                                    <asp:TextBox ID="txtCompanyCode" runat="server" AutoCompleteType="disabled" CssClass="TextBox UpperCase"
                                        MaxLength="10" TabIndex="1" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RValidateCompCode" runat="server" ControlToValidate="txtCompanyCode"
                                        Display="None" ErrorMessage="*Enter Company Code" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" colspan="2" rowspan="2" valign="top" style="text-align: center">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Image ID="previewField" runat="server" Height="77px" Width="164px" onchange="PreviewImg(this);"
                                        onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown=""
                                        ImageUrl="~/CommonImages/No-photo-Available.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    *Company Name :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="TextBox UpperCase" TabIndex="2"
                                        Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFCompName" runat="server" ControlToValidate="txtCompanyName"
                                        Display="None" ErrorMessage="*Enter Company Name" ValidationGroup="M1" Width="106px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    * Group Name :
                                </td>
                                <td align="left" valign="top" width="280px">
                                    <cc2:OboutDropDownList ID="ddlGroupName" runat="server" CssClass="gCtrTxt" RepeatColumns="4"
                                        RepeatDirection="horizontal" RepeatLayout="flow" TabIndex="3" Width="150px">
                                    </cc2:OboutDropDownList>
                                    <asp:RequiredFieldValidator ID="RFGrpName" runat="server" ControlToValidate="ddlGroupName"
                                        Display="None" ErrorMessage="*Select Group Name" ValidationGroup="M1">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td align="left" valign="top" width="150px" style="text-align: right">
                                    &nbsp;Select Image:
                                </td>
                                <td align="left" valign="top">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="TextBox" TabIndex="4" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                                        Display="None" ErrorMessage="Please Upload  Company Image" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1"
                                        Display="None" ErrorMessage="Invalid File Type(Support Only Jpg |Bmp |Gif Extension))"
                                        ValidationExpression="[a-zA-Z0_9].*\b(.jpeg|.JPEG|.jpg|.JPG|.bmp|.BMP|.gif|.GIF)\b"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="td" colspan="4">
                        <table width="100%">
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    *Address :
                                </td>
                                <td align="left" colspan="3" valign="top">
                                    <asp:TextBox ID="txtCompAddress" runat="server" CssClass="gCtrTxt" Rows="2" TabIndex="4"
                                        ValidationGroup="M1" Width="590px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFCompAddress" runat="server" ControlToValidate="txtCompAddress"
                                        Display="None" ErrorMessage="Please Enter Address" ValidationGroup="M1">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    *State :
                                </td>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCompState" runat="server" CssClass="gCtrTxt" MaxLength="150"
                                        TabIndex="5" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFCompState" runat="server" ControlToValidate="txtCompState"
                                        Display="None" ErrorMessage="*Please Enter Company State" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    *Country :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompCountry" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="6" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFCountry" runat="server" ControlToValidate="txtCompCountry"
                                        Display="None" ErrorMessage="*Please Enter Country Name of the Company " ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    * Phone Number :
                                </td>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtComPhoneNo" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="7" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFCompPhoneNo" runat="server" ControlToValidate="txtComPhoneNo"
                                        Display="None" ErrorMessage="*Please Enter Company Phone No" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    Fax Number :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompFaxNo" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="8" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    *E-Mail Id :
                                </td>
                                <%--<td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCompEmailId" runat="server" MaxLength="200" TabIndex="9" TextMode="singleLine"
                                        ValidationGroup="M1" Width="150px" CssClass="TextBox "></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFCompEMailId" runat="server" ControlToValidate="txtCompEmailId"
                                        Display="None" ErrorMessage="*Please Enter Company eMail Id " ValidationGroup="M1"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;
                                    <asp:RegularExpressionValidator ID="RECompEMailId" runat="server" ControlToValidate="txtCompEmailId"
                                        Display="None" ErrorMessage="Please Enter a vaild Email Id !" Font-Bold="False"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="M1"></asp:RegularExpressionValidator>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    Company PAN Number :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompPANNumber" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="10" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="td" colspan="4">
                        <table>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    Company CST Number :
                                </td>
                                <%-- <td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCompCSTNumber" runat="server" CssClass="gCtrTxt" MaxLength="150"
                                        TabIndex="11" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    Company CST Date :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompCSTDate" runat="server" CssClass="gCtrTxt" MaxLength="150"
                                        TabIndex="12" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtCompCSTDate" PromptCharacter="_">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    Company TIN Number :
                                </td>
                                <%--<td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCompanyTINNumber" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="13" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    Company TIN Date :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompanyTINDate" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="14" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtCompanyTINDate" PromptCharacter="_">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    Company ESI Number :
                                </td>
                                <%--<td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCompESINumber" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="15" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    Company ESI Date :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompESIDate" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="16" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtCompESIDate" PromptCharacter="_">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    Company Service TAX Number :
                                </td>
                                <%--<td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCompServiceTaxNo" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="17" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    Company Service TAX Date :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompServiceTaxDate" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="18" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtCompServiceTaxDate" PromptCharacter="_">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                                <tr>
                                <td align="right" valign="top" width="155px">
                                    ECC No:</td>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtEccNo" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="17" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="150px">
                                    TAN No:</td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtTanNo" runat="server" CssClass="gCtrTxt" MaxLength="10"
                                        TabIndex="18" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    CIN No:</td>
                                <td align="left" valign="top" width="280px">
                                    <asp:TextBox ID="txtCIN" runat="server" CssClass="gCtrTxt" MaxLength="200"
                                        TabIndex="19" TextMode="singleLine" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="150px">
                                  Remarks : </td>
                                <td align="left" valign="top">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="gCtrTxt" Rows="2" TabIndex="20"
                                        ValidationGroup="M1" Width="150px"></asp:TextBox>
                                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="td" colspan="4">
                        <table>
                           
                            <tr>
                                <td align="right" valign="top" width="155px">
                                    Company History :
                                </td>
                                <%--<td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompanyHistory" runat="server" CssClass="gCtrTxt" Rows="2" TabIndex="22"
                                        ValidationGroup="M1" Width="592px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" class="style2" width="155px">
                                    Status :
                                </td>
                                <%-- <td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                                <td align="left" style="height: 8px" valign="top">
                                    <asp:CheckBox ID="chk_Status" runat="server" TabIndex="23" />
                                    <asp:TextBox ID="TxtImagepath" Visible="false"  runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4" width="100%" class="td">
                        <b>Total Record :<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4" valign="top" class="td">
                        <br />
                        <cc2:Grid ID="gvCompMaster" runat="server" AllowPaging="true" AllowSorting="true"
                            AutoGenerateColumns="false" BorderWidth="1px" CssClass="tContentArial" 
                            PageSize="10" >
                            <Columns>
                                <cc2:Column DataField="COMP_CODE" HeaderText="Company Code" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top" Width="150px" />
                                <cc2:Column DataField="GRP_NAME" HeaderText="Group Name" ItemStyle-HorizontalAlign="left"
                                    Width="150px" ItemStyle-VerticalAlign="top" />
                                <cc2:Column DataField="COMP_NAME" HeaderText="Company Name" ItemStyle-HorizontalAlign="left"
                                    Width="200px" ItemStyle-VerticalAlign="top" />
                                <cc2:Column DataField="COMP_STATE" HeaderText="Company State" ItemStyle-HorizontalAlign="center"
                                    Width="150px" ItemStyle-VerticalAlign="top" />
                                <cc2:Column DataField="COMP_PHONE_NO" HeaderText="Comapny Phone No" ItemStyle-HorizontalAlign="left"
                                    Width="100px" ItemStyle-VerticalAlign="top" />
                                <cc2:Column DataField="DEL_STATUS" HeaderText="Delete Status" ItemStyle-HorizontalAlign="left"
                                    Width="100px" ItemStyle-VerticalAlign="top" />
                           
                            </Columns>                          
                        </cc2:Grid>
                        <calendarextender id="ComCSTDATE" runat="server" targetcontrolid="txtCompCSTDate">
                                    </calendarextender>
                        <validatorcalloutextender id="CalOutCompCode" runat="server" targetcontrolid="RValidateCompCode">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutGrpName" runat="server" targetcontrolid="RFGrpName">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutCompNAme" runat="server" targetcontrolid="RFCompName">
                                    </validatorcalloutextender>
                        <calendarextender id="CompTINDATE" runat="server" targetcontrolid="txtCompanyTINDate">
                                    </calendarextender>
                        <validatorcalloutextender id="CalOutCompAddress" runat="server" targetcontrolid="RFCompAddress">
                                    </validatorcalloutextender>
                        <calendarextender id="CompESIDate" runat="server" targetcontrolid="txtCompESIDate">
                                    </calendarextender>
                        <validatorcalloutextender id="CalOutState" runat="server" targetcontrolid="RFCompState">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutCountry" runat="server" targetcontrolid="RFCountry">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutPhoneNo" runat="server" targetcontrolid="RFCompPhoneNo">
                                    </validatorcalloutextender>
                        <calendarextender id="CompServiceTaxDate" runat="server" targetcontrolid="txtCompServiceTaxDate">
                                    </calendarextender>
                        <calendarextender id="CompFinanceFrom" runat="server" targetcontrolid="txtComFinicialYearFrom">
                                    </calendarextender>
                        <calendarextender id="CompFinanceTo" runat="server" targetcontrolid="txtCompFinicialYearTo">
                                    </calendarextender>
                        <autocompleteextender id="Find" runat="server" completioninterval="1000" minimumprefixlength="1"
                            servicemethod="GetCompanyName" servicepath="../AutoComplete.asmx" targetcontrolid="txtFind"
                            usecontextkey="true">
                                    </autocompleteextender>
                        <validatorcalloutextender id="CalOutRFEmailId" runat="server" targetcontrolid="RFCompEMailId">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutREEmailId" runat="server" targetcontrolid="RECompEMailId">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutCompFYRFrom" runat="server" targetcontrolid="RFCompFYrFrom">
                                    </validatorcalloutextender>
                        <validatorcalloutextender id="CalOutFYRTo" runat="server" targetcontrolid="RFCompFYrTo">
                                    </validatorcalloutextender>
                        &nbsp; &nbsp;&nbsp;<asp:HiddenField ID="HF1" runat="server" />
                        <cc1:ValidatorCalloutExtender ID="CalOutCompCode2" runat="server" TargetControlID="RegularExpressionValidator1">
                        </cc1:ValidatorCalloutExtender>
                        &nbsp;<cc1:CalendarExtender ID="ComCSTDATE0" runat="server" TargetControlID="txtCompCSTDate">
                        </cc1:CalendarExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutCompCode1" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutCompCode0" runat="server" TargetControlID="RValidateCompCode">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutGrpName0" runat="server" TargetControlID="RFGrpName">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutCompNAme0" runat="server" TargetControlID="RFCompName">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:CalendarExtender ID="CompTINDATE0" runat="server" TargetControlID="txtCompanyTINDate">
                        </cc1:CalendarExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutCompAddress0" runat="server" TargetControlID="RFCompAddress">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:CalendarExtender ID="CompESIDate0" runat="server" TargetControlID="txtCompESIDate">
                        </cc1:CalendarExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutState0" runat="server" TargetControlID="RFCompState">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutCountry0" runat="server" TargetControlID="RFCountry">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutPhoneNo0" runat="server" TargetControlID="RFCompPhoneNo">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:CalendarExtender ID="CompServiceTaxDate0" runat="server" TargetControlID="txtCompServiceTaxDate">
                        </cc1:CalendarExtender>
                        <%--<td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                        <%-- <td valign="top" align="center" width="2%">
                                <b>:</b></td>--%>
                        <cc1:ValidatorCalloutExtender ID="CalOutRFEmailId0" runat="server" TargetControlID="RFCompEMailId">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:ValidatorCalloutExtender ID="CalOutREEmailId0" runat="server" TargetControlID="RECompEMailId">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
         </table>
          </td>
    </tr>
</table>
                     
</asp:Content>
