<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_Dyeing_Approval.ascx.cs" Inherits="Module_OrderDevelopment_Controls_Yarn_Dyeing_Approval" %>
<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="td tContentArial" width="95%">
            <tr>
                <td class="td tContentArial">
                    <table>
                        <tr>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td id="tdPrint" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Width="48" onclick="imgbtnPrint_Click" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading"><b>Production Planning For Dyeing</b></span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top">
                               PA Order No :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBox" TabIndex="1"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Party Code :
                            </td>
                            <td align="left" valign="top">
                                <uc1:PartyCodeLOV ID="ddlprtycode" runat="server" TabIndex="4" Width="75px" />
                            </td>
                            <td align="right" valign="top">
                                Order Type :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlordertype" runat="server" TabIndex="3" Width="120px" CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                From Date : &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TxtFdate" runat="server" CssClass="TextBox" TabIndex="5"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                To Date:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TxtTdate" runat="server" CssClass="TextBox" TabIndex="4"></asp:TextBox>
                            </td>
                            <td align="right">
                                Status:
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    TabIndex="3" Width="120px">
                                    <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem Selected="True">Not-Planned</asp:ListItem>
                                    <asp:ListItem>Planned</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnview" runat="server" CssClass="SmallFont" OnClick="btnview_Click"
                                    Text="Show" />
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            
            <tr>
                <td class="td" width="100%" valign="top" align="center">
                   
                         <b style="font-size:larger;">Total Records :
                       <asp:Label ID="lblTotalRecord" runat="server" Text=""></asp:Label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Refresh Grid Data" Width="100px"
                        CssClass="SmallFont" />  </td>
                
            </tr>
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                        <span class="titleheading"><b>
                            <%--<asp:GridView ID="OrderCapt_Grid" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="4" Font-Size="10px" ForeColor="#333333"
                                GridLines="None" OnPageIndexChanging="OrderCapt_Grid_PageIndexChanging" OnRowDataBound="OrderCapt_Grid_RowDataBound"
                                Width="100%" OnRowCommand="OrderCapt_Grid_RowCommand" OnSelectedIndexChanged="OrderCapt_Grid_SelectedIndexChanged">
                                <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField Visible="false" HeaderText=" PROS ROUTE CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROS_ROUTE_CODE" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Comp Code" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOMP_CODE" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PA NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpinov" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ARTICAL_CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblARTICAL_CODE" runat="server" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPI_TYPE" runat="server" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="YEAR" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYEAR" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderTYPE" runat="server" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER CAT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblordercat" runat="server" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ORDER NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderno" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER DATE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderdt" runat="server" Text='<%# Eval("ORDER_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER PROCESS" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprderpro" runat="server" Text='<%# Eval("ORDER_PROCESS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRTY CODE " ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPRTYCODE" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRTY NAME" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="180px" Visible="false" HeaderText="PRTY ADDRESS"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPRTYADD" runat="server" Text='<%# Eval("PRTY_ADD1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="BUSINESS TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbusinesstype" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PRODUCT TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblproducttype" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PARTY REF No" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyrefno" runat="server" Text='<%# Eval("PARTY_REF_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PARTY REF DATE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyrefdt" runat="server" Text='<%# Eval("PARTY_REFERENCE_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="CONSIGNEE CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconscode" runat="server" Text='<%# Eval("CONSIGNEE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="90px" Visible="false" HeaderText="CONSIGNEE NAME"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconsname" runat="server" Text='<%# Eval("CONSIGNEE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="140px" Visible="false" HeaderText="CONSIGNEE ADD"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1240px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconsadd" runat="server" Text='<%# Eval("CONSIGNEE_ADD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="140px" />
                                        <ItemStyle HorizontalAlign="Left" Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PAYMENT MODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaymentmod" runat="server" Text='<%# Eval("PAYMENT_MODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PAYMENT TERMS" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaymenttrm" runat="server" Text='<%# Eval("PAYMENT_TERMS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="FROM BRANCH" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfrombrnch" runat="server" Text='<%# Eval("FROM_BRANCH") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PA NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpino" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PI TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpitype" runat="server" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ARTICAL CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblarticlecode" runat="server" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ARTICAL DESC" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYARN_DESC" runat="server" Text='<%# Eval("YARN_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SHADE CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHADE_CODE" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="UOM" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluom" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ORD&nbsp;QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblordqty" runat="server" Text='<%# Eval("ORD_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="DESIGN" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesign" runat="server" Text='<%# Eval("DESIGN") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="LOT ID" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllotid" runat="server" Text='<%# Eval("LOT_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="SRINKAGE" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrinkge" runat="server" Text='<%# Eval("SRINKAGE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="TOTAL COST" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TOTAL_COST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PACKING QTY" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpakingqty" runat="server" Text='<%# Eval("PACKING_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="INVOICE QTY" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoice" runat="server" Text='<%# Eval("INVOICE_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="20px" Visible="false" HeaderText="PROS ROUTE CODE"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprosrtcode" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="CUST REQ NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustrqno" runat="server" Text='<%# Eval("CUST_REQ_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="DEL ADDRESS" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeladd" runat="server" Text='<%# Eval("DEL_ADDRESS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View CR" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>                                           
                                            <asp:LinkButton ID="linkViewCR" runat="server" CommandName="viewCR" CssClass="Label SmallFont"
                                                Text="View CR">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOM" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbom" runat="server" Text=""></asp:Label>
                                            <asp:LinkButton ID="linkbom" runat="server" CommandName="viewbom" CssClass="Label SmallFont"
                                                Text="View BOM">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="BOM FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBOM_FLAG" runat="server" Text='<%# Eval("BOM_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText=" FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFINAL_ORDER_CONF_CLAG" runat="server" Text='<%# Eval("FINAL_ORDER_CONF_CLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COST" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcost" runat="server" Text=""></asp:Label>
                                            <asp:LinkButton ID="Linkcost" runat="server" CommandName="viewcost" CssClass="Label SmallFont"
                                                Text="View Cost">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="COST PRICE FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOST_PRICE_FLAG" runat="server" Text='<%# Eval("COST_PRICE_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PROCESS ROOT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Linkprocessroot" runat="server" Text="ViewProcessRoute" CommandName="viewprroot"
                                                CssClass="Label SmallFont">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PROCESS ROUTE FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROCESS_ROUTE_FLAG" runat="server" Text='<%# Eval("PROCESS_ROUTE_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FLAG">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>--%>
                            
                            
                            <asp:GridView ID="OrderCapt_Grid" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CellPadding="4" Font-Size="10px" ForeColor="#333333"
                                GridLines="None" OnPageIndexChanging="OrderCapt_Grid_PageIndexChanging" OnRowDataBound="OrderCapt_Grid_RowDataBound"
                                Width="100%" OnRowCommand="OrderCapt_Grid_RowCommand" >
                                <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField Visible="false" HeaderText=" PROS ROUTE CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROS_ROUTE_CODE" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Comp Code" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOMP_CODE" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false" HeaderText="Year" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PA NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpinov" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ARTICAL_CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblARTICAL_CODE" runat="server" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPI_TYPE" runat="server" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderTYPE" runat="server" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="ORDER CAT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblordercat" runat="server" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PA ORDER NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderno" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField Visible="false" HeaderText="ORDER PROCESS" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprderpro" runat="server" Text='<%# Eval("ORDER_PROCESS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRTY CODE " ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPRTYCODE" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRTY NAME" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>' ToolTip='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="180px" Visible="false" HeaderText="PRTY ADDRESS"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="180px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPRTYADD" runat="server" Text='<%# Eval("PRTY_ADD1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="BUSINESS TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbusinesstype" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PRODUCT TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblproducttype" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PARTY REF No" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyrefno" runat="server" Text='<%# Eval("PARTY_REF_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PARTY REF DATE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyrefdt" runat="server" Text='<%# Eval("PARTY_REFERENCE_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="CONSIGNEE CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconscode" runat="server" Text='<%# Eval("CONSIGNEE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="90px" Visible="false" HeaderText="CONSIGNEE NAME"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconsname" runat="server" Text='<%# Eval("CONSIGNEE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="140px" Visible="false" HeaderText="CONSIGNEE ADD"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1240px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconsadd" runat="server" Text='<%# Eval("CONSIGNEE_ADD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="140px" />
                                        <ItemStyle HorizontalAlign="Left" Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PAYMENT MODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaymentmod" runat="server" Text='<%# Eval("PAYMENT_MODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PAYMENT TERMS" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaymenttrm" runat="server" Text='<%# Eval("PAYMENT_TERMS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="FROM BRANCH" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfrombrnch" runat="server" Text='<%# Eval("FROM_BRANCH") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  Visible="false" HeaderText="PA NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpino" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderReq" runat="server" Text='<%# Eval("CUST_REQ_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField Visible="false" HeaderText="PI TYPE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpitype" runat="server" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Technical Quality Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYARN_DESC" runat="server" Text='<%# Eval("YARN_DESC") %>' ToolTip='<%# Eval("YARN_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Quality Name" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblarticlecode" runat="server" Text='<%# Eval("ARTICAL_DESC") %>'  ToolTip='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="SHADE CODE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHADE_CODE" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="UOM" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluom" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ORD&nbsp;QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblordqty" runat="server" Text='<%# Eval("ORD_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="&nbsp;Total Plan Qty" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlanqty" runat="server" Text='<%# Eval("PLAN_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                     
                                 
                    <asp:TemplateField HeaderText="RM Unit-1" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                          <asp:Label ID="lblYarnAvlQty" runat="server" Text='<%# Eval("YARN_STOCK_QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>                                   
                                      
                     <asp:TemplateField HeaderText="RM Unit-3" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                          <asp:Label ID="lblYarnAvlQty1" runat="server" Text='<%# Eval("YARN_STOCK_QTY1") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>                 
                                      
                     <asp:TemplateField HeaderText="Total Stock" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                          <asp:Label ID="lblYarnAvlQtyTotal" runat="server" Text='<%# Eval("YARN_STOCK") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                                     <asp:TemplateField  HeaderText="ORDER DATE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderdt" runat="server" Text='<%# Eval("ORDER_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                       <asp:TemplateField  HeaderText="Del. DATE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeldt" runat="server" Text='<%# Eval("DEL_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField Visible="false" HeaderText="DESIGN" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesign" runat="server" Text='<%# Eval("DESIGN") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="LOT ID" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllotno" runat="server" Text='<%# Eval("LOT_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="SRINKAGE" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrinkge" runat="server" Text='<%# Eval("SRINKAGE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="TOTAL COST" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TOTAL_COST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PACKING QTY" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpakingqty" runat="server" Text='<%# Eval("PACKING_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="INVOICE QTY" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoice" runat="server" Text='<%# Eval("INVOICE_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="20px" Visible="false" HeaderText="PROS ROUTE CODE"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprosrtcode" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="CUST REQ NO" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustrqno" runat="server" Text='<%# Eval("CUST_REQ_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="DEL ADDRESS" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeladd" runat="server" Text='<%# Eval("DEL_ADDRESS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Machine" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>                                           
                                            <asp:LinkButton ID="linkViewCR" runat="server" CommandName="viewCR" CssClass="Label SmallFont"
                                                Text="View Machine Plan">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ISSUE PLAN" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbom" runat="server" Text=""></asp:Label>
                                            <asp:LinkButton ID="linkbom" runat="server" CommandName="viewbom" CssClass="Label SmallFont"
                                                Text="View BOM">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="BOM FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBOM_FLAG" runat="server" Text='<%# Eval("BOM_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText=" FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFINAL_ORDER_CONF_CLAG" runat="server" Text='<%# Eval("FINAL_ORDER_CONF_CLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Machine Planning" ItemStyle-HorizontalAlign="Left"  Visible ="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcost" runat="server" Text=""></asp:Label>
                                            <asp:LinkButton ID="Linkcost" runat="server" CommandName="viewPlan" Visible ="false" CssClass="Label SmallFont"
                                                Text="View Plan">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="COST PRICE FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOST_PRICE_FLAG" runat="server" Text='<%# Eval("COST_PRICE_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PROCESS ROOT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Linkprocessroot" runat="server" Text="ViewProcessRoute" CommandName="viewprroot"
                                                CssClass="Label SmallFont">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="PROCESS ROUTE FLAG" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROCESS_ROUTE_FLAG" runat="server" Text='<%# Eval("PROCESS_ROUTE_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FLAG">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </b></span>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="TxtFdate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="TxtTdate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="TxtTdate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>