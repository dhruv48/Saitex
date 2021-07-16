<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoardAttndance.ascx.cs" Inherits="Module_HRMS_Controls_DashBoardAttndance" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
     .c3
    {
        width: 50px;
    }
    .c4
    {
        margin-left: 4px;
        width: 100px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<div>
    <asp:GridView ID="GVMonths" runat="server" AutoGenerateColumns="false" CssClass="tContentArial"  HorizontalAlign="Left"  ShowFooter="false"  Width="100%" onrowdatabound="GVMonths_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                        <ItemTemplate>
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="tContentArial" width="100%">
                                                <tr>
                                                    <td align="left" valign="top" width="50%">
                                                        <asp:Label ID="LblMonths" runat="server" Text='<%# Eval("MONTH_DESCR")%>'></asp:Label>
                                                        <asp:Label ID="lblMonthId" runat="server" Text='<%# Eval("MONTH_IND")%>' Visible="false" Width="25px"></asp:Label>
                                                    </td>                                              
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="600px" />
                                        <ItemTemplate>
                                            <asp:GridView ID="GVDate" runat="server" AutoGenerateColumns="False" 
                                                CssClass="tContentArial" HorizontalAlign="Center" ShowHeader="false"  Width="100%">
                                               
                                                <Columns>
                                                    <asp:BoundField DataField="1" ControlStyle-Width="10px" HeaderText="01" />
                                                    <asp:BoundField DataField="2" HeaderText="02" />
                                                    <asp:BoundField DataField="3" HeaderText="03" />
                                                    <asp:BoundField DataField="4" HeaderText="04" />
                                                    <asp:BoundField DataField="5" HeaderText="05" />
                                                    <asp:BoundField DataField="6" HeaderText="06" />
                                                    <asp:BoundField DataField="7" HeaderText="07" />
                                                    <asp:BoundField DataField="8" HeaderText="08" />
                                                    <asp:BoundField DataField="9" HeaderText="09" />
                                                    <asp:BoundField DataField="10" HeaderText="10" />
                                                    <asp:BoundField DataField="11" HeaderText="11" />
                                                    <asp:BoundField DataField="12" HeaderText="12" />
                                                    <asp:BoundField DataField="13" HeaderText="13" />
                                                    <asp:BoundField DataField="14" HeaderText="14" />
                                                    <asp:BoundField DataField="15" HeaderText="15" />
                                                    <asp:BoundField DataField="16" HeaderText="16" />
                                                    <asp:BoundField DataField="17" HeaderText="17" />
                                                    <asp:BoundField DataField="18" HeaderText="18" />
                                                    <asp:BoundField DataField="19" HeaderText="19" />
                                                    <asp:BoundField DataField="20" HeaderText="20" />
                                                    <asp:BoundField DataField="21" HeaderText="21" />
                                                    <asp:BoundField DataField="22" HeaderText="22" />
                                                    <asp:BoundField DataField="23" HeaderText="23" />
                                                    <asp:BoundField DataField="24" HeaderText="24" />
                                                    <asp:BoundField DataField="25" HeaderText="25" />
                                                    <asp:BoundField DataField="26" HeaderText="26" />
                                                    <asp:BoundField DataField="27" HeaderText="27" />
                                                    <asp:BoundField DataField="28" HeaderText="28" />
                                                    <asp:BoundField DataField="29" HeaderText="29" />
                                                    <asp:BoundField DataField="30" HeaderText="30" />
                                                    <asp:BoundField DataField="31" HeaderText="31" />
                                                </Columns>
                                               
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:GridView>
</div>