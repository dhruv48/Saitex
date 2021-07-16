<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true"
    CodeFile="LV_Details_HR.aspx.cs" Inherits="Module_HRMS_Pages_LV_Details_HR" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
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
                    <td colspan="4" align="center" valign="top" style="height: 25px;">
                        <asp:Button ID="btnView" Text="View" runat="server" Width="75" OnClick="btnView_Click"
                            CssClass="AButton" />
                </tr>
                <tr>
                    <td width="100%" align="center" class="TableHeader" colspan="4">
                        <b class="titleheading">Leaves Detail</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvReportDisplayGrid" runat="server" AllowPaging="true" PageSize="10"
                            PagerSettings-Position="Bottom" AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                            PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging"
                            DataKeyNames="LV_APP_ID" EmptyDataText="There is no record found" ForeColor="#333333"
                            CssClass="smallfont" Font-Size="X-Small">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="tContentArial" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
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
                                        <asp:Label ID="LblEmpName" Text='<%#Eval("EMP_NAME") %>' runat="server"></asp:Label>
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
                                <asp:BoundField HeaderText="Leave Days" DataField="DAYS" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40"></asp:BoundField>
                                <asp:BoundField HeaderText="Purpose" DataField="LV_PURPOSE" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150"></asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="leave_status" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70" HtmlEncode="false"></asp:BoundField>
                                <asp:BoundField HeaderText="Approval Authority" DataField="POSITION_NAME" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70" HtmlEncode="false"></asp:BoundField>
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#008000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
