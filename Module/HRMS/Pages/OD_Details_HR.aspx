<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true"
    CodeFile="OD_Details_HR.aspx.cs" Inherits="Module_HRMS_Pages_OD_Details_HR" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
                <tr>
                    <td width="10%" style="width: 20%">
                        Select Month :
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="160px" CssClass="gCtrTxt" ValidationGroup="M1">
                            <asp:ListItem Value="1" Text="January"></asp:ListItem>
                            <asp:ListItem Value="2" Text="February"></asp:ListItem>
                            <asp:ListItem Value="3" Text="March"></asp:ListItem>
                            <asp:ListItem Value="4" Text="April"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="June"></asp:ListItem>
                            <asp:ListItem Value="7" Text="July"></asp:ListItem>
                            <asp:ListItem Value="8" Text="August"></asp:ListItem>
                            <asp:ListItem Value="9" Text="September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="December"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonth"
                            ErrorMessage="Pls select month" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 20%">
                        Select Year :
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddlYear" runat="server" Width="160px" CssClass="gCtrTxt" ValidationGroup="M1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                            ErrorMessage="Pls select year" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="10%" style="width: 20%">
                        Select Branch:
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" CssClass="gCtrTxt">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%">
                        Select Department :
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" CssClass="gCtrTxt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" align="center" valign="top" style="height: 25px;">
                        <asp:Button ID="btnView" Text="View" runat="server" Width="75" OnClick="btnView_Click"
                            CssClass="AButton" />
                </tr>
                <tr>
                    <td width="100%" align="center" class="TableHeader" colspan="4">
                        <b class="titleheading">Out-Door Duty Detail </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvReportDisplayGrid" runat="server" AllowPaging="True" PagerSettings-Position="Bottom"
                            AutoGenerateColumns="False" PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="Left"
                            OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging" Font-Size="X-Small"
                            CaptionAlign="Top" ForeColor="#333333" GridLines="None" DataKeyNames="OD_ID"
                            EmptyDataText="No Record Found" PageSize="10">
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Apply Date" HeaderStyle-HorizontalAlign="Left" DataField="APPLY_DATE"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeId" Text='<%#Eval("Name") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dept." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDept" Text='<%#Eval("DEPT_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblBranch" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Duty From" HeaderStyle-HorizontalAlign="Center" DataField="FROM_DATE"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Duty To" HeaderStyle-HorizontalAlign="Center" DataField="TO_DATE"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="From" HeaderStyle-HorizontalAlign="Center" DataField="ON_FROM"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="To" HeaderStyle-HorizontalAlign="Center" DataField="ON_TO"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Place" HeaderStyle-HorizontalAlign="Center" DataField="PLACE"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" DataField="OD_status"
                                    ItemStyle-HorizontalAlign="Center" HtmlEncode="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Approval Authority" HeaderStyle-HorizontalAlign="Center"
                                    DataField="auth_by" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" Font-Size="Medium" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                                ForeColor="White" Font-Bold="True" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
