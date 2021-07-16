<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MachineStop.aspx.cs" Inherits="Module_Production_Pages_MachineStop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Machine Stopage</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
    }

    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <table class="tContentArial" style="background-color: #afcae4" width="100%">
            <tr>
                <td class="TableHeader td tdCenter" colspan="4">
                    <span class="titleheading">Machine Stopage Details</span>
                </td>
            </tr>
            <tr>
                <td class="tdRight td SmallFont">
                    Machine&nbsp;Code&nbsp;:
                </td>
                <td class="tdLeft td SmallFont">
                    <asp:TextBox ID="txtMachineCode" runat="server" ReadOnly="true" CssClass="SmallFont Textbox Textboxdisplay" Width="150px"></asp:TextBox>
                </td>
                <td class="tdRight td SmallFont">
                    Total&nbsp;Machine&nbsp;Stopage&nbsp;:</td>
                <td class="tdLeft td SmallFont">
                    <asp:TextBox ID="txtTotalMachineStop" runat="server" ReadOnly="true" CssClass="SmallFont TextboxNo Textboxdisplay" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdRight td SmallFont">
                    Lot&nbsp;Load&nbsp;Time&nbsp;:
                </td>
                <td class="tdLeft td SmallFont">
                    <asp:TextBox ID="txtMachineLoadTime" runat="server" ReadOnly="true" CssClass="SmallFont TextboxNo Textboxdisplay" Width="150px"></asp:TextBox>
                </td>
                <td class="tdRight td SmallFont">
                    Lot&nbsp;Unload&nbsp;Time&nbsp;:
                </td>
                <td class="tdLeft td SmallFont">
                    <asp:TextBox ID="txtMachineUnLoadTime" runat="server" ReadOnly="true" CssClass="SmallFont TextboxNo Textboxdisplay" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont" colspan="4">
                    <asp:Label ID="lblValidateStartTime" runat="server" Visible="False"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblValidateEndTime" runat="server" Visible="False"></asp:Label>
                    <asp:HiddenField ID="hfTextBoxId" runat="server" /><asp:HiddenField ID="hfPROS_CODE" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="tdLeft td SmallFont" colspan="4">
                    <table>
                        <tr>
                            <td class="tdLeft">
                                Stopage&nbsp;From
                            </td>
                            <td class="tdLeft">
                                Stopage&nbsp;To
                            </td>
                            <td class="tdRight">
                                Stop&nbsp;Time&nbsp;(Minutes)
                            </td>
                            <td class="tdLeft">
                                Stopage&nbsp;Reason
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtMacStopBeginTime" runat="server"  CssClass="SmallFont TextBox"
                                    OnTextChanged="txtMacStopBeginTime_TextChanged" Width="125px"></asp:TextBox>
                                <cc1:MaskedEditExtender TargetControlID="txtMacStopBeginTime" ID="meeLoadingDate"
                                    runat="server" Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="True" />
                                <cc1:MaskedEditValidator ID="mevLoadingDate" runat="server" ControlExtender="meeLoadingDate"
                                    ControlToValidate="txtMacStopBeginTime" IsValidEmpty="false" InvalidValueMessage="Invalid Loading Date"
                                    Display="Dynamic" ValidationGroup="YM" />
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtMacStopEndTime" runat="server" AutoPostBack="True" CssClass="SmallFont TextBox"
                                    OnTextChanged="txtMacStopEndTime_TextChanged" Width="125px"></asp:TextBox>
                                <cc1:MaskedEditExtender TargetControlID="txtMacStopEndTime" ID="meeUnLoadingDate"
                                    runat="server" Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="true" />
                                <cc1:MaskedEditValidator ID="mevUnLoadingDate" runat="server" ControlExtender="meeUnLoadingDate"
                                    ControlToValidate="txtMacStopEndTime" IsValidEmpty="false" InvalidValueMessage="Invalid Un-Loading Date"
                                    Display="Dynamic" ValidationGroup="YM" />
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtMacStopDuration" runat="server" CssClass="SmallFont TextBox" Width="125px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlMacStopReason" runat="server" CssClass="SmallFont TextBox"
                                    AppendDataBoundItems="True" Width="125px">
                                </asp:DropDownList>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnMacStopSave" runat="server" Text="Add" CssClass="SmallFont" OnClick="btnMacStopSave_Click"  Width="70px"/>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnMacStopCancel" runat="server" Text="Cancel" CssClass="SmallFont"
                                    OnClick="btnMacStopCancel_Click" Width="70px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont" colspan="7" width="100%">
                   <%-- <asp:Panel ID="pnlBOMAdjustment" runat="server" ScrollBars="Vertical" Height="200px" width="100%"
                        BackColor="#afcae4">--%>
                        <asp:GridView ID="grdMachineStopage" runat="server" AutoGenerateColumns="False" OnRowCommand="grdMachineStopage_RowCommand"
                            Width="100%" >
                            <Columns>
                                <asp:TemplateField HeaderText="S No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdUniqueid" runat="server" CssClass="SmallFont" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Begin Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdBeginTime" runat="server" CssClass="SmallFont" Text='<%# Bind("STOP_TIME_BEGIN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdEndTime" runat="server" CssClass="SmallFont" Text='<%# Bind("STOP_TIME_END") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Duration">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdDuration" runat="server" CssClass="SmallFont" Text='<%# Bind("STOP_TIME_DURATION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdReason" runat="server" CssClass="SmallFont" Text='<%# Bind("STOP_REASON") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btngrdDelete" runat="server" Text="Remove" CssClass="SmallFont" CommandName="DelRow"
                                            CommandArgument='<%# Bind("UNIQUE_ID") %>'  Width="65px"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   <%-- </asp:Panel>--%>
                   <asp:Button ID="btnMacStopSubmit" runat="server" Text="Submit" CssClass="SmallFont"
                        OnClick="btnMacStopSubmit_Click" width="100px"/>
                </td>
            </tr>
            
        </table>
        <%--        </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
