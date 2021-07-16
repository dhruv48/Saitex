<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShadeFamilyTrn.ascx.cs" Inherits="Module_OrderDevelopment_Controls_ShadeFamilyTrn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>

<style type="text/css">

.SmallFont
{
	font-size:8pt;
	margin-left: 1px;
    width: 39px;
}

.TextBox
{
	font-family: Arial, Arial, Helvetica, sans-serif;
	font-size: 12px;
	border-style: solid;
	border-color: #C1D3FB;
	text-align: left;
	vertical-align: top;
}
    .style1
    {
        height: 25px;
    }
    .style2
    {
        height: 27px;
    }
</style>
<%--
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%> 
<table align="left" cellpadding="0" cellspacing="0" class="tContentArial" width ="100%">
    <tr>
        <td align="right" class="td" style="text-align: left" valign="top">
            <table cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="ss" 
                            TabIndex="1" />
                    </td>
                     <td id="tdFind" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgfind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgfind_Click" ToolTip="Find" ValidationGroup="M1" Width="48" 
                             TabIndex="2" />
                    </td>
                    <td id="tdUpdate" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="ss" Width="48"
                            
                            
                            OnClientClick="javascript:return window.confirm('Are you sure you want to Update this Form')" 
                            TabIndex="3" />
                    </td>
                    <td align="left" style="height: 46px" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" 
                            OnClientClick="javascript:return window.confirm('Are you sure you want to Clear this Form')" 
                            TabIndex="4" />
                    </td>
                    <td align="left" style="width: 42px; height: 46px">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="43px" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48px" TabIndex="5" />
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" 
                            OnClientClick="javascript:return window.confirm('Are you sure you want to Exit from this Form')" 
                            TabIndex="6" />
                    </td>
                    <td style="width: 48px">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" TabIndex="7" />
                    </td>
                </tr>
            </table>
           
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <b class="tRowColorAdmin">Shade Name Detail</b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" style="height: 16px" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>
                &nbsp;Mode 
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="ss" />
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr >
                    <td align="right" valign="top" width="20%">
                        Product&nbsp;Type </td>
                    <td align="center" valign="top" width="2%">
                        :</td>
                    <td align="left" valign="top" width="78%">
                        <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" 
                            CssClass="SmallFont" 
                            onselectedindexchanged="ddlProductType_SelectedIndexChanged1" TabIndex="8" Width="150px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlProductType" Display="None" 
                            ErrorMessage="Please Select Product Type" InitialValue="0" ValidationGroup="ss"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        <%--Shade&nbsp;Family&nbsp;Name--%> Shade&nbsp;Name</td>
                    <td align="center" valign="top" width="2%">
                        :</td>
                    <td align="left" valign="top" width="78%">
                        <asp:DropDownList ID="ddlShadeFamilyName" runat="server" AutoPostBack="True" 
                            CssClass="SmallFont" 
                            onselectedindexchanged="ddlShadeFamilyName_SelectedIndexChanged" 
                            TabIndex="9" Width="150px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="ddlShadeFamilyName" Display="None" 
                            ErrorMessage="Please Select Shade Family Type" InitialValue="0" 
                            ValidationGroup="ss"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr >
                    <td align="right" valign="top" width="20%">
                        Shade&nbsp;Code</td>
                    <td align="center" valign="top" width="2%">
                        :</td>
                    <td align="left" valign="top" width="78%">
                        <asp:TextBox CssClass=" SmallFont" ID="txtShadecode" 
                            runat="server" AutoPostBack="True" 
                            ontextchanged="txtShadecode_TextChanged" MaxLength="25" TabIndex="10" Width="150px"></asp:TextBox>
                        <asp:DropDownList CssClass="SmallFont" ID="ddlShadeCodes" runat="server" 
                            AutoPostBack="True"  Visible = "false"
                            onselectedindexchanged="ddlShadeCodes_SelectedIndexChanged" TabIndex="11" Width="150px">
                        </asp:DropDownList>
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtShadecode" Display="None" 
                            ErrorMessage="Please Enter Shade Code" ValidationGroup="ss"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%" class="style1">
                        Remarks
                    </td>
                    <td align="center" valign="top" width="2%" class="style1">
                        :
                    </td>
                    <td align="left" valign="top" width="78%" class="style1">
                        <asp:TextBox CssClass="SmallFont" Width ="150px" ID="txtRemarks" runat="server" 
                            MaxLength="50" TabIndex="12"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2" valign="top" width="20%">
                        RGB</td>
                    <td align="center" class="style2" valign="top" width="2%">
                        :</td>
                    <td align="left" class="style2" valign="top" width="78%">
                        <asp:TextBox ID="txtRGB" runat="server" AutoPostBack="true" 
                            CssClass="TextBox SmallFont" MaxLength="11" ontextchanged="txtRGB_TextChanged" 
                            TabIndex="55" Width="74px"></asp:TextBox>
                              <cc11:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
            TargetControlID="txtRGB"
             Mask="999,999,999"
             MessageValidatorTip="true"             
             MaskType="Number"
             InputDirection="LeftToRight"            
             ErrorTooltipEnabled="True" />
                        <asp:TextBox ID="txtRGBColor" runat="server" CssClass="TextBox SmallFont" 
                            Enabled="False" MaxLength="9" TabIndex="17" Width="62px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td id="tdbtn" runat="server" align="center" colspan="3" visible="false">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" class="td">
            <table align="center" border="0" width="100%" cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td align="left" class="td">
                       <%-- <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label
                            ID="lblTotalRecord" runat="server"></asp:Label>
                        </b>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                       <asp:GridView ID="gvShadefamily" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvShadefamily_PageIndexChanging"
                            OnRowCommand="gvShadefamily_RowCommand" 
                            onselectedindexchanged="gvShadefamily_SelectedIndexChanged" 
                            CellPadding="4" ForeColor="#333333" GridLines="None">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                             <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="Product Type" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                             </asp:BoundField>
                             <asp:BoundField DataField="SHADE_FAMILY_NAME" HeaderText="Shade Family Name" ItemStyle-HorizontalAlign="center"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                             </asp:BoundField>
                          
                             <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                             </asp:BoundField>
                             <asp:BoundField DataField="REMARKS" HeaderText="Remakrs" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                              </asp:BoundField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" BackColor="#336799" ForeColor="White" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <br />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" valign="top" visible="false">
            <%--<table id="tdSearch" runat="server" border="1" cellpadding="0" cellspacing="0" 
                    class="tContentArial">
                    <tr>
                        <td align="center" style="width: 100%" valign="top">
                            <b>Search Pannel</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            Active/De-Active</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            Deleted/Not-Deleted</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            <asp:RadioButtonList ID="radDeletedNNotDelted" runat="server" RepeatColumns="1" 
                                RepeatDirection="Horizontal" RepeatLayout="flow" TabIndex="11">
                                <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                <asp:ListItem Value="0">Not-Deleted</asp:ListItem>
                                <asp:ListItem Value="1">Deleted</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 100%" valign="top">
                            <asp:Button ID="btnView" runat="server" CssClass="AButton" 
                                OnClick="btnView_Click" TabIndex="10" Text="View" Width="75px" />
                        </td>
                    </tr>
                </table>--%>
        </td>
    </tr>
</table>
</ContentTemplate>
