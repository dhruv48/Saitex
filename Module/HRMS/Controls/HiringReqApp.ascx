<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HiringReqApp.ascx.cs" Inherits="Module_HRMS_Controls_HiringReqApp" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
<table>
    <tr>
        <td>
             <asp:GridView ID="GrdViewHiring" Width="100%" CssClass="SmallFont" 
                                                runat="server"  AutoGenerateColumns="False" 
                                                onrowcommand="GrdViewHiring_RowCommand" >
                                                <Columns>
                                                    <asp:BoundField DataField="HIR_RQ_ID" HeaderText="ID" />
                                                    <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" />
                                                    <asp:BoundField DataField="POSITION_NAME" HeaderText="POSITION" />
                                                    <asp:BoundField DataField="TOTAL_VAC" ItemStyle-HorizontalAlign="Right"  HeaderText="VACANCIES" />
                                                    <asp:BoundField DataField="PRIORITY" HeaderText="PRIORITY" />
                                                    <asp:BoundField DataField="VACTYPE" HeaderText="VAC.TYPE" />
                                                    <asp:BoundField DataField="POSTEDBY" HeaderText="POSTEDBY" />
                                                    <asp:BoundField DataField="POSTEDON" HeaderText="POSTED ON" />
                                                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                                    <asp:BoundField DataField="STATUS" HtmlEncode="false"  HeaderText="STATUS" />
                                                     <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkApp" runat="server" CommandArgument='<%# Eval("HIR_RQ_ID") %>'
                                                            CommandName="Approv" CssClass="ContolStyle" Text="Approved" OnClientClick="javascript:return window.confirm('Are you sure you want to Approved?')" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lnkreject" runat="server" CommandArgument='<%# Eval("HIR_RQ_ID") %>' CommandName="Reject" CssClass="ContolStyle" Text="Reject"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>                                                    
                                                </Columns>
                                                 <HeaderStyle CssClass="HeaderStyle GrdHeader" BackColor="#336699" />
                                            </asp:GridView>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
