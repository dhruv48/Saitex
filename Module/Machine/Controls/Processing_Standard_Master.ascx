<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Processing_Standard_Master.ascx.cs"
    Inherits="Module_Machine_Controls_Processing_Standard_Master" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    //    function Calculation(val)
    //    { 
    //        var name=val;
    //                   
    //        document.getElementById('ctl00_cphBody_POCredit1_txtAdvanceAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtAdvance').value)*(parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtFinalTotal').value)/100)).toFixed(3) ;
    //     }           
    ////    function SetFocus(ControlId)
    ////    {    
    ////        document.getElementById(ControlId).focus();       
    ////    }
    //    function GetClick(ButtonId)
    //    {
    //        document.getElementById(ButtonId).click();
    //    }
    //  
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
        margin-left: 2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        margin-left: 4px;
        width: 350px;
    }
    .SmallFont
    {
        width: 0%;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table class="tdMain" width="95%">
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <table align="left">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    ToolTip="Save" ValidationGroup="PMM" Width="48" OnClick="imgbtnSave_Click" />
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="PMM" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <span class="titleheading"><b>Processing Standard Master</b></span>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table class="tContentArial" width="100%">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="lblFindprocess" runat="server" Text="Process Route Code :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtProcessRouteCode" runat="server" CssClass="SmallFont TextBoxNo "
                                    Width="120px" TabIndex="1"></asp:TextBox>
                                <cc1:ComboBox ID="ddlProcessRouteCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="PROS_ROUTE_CODE" DataValueField="PROS_ROUTE_CODE" EmptyText="Select Process Route"
                                    EnableLoadOnDemand="true" Height="200px" MenuWidth="600px" OnLoadingItems="ddlProcessRouteCode_LoadingItems"
                                    OnSelectedIndexChanged="ddlProcessRouteCode_SelectedIndexChanged" TabIndex="2"
                                    Width="150px" Visible="false">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Pros Route Code</div>
                                        <div class="header c4">
                                            Description</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("PROS_DESC") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Process :
                            </td>
                            <td align="left" valign="top" width="55%">
                                <asp:DropDownList ID="ddlProcess" runat="server" TabIndex="3" Width="99px" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Description :
                            </td>
                            <td align="left" valign="top" width="85%" colspan="3">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt TextBox" MaxLength="50"
                                    TabIndex="4" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                    <td align="right" valign="top" width="10%">
                        Ground :
                    </td>
                    <td align="left" valign="top" width="10%">
                        <asp:DropDownList ID="ddlGround" runat="server" Width="99px" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="10%">
                        Print Style :
                    </td>
                    <td align="left" valign="top" width="50%">
                        <asp:DropDownList ID="ddlPrintStyle" runat="server" Width="99px" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="td SmallFont" valign="top" width="100%">
                    <table width="100%" class="tContentArial">
                        <tr bgcolor="#336699">
                            <td width="23%">
                                <span class="titleheading">Process Code</span>
                            </td>
                            <td width="7%">
                                <span class="titleheading">Machine Code</span>
                            </td>
                            <td width="25%">
                                <span class="titleheading">Process Description</span>
                            </td>
                            <td width="5%">
                                <span class="titleheading">Test Code</span>
                            </td>
                            <td width="25%">
                                <span class="titleheading">Remarks</span>
                            </td>
                            <td width="15%">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="23%">
                                <cc1:ComboBox ID="cmbProcessCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="PROS_CODE" DataValueField="PRODUCT_TYPE" EmptyText="Code" EnableVirtualScrolling="true"
                                    EnableLoadOnDemand="true" Height="200px" MenuWidth="900px" OnLoadingItems="cmbProcessCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbProcessCode_SelectedIndexChanged" TabIndex="5" Width="80px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Proc COde</div>
                                        <div class="header c4">
                                            Proc Description</div>
                                        <div class="header c2">
                                            Product Type</div>
                                        <div class="header c2">
                                            Department</div>
                                        <div class="header c2">
                                            Machine Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PROS_DESC") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("DEPARTMENT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("MAC_CODE") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                                <asp:TextBox ID="ddlProcessCode" runat="server" ReadOnly="true" TabIndex="6" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td valign="top" width="7%">
                                <asp:TextBox ID="txtMachineCode" runat="server" ReadOnly="true" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="120px" TabIndex="7"></asp:TextBox>
                                <br />
                            </td>
                            <td valign="top" width="25%">
                                <asp:TextBox ID="txtProcessDescription" runat="server" ReadOnly="true" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="98%" TabIndex="8"></asp:TextBox>
                            </td>
                            <td valign="top" width="5%">
                                <asp:TextBox ID="txtTestCode" TabIndex="9" runat="server" CssClass="SmallFont TextBox "
                                    Width="120px"></asp:TextBox>
                            </td>
                            <td valign="top" width="25%">
                                <asp:TextBox ID="txtRemarks" TabIndex="10" runat="server" CssClass="SmallFont TextBox "
                                    Width="98%"></asp:TextBox>
                            </td>
                            <td valign="top" width="115%">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="11"
                                    ValidationGroup="standard" Width="50px" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" T abIndex="12" OnClick="btnCancel_Click"
                                    Width="60px" />
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                    <asp:GridView ID="gvProcessingStandardMaster" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="Smallfont" OnRowCommand="gvProcessingStandardMaster_RowCommand"
                        TabIndex="13" Width="98%">
                        <RowStyle CssClass="SmallFont" Width="100%" />
                        <Columns>
                            <asp:TemplateField HeaderText="Process Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txcProcessCode" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                        TabIndex="19" Text='<%# Bind("PCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txtMachineCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process Description" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txtProcessDescription" runat="server" CssClass="LabelNo smallfont"
                                        ReadOnly="true" TabIndex="21" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Test Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txtTestcode" runat="server" CssClass="LabelNo smallfont" ReadOnly="true"
                                        TabIndex="21" Text='<%# Bind("TestCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txtRemakrs" runat="server" CssClass="LabelNo smallfont" ReadOnly="true"
                                        TabIndex="21" Text='<%# Bind("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                        CommandName="ProcessingStandardEdit" TabIndex="14" Text="Edit"></asp:LinkButton>
                                    /
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                        CommandName="ProcessingStandardDelete" TabIndex="15" Text="Del"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheading" Font-Bold="True"
                            ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="PM"
            ShowMessageBox="True" ShowSummary="False" />
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
