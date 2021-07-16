<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobCardQuery.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_JobCardQuery" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
     .c4
    {
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 300px;
    }
</style>
   <table width="100%">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td id="tdPrint" runat="server" align="left">
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading"> Job Card Entry Query</b>
                </td>
            </tr>
        </table>
            <fieldset>
            <table width="100%">
                <tr>
                    <td align="right" style="width: 14%;">
                        Job Cd. From :
                    </td>
                    <td align="left" style="width: 10%;">
                       <asp:TextBox ID="txtJobCodeFrom" CssClass="TextBox SmallFont" Width="123px"
                             runat="server"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 14%;">
                       Job Cd. To :
                    </td>
                    <td align="left" style="width: 10%;">
                       <asp:TextBox ID="txtJobCodeTo" CssClass="TextBox SmallFont" Width="123px"
                             runat="server"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 14%;">
                        Year :
                    </td>
                    <td align="left" style="width: 10%;">
               <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="125px" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 14%;">
                        Machine Name :
                    </td>
                    <td align="left" style="width: 10%;">
                    <cc2:ComboBox ID="ddlMachine" runat="server" TabIndex="1" Width="125px" MenuWidth="300"
                            AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                            Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlMachine_LoadingItems"
                            OnSelectedIndexChanged="ddlMachine_SelectedIndexChanged" EmptyText="Select Machine">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Machine Code</div>
                                <div class="header c2">
                                 Machine Name </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("MACHINE_CODE")%></div>
                                <div class="item c2">
                                    <%# Eval("MACHINE_MAKE")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                   <td align="left" style="width: 4%;"> 
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="8"
                        Width="120px" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 14%;">
                        Lot No :
                    </td>
                    <td align="left" style="width: 10%;">
                       <asp:TextBox ID="txtLotNo" CssClass="TextBox SmallFont" Width="123px"
                             runat="server"></asp:TextBox>
                    </td>
                     <td align="right" style="width: 14%;">
                        Process Type :
                    </td>
               <td align="left" style="width: 10%;">
            <cc2:ComboBox ID="ddlProcessCode" runat="server" AutoPostBack="True" CssClass="smallfont" Width="125px"
                        DataTextField="PROS_CODE" DataValueField="PROS_CODE" EnableLoadOnDemand="true"
                        MenuWidth="250px" OnLoadingItems="ddlProcessCode_LoadingItems" OnSelectedIndexChanged="ddlProcessCode_SelectedIndexChanged"
                        Height="200px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9" EmptyText="Select Process"
                        Visible="true">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Pros Code
                                </div>
                                <div class="header c1">
                                    Pros Desc
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                               <%# Eval("PROS_CODE")%></div>
                                <div class="item c1">
                                    <%# Eval("PROS_DESC")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    
                    <td align="right" style="width: 14%;">
                     Job Date From:
                    </td>
                    <td align="left" style="width: 10%;">
                          <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    Width="123px" OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="TxtFromDate" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 14%;">
                       Job Date To:
                    </td>
                    <td align="left" style="width: 10%;">
                  <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    Width="123px" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>
                    </td>
                   <td align="left" style="width: 4%;"> 
                    <asp:Button ID="btnShow" runat="Server" Text="Get Records" OnClick="btnShow_Click" />
                    </td>
                </tr> 
            </table>
        </fieldset>
             <table width="100%">
            <tr>
                <td align="left" width="50%">
                    <b>
                      <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
                <td align="left" valign="top" width="50%" cssclass="Label">
                    <b>
                        <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                            <ProgressTemplate>
                                Loading... </ProgressTemplate>
                        </asp:UpdateProgress>
                    </b>
                </td>
            </tr>
            <tr>
            <td class="td tContentArial" colspan="9">
           <asp:GridView ID="grdBindJobcardDetail" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" PageSize="10" Width="100%" 
           OnRowDataBound="grdBindJobcardDetail_RowDataBound" OnPageIndexChanging="grdBindJobcardDetail_PageIndexChanging">
           <Columns>
           <asp:TemplateField HeaderText="Job Code" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left">
           <ItemTemplate>
           <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
              CssClass="SmallFont LabelNo" ></asp:Label>
           <asp:Label ID="lblCompCode" runat="server" Text='<%Bind("COMP_CODE") %>' Visible="false"></asp:Label>
           <asp:Label ID="lblBranchCode" runat="server" Text='<%Bind("BRANCH_CODE") %>' Visible="false"></asp:Label>
           <asp:Label ID="lblYEAR" runat="server" Text='<%# Bind("YEAR") %>' Visible="false"></asp:Label>
           </ItemTemplate>
             <ItemStyle CssClass="labelNo SmallFont" Wrap="true" HorizontalAlign="Left" VerticalAlign="Top" />
           </asp:TemplateField>
           <asp:BoundField DataField="BATCH_DATE" HeaderText="Job Date" DataFormatString="{0:dd/MM/yyyy}">
           <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
           </asp:BoundField>
            <asp:BoundField DataField="GREY_LOT_NO" HeaderText="Lot Number">
            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont"  VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="PROS_CODE" HeaderText="Process" >
            <ItemStyle  HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField HeaderText="PA NO." DataField="PA_NO">
            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont"  VerticalAlign="Top" />
           </asp:BoundField>
           <asp:BoundField HeaderText="Machine Code" DataField="MACHINE_CODE" >
           <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
           </asp:BoundField> 
           <asp:BoundField DataField="MACHINE_MAKE" HeaderText="Machine Name" >
                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
            </asp:BoundField>
             <asp:BoundField DataField="SPRINGS" HeaderText="No Of Springs" Visible="false" >
                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
            </asp:BoundField>
             <asp:BoundField DataField="LOT_SIZE" HeaderText="Lot Size" >
                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Machine Vol."  > 
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
           
           
            <asp:TemplateField HeaderText="Chemical Detail">
            <ItemTemplate>
            <asp:LinkButton ID="lnkJobCardDetail" runat="server"  Text="View Chemical"></asp:LinkButton>
            <asp:Panel ID="pnlJobCardTrn" runat="server" ScrollBars="Auto" Width="520px" BackColor="Beige" BorderWidth="2px" Height="200px">
            <asp:GridView ID="grdJobCardTrn" runat="server" AutoGenerateColumns="false" Width="520px" CssClass="SmallFont" ShowFooter="true" Height="200px" OnRowDataBound="grdJobCardTrn_RowDataBound">
            <Columns>
            <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="10px">
            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
            <ItemTemplate >
            <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Job Code" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
            CssClass="SmallFont LabelNo" ></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item&nbsp;Code" Visible="false">
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description" ItemStyle-Width="200px" >
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM" ItemStyle-Width="50px">
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="QTY"  Visible="false">
                <ItemTemplate>
                <asp:Label ID="lblLotQty" runat="server" Text='<%#Bind("LOT_SIZE") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QTY" HeaderText="Doses" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                </asp:BoundField>
               <asp:BoundField DataField="TRN_QTY" HeaderText="Item Qty(Kg)" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="YEAR" HeaderText="Year" Visible="false" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                 <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="Chem Cost">
                 <ItemTemplate>
                 <asp:Label ID="lblRate" runat="server" DataFormatString="{0:0.00}" Text='<%#Bind("Rate","{0:#0.00}")%>' ></asp:Label>
                 </ItemTemplate>
                 <FooterTemplate>
                 <asp:Label ID="lblTotalRate" runat="server" Text="Total&nbsp;Rate=" Font-Bold="true" ForeColor="Red">
                 </asp:Label>&nbsp;<asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>
                 <br />
                 <asp:Label ID="lblPerCost"  runat="server" Text="Cost(/KG)=" Font-Bold="true" ForeColor="Red">
                 </asp:Label>&nbsp;<asp:Label ID="lblCost" runat="server" Font-Bold="true"></asp:Label>
                 </FooterTemplate>
                 </asp:TemplateField>
                 
            </Columns>
                  <RowStyle CssClass="SmallFont" Width="98%" />
                   <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />                   
                </asp:GridView>
                </asp:Panel>
                <cc1:HoverMenuExtender ID="hmeJobCardTRN" runat="server" PopupPosition="Left" PopupControlID="pnlJobCardTrn" TargetControlID="lnkJobCardDetail">
                </cc1:HoverMenuExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                </asp:TemplateField>
                
                
                 <asp:TemplateField HeaderText=" View Dyes">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewJobCardDyes" runat="server" Text="View Chemical "></asp:LinkButton>
                    <asp:Panel ID="pnlJobCardDYES" runat="server"  Width="520px" BackColor="Beige" BorderWidth="2px"
                    Height="140px" ScrollBars="Auto">
                    <asp:GridView ID="grdJobCardDYES" runat="server" AutoGenerateColumns="False" Width="520px"
                        CssClass="SmallFont" Height="140px" ShowFooter="true" OnRowDataBound="grdJobCardDYES_RowDataBound">
                        <Columns>
                 <asp:TemplateField HeaderText="Sr. No.">
                 <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
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
                     <asp:BoundField DataField="DYE_PROCESS" HeaderText="Dye Process">
                     <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                     </asp:BoundField>
                        <asp:BoundField DataField="DYE_NAME" HeaderText="Dyes&nbsp;Code" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DYE_DTL" HeaderText="Dyes&nbsp;Description">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="DOSE" HeaderText="DOSE%" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LOT_SIZE" HeaderText="Lot Size" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                       
                       <asp:BoundField DataField="TRN_QTY" HeaderText="Item Qty(Kg)" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YEAR" HeaderText="Year" Visible="false" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                         <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Dyes Cost">
                 <ItemTemplate>
                 <asp:Label ID="lblRate" runat="server" DataFormatString="{0:0.00}" Text='<%#Bind("Rate","{0:#0.00}")%>' ></asp:Label>
                 </ItemTemplate>
                 <FooterTemplate>
                 <asp:Label ID="lblTotalRate" runat="server" Text="Total&nbsp;Rate=" Font-Bold="true" ForeColor="Red">
                 </asp:Label>&nbsp;<asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>
                 <br />
                 <asp:Label ID="lblPerCost"  runat="server" Text="Cost(/KG)=" Font-Bold="true" ForeColor="Red">
                 </asp:Label>&nbsp;<asp:Label ID="lblCost" runat="server" Font-Bold="true"></asp:Label>
                 </FooterTemplate>
                 </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" Width="98%" />
                       <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                      </asp:GridView>
                   </asp:Panel>
               <cc1:HoverMenuExtender ID="hmeJobCardDYES" runat="server" PopupPosition="Left" PopupControlID="pnlJobCardDYES"
                  TargetControlID="lbtnViewJobCardDYES">
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
        <table width="100%">
            <tr>
                <td align="left" class="td" width="100%">
                    <div id="divPrint" runat="server">
                      </div>
                </td>
            </tr>
        </table>