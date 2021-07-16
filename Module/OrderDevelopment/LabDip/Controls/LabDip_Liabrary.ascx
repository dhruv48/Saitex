<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LabDip_Liabrary.ascx.cs"
    Inherits="Module_OrderDevelopment_LabDip_Controls_LabDip_Liabrary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../../../StyleSheet/abhishek.css" rel="stylesheet" type="text/css" />
<table width="100%" style="height: 223px" class="ta">
    <tr>
        <td>
            <table class="ta">
                <tr>
                    <td id="tdClear" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                    </td>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" valign="top" class="hd">
                        LabDip Library
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Date range FROM
                    </td>
                    <td align="left" valign="top" width="40%" class="cl">
                        <asp:TextBox ID="TxtFromDate" runat="server" Width="118px"></asp:TextBox>
                        To<asp:TextBox ID="TxtToDate" runat="server" Width="118px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="10%" class="cl">
                        Shade
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:DropDownList ID="ddlshade" runat="server" Width="120px" Height="20px" DataValueField="Shade"
                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlshade_SelectedIndexChanged"
                            TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <td align="right" valign="top" width="25%" class="cl">
                    Depth
                </td>
                <td align="left" valign="top" width="25%" class="cl">
                    <asp:DropDownList ID="ddldepth" runat="server" Width="120px" Height="20px" DataValueField="Depth"
                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddldepth_SelectedIndexChanged"
                        TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td align="center" valign="top">
                </td>
                <td align="left" valign="top">
                    <asp:Button ID="Btnview" runat="server" Text="View In Grid" Width="123px" Height="21px"
                        OnClick="Btnview_Click" />
                </td>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="LABDIPLIBRARY" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            AllowSorting="True" Font-Size="10px" ForeColor="#333333" Width="100%" CellPadding="4"
                            OnSelectedIndexChanged="LABDIPLIBRARY_SelectedIndexChanged" GridLines="None">
                            <FooterStyle BackColor="#CCCCCC" />
                            <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="PRTY Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprtycode" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRTY NAME" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRTY ADDRESS" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprtyadd" runat="server" Text='<%# Eval("PRTY_ADD1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YEAR" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblyear" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LAB DIP NO" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllabdipno" runat="server" Text='<%# Eval("LAB_DIP_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SHADE CODE" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshade" runat="server" Text='<%# Eval("SHADE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SHADE NAME" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshadename" runat="server" Text='<%# Eval("SHADE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LR OPTION" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllroption" runat="server" Text='<%# Eval("LR_OPTION") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUBMISSION" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsubdate" runat="server" Text='<%# Eval("SUBMISSION") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DEPTH" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldepth" runat="server" Text='<%# Eval("DEPTH") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="APPROVED " HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblappdate" runat="server" Text='<%# Eval("APPROVED") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IS APPROVED" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblisapproved" runat="server" Text='<%# Eval("IS_APPROVED") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <cc1:CalendarExtender ID="cetsub_rngf_dt" Format="dd/MM/yyyy" TargetControlID="TxtFromDate"
                            runat="server">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="cetsub_rngt_dt1" Format="dd/MM/yyyy" TargetControlID="TxtToDate"
                            runat="server">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
