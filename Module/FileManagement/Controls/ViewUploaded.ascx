<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewUploaded.ascx.cs"
    Inherits="Module_FileManagement_Controls_ViewUploaded" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">View Uploaded Files</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%" class="td">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr id="trTotalRecord" runat="server">
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"
                        CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr id="trgrid" runat="server">
                <td align="left" class="td">
                    <cc2:Grid ID="grdViewUploadedFile" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" OnSelect="grdViewUploadedFile_Select"
                        AutoPostBackOnSelect="True">
                        <Columns>
                            <cc2:Column DataField="FILE_CODE" Align="Left" HeaderText="Code" Width="80px">
                            </cc2:Column>
                            <cc2:Column DataField="FILE_NAME" Align="Left" HeaderText="File Name" Width="180px">
                            </cc2:Column>
                            <cc2:Column DataField="FILE_TYPE" Align="Left" HeaderText="File Type" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="FILE_GROUP" Align="Left" HeaderText="File Group" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="FILE_REF" Align="Left" HeaderText="File Reference" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="DESCRIPTION" Align="Left" HeaderText="Description" Width="325px">
                            </cc2:Column>
                        </Columns>
                        <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                        <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                    <%--<asp:GridView ID="grdViewUploadedFile" CssClass="SmallFont" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowCommand="grdViewUploadedFile_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="FILE_CODE" HeaderText="File Code">
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FILE_NAME" HeaderText="File Name">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FILE_TYPE" HeaderText="File Type">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FILE_GROUP" HeaderText="File Group">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FILE_REF" HeaderText="File Reference">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="File Description">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="View Upload File">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnFileCode" runat="server" Text="Click To View" CommandArgument='<%# Bind("FILE_CODE") %>'
                                        CssClass="Label smallfont" OnClick="lnkbtnFileCode_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="SmallFont" Width="98%" />
                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                    </asp:GridView>--%>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
