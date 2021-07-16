<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceOrderList.ascx.cs" 
Inherits="Module_OrderDevelopment_CustomerRequest_Controls_InvoiceOrderList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table align="left" class="tContentArial" width="100%">
     <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1"  OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png"  OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" ></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg"  OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" ></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png"  OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
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
                <td>
                    <asp:Panel ID="pnlFilter" runat="server">
                        <table>
                            <tr>
                            <td align="right" style="width: 12%;">
                                    Branch :
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 12%;">
                                    INVOICE&nbsp;NO :
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:DropDownList ID="ddlinvoice" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 12%;">
                                    YARN&nbsp;CODE :
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:DropDownList ID="ddlyrancode" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                                 <td align="right" style="width: 12%;">
                                    LOT&nbsp;NO:
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:DropDownList ID="ddllotno" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td></tr>
                                <tr>
                                  <td align="right" style="width: 12%;">
                                    FROM&nbsp;DATE:
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:TextBox ID="txtFROMDATE" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                                </td>
                                 <td align="right" style="width: 12%;">
                                    TO&nbsp;DATE:
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:TextBox ID="txtTODATE" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                                </td>
                                  <td align="right" style="width: 12%;">
                                    YEAR :
                                    
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                                 <td align="left" style="width: 4%;">
                                    <asp:Button ID="btnShow" runat="Server" Text="Get Records"  CssClass="AButton" OnClick="btn_serch" />
                                </td>
                                </tr></table>
                                </asp:Panel>
            
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Invoice Order ListQuery Form</b>
                </td>
            
            
            </tr>
             <tr>
                <td align="left" class="td" width="100%" valign="top">
                    <table style="width: 550px">
                        <tr>
                            <td>
                                <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                                <%--<asp:Button ID="btnPrint" runat="Server" Text="Print Records" 
                                    onclick="btnPrint_Click" />--%>
                                <asp:Button ID="btnExcel" runat="Server" Text="Excel"  Visible="false" />
                                <asp:Button ID="btnWord" runat="Server" Text="Word"  Visible="false" />
                            </td>
                            <td align="center">
                                <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                                    <ProgressTemplate>
                                        <h3>
                                            Loading...
                                        </h3>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            <td align="left" class="td" width="100%">
             <asp:GridView ID="grdInvoiceOrderList" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" Font-Size="7pt" CellPadding="3" GridLines="Vertical"
                            Width="100%"  PagerStyle-HorizontalAlign="Left" 
                            EmptyDataText="No Record Found" PageSize="15" 
                             BackColor="White" 
                            BorderStyle="Ridge" OnPageIndexChanging="grdInvoiceOrderList_PageIndexChanging">
                                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                             <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                             <Columns>
                             
                                    <asp:TemplateField HeaderText="year" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("YEAR") %>' runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("INVOICE_NUMB") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Invoice&nbsp;Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("INVOICE_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                   
                                      <asp:TemplateField HeaderText="Party&nbsp;Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Product&nbsp;Type" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("PRODUCT_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product&nbsp;Details" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("PRODUCT_DETAILS") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Agents&nbsp;Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("AGENT_NAME") %>' ToolTip='<%#Eval("AGENT_CODE") %>'  runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Po&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("PO_NUMB") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="LR&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("LR_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Yarn&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("YARN_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Yarn&nbsp;Desc" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("YARN_DESC") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shade&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("SHADE_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="LOT&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("LOT_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Do&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("DOC_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="NO&nbsp;Of&nbsp;Packages" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("NO_OF_UNIT") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("QUANTITY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("RATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("AMOUNT") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Vehicle&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("VEHICLE_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TRSP&nbsp;NAME" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("TRSP_NAME") %>' ToolTip='<%#Eval("TRSP_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                            
                            
                            
                            
                           </Columns>
                             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
            
            
            </td>
            
            
            </tr>
            </table>
        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFROMDATE" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtTODATE" Format="dd/MM/yyyy"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="ME1" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtFROMDATE" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="ME2" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtTODATE" PromptCharacter="_">
        </cc1:MaskedEditExtender>
    
    
    
    
    
    </ContentTemplate>
    </asp:UpdatePanel>
