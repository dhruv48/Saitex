<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true"
    CodeFile="EmpLeaveDetail.aspx.cs" Inherits="Module_HRMS_Pages_EmpLeaveDetail"
    Title="Leave Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
                <tr>
                    <td width="100%" align="center" class="TableHeader">
                        <b class="titleheading">Leaves Detail</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvReportDisplayGrid" runat="server" AllowPaging="true" PageSize="10"
                            PagerSettings-Position="Bottom" AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                            PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging"
                            OnSelectedIndexChanged="gvReportDisplayGrid_SelectedIndexChanged" DataKeyNames="LV_APP_ID"
                            EmptyDataText="There is no record found">
                            <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Wrap="False" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Apply Date" HeaderStyle-HorizontalAlign="Center" DataField="APPLIEDDATE"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px"></asp:BoundField>
                                <asp:TemplateField HeaderText="Employee Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMP_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblEmpName" Text='<%#Eval("EMPLOYEENAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
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
                                <asp:TemplateField HeaderText="Leave Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeaveName" Text='<%#Eval("LV_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Leave From" DataField="From_Date" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100"></asp:BoundField>
                                <asp:BoundField HeaderText="Leave To" DataField="To_Date" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100"></asp:BoundField>
                                <asp:BoundField HeaderText="Leave Days" DataField="DAYS_LV" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40"></asp:BoundField>
                                <asp:BoundField HeaderText="Purpose" DataField="LV_PURPOSE" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150"></asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="leave_status" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70" HtmlEncode="false"></asp:BoundField>
                                <asp:TemplateField HeaderText="Print" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a target="_blank" href="../Reports/LeaveApplication.aspx?EmpCode=<%# Eval("EMP_CODE") %>&LV_APP_ID=<%# Eval("LV_APP_ID") %>">
                                            <b>Print</b></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                                ForeColor="White" Font-Bold="True" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
