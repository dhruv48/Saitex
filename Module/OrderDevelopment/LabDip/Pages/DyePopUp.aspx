<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DyePopUp.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_DyePopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="~/CommonControls/LOV/ItemCodeLOV.ascx" TagName="ItemCodeLOV"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LabDip Submission Dye Name</title>
    <link rel="stylesheet" type="text/css" href="../../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
        function GetRowValue(val,TextBoxId, val1, TextBoxId1) {
            window.opener.document.getElementById(TextBoxId).value = val;
            window.opener.document.getElementById(TextBoxId1).value = val1;
            window.opener.document.forms[0].submit();
            window.close();
        }
    </script>

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
            width: 100px;
        }
        .c2
        {
            margin-left: 4px;
            width: 250px;
        }
    </style>
</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="false" ValidationGroup="M1" />
                            <strong></strong>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Enter Dye Name And Dose</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <table>
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>LR&nbsp;Number</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Option</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Grey Lot</b></span>
                                    </td>
                                    <td align="left" class="tdCenter SmallFont" valign="top">
                                        <span class="titleheading"><b>Dye&nbsp;Name</b></span>
                                    </td>
                                    <td align="left" class="tdCenter SmallFont" valign="top">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>                                    
                                   <%-- <td align="left" class="tdRight SmallFont" valign="top">
                                        <span class="titleheading"><b>Rate</b></span>
                                    </td>--%>
                                    <td align="left" class="tdRight SmallFont" valign="top">
                                        <span class="titleheading"><b>Dose GM/KG %</b></span>
                                    </td>
                                    <td align="left" class="tdRight SmallFont" valign="top">
                                        <span class="titleheading"><b>Cost</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" width="150px">
                                        <span class="titleheading"><b></b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtLRNo" Width="100px" runat="server" CssClass="TextBox TextBoxDisplay"
                                            ReadOnly="true" TabIndex="1"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtOption" Width="70px" runat="server" CssClass="TextBox TextBoxDisplay"
                                            ReadOnly="true" TabIndex="2"></asp:TextBox>
                                        <br />
                                    </td>
                                     <td align="left" valign="top">
                                        <asp:TextBox ID="txtGeryLot" Width="70px" runat="server" CssClass="TextBox TextBoxDisplay"
                                            ReadOnly="true" TabIndex="2"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top" width="220px">
                                        <cc2:ComboBox ID="ddlItemCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            EnableLoadOnDemand="True" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE"
                                            EmptyText="Select Dye" MenuWidth="400" OnLoadingItems="ddlItemCode_LoadingItems"
                                            OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" EnableVirtualScrolling="true"
                                            OpenOnFocus="true" TabIndex="11" Visible="true" Height="200px" Width="100px">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Code</div>
                                                <div class="header c2">
                                                    Item Description</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("ITEM_CODE")%></div>
                                                <div class="item c2">
                                                    <%# Eval("ITEM_DESC")%></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                        <asp:TextBox ID="cmbItemCode" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                            ReadOnly="true" ValidationGroup="M1" Width="100px"></asp:TextBox>
                                       
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtUOM" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                            ReadOnly="true" ValidationGroup="M1" Width="50px"></asp:TextBox>                                                    
                                    </td>
                                   <%-- <td align="left" valign="top">--%>
                                        <asp:TextBox ID="txtRate" Visible="false" Width="70px" runat="server" CssClass="TextBoxNo" TabIndex="4"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RFRate" runat="server" ValidationGroup="M1" Display="None"
                                            ErrorMessage="Please.. Enter Rate" ControlToValidate="txtRate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RVRate" runat="server" ValidationGroup="M1" Display="None"
                                            ControlToValidate="txtRate" MinimumValue="1" MaximumValue="9999999" ErrorMessage="Please Enter Numeric Only"
                                            Type="Double" SetFocusOnError="true"></asp:RangeValidator>--%>
                                       <%-- <br />
                                    </td>--%>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtDose" Width="70px" runat="server" CssClass="TextBoxNo" AutoPostBack="true"
                                            TabIndex="5" ontextchanged="txtDose_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFDose" runat="server" ValidationGroup="M1" Display="None"
                                            ErrorMessage="Please.. Enter Dose" ControlToValidate="txtDose" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RVDose" runat="server" ValidationGroup="M1" Display="None"
                                            ControlToValidate="txtDose" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Please Enter Numeric Only"
                                            Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtCost" Width="70px" runat="server" CssClass="TextBoxNo TextBoxDisplay"
                                            ReadOnly="true" TabIndex="6"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top" width="120px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="M1" TabIndex="7"
                                            OnClick="btnSave_Click" CssClass="SmallFont" Width="60px" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" OnClick="btnCancel_Click" CssClass="SmallFont" Width="60px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="7" valign="top">
                                        <asp:GridView ID="grdDyeName" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" ShowFooter="true" OnRowCommand="grdDyeName_RowCommand"
                                            Width="98%" TabIndex="9">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Unique&nbsp;ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnique" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LR&nbsp;Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLRNo" runat="server" Text='<%# Bind("LAB_DIP_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOption" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LR_OPTION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Grey Lot">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgreyLot" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GREY_LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dye&nbsp;Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDyeName" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DYE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEM_DESC" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("RATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dose GM/KG %" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                    FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDose" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DOSE","{0:00.000000}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="txtTotalAmt" runat="server" Width="100px"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                    FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCost" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("RECIPE_COST","{0:00.000000}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblCostFooter" runat="server" Width="100px"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" CommandArgument='<%#bind("UNIQUE_ID") %>'
                                                            CommandName="EditTRN" Text="Edit" TabIndex="10" CssClass="SmallFont" Width="50px" />
                                                        <asp:Button ID="btnDelete" runat="server" CommandArgument='<%#bind("UNIQUE_ID") %>'
                                                            CommandName="DeleteTRN" Text="Delete" TabIndex="11" CssClass="SmallFont" Width="50px"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
