<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobCard_Completion.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_JobCard_Completion" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"> <ContentTemplate>
<table class="tdMain tContentArial" width="1200px" align="left">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left" class="tContentArial">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Visible="false" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" onclick="imgbtnFindTop_Click"></asp:ImageButton>
                    </td>
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
            <b class="titleheading"> Job Card Order Completion </b>
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
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
    <td align="left" class="td" width="100%">
     <asp:GridView ID="gvJobSheetApproval" CssClass="SmallFont" runat="server" AllowSorting="True"
       AutoGenerateColumns="False" OnRowDataBound="gvJobSheetApproval_RowDataBound" Width="100%" >
                <Columns>
                    <asp:TemplateField HeaderText="Job Code" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                       <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
                            CssClass="SmallFont LabelNo" ></asp:Label>
                          <asp:Label ID="lblCOMP_CODE" runat="server" Text='<%# Bind("COMP_CODE") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%# Bind("BRANCH_CODE") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblYEAR" runat="server" Text='<%# Bind("YEAR") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo SmallFont" Wrap="true" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="BATCH_DATE" HeaderText="Job Date" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GREY_LOT_NO" HeaderText="Lot No">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                   <asp:BoundField DataField="LOT_SIZE" HeaderText="Lot Size" >
                  <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                  </asp:BoundField> 
                    <asp:BoundField DataField="PA_NO" HeaderText="PA No" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MACHINE_CODE" HeaderText="Machine Code" Visible="false" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="MACHINE_MAKE" HeaderText="Machine Name" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="SPRINGS" HeaderText="No Of Springs" Visible="false" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>                           
                    <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Machine Volumn"  > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField> 
                     <asp:BoundField DataField="PRTY_CODE" HeaderText="Party Code" Visible="false" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="PRTY_NAME" HeaderText="Party" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ARTICLE_CODE" HeaderText="Yarn Code" Visible="false" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                  <asp:TemplateField HeaderText="Confirm">
                     <%-- <HeaderTemplate>
                        <asp:CheckBox ID="chkApprovedheader" runat="server" OnCheckedChanged="chkApprovedheader_CheckedChanged"
                            AutoPostBack="true" Text="Confirm" />
                    </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Completion Date">
                    <ItemTemplate>
                        <asp:TextBox ID="txtConfirmDate" runat="server" ReadOnly="true" Width="60px" Text='<%# Bind("COMP_DATE") %>'
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Complete By">
                    <ItemTemplate>
                        <asp:TextBox ID="txtConfirmBy" runat="server" Width="60px" ReadOnly="true" Text='<%# Bind("COMP_BY") %>'
                            ToolTip='<%# Bind("COMP_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Completion Remarks">
            <ItemTemplate>
            <asp:TextBox ID="txtComptRem" runat="server"  Width="150px" CssClass="TextBox SmallFont" ></asp:TextBox> 
            </ItemTemplate>
             <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewJobSheetTRN" runat="server" Text="View Details"></asp:LinkButton>
                    <asp:Panel ID="pnlJobSheetTRN" runat="server"  Width="520px" BackColor="Beige" BorderWidth="2px"
                    Height="140px" ScrollBars="Auto">
                    <asp:GridView ID="grdJobSheetTRN" runat="server" AutoGenerateColumns="False" Width="520px"
                        CssClass="SmallFont" Height="140px" ShowFooter="true" OnRowDataBound="grdJobSheetTRN_RowDataBound">
                        <Columns>
                 <asp:TemplateField HeaderText="Sr. No.">
                 <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ></ItemStyle>
                <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
                </ItemTemplate> 
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Job Code" Visible="false">
                 <ItemTemplate>
                  <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
                    CssClass="SmallFont LabelNo" ></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                     <asp:BoundField DataField="DYE_PROCESS" HeaderText="Dye Process" Visible="false">
                     <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                     </asp:BoundField>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item&nbsp;Code" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LOT_SIZE" HeaderText="Lot Size" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Machine Volumn" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                       <asp:BoundField DataField="TRN_QTY" HeaderText="Item Qty(Kg)" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YEAR" HeaderText="Year" Visible="false" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                         <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" Width="98%" />
                       <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                      </asp:GridView>
                   </asp:Panel>
               <cc1:HoverMenuExtender ID="hmeJobSheetTRN" runat="server" PopupPosition="Left" PopupControlID="pnlJobSheetTRN"
                  TargetControlID="lbtnViewJobSheetTRN">
                    </cc1:HoverMenuExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
            </asp:TemplateField>
        </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
</ContentTemplate></asp:UpdatePanel>