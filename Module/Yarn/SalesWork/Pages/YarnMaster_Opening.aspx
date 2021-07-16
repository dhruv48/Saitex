<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="YarnMaster_Opening.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_YarnMaster_Opening"
    Title="Untitled Page" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
    <style type="text/css">
        .tdLeft
        {
            width: -110%;
        }
    </style>
    
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
    .style1
    {
        height: 207px;
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

    <style type="text/css">
    .style1
    {
        height: 21px;
        color:White;
    }
</style>

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
       <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table>
                <tr>
                    <td valign="top">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <table>
                                        <tr>
                                            
                                            
                                            <td id="tdUpdate" runat="server">
                                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg" TabIndex="65"
                                                    OnClick="imgbtnUpdate_Click" OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }"
                                                    ToolTip="Update" ValidationGroup="M1" />
                                            </td>
                                            <td id="tdDelete" runat="server">
                                                <asp:ImageButton ID="imgbtnDelete" Visible="false" runat="server" Enabled="false" ImageUrl="~/CommonImages/del6.png" TabIndex="66"
                                                    OnClick="imgbtnDelete_Click" OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }"
                                                    ToolTip="Delete" CausesValidation="false" />
                                            </td>
                                            <td id="tdFind" runat="server" visible="false">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png" TabIndex="66"
                                                    OnClick="imgbtnFind_Click1" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }"
                                                    ToolTip="Find"  CausesValidation="false" />
                                            </td>
                                             <td id="tdList" runat="server">
                                                <asp:ImageButton ID="imgbtnList" runat="server" ImageUrl="~/CommonImages/list.jpg" TabIndex="67"
                                                    ToolTip="Poy Master List" onclick="imgbtnList_Click" />
                                            </td>
                                            <td id="tdPrint" runat="server">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png" 
                                                    OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }"
                                                    ToolTip="Print" TabIndex="68" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }"
                                                    ToolTip="Clear" TabIndex="69" CausesValidation="false"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="70" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="71"  CausesValidation="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TableHeader td">
                                    <span class="titleheading"><b>Yarn Opening</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                                        &nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td class="tdRight" width="15%">
                                                Yarn Code*
                                            </td>
                                            <td width="15%" valign="top">
                                                <asp:TextBox ID="txtYarnCode" runat="server" CssClass="SmallFont TextBoxNo TextBoxDisplay"
                                                    ReadOnly="True" Width="125" TabIndex="1"></asp:TextBox>
                                               <%-- <asp:DropDownList ID="ddlyarncode" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                 CssClass="SmallFont" TabIndex="4" Width="125" Visible="False" 
                                                 onselectedindexchanged="ddlyarncode_SelectedIndexChanged1"
                                            
                                                    >
                                                    
                                                </asp:DropDownList>--%>
                                        <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                                    DataTextField="YARN_DESC" DataValueField="YARN_CODE" EmptyText="Find Yarn Code" EnableLoadOnDemand="true"
                                                    Height="200px" MenuWidth="500" OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged1"
                                                    TabIndex="1"  Width="125px" Visible="False"  EnableVirtualScrolling="true">
                                                    <HeaderTemplate>
                                                        <div class="header c5">
                                                            YARN CODE</div>
                                                       
                                                        <div class="header c3">
                                                            YARN Description</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c5">
                                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("YARN_CODE") %>' /></div>
                                                        <div class="item c3">
                                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_DESC") %>' /></div>
                                                        
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                                
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtYarnCode"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM12"></asp:RequiredFieldValidator>
                                            </td>
                                             <td class="tdRight" width="15%">
                                                Yarn&nbsp;Description*
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server"
                                                    ControlToValidate="txtYarnDescription" Display="Dynamic" ErrorMessage="*"
                                                    SetFocusOnError="True" ValidationGroup="YM" ></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="94%">
                                                <asp:TextBox ID="txtYarnDescription" runat="server" CssClass="SmallFont TextBox"
                                                    MaxLength="50" TabIndex="6" Width="84%" ReadOnly="false" 
                                                   AutoPostBack="true" ></asp:TextBox> 
                                                   <asp:TextBox ID="txtYarnType" runat="server" CssClass="SmallFont TextBox"
                                                    ReadOnly="True" Width="125" ></asp:TextBox>
                                               
                                            </td>
                                             
                                     
                                        
                                    </table>
                                </td>
                            </tr>
                           
                            
       
                               <tr id="tropeningbalstock"  runat="server"   >
                                <td class="td" >
                                    <b>Opening Balance Detail....</b>
                  <table width="100%" >
                 <tr class="TableHeader td" width="100%">
                 <td  width="5%" class="tdLeft SmallFont"> 
                  <span class="titleheading"><b>Party</b></span>
                 </td>
                 
                 <td  width="5%" class="tdLeft SmallFont"> 
                  <span class="titleheading"><b>Jober Name</b></span>
                 </td>
                 
                 <td  width="5%" class="tdLeft SmallFont"> 
                  <span class="titleheading"><b>Color</b></span>
                 </td>
                 <td  width="10%" class="tdLeft SmallFont"> <span class="titleheading"><b>RGB</b></span> &nbsp;
                 <asp:TextBox ID="txtRGBColor" runat="server" CssClass="TextBox SmallFont" Width="15px"
                             TabIndex="17" MaxLength="9" Enabled="False"></asp:TextBox>
                 </td>
                 <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Location</b></span></td>
                         <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Store</b></span></td>
                         <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Lot No</b></span></td>
                         <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Grade</b></span></td>
                  
                 <td width="5%" class="tdLeft SmallFont"> <span class="titleheading"><b>Cartoons</b></span>
                 </td>
                 <td width="5%" class="tdLeft SmallFont"> <span class="titleheading"><b>Op.Stock</b></span>
                 </td>
                 <td width="5%" class="tdLeft SmallFont"> <span class="titleheading"><b>Op&nbsp;Rate</b></span>
                 </td>
                    <td  class="tdLeft SmallFont"> <span class="titleheading"></span>
                 </td>
                 </tr>
                 
                 <tr>
                     <td width="5%">
                   <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="400px" Height="200px"  >
                            <HeaderTemplate>
                                <div class="header c5">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>

                                  <asp:HiddenField ID="txtPartyName" runat="server"  />                                      
                 
                                            
                 </td>  
                 
                 
                 
                   <td width="5%">
                   <cc2:ComboBox ID="cmbJober" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtJoberCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="" 
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="400px" Height="200px"  >
                            <HeaderTemplate>
                                <div class="header c5">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>ssss
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>

                                  <asp:HiddenField ID="HiddenField1" runat="server"  />                                      
                 
                                            
                 </td>  
                 
                 
                 
                 <td width="5%">
                 <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="Combined" EnableLoadOnDemand="True"
                                                MenuWidth="300" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="54" EmptyText="Select Color" 
                                                Height="200px" Visible="true" Width="100px" OnLoadingItems="cmbShade_LoadingItems"
                                                OnSelectedIndexChanged="cmbShade_SelectedIndexChanged">
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
                                            <asp:TextBox ID="txtShadeFamily" runat="server" Width="30px" Visible="false" ></asp:TextBox>      
                                            <asp:TextBox ID="txtShade" runat="server" Width="30px" Visible="false"></asp:TextBox>                                    
                 
                                            
                 </td>  
                 <td width="10%">
                  <asp:TextBox ID="txtRGB" runat="server" CssClass="TextBox SmallFont" Width="60px"
                             TabIndex="55" MaxLength="11" AutoPostBack="true" 
                         ontextchanged="txtRGB_TextChanged"></asp:TextBox>
                                               
                 
                 </td>     
                    <td>
                     <asp:DropDownList ID="ddlLocation" runat="server" TabIndex="56" CssClass="SmallFont" 
                Width="80px">
            </asp:DropDownList>
                     </td>
                     <td>
                     
                   <asp:DropDownList ID="ddlStore" runat="server" TabIndex="57" CssClass="SmallFont" 
                Width="80px">
            </asp:DropDownList>
          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStore"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="True"
                                                    ></asp:RequiredFieldValidator>--%>
            
                     </td>      
                     <td >
                  <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Lot No" 
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="300px" Height="200px"  TabIndex="28">
                            <HeaderTemplate>
                                <div class="header c5">
                                    Lot No</div>
                                <div class="header c5">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                 </td>
                 <td >
                  <cc2:ComboBox ID="txtGrade" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtGrade_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Grade" 
                            EnableVirtualScrolling="true" Width="80px" MenuWidth="100px" Height="200px"  TabIndex="29">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Grade</div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' />
                                    </div>
                               
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                 </td> 
                 <td align="center" width="5%">
                  <asp:Button ID="btnLotDetails" runat="server" Text="Cartoons"    TabIndex="59" 
                         Width="60px" CssClass="SmallFont" onclick="btnLotDetails_Click"  />                                           
                  
                 </td>  
                  <td width="8%">
                  <asp:TextBox ID="txtOpeningBal" runat="server" CssClass="TextBox SmallFont" Width="60px"
                             TabIndex="58" MaxLength="7"></asp:TextBox>
                 </td>
                 
                 <td width="5%">
                  <asp:TextBox ID="txtOpenRate" runat="server" CssClass="TextBox SmallFont" Width="50px"
                             TabIndex="60" MaxLength="5"></asp:TextBox>
                             <asp:TextBox ID="txtMinStock" runat="server" CssClass="TextBox SmallFont" Width="50px"
                             TabIndex="61" MaxLength="5" Visible="false"></asp:TextBox>
                             <asp:TextBox ID="txtMaxStock" runat="server" CssClass="TextBox SmallFont" Width="50px"
                             TabIndex="62" MaxLength="5" Visible="false"></asp:TextBox>
                   <asp:HiddenField ID="txtCartoons" runat="server" />
                             <asp:HiddenField ID="txtGrossWt" runat="server" />         
                             <asp:HiddenField ID="txtTareWt" runat="server" />
                            <asp:HiddenField ID="txtNoOfUnit" runat="server"      />
                            <asp:HiddenField ID="txtWeightOfUnit" runat="server"   />
                            <asp:HiddenField ID="txtNoOfPallet" runat="server"   />
                            <asp:HiddenField ID="txtDyedBatchNo" runat="server"   />
                 </td>
                   
                 
                  <td  width="14%"> 
                  
                  <asp:Button ID="lbtnsavedetail" runat="server" Text="Add"   
                          onclick="lbtnsavedetailColor_Click" TabIndex="63" Width="50px" 
                          CssClass="SmallFont"  />                                           
                        <asp:Button ID="lbtnCancel" runat="server" Text="Cancel" TabIndex="64"
                            OnClick="lbtnCancel_Click1" Width="50px"  CssClass="SmallFont" />
                 </td>
                 </tr>
                 <tr >
                 <td colspan="11">
                    <asp:GridView ID="grdColorDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                    BorderWidth="1px"  
                    AutoGenerateColumns="False" AllowSorting="True" Width="98%"  ShowFooter="true"
                         onrowcommand="grdColorDetail_RowCommand" 
                         onrowdatabound="grdColorDetail_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="2%" />
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Party">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtParty" runat="server" Text='<%# Bind("PRTY_NAME") %>' ToolTip='<%# Bind("PRTY_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>       
                        
                        <asp:TemplateField HeaderText="Jober">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtJober" runat="server" Text='<%# Bind("JOBER_NAME") %>' ToolTip='<%# Bind("JOBER") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        
                                    
                        <asp:TemplateField HeaderText="Shade Family">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtShadeFamily" runat="server" Text='<%# Bind("SHADE_FAMILY") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Shade">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtShade" runat="server" Text='<%# Bind("SHADE") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RGB">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtRGB" runat="server" Text='<%# Bind("RGB") %>' CssClass="Label SmallFont"></asp:Label>
                                <asp:TextBox ID="txtRGBColor" runat="server" CssClass="TextBox SmallFont" Width="15px" Enabled="False"></asp:TextBox>
                 
                       </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtlocation" runat="server" Text='<%# Bind("LOCATION") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Store">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtstore" runat="server" Text='<%# Bind("STORE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                         <asp:TemplateField HeaderText="Lot No">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtLotNo" runat="server" Text='<%# Bind("LOT_NO") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderText="Grade">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="7%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtGrade" runat="server" Text='<%# Bind("GRADE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Batch No">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate> 
                                <asp:Label ID="txtDyedBatchNo" runat="server" Text='<%# Bind("DYED_BATCH") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                             <asp:Label ID="lblDyedBatchNo" runat="server" 
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Op.Stock">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningStock" runat="server" Text='<%# Bind("OP_BAL_STOCK") %>'
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                             <asp:Label ID="lblTotalOpeningStock" runat="server" 
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Op.Rate">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="Min Stock" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_MinStock" runat="server" Text='<%# Bind("MIN_STOCK") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Max Stock" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_MaxStock" runat="server" Text='<%# Bind("MAX_STOCK") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cops" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_NO_OF_UNIT" runat="server" Text='<%# Bind("NO_OF_UNIT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Avg Weight" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_WEIGHT_OF_UNIT" runat="server" Text='<%# Bind("WEIGHT_OF_UNIT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cortoons">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtCortoons" runat="server" Text='<%# Bind("CARTONS") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                             <asp:Label ID="lblTotalCortoons" runat="server"  CssClass="LabelNo SmallFont"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="G.Wt.">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtGrossWt" runat="server" Text='<%# Bind("GROSS_WT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblTotalGrossWt" runat="server" Text='<%# Bind("TARE_WT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T.Wt." >
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtTareWt" runat="server" Text='<%# Bind("TARE_WT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblTotalTareWt" runat="server" Text='<%# Bind("TARE_WT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                              
                         <asp:TemplateField HeaderText="Lot Details">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                 <asp:LinkButton ID="lnkunige" runat="server" CssClass="Label SmallFont" Text="View"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'>
                                </asp:LinkButton>
                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                    <asp:TemplateField HeaderText="Sl&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="PI No" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Batch&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblDyedBatchNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DYED_BATCH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                              <FooterTemplate>
                                                    <asp:Label ID="flblBOMUOM" runat="server" CssClass="LabelNo SmallFont"  >Total:</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carton&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbtcartonno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTON_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flbtcartonno" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cops">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblNoUnit" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Gross&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblGrossWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tare&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate >
                                                    <asp:Label ID="flblTareWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                      <FooterTemplate>
                                                    <asp:Label ID="flblQTY" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar&nbsp;Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarcodeNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BARCODE_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is&nbsp;Pallet">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIsPallet" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("IS_PALLET") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               
                                                <asp:TemplateField HeaderText="WeightofUnit">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc11:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="lnkunige"
                                    PopupPosition="Left">
                                </cc11:HoverMenuExtender>
                            </ItemTemplate>
                        </asp:TemplateField>              
                        <asp:TemplateField >
                                    <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="colorEdit" 
                                            CommandArgument='<%# Eval("UniqueId") %>' Width="45px" CssClass="SmallFont" CausesValidation="false" >
                                            </asp:Button>
                                            
                                            <asp:Button ID="lnkDelete"
                                                runat="server" Text="Delete" CommandName="colorDelete"  CommandArgument='<%# Eval("UniqueId") %>' Width="45px" CssClass="SmallFont" CausesValidation="false" >
                                            </asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                   </td>
                
                 </tr>
                 </table>   
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <cc11:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
            TargetControlID="txtRGB"
             Mask="999,999,999"
             MessageValidatorTip="true"             
             MaskType="Number"
             InputDirection="LeftToRight"            
             ErrorTooltipEnabled="True" />
                                           
               
           <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="YM" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="AC" />
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="BB" />
            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="BA" />--%>
       <%-- </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="txtLotNo" />
         <asp:PostBackTrigger ControlID="txtGrade" />
         <asp:PostBackTrigger ControlID="txtYarnDescription" />
          <asp:PostBackTrigger ControlID="imgbtnUpdate" />
           <asp:PostBackTrigger ControlID="imgbtnClear" />
        </Triggers>
    </asp:UpdatePanel>--%>
    
</asp:Content>
