<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WelcomeUser.ascx.cs" Inherits="Admin_UserControls_WelcomeUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonControls/DashBoard.ascx" TagName="DashBoard" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
        <div id="divDashBoard" runat="server">
            <uc1:DashBoard ID="DashBoard1" runat="server" />
        </div>
        <div id="divNavLink" runat="server">
            <table bgcolor="white" class="tContentArial">
                <tr>
                    <td>
                        <cc1:TabContainer ID="tcWelcome" runat="server" ActiveTabIndex="0" Width="600px"
                            BackColor="white" AutoPostBack="True" OnActiveTabChanged="tcWelcome_ActiveTabChanged">
                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" BackColor="White">
                                <HeaderTemplate>
                                    Transaction
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="580" bgcolor="white">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" BackColor="White">
                                <HeaderTemplate>
                                    Query
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="580px" bgcolor="white">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel3" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4" BackColor="White">
                                <HeaderTemplate>
                                    Report
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="580px" bgcolor="white">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel4" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel4" BackColor="White">
                                <HeaderTemplate>
                                    Setup
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="580px" bgcolor="white">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel5" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Master" BackColor="White">
                                <HeaderTemplate>
                                    Master
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="580px" bgcolor="white">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click" />
    </Triggers>--%>
</asp:UpdatePanel>
