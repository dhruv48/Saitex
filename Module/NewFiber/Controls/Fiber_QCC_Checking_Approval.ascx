<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_QCC_Checking_Approval.ascx.cs" Inherits="Module_NewFiber_Controls_Fiber_QCC_Checking_Approval" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
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
    .d1
    {
        width: 150px;
    }
    .d2
    {
        margin-left: 4px;
        width: 350px;
    }
    .d3
    {
        width: 80px;
    }
    .HeaderRow
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .Smallfont
    {
        font-size: 8pt;
    }
</style>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
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
            <b class="titleheading">Fiber QC Checking Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="90%" class="td" align="center">
            <fieldset>
                <legend style="font-size: small">Search Section:</legend>
                <table width="50%" align="left">
                    <tr>
                        <td width="5%" class="tdRight">
                            <asp:Label ID="Label15" runat="server" Text="MRN Number : " CssClass="LabelNo SmallFont"></asp:Label>
                        </td>
                        <td width="5%" class="tdLeft">
                            <b>
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="combined"
                                    EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                                    Width="180px" Height="200px" MenuWidth="200px" TabIndex="1" EmptyText="Select MRN">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            MRN #</div>
                                        <div class="header c1">
                                            Year</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container5" Text='<%# Eval("YEAR") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:HiddenField ID="hdTRN_TYPE" runat="server" />
                                <asp:HiddenField ID="hdTRN_NUMB" runat="server" />
                                <asp:HiddenField ID="hdYEAR" runat="server" />
                            </b>
                        </td>
                        <td class="tdRight" width="5%">
                            <asp:Label ID="Label1" runat="server" CssClass="LabelNo SmallFont" Text=" Yarn Code:"></asp:Label>
                        </td>
                        <td class="tdLeft" width="5%">
                            <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                DataTextField="FIBER_CODE" DataValueField="FIBER_DESC" EmptyText="Find Fiber Code"
                                EnableLoadOnDemand="true" EnableVirtualScrolling="true" Height="200px" MenuWidth="500"
                                OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged"
                                TabIndex="2" Width="180px">
                                <HeaderTemplate>
                                    <div class="header c1">
                                        YARN CODE</div>
                                    <div class="header c2">
                                        YARN Description</div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FIBER_CODE") %>' /></div>
                                    <div class="item c2">
                                        <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FIBER_DESC") %>' /></div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Displaying items
                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                    out of
                                    <%# Container.ItemsCount %>.
                                </FooterTemplate>
                            </cc2:ComboBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                AllowSorting="True" AutoGenerateColumns="False" Width="95%" OnRowDataBound="gvMaterialReceiptApproval_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sr.&nbsp;No." HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="QC&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                           
                            <asp:Label ID="lblQC_NUMB" runat="server" Text='<%#Eval("QC_NUMB") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN&nbsp;Year" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("TRN_YEAR") %>'></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" Text='<%#Eval("TRN_NUMB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRN_DATE" HeaderText="MRN&nbsp;Date" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Party" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTY_DATA") %>'></asp:Label>
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%#Eval("TRN_TYPE") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QC_DATE" HeaderText="QC&nbsp;Date" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Fiber&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblYARN_CODE" runat="server" Text='<%#Eval("FIBER_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FIBER_DESC" HeaderText="Fiber&nbsp;Description" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:BoundField DataField="Y_COUNT" HeaderText="Yarn&nbsp;Count" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:TemplateField HeaderText="Std&nbsp;Type" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblSTD_TYPE" runat="server" Text='<%#Eval("STD_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MAX_VALUE" HeaderText="Max&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:BoundField DataField="MIN_VALUE" HeaderText="Min&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:BoundField DataField="QC_VALUE" HeaderText="QC&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:TemplateField HeaderText="Result" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Result" runat="server" Text='<%#Eval("Q_Result") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;QC&nbsp;Confirm&nbsp;&nbsp;&nbsp;&nbsp;">
                        <ItemTemplate>
                            <asp:RadioButton ID="chkQCPass" runat="server" Text="Pass" GroupName="chk" />
                            <asp:RadioButton ID="chkQCFail" runat="server" Text="Fail" GroupName="chk" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox smallfont" MaxLength="500"
                                Width="90%" TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>

