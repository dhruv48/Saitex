<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_NewLRGenerationNew.ascx.cs" Inherits="Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fiber_NewLRGenerationNew" %>
<%@ Register Src="~/CommonControls/LOV/ApproveLRLOV.ascx"TagName="ApproveLRLOV"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<style type="text/css">
    .header
    {
        font-weight: bold;
        position: absolute;
        background-color: #507CD1;
        font-weight: bold;
        color: White;
        text-align: left;
        vertical-align: top;
    }
</style>--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" class="tContentArial">
            <tr>
                <td width="100%">
                    <table>
                        <tr>
                            <td id="tdUpdate" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    ToolTip="Update" Width="48" OnClick="imgbtnUpdate_Click" />
                            </td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td id="tdPrint" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">LR Generation</b>
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
                <td>
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Panel ID="pnlLRGeneration" runat="server" Height="400px" Width="99%" ScrollBars="Auto">                        
                        <asp:GridView ID="grdLrGeneration" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            RowStyle-VerticalAlign="Bottom" Font-Size="10px" ForeColor="#333333" 
                            GridLines="None" onrowdatabound="grdLrGeneration_RowDataBound" Width="99%">
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CR No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_NO" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CR Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_DATE" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}" ) %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField Visible="FALSE" HeaderText="Customer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRTY_CODE" runat="server" Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="Customer Details">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRTY_ADD" runat="server" Text='<%# Bind("PRTY_ADD") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField Visible="FALSE" HeaderText="Article">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBSTRATE" runat="server" Text='<%# Bind("SUBSTRATE") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFABRIC_DESC" runat="server" Text='<%# Bind("FABR_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>                                                      
                                <asp:TemplateField HeaderText="Shade Family Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShadeFamilyCode" runat="server" Text='<%# Bind("SHADE_FAMILY_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade Family"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShadeFamilyName" runat="server" Text='<%# Bind("SHADE_FAMILY_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShadeName" runat="server" Text='<%# Bind("SHADE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Artical No" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderRefNo" runat="server" Text='<%# Bind("ORDER_REF_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="1st Light Source">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLIGHT_SOURCE" runat="server" Text='<%# Bind("LIGHT_SOURCE") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="2nd Light Source" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblLIGHT_SOURCE1" runat="server" Text='<%# Bind("LIGHT_SOURCE1") %>'></asp:Label>
                                    </ItemTemplate>
                                 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Generate&nbsp;LR" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkLRGenerate" runat="server" Checked="false" />
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lab&nbsp;Dip&nbsp;No">
                                    <ItemTemplate>                                        
                                        <asp:DropDownList ID="ddlApproveLR" runat="server" AppendDataBoundItems="True"                                              
                                             CssClass="SmallFont TextBox UpperCase">
                                        </asp:DropDownList>                                       
                                    </ItemTemplate>                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Series" >
                                    <ItemTemplate>                                        
                                        <asp:DropDownList ID="ddlLRSeries" AppendDataBoundItems="true" runat="server" Width="40px"
                                            CssClass="SmallFont">
                                             <asp:ListItem>P</asp:ListItem>
                                            <asp:ListItem>C</asp:ListItem>
                                            <asp:ListItem>N</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req Delivery Date" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReqDelDate" CssClass="SmallFont" runat="server" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtReqDelDate" runat="server"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle Height="25px" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />                            
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <%-- </div>--%></asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

