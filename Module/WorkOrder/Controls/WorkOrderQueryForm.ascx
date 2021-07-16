<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkOrderQueryForm.ascx.cs" Inherits="Module_WorkOrder_Controls_WorkOrderQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table align="left" class="tContentArial td " width="100%">
    <tr>
        <td class="tContentArial td ">
            <table> 
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                    <td><asp:ImageButton ID="ImportExcel" runat="server" Height="41" ImageUrl="~/APP_IMAGES/export.png"
                            ToolTip="Import To Excel" Width="48" OnClick="ImportExcel_Click" /></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td">
                        <span class="titleheading"><strong>Work Order Entry </strong></span>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left" valign="top">Branch:
                        <asp:DropDownList ID="ddlbranch" runat="server" Width="170px"  AutoPostBack="true"
                            onselectedindexchanged="ddlbranch_SelectedIndexChanged">
                        </asp:DropDownList>
                        
                    </td>
                    <td align="left" valign="top"><asp:Label ID="lblwo" runat="server" Text="Work Order No"></asp:Label>
                   
                     <asp:DropDownList ID="ddwo" runat="server" Width="170px"  AutoPostBack="true" 
                            onselectedindexchanged="ddwo_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top" ><asp:Label ID="Label2" runat="server" Text="Party"></asp:Label>
                    <asp:DropDownList ID="ddprty" runat="server" Width="170px"  AutoPostBack="true" 
                            onselectedindexchanged="ddprty_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top" ><asp:Label ID="Label3" runat="server" Text="From"></asp:Label>
                    <asp:TextBox ID="txtfrom" runat="server" Width="170px"  AutoPostBack="TRUE" 
                            ontextchanged="txtfrom_TextChanged">
                            
                        </asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
        TargetControlID="txtfrom" PopupButtonID="ImageButton1"/>
                    </td>
                    <td align="left" valign="top" ><asp:Label ID="Label4" runat="server" Text="To"></asp:Label>
                    <asp:TextBox ID="txtTo" runat="server" Width="170px"  AutoPostBack="TRUE" 
                            ontextchanged="txtTo_TextChanged">
                            
                        </asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
        TargetControlID="txtTo" PopupButtonID="ImageButton1"/>
                    </td>
                    <td align="left" valign="top">
                        <asp:Button ID="btngetbranch" runat="server" Text="Get Data" Width="150px"  CssClass="Small-Font"
                            OnClick="btngetbranch_Click" Visible="false"/>
                    </td>
                </tr>
            </table>
            <table width="100%" class="tContentArial td">
                <tr>
                    <td align="left" width="50%">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass ="Label"></asp:Label>
                            <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="Get_WO_Detail" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%" OnPageIndexChanging="Get_WO_Detail_PageIndexChanging" OnRowCommand="Get_WO_Detail_RowCommand"
                            OnRowDataBound="Get_WO_Detail_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="BRANCH CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BRANCH_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("BRANCH_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRODUCT TYPE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO TYPE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWO_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("WO_TYPE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO NUMB" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWO_NUMB" runat="server" CssClass=" SmallFont" Text='<%# Eval("WO_NUMB") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO DATE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWO_DATE" runat="server" CssClass=" SmallFont" Text='<%# Eval("WO_DATE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRTY CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRTY_CODE" runat="server" CssClass=" SmallFont"  Text='<%# Eval("PRTY_NAME") %>' ToolTip='<%# Eval("PRTY_CODE") %>'
                                           />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WORK CAT" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOB_WORK_CAT" runat="server" CssClass=" SmallFont" Text='<%# Eval("JOB_WORK_CAT") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAYMENT TERMS" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAYMENT_TERMS" runat="server" CssClass=" SmallFont" Text='<%# Eval("PAYMENT_TERMS") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DELIVERY LOC" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDELIVERY_LOCATION" runat="server" CssClass=" SmallFont" Text='<%# Eval("DELIVERY_LOCATION") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRSP CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRSP_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("TRSP_NAME") %>' ToolTip='<%# Eval("TRSP_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="Issue">
                                <ItemTemplate>
                                <asp:LinkButton ID="btn_trn" Text ="View Detail" runat="server"></asp:LinkButton>
                                <asp:Panel ID="grdTrn" runat="server" Width="470px" BackColor="Beige" BorderWidth="2px"
                                Height="47px" ScrollBars="Auto">
                                 <asp:GridView ID="Grd_Wo_trn" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%" OnPageIndexChanging="Grd_Wo_trn_PageIndexChanging" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="Wo&nbsp;Numb" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwo_numb" runat="server" CssClass=" SmallFont" Text='<%# Eval("WO_NUMB") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblARTICLE_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ARTICLE_CODE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 
                                <%--<asp:TemplateField HeaderText="Shade&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="PRODUCT_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="WO Qty" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" runat="server" CssClass=" SmallFont" Text='<%# Eval("QTY") %>'/>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" CssClass=" SmallFont" Text='<%# Eval("UOM") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shrinkage" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="SHRINKAGE" runat="server" CssClass=" SmallFont" Text='<%# Eval("SHRINKAGE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                               
                                
                                
                            </Columns>
                            <RowStyle CssClass="SmallFont" Width="98%" />
                          <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                        </asp:GridView>
                                
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmeTRN" runat="server" PopupPosition="Left" PopupControlID="grdTrn"
                                TargetControlID="btn_trn">
                            </cc1:HoverMenuExtender>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText ="View Bom">
                                <ItemTemplate>
                                <asp:LinkButton ID ="lnkbtn" runat="server" Text ="View Bom"></asp:LinkButton>
                               <asp:Panel ID="pnlShowHover" runat="server"  BackColor="Beige" BorderWidth="2px">

                                   <asp:GridView ID="grd_bom" runat="server" CssClass="SmallFont" AutoGenerateColumns="False"
                                                Height="60px">
                                                <Columns>
                                                    <asp:BoundField DataField="BASE_ARTICLE_TYPE" HeaderText="Base&nbsp;Article&nbsp;Type" HeaderStyle-Font-Size="8" Visible="false">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <%--<asp:BoundField DataField="BASE_ARTICLE_CODE" HeaderText="Base&nbsp;Article&nbsp;Code" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>--%>
                                                     <%--<asp:BoundField DataField="BASE_ARTICLE_DESC" HeaderText="Base&nbsp;Article&nbsp;Desc" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>--%>
                                                    <asp:BoundField DataField="BASE_SHADE_CODE" HeaderText="Base&nbsp;Shade&nbsp;Code" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTY" HeaderText="Qty" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTY_ISS" HeaderText="Qty&nbsp;Issue" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="lnkbtn"
                                            PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
                                        </cc1:HoverMenuExtender>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="View Tax">
                                <ItemTemplate>
                                 <asp:LinkButton ID="linkbtn_adjust" runat="server">
                                       View Tax
                                        </asp:LinkButton>
                                <asp:Panel ID="pnlShowHover2" runat="server" Width="400px" BackColor="Beige" BorderWidth="2px"
                                         >    <%--Height="150px" ScrollBars="Auto"--%>
                                            <asp:GridView ID="grd_adjustment" runat="server" Width="400px" CssClass="SmallFont"
                                                AutoGenerateColumns="False" Height="60px">
                                                <Columns>
                                                    <asp:BoundField DataField="COMPO_CODE" HeaderText="Comp&nbsp;Code" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BASE_COMPO_CODE" HeaderText="Base&nbsp;Compo&nbsp;Code" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPO_SL" HeaderText="Comp&nbsp;Sl" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPO_TYPE" HeaderText="Compo&nbsp;type" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RATE" HeaderText="Rate" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="HoverMenuExtender12" runat="server" TargetControlID="linkbtn_adjust"
                                            PopupControlID="pnlShowHover2" PopupPosition="Left" PopDelay="10">
                                        </cc1:HoverMenuExtender>
                                
                                </ItemTemplate>
                                
                                </asp:TemplateField>
                               

                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle"/>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" valign="top" id="tdtran_id" runat="server" class="tContentArial td ">
                        <strong>Transcation Detail </strong>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                       
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
