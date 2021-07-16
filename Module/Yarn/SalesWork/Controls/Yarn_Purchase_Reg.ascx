<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_Purchase_Reg.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_Purchase_Reg" %>
    <%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 130px;
    }
    .c2
    {
        margin-left: 4px;
        width: 500px;
    }
    .c3
    {
        margin-left: 4px;
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
    .c6
    {
        margin-left: 5px;
        width: 80px;
    }
    .c7
    {
        margin-left: 4px;
        width: 430px;
    }
    .style1
    {
        height: 207px;
    }
    .style3
    {
        border: .05em ridge #C1D3FB;
        height: 20px;
    }
</style>

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
        width: 200px;
    }
    .c2
    {
        margin-left: 4px;
        width: 300px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>
<table align="left" class="tContentArial" width="100%" >
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                
                 <td>
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
                    </td>
                
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            <table width="100%" class=" td tContentArial">
                <tr>
                    <td align="center" class="TableHeader td" colspan="8">
                        <span class="titleheading"><strong>Yarn Purchase Register Detail</strong> </span>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1112" runat="server">
                <ContentTemplate>
                 <table width="100%" >  
                 <tr>
                   <td align=center>
                    <table class=" td tContentArial" width ="80%" >
                        <tr>
                            <td align="right">
                                Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="8"
                                    Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Year:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                From&nbsp;date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td align="right">
                                To&nbsp;Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                                    runat="server" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Yarn&nbsp;Catagory:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlyarncat" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Yarn&nbsp;Type:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlyarntype" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Party:
                            </td>
                            <td class="tdLeft">
                                <%--<asp:DropDownList ID="ddlpartycode" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>--%>
                                
                                <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"
                            EmptyText="Select Vendor" 
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="400px" Height="150px" 
                                   >
                            <HeaderTemplate>
                                <div class="header c6">
                                    Code</div>
                                <div class="header c7">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c6">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c7">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                                
                                
                            </td>
                            <td align="right">
                                Location:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddllocation" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            
                            <td>
                            </td>
                            
                        </tr>
                        <tr>
                        <td align="right">
                                Store:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
            Yarn&nbsp;Shade:
        </td>
        <td>
           <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_NAME" EnableLoadOnDemand="True"
                                                MenuWidth="300" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="150px" OnLoadingItems="cmbShade_LoadingItems"
                                                >
                                                <HeaderTemplate>                                                  
                                                    <div class="header d2">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox> 
        </td>
                           
                                   <td align="right"> Yarn Code</td>
                                   <td align="center"> 
                                   
                                                       <cc2:ComboBox ID="ddlYarn" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                        DataTextField="YARN_CODE" DataValueField="YARN_CODE" EnableLoadOnDemand="true"
                                                        MenuWidth="680" OnLoadingItems="Item_LOV_LoadingItems"  
                                                        EnableVirtualScrolling="true" OpenOnFocus="true"  Width="150px"
                                    TabIndex="9" Visible="true"
                                                        Height="200px" >
                                                        <HeaderTemplate>
                                                            <div class="header c6">
                                                                YARN CODE</div>
                                                            <div class="header c7">
                                                                YARN DESCRIPTION</div>
                                                            <div class="header c5">
                                                                TYPE</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div class="item c6">
                                                                <%# Eval("YARN_CODE") %></div>
                                                            <div class="item c7">
                                                                <%# Eval("YARN_DESC") %></div>
                                                            <div class="item c5">
                                                                <%# Eval("YARN_TYPE")%></div>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Displaying items
                                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                            out of
                                                            <%# Container.ItemsCount %>.
                                                        </FooterTemplate>
                                                    </cc2:ComboBox></td>
                                        <td align="left">
                                            <asp:Button ID="btngetdata" runat="server" CssClass="AButton" 
                                                OnClick="btngetdata_Click" Text="Get Data" />
                                        </td>
                                 
                               
                            </tr>
                            <tr>
                            <td class="TdBackVir" width="45%" colspan = "8">
                                <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                          
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                            
                        </tr>
                    </table>
                    </td>
                  </tr>
                  </table>
                    <table width="100%" >
                        <tr>
                            <td >
                                
                                <asp:Panel ID="pnl121" runat="server" ScrollBars="Auto" Width="100%" 
                                    Height="300px" BorderStyle="None">
                                    <asp:GridView ID="grd_yarnPurchase_query" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="3" BorderStyle="None" CssClass="smallfont"
                                        Font-Size="X-Small" PagerStyle-HorizontalAlign="Left"
                                        Width="200%" 
                                    
                                        OnPageIndexChanging="grd_yarnPurchase_query_PageIndexChanging" 
                                        
                                        OnRowDataBound="GridSpinningThread_RowDataBound"
                                       ForeColor="#333333" PageSize="14">
                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                        <Columns>
                                            <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
                                            
                                            <asp:TemplateField  HeaderText="YEAR" Visible="false" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblYEAR" runat="server" Text='<%#Eval("YEAR") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField  HeaderText="YARN DESCRIPTION" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblYarnDesc" runat="server" Text='<%#Eval("YARN_DESC") %>' ToolTip='<%#Eval("YARN_CODE") %>'></asp:Label>
      
       <asp:Label ID="lblYarn" runat="server" Text='<%#Eval("YARN_CODE") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="YARN SHADE FAMILY" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblshadfam" runat="server" Text='<%#Eval("SHADE_FAMILY") %>' ToolTip='<%#Eval("SHADE_FAMILY") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="YARN SHADE CODE" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblshadcode" runat="server" Text='<%#Eval("SHADE_CODE") %>' ToolTip='<%#Eval("SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="TRN_DESC" HeaderText="TRNAS. DESCRIPTION" />
                                            <asp:BoundField DataField="MRN" HeaderText="MRN" />
                                            <asp:BoundField DataField="MRN_DATE" HeaderText="MRN DATE" />
                                            <asp:BoundField DataField="GATE_NUMB" HeaderText="GATE NUMBER" />
                                            <asp:BoundField DataField="GATE_DATE" HeaderText="GATE DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                           
                                          
                                            <asp:TemplateField  HeaderText="PARTY NAME" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblPartyName" runat="server" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>       
                                            <asp:TemplateField  HeaderText="TRNSPORT" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblTRSP_CODE" runat="server" Text='<%#Eval("TRSP_NAME") %>' ToolTip='<%#Eval("TRSP_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>                                    
                                       
                                            <asp:BoundField DataField="LORY_NUMB" HeaderText="LORY NUMB" />
                                            <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="PRTY NUMB" />
                                            <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMB" />
                                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                                            <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                                            <asp:BoundField DataField="STORE" HeaderText="STORE" />
                                            
                                            
                                            
                                            
                                                <asp:TemplateField HeaderText="Delivery&nbsp;Date">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Idn_Adj" runat="server" CommandArgument='<%#Bind("PO_NUMB") %>'
                                                                    Text="Delivery Dates"></asp:LinkButton>
                                                                <asp:Panel ID="IdnPanel" runat="server" BackColor="White">
                                                                    <asp:GridView runat="server" ID="Idn_grid" AutoGenerateColumns="false" CssClass="SmallFont">
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                             <asp:TemplateField HeaderText="Yarn&nbsp;Code" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtYarnCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delivery&nbsp;Date" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DELIVERY_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <cc4:HoverMenuExtender ID="idnHover" runat="server" PopupControlID="IdnPanel" PopupPosition="Left"
                                                                    TargetControlID="Idn_Adj">
                                                                </cc4:HoverMenuExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                            
                                            
                                            
                                            
                                            
                                        </Columns>
                                       
  <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
