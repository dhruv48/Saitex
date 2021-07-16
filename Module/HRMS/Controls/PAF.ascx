<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PAF.ascx.cs" Inherits="Module_HRMS_Controls_PAF" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        display: inline;
        overflow: hidden;
        white-space: nowrap;
    }
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
        width: 200px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .tContentArial
    {
        width: 762px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click"
                                    TabIndex="10" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1">
                                </asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <b class="titleheading">Performance Appraisal Form</b>
                </td>
            </tr>
            <tr>
                <td class="td" valign="top" align="left">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ValidationGroup="M1" ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <table align="left" class="tContentArial" width="100%">
                        <tr>
                            <td class="td">
                                <asp:Panel ID="pnlEmpDTL" runat="server">
                                    <table border="2" width="100%">
                                        <tr>
                                            <td bgcolor="#0099ff" colspan="2" align="center">
                                                <b><i style="color: #FFFFFF">Employee Detail</i></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                *Employee :
                                            </td>
                                            <td>
                                                <%--<cc2:ComboBox runat="server" ID="cmbEmpCode" EnableVirtualScrolling="true" Width="185px"
                                            Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                                            DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="cmbEmpCode_LoadingItems"
                                            AutoPostBack="True" OnSelectedIndexChanged="cmbEmpCode_SelectedIndexChanged"
                                            MenuWidth="300px" EmptyText="Select Employee" TabIndex="1">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Emp Code</div>
                                                <div class="header c2">
                                                    Employee Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("EMP_CODE")%></div>
                                                <div class="item c2">
                                                    <%# Eval("EMPLOYEENAME")%></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                        <cc2:ComboBox runat="server" ID="cmbEmpSelect" Width="185px" Height="200px" EnableLoadOnDemand="true"
                                            AutoPostBack="True" MenuWidth="300px" EmptyText="Select Employee" 
                                            TabIndex="1" onloadingitems="cmbEmpSelect_LoadingItems" 
                                            onselectedindexchanged="cmbEmpSelect_SelectedIndexChanged">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Emp Code</div>
                                                <div class="header c2">
                                                    Employee Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("EMP_CODE")%></div>
                                                <div class="item c2">
                                                    <%# Eval("EMPLOYEENAME")%></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>--%>
                                                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="TextBoxDisplay SmallFont" ValidationGroup="M1"
                                                    Width="1px" TabIndex="1" MaxLength="30" ReadOnly="true" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtEmpName" runat="server" CssClass="TextBoxDisplay SmallFont" ValidationGroup="M1"
                                                    Width="182px" TabIndex="1" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Department :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                    ValidationGroup="M1" Width="182px" TabIndex="2" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Designation :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                    ValidationGroup="M1" Width="182px" TabIndex="3" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Grade :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGrade" runat="server" CssClass="TextBoxDisplay SmallFont" ValidationGroup="M1"
                                                    Width="182px" TabIndex="4" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                *Assessment Year :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAssessmentYr" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                                    ValidationGroup="M1" Width="182px" TabIndex="5" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Branch :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBranch" runat="server" CssClass="TextBoxDisplay SmallFont" ValidationGroup="M1"
                                                    Width="182px" TabIndex="6" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td class="td">
                                <asp:Panel ID="pnlAppDTL" runat="server">
                                    <table border="2" width="100%">
                                        <tr>
                                            <td bgcolor="#0099ff" colspan="2" align="center">
                                                <b><i style="color: #FFFFFF">Other Details</i></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                *Appraisal Type :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAppraisalType" runat="server" Width="153px" TabIndex="7"
                                                    CssClass="SmallFont">
                                                    <asp:ListItem Value="Mid-Term Assessment" Text="Mid-Term Assessment" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Annual Appraisal" Text="Annual Appraisal"></asp:ListItem>
                                                    <asp:ListItem Value="On Confirmation" Text="On Confirmation"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Function :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFunction" runat="server" ValidationGroup="M1" Width="150px" MaxLength="30"
                                                    TabIndex="8" CssClass="SmallFont"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sub-Function :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubFunction" runat="server" ValidationGroup="M1" Width="150px"
                                                    MaxLength="30" TabIndex="9" CssClass="SmallFont"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Appraiser :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAppraiser" runat="server" ValidationGroup="M1" Width="150px"
                                                    MaxLength="30" TabIndex="10" CssClass="SmallFont"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Reviewer :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReviewer" runat="server" ValidationGroup="M1" Width="150px" MaxLength="30"
                                                    TabIndex="11" CssClass="SmallFont"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                HOD :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtHOD" runat="server" ValidationGroup="M1" Width="150px" MaxLength="30"
                                                    TabIndex="12" CssClass="SmallFont"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table align="left" class="tContentArial">
                        <tr>
                            <td bgcolor="#0099ff" colspan="4" align="center">
                                <b><i style="color: #FFFFFF">The Performance Rating Scale & Parameters</i></b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pnlRating" runat="server" Width="100%">
                                    <table width="100%" border="2" style="height: 75px">
                                        <tr>
                                            <td align="left">
                                                <b>Rating Point</b>
                                            </td>
                                            <td align="center">
                                                5
                                            </td>
                                            <td align="center">
                                                4
                                            </td>
                                            <td align="center">
                                                3
                                            </td>
                                            <td align="center">
                                                2
                                            </td>
                                            <td align="center">
                                                1
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Rating</b>
                                            </td>
                                            <td align="center">
                                                Excellent
                                            </td>
                                            <td align="center">
                                                Very Good
                                            </td>
                                            <td align="center">
                                                Good
                                            </td>
                                            <td align="center">
                                                Fair
                                            </td>
                                            <td align="center">
                                                Poor
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Achievement(%)</b>
                                            </td>
                                            <td align="center">
                                                >=111%
                                            </td>
                                            <td align="center">
                                                95-110%
                                            </td>
                                            <td align="center">
                                                85-94%
                                            </td>
                                            <td align="center">
                                                75-84%
                                            </td>
                                            <td align="center">
                                                <=74%
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table align="left" class="tContentArial" width="100%" border="2">
                        <tr>
                            <td class="td">
                                <table style="width: 755px" border="2">
                                    <tr>
                                        <td bgcolor="#0099ff" align="center" colspan="2">
                                            <b><i style="color: #FFFFFF">Self-Appraisal</i></b>
                                        </td>
                                        <td bgcolor="#0099ff" align="center" colspan="2">
                                            <b><i style="color: #FFFFFF">Appraisal By Appraiser</i></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" border="2">
                                                            <tr bgcolor="#336699" class="titleheading">
                                                                <td style="text-align: left">
                                                                    KRA No.
                                                                </td>
                                                                <td style="text-align: right">
                                                                    Achievement%
                                                                </td>
                                                                <td>
                                                                    Weightage(W)
                                                                </td>
                                                                <td>
                                                                    Rating Point(R)
                                                                </td>
                                                                <td align="center">
                                                                    W*R
                                                                </td>
                                                                <td style="text-align: right">
                                                                    Achievement%
                                                                </td>
                                                                <td>
                                                                    Weightage(W)
                                                                </td>
                                                                <td>
                                                                    Rating Point(R)
                                                                </td>
                                                                <td align="center">
                                                                    W*R
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <cc2:ComboBox ID="cmbKRANumber" runat="server" Width="63px" Height="150px" AutoPostBack="True"
                                                                        DataTextField="KRA_NO" DataValueField="KRA_NO" TabIndex="13" EnableLoadOnDemand="True"
                                                                        MenuWidth="200px" EmptyText="KRA" OnLoadingItems="cmbKRANumber_LoadingItems"
                                                                        OnSelectedIndexChanged="cmbKRANumber_SelectedIndexChanged">
                                                                        <HeaderTemplate>
                                                                            <div class="header c2">
                                                                                KRA No</div>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <div class="item c2">
                                                                                <%# Eval("MST_CODE")%></div>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            Displaying items
                                                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                                            out of
                                                                            <%# Container.ItemsCount %>.
                                                                        </FooterTemplate>
                                                                    </cc2:ComboBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEmpAchievement" runat="server" Width="80px" TabIndex="14" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEmpWeightage" runat="server" Width="80px" TabIndex="15" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEmpRatingPoint" runat="server" Width="80px" TabIndex="16" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right" OnTextChanged="txtEmpRatingPoint_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEmpWR" runat="server" Width="50px" TabIndex="17" CssClass="TextBoxDisplay SmallFont"
                                                                        Style="text-align: right" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtApprAchievement" runat="server" Width="80px" TabIndex="18" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtApprWeightage" runat="server" Width="80px" TabIndex="19" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtApprRatingPoint" runat="server" Width="80px" TabIndex="20" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtApprWR" runat="server" Width="50px" TabIndex="21" CssClass="TextBoxNo SmallFont"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnSaveDetail" runat="server" Style="top: 0px; left: -1px" Text="Save"
                                                                        Width="60px" TabIndex="22" ValidationGroup="M1" OnClick="btnSaveDetail_Click">
                                                                    </asp:Button>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" TabIndex="23"
                                                                        OnClick="btnCancel_Click"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grdAppraisal" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                                            Width="762px" TabIndex="24" OnRowCommand="grdAppraisal_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="KRA No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblKRANo" runat="server" Text='<%# Bind("KRA_NO") %>' CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                                                            Width="40px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Achievement%" HeaderStyle-HorizontalAlign="Right"
                                                                    ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblEmpAchievement" runat="server" Text='<%# Bind("EMP_ACHIEVEMENT") %>'
                                                                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="80px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weightage(W)" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblEmpWeightage" runat="server" Text='<%# Bind("EMP_WEIGHTAGE") %>'
                                                                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="80px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rating Point(R)" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblEmpRatingPoint" runat="server" Text='<%# Bind("EMP_RATING_POINT") %>'
                                                                            CssClass="TextBoxNo SmallFont TextBoxDisplay" ReadOnly="true" Width="80px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="W*R" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblEmpWR" runat="server" Text='<%# Bind("EMP_WP") %>' CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                                                            Width="30px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Achievement%" HeaderStyle-HorizontalAlign="Right"
                                                                    ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblApprAchievement" runat="server" Text='<%# Bind("APPR_ACHIEVEMENT") %>'
                                                                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="80px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weightage(W)" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblApprWeightage" runat="server" Text='<%# Bind("APPR_WEIGHTAGE") %>'
                                                                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="80px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rating Point(R)" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblApprRatingPoint" runat="server" Text='<%# Bind("APPR_RATING_POINT") %>'
                                                                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="80px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="W*R" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="lblApprWP" runat="server" Text='<%# Bind("APPR_WP") %>' CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                                                            Width="30px" ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnEdit" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                                            CommandName="EditTRN" Text="Edit" />
                                                                        <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                                            CommandName="DeleteTRN" Text="Delete" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RFAssessmentYear" runat="server" ErrorMessage="Please. Enter Assessment Year"
            ValidationGroup="M1" Display="None" SetFocusOnError="True" ControlToValidate="txtAssessmentYr"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVAssessmentYear" runat="server" ErrorMessage="Enter valid year only.."
            MinimumValue="1" MaximumValue="99999" Type="Double" ValidationGroup="M1" Display="None"
            SetFocusOnError="True" ControlToValidate="txtAssessmentYr"></asp:RangeValidator>
        <asp:RangeValidator ID="RVEmpAchievement" runat="server" ErrorMessage="Enter Only Numeric Value between 1 To 100.."
            MinimumValue="1" MaximumValue="100" Type="Double" ValidationGroup="M1" Display="None"
            SetFocusOnError="True" ControlToValidate="txtEmpAchievement"></asp:RangeValidator>
        <asp:RangeValidator ID="RVEmpWeightage" runat="server" ErrorMessage="Enter Only Numeric Value between 1 To 100.."
            MinimumValue="1" MaximumValue="100" Type="Double" ValidationGroup="M1" Display="None"
            SetFocusOnError="True" ControlToValidate="txtEmpWeightage"></asp:RangeValidator>
        <asp:RangeValidator ID="RVEmpRatingPoint" runat="server" ErrorMessage="Enter Only Numeric Value between 1 To 100.."
            MinimumValue="1" MaximumValue="100" Type="Double" ValidationGroup="M1" Display="None"
            SetFocusOnError="True" ControlToValidate="txtEmpRatingPoint"></asp:RangeValidator>
        <asp:RangeValidator ID="RVEmpWR" runat="server" ErrorMessage="Enter Only Numeric Value between 1 To 100.."
            MinimumValue="1" MaximumValue="100" Type="Double" ValidationGroup="M1" Display="None"
            SetFocusOnError="True" ControlToValidate="txtEmpWR"></asp:RangeValidator>
    </ContentTemplate>
</asp:UpdatePanel>
