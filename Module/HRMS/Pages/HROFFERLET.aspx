<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="HROFFERLET.aspx.cs" Inherits="Module_HRMS_Pages_HROFFERLET" Title="Offer Letter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
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
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 180px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0"  border="0" align="left" class="tContentArial">
                <tr>
                    <td class="td" align="left">
                        <table class="tContentArial" width="100%" cellspacing="0" cellpadding="0" border="1">
                            <tbody>
                                <tr>
                                    <td id="tdSave" width="48" runat="server">
                                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                                    </td>
                                    <td id="tdUpdate" width="48" runat="server">
                                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                            ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                                        </asp:ImageButton>
                                    </td>
                                                                       <td id="tdFind" width="48" runat="server">
                                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                                            ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41"></asp:ImageButton>
                                    </td>
                                    <td width="48" id="tdClear" runat="server">
                                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                                            ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41"></asp:ImageButton>
                                    </td>
                                    <td width="48" id="tdprint" runat="server">
                                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                                            ImageUrl="~/CommonImages/link_print.png" Width="48" Height="41"></asp:ImageButton>
                                    </td>
                                    <td width="48" id="tdExit" runat="server">
                                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" OnClientClick="javascript:return window.confirm('Are you sure you want to Exit from this Form')">
                                        </asp:ImageButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td class="TableHeader td" align="center">
                        <span class="titleheading">HR OFFER LETTER</span>
                    </td>
                </tr>
                <tr>
                    <td class="td">
                        <table class="tContentArial"  cellspacing="0" cellpadding="0" border="1">
                            <tr>
                                <td id="ValidationSummary2" align="center" runat="server" valign="top" visible="false"
                                    colspan="2">
                                    
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="M1"
                                        ShowMessageBox="True" ShowSummary="True"></asp:ValidationSummary>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    OFFER REF_NO :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOFFERR" runat="server"   MaxLength="50"  Width="200px" 
                                        ontextchanged="txtOFFERR_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                        ErrorMessage="Please enter the Offer Ref no" ControlToValidate="txtOFFERR" ValidationGroup="M1"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    OFFER DATE :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOFFERDATE" runat="server" CssClass="SmallFont gCtrTxt" Width="200px" 
                                        TabIndex="2" MaxLength="50"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtOFFERDATE" Format="dd/MM/yyyy" runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                        ErrorMessage="Please enter the Offer Date" ControlToValidate="txtOFFERDATE" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    OFFER NAME :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOFFERName" runat="server" CssClass="SmallFont gCtrTxt" Width="200px"
                                        TabIndex="3" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" runat="server"
                                        ErrorMessage="Please enter the Offer Name" ControlToValidate="txtOFFERName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdLoginId1" runat="server" align="right" valign="top">
                                    ADDRESS1 :
                                </td>
                                <td id="tdLoginId3" runat="server" align="left" valign="top">
                                    <asp:TextBox ID="txtADDRESS1" runat="server" CssClass="SmallFont gCtrTxt" Width="200px" TabIndex="4"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                        ErrorMessage="Please enter the Address" ControlToValidate="txtADDRESS1" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    ADDRESS2 :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtADDRESS2" runat="server" CssClass="SmallFont gCtrTxt" Width="200px" TabIndex="5"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
                                        ErrorMessage="Please enter the Address2" ControlToValidate="txtADDRESS2" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    SUBJECT :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtSUBJECT" runat="server" CssClass="SmallFont gCtrTxt" Width="200px" TabIndex="6"
                                        MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="None" runat="server"
                                        ErrorMessage="Please enter the subject" ControlToValidate="txtSUBJECT" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    OFFER JOING_DATE :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOFFERJOING" runat="server" CssClass="SmallFont gCtrTxt"   Width="200px" TabIndex="7"
                                        MaxLength="100"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtOFFERJOING" Format="dd/MM/yyyy" runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="None" runat="server"
                                        ErrorMessage="Please enter the OfferJoining date" ControlToValidate="txtOFFERJOING"
                                        ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    POSITION :
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="DDLPosition" Width="200px" runat="server" 
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">------------Select------------</asp:ListItem>
                                    </asp:DropDownList>  
                                     <asp:RequiredFieldValidator ID="RQPosition" runat="server" ControlToValidate="DDLPosition"
                                                    ErrorMessage="*Please Select Position" InitialValue="0" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>                                  
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Status :
                                </td>
                                <td align="left" valign="top">
                                    <asp:CheckBox ID="chk_Status" runat="server" TabIndex="9" Checked="True"   />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
         <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtOFFERR" EventName="TextChanged"></asp:AsyncPostBackTrigger>        
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
