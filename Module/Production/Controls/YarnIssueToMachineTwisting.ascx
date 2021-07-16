<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YarnIssueToMachineTwisting.ascx.cs" Inherits="Module_Production_Controls_YarnIssueToMachineTwisting" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        
        zoom: 1;
        }
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100;
    }
    .c4
    {
        margin-left: 4px;
        width: 150;
    }
     .c5
    {
       
        width:200;
    }
</style>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="YM"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" CausesValidation="false"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" CausesValidation="false"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" CausesValidation="false"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" CausesValidation="false"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" CausesValidation="false"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" CausesValidation="false"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">ISSUE TO MACHINE FORM</asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="17%">
                   Lot No
                    </td>
                    <td class="tdLeft" width="17%">
                       <cc2:ComboBox ID="cmbLotNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="LOT_DATA" EmptyText="Select Lot No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="650px" TabIndex="1" EnableVirtualScrolling="true" Width="98%"
                OnLoadingItems="cmbLotNo_LoadingItems"  OnSelectedIndexChanged="cmbLotNo_SelectedIndexChanged" >
                <HeaderTemplate>
                    <div class="header c3">
                        Lot No
                        </div>         
                        <div class="header c3">
                        Merge No
                        </div>    
                        <div class="header c3">
                        Lot Type
                        </div>    
                        <div class="header c3">
                        Purpose
                        </div>    
                        <div class="header c5">
                        Yarn
                        </div>    
                        <div class="header c5">
                        Finish
                        </div>             
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c3">
                        <asp:Literal ID="ltr2" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                    <div class="item c3">
                        <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("MERGE_NO")%>'></asp:Literal>
                    </div>
                    <div class="item c3">
                        <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("LOT_TYPE")%>'></asp:Literal>
                    </div>
                    <div class="item c3">
                        <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("PURPOSE")%>'></asp:Literal>
                    </div>
                    <div class="item c5">
                        <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("POY_DESC")%>'></asp:Literal>
                    </div>
                    <div class="item c5">
                        <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("FINISHED_DENIER_DESC")%>'></asp:Literal>
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
                    <td class="tdLeft" colspan="5" width="66%">
                       Merge No:<asp:Label ID="txtMergeNo" runat="server"  Width="125px" CssClass="SmallFont" Font-Bold="true"></asp:Label>&nbsp;
                        Finish Type:<asp:Label ID="txtFinishType" runat="server" Width="125px"    CssClass="SmallFont" Font-Bold="true"></asp:Label>&nbsp;
                         Lot Type:<asp:Label ID="txtLotType" runat="server"   Width="125px" CssClass="SmallFont" Font-Bold="true"></asp:Label>&nbsp; 
                         Purpose:<asp:Label ID="txtQuality" runat="server"    CssClass="SmallFont"   Width="125px" Font-Bold="true"></asp:Label>
                   <%-- <td class="tdRight" width="17%">
                        Side
                    </td>
                    <td class="tdLeft" width="15%">--%>
                        <asp:DropDownList ID="ddlSide" Width="1%" runat="server" CssClass="SmallFont"  Visible="false"
                            Font-Size="XX-Small">
                            <asp:ListItem Selected="true" Value="0">-Select-</asp:ListItem>
                             <asp:ListItem Value="Side-A">Side-A</asp:ListItem>
                             <asp:ListItem  Value="Side-B">Side-B</asp:ListItem>
                             <asp:ListItem  Value="Both">Both</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="ddlSideValidator" ControlToValidate="ddlSide" InitialValue="0" runat="server" ErrorMessage="Pls Select Machine Side."  ValidationGroup="YM"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                
                   <tr>
                    <td class="tdRight" width="17%">
                            Machine Code  
                    </td>
                    <td class="tdLeft" width="17%">
                    
                    <cc2:ComboBox ID="cmbMachineGroup" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MACHINE_GROUP" DataValueField="MACHINE_GROUP"  
                            Width="98%" MenuWidth="200px"
                                    Height="200px" CssClass="SmallFont" TabIndex="2" EmptyText="Select Machine Group"
                                    OnLoadingItems="cmbMachineGroup_LoadingItems" 
                            OnSelectedIndexChanged="cmbMachineGroup_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Machine Group
                                        </div>
                                      
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MACHINE_GROUP") %>' /></div>
                                     
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>


                    <cc2:ComboBox ID="ddlMachineCode" runat="server" DataTextField="MACHINE_CODE" DataValueField="MACHINE_DATA" Visible="false"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="350px" OnLoadingItems="ddlMachineCode_LoadingItems" EmptyText="Select Machine"
                            OnSelectedIndexChanged="ddlMachineCode_SelectedIndexChanged" Width="98%" AutoPostBack="True" TabIndex="2">
                            <HeaderTemplate>
                                <div class="header c3">
                                    Machine Code</div>
                                <div class="header c3">
                                    Machine Group</div>
                                <div class="header c3">
                                    Machine Make</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                    -
                                    <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("OLD_MACHINE_NAME") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container10" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container11" runat="server" Text='<%# Eval("MACHINE_MAKE") %>' />
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
                    <td class="tdRight" width="17%" colspan="2">
                       <asp:TextBox ID="txtMachineDesc" runat="server" Width="90%" ReadOnly="true" CssClass="TextBoxDisplay SmallFont"
                            Font-Size="XX-Small"></asp:TextBox>
                    
                         <asp:TextBox ID="txtProsDesc" runat="server" Width="1%" ReadOnly="true" CssClass="TextBoxDisplay SmallFont"
                            Font-Size="XX-Small" Visible="false"></asp:TextBox>
                        <asp:Label ID="lblProsCode" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td class="tdRight" width="17%">
                        Process Code
                    </td>
                    <td class="tdLeft" width="15%">
                        <cc2:ComboBox ID="ddlProsCode" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="250px" OnLoadingItems="ddlProsCode_LoadingItems" EmptyText="Select Process"
                            OnSelectedIndexChanged="ddlProsCode_SelectedIndexChanged" Width="98%" AutoPostBack="True">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Process Code</div>
                                <div class="header c5">
                                    Process Description</div>
                               <%-- <div class="header c2">
                                    Product Type</div>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal ID="Container10" runat="server" Text='<%# Eval("PROS_DESC") %>' />
                                </div>
                               <%-- <div class="item c2">
                                    <asp:Literal ID="Container11" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                </div>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Process Id No
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtProsIdNo" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Font-Size="XX-Small" Width="99%"></asp:TextBox>
                        <cc2:ComboBox ID="ddlProsIdNo" runat="server" DataTextField="PROS_ID_NO" DataValueField="PROS_DATA" EmptyText="Select Process No"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="350px" OnLoadingItems="ddlProsIdNo_LoadingItems"
                            OnSelectedIndexChanged="ddlProsIdNo_SelectedIndexChanged" Width="99%" AutoPostBack="True" TabIndex="3"
                            Font-Size="XX-Small">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Process Id No</div>
                                <div class="header c2">
                                    Process Code</div>
                                <div class="header c3">
                                    Machine Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("PROS_ID_NO") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container12" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container13" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="txtProsIdNo"
                            Display="None" ErrorMessage="Please Enter Process Id No in Numeric" MaximumValue="999999999"
                            MinimumValue="0" Type="Integer" ValidationGroup="YM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txtProsIdNo"
                            Display="None" ErrorMessage="Please Enter Pros Id No" SetFocusOnError="True"
                            ValidationGroup="YM"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="17%">
                        Entry Date
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtEntryDate" runat="server"  CssClass="TextBoxDisplay SmallFont"
                           Width="99%" ></asp:TextBox>
                           <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEntryDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="tdRight" width="17%">
                        Shift
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlShift" Width="99%" runat="server" CssClass="SmallFont" 
                            Font-Size="XX-Small">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td class="tdRight" width="17%">
                       PLT&nbsp;Color:
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtPaperTubeColor" runat="server"  CssClass="TextBoxNo SmallFont"
                            Font-Size="XX-Small" Width="99%" TabIndex="4"></asp:TextBox>
                   
                    </td>
                    <td class="tdRight" width="17%">
                       Air&nbsp;Pressure
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtAirPressure" runat="server"  CssClass="TextBoxNo SmallFont"
                           Width="99%" Font-Size="XX-Small" TabIndex="5"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        Roto&nbsp;Jet&nbsp;No:
                    </td>
                    <td class="tdLeft" width="15%">
                      <asp:TextBox ID="txtRotoJetNo" runat="server" CssClass="TextBoxNo SmallFont"
                           Width="99%" Font-Size="XX-Small" TabIndex="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                      Machine Start Time
                    </td>
                    <td class="tdLeft" width="17%">
                    <%--    <asp:TextBox ID="txtLoadingDate" runat="server"  OnTextChanged="txtLoadingDate_TextChanged"
                            CssClass="SmallFont" Font-Size="XX-Small" Width="99%" TabIndex="7"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtLoadingDate" ID="meeLoadingDate" runat="server"
                            Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="True" />
                        <cc1:MaskedEditValidator ID="mevLoadingDate" runat="server" ControlExtender="meeLoadingDate"
                            ControlToValidate="txtLoadingDate" IsValidEmpty="false" InvalidValueMessage="Invalid Loading Date"
                            Display="Dynamic" ValidationGroup="YM" />
                             <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy hh:mm:ss tt" TargetControlID="txtLoadingDate">
                        </cc1:CalendarExtender>--%>
                         <asp:TextBox ID="txtLoadingDate" runat="server"  CssClass="TextBoxNo SmallFont"  Width="40%" TabIndex="7"></asp:TextBox> <%--OnTextChanged="txtLoadingDate_TextChanged"--%>
                               <cc1:MaskedEditExtender ID="txtFromMask" runat="server" Mask="99/99/9999" MaskType="Date"       PromptCharacter="_" TargetControlID="txtLoadingDate">
                               </cc1:MaskedEditExtender>
                          <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLoadingDate">
                        </cc1:CalendarExtender>                        
                         <cc1:TimeSelector ID="startTime" runat="server" SelectedTimeFormat="Twelve"      TabIndex="8" Width="50%" CssClass="SmallFont"  Font-Size="XX-Small"   DisplaySeconds="false" AmPm="AM"    > </cc1:TimeSelector>



                    </td>
                    <td class="tdRight" width="17%">
                          Operator 
                    </td>
                    <td class="tdLeft" width="17%">
                      <asp:TextBox ID="txtOperator" runat="server" Font-Size="XX-Small" Width="99%" CssClass="SmallFont" TabIndex="8"></asp:TextBox>
                       
                    </td>
                     <td class="tdRight" width="17%">
                        Supervisor
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtSupervisor" runat="server" Font-Size="XX-Small" Width="99%" CssClass="SmallFont" TabIndex="9"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr id="timetr" runat="server" visible="false">
                    <td class="tdRight" width="17%">
                        Machine Stopage
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtMachineStopage" runat="server" AutoPostBack="True" OnTextChanged="txtMachineStopage_TextChanged"
                            CssClass="SmallFont"  Font-Size="XX-Small" Width="80px" TabIndex="10">0</asp:TextBox>Min.<asp:LinkButton ID="lbtnmacStopDetail" runat="server" OnClick="lbtnmacStopDetail_Click"
                            Text="Mac Stopage"  Font-Size="XX-Small"></asp:LinkButton>
                    </td>
                    <td class="tdRight" width="17%">
                     End Time
                    </td>
                    <td class="tdLeft" width="17%">
                       <asp:TextBox ID="txtUnLoadingDate" runat="server" AutoPostBack="True" OnTextChanged="txtUnLoadingDate_TextChanged"
                            CssClass="SmallFont" Font-Size="XX-Small" Width="99%" TabIndex="11"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtUnLoadingDate" ID="meeUnLoadingDate"
                            runat="server" Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="true" />
                       <%-- <cc1:MaskedEditValidator ID="mevUnLoadingDate" runat="server" ControlExtender="meeUnLoadingDate"
                            ControlToValidate="txtUnLoadingDate" IsValidEmpty="false" InvalidValueMessage="Invalid Un-Loading Date"
                            Display="Dynamic" ValidationGroup="YM" />--%>
                    </td>
                   <td class="tdRight" width="17%">
                        Total Time
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtProcessTime" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay"
                            Font-Size="XX-Small" Width="70%" TabIndex="12"></asp:TextBox>
                       Hours <%--Min.--%>
                    </td>
                </tr>
                <tr>    <td class="tdRight" width="17%">
                      Remarks
                    </td>
                    <td class="tdLeft" width="17%">
                          <asp:TextBox ID="txtRemarks" runat="server" Font-Size="XX-Small" Width="100%" CssClass="SmallFont" TabIndex="13"></asp:TextBox> 
                    </td>
                     <td class="tdRight" width="17%">
                    <%--  Net&nbsp;Wt.--%>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtDoffingNetWt" runat="server"    CssClass="SmallFont"  Font-Size="XX-Small" Width="99%" Visible="false"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%" colspan="2">
                       
 <asp:Label ID="lblFinishDenier" runat="server"   CssClass="SmallFont" Font-Bold="true"></asp:Label>
                 <%--   </td>
                    <td class="tdLeft"  width="17%">--%>
                       
                    </td>
                  <%--  <td class="tdRight" width="17%">
                     
                    </td>
                    <td class="tdLeft" width="17%">
                          </td>
                </tr>--%>
            </table>
        </td>
    </tr></tr>
    <tr>
        <td align="left" class="td" width="100%">
            <%--<fieldset>
             Lot Detail--%>
                <table width="100%">
                <tr>
                    <td class="tdRight" width="100%">
                        <table width="100%">
                            <tr bgcolor="#336699" class="SmallFont titleheading">
                                <td width="10%" class="tdLeft">
                                     Batch No
                                </td>
                                <td width="10%" class="tdLeft">
                                    Base Lot No
                                </td><td width="10%" class="tdLeft">
                                    Base Yarn
                                </td>
                                <td width="15%" class="tdLeft">
                                    Order Description
                                </td>
                                
                                <%--   <td width="15%" class="tdLeft">
                                    Dyed Lot No
                                </td>--%>
                                <td>
                                    <%--Pattern&nbsp;No.--%>
                                    Artical Code
                                </td>
                                <td width="5%" class="tdLeft">
                                   <%-- L.N.O.U.--%>Cops
                                </td>
                                <td width="5%" class="tdLeft" id="UOMH" runat="server" visible="false">
                                    <%--L.UOM.--%>UOM
                                </td>
                                <td width="5%" class="tdLeft" id="AWH" runat="server" >
                                   <%-- L.W.O.U.--%>Avg Wt(KG)
                                </td>
                                <td width="5%" class="tdLeft" id="WTH" runat="server" >
                                 <%--   L.&nbsp;Qty.--%> Weight(KG)
                                </td>
                                <td width="5%" class="tdLeft" id="ULNOUH" runat="server" visible="false">
                                    U.L.N.O.U.
                                </td>
                                <td width="5%" class="tdLeft" id="UIUH" runat="server" visible="false">
                                    U.L.UOM.
                                </td>
                                <td width="5%" class="tdLeft" id="ULWOUH" runat="server" visible="false">
                                    U.L.W.O.U.
                                </td>
                                <td width="5%" class="tdRight" id="ULQH" runat="server" visible="false">
                                    U.L.Qty.
                                </td>
                                <td width="10%" class="tdLeft" id="TLH" runat="server" visible="false">
                                    To&nbsp;Location
                                </td>
                                <td width="10%" class="tdLeft" id="TBNH" runat="server" visible="false">
                                    To&nbsp;Batch&nbsp;No
                                </td>
                                <%--      <td width="8%" class="tdRight">
                                    Loading Date Time
                                </td>
                                <td width="8%" class="tdLeft">
                                    Unloading Date Time
                                </td>--%>
                                <td width="11%" class="tdLeft">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="10%" class="tdLeft">
                                    <cc2:ComboBox ID="ddlLotNo" runat="server" DataTextField="LOT_NUMBER" DataValueField="LOT_DATA"
                                        EnableLoadOnDemand="true" Height="200px" MenuWidth="450px" OnLoadingItems="ddlLotNo_LoadingItems"
                                        OnSelectedIndexChanged="ddlLotNo_SelectedIndexChanged" Width="99%" AutoPostBack="True" TabIndex="14">
                                        <HeaderTemplate>
                                            <div class="header c4">
                                                Batch No</div>
                                            <div class="header c4">
                                                Base Lot</div>
                                            <div class="header c2">
                                                Department</div>
                                            <div class="header c2">
                                                P.Code</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c4">
                                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("LOT_NUMBER") %>' />
                                            </div>
                                            <div class="item c4">
                                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("DEPT_CODE") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PROS_CODE") %>' />
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
                                <td width="10%" class="tdLeft">
                                    <asp:TextBox ID="txtLotQty" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtMaxLoadQty" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true"></asp:TextBox>
                                </td>  
                                <td width="10%" class="tdLeft">
                                    <asp:TextBox ID="txtGreyArticleCode" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtGreyArticleDesc" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true"></asp:TextBox>
                                    <asp:TextBox ID="txtDyedLot" runat="server" CssClass=" TextBox SmallFont" Width="99%"
                                        Wrap="true" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lblPROS_CODE" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblBIN_LOCT" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblBATCH_NO" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtOrderDescription" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true"></asp:TextBox>
                                </td>
                              
                                <%--   <td width="14%" class="tdLeft">
                                
                                </td>--%>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtPattern" runat="server" CssClass="TextBoxDisplay TextBox SmallFont" Width="99%"
                                        Wrap="true" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont" Width="90%"
                                        AutoPostBack="True" OnTextChanged="txtLoadNoOfUnit_TextChanged" Wrap="true" TabIndex="15"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft" id="UOMT" runat="server" visible="false">
                                    <asp:DropDownList ID="ddlLoadUOM" runat="server" Width="99%" CssClass="SmallFont">
                                    </asp:DropDownList>
                                </td>
                                <td width="5%" class="tdLeft" id="ATT" runat="server" >
                                    <asp:TextBox ID="txtLoadWeightOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtLoadWeightOfUnit_TextChanged" ReadOnly="true"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft" id="WTT" runat="server" >
                                    <asp:TextBox ID="txtLoadQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" ReadOnly="true" Wrap="true"></asp:TextBox>
                                        <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtLoadQty" ErrorMessage="*Enter Quantity"
                                MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                </td>
                                <td width="5%" class="tdLeft" id="ULNOUR" runat="server" visible="false">
                                    <asp:TextBox ID="txtUnloadNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtUnloadNoOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft" id="UOIR" runat="server" visible="false">
                                    <asp:DropDownList ID="ddlUnloadUOM" runat="server" Width="99%" CssClass="SmallFont">
                                    </asp:DropDownList>
                                </td>
                                <td width="5%" class="tdLeft" id="UWOUR" runat="server" visible="false">
                                    <asp:TextBox ID="txtUnloadWeightOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtUnloadWeightOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdRight" id="ULQR" runat="server" visible="false">
                                    <asp:TextBox ID="txtUnloadQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" ReadOnly="true" Wrap="true" ></asp:TextBox>
                                        <asp:RangeValidator ID="r2" runat="server" ControlToValidate="txtUnloadQty" ErrorMessage="*Enter Quantity"
                                MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                </td>
                                <td width="5%" class="tdLeft" id="UTLR" runat="server" visible="false">
                                    <asp:TextBox ID="txtToLocation" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                        Wrap="true" Visible="false"></asp:TextBox>
                                         <asp:DropDownList ID="ddlPackaging" Width="99%" runat="server" CssClass="SmallFont" 
                            Font-Size="XX-Small"> 
                            <asp:ListItem Selected="true" Value="D00006">Packaging</asp:ListItem>
                           
                             </asp:DropDownList>
                                </td>
                                <td width="5%" class="tdLeft" id="TBNR" runat="server" visible="false">
                                    <asp:TextBox ID="txtToBatchNo" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                        Wrap="true"></asp:TextBox>
                                       
                             
                      
                                </td>
                                <%-- <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadingDateTime" runat="server" CssClass="TextBox SmallFont"
                                        Width="99%"></asp:TextBox>
                                </td>
                                <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtUnLoadingDateTime" runat="server" CssClass="TextBox SmallFont"
                                        Width="99%"></asp:TextBox>
                                </td>--%>
                                <td width="14%" class="tdLeft">
                                    <asp:Button ID="btnsaveLotDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveLotDetail_Click"
                                        Text="Save" ValidationGroup="T1" Width="50px" TabIndex="16"/>
                                    <asp:Button ID="btnCancelLotDetail" runat="server" CssClass="SmallFont" OnClick="btnCancelLotDetail_Click"
                                        Text="Cancel" Width="50px" TabIndex="17"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <asp:Panel ID="pnlLotDetail" runat="server" Width="100%" Height="200px" ScrollBars="Auto">
                            <asp:GridView ID="grdLotDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                OnRowCommand="grdLotDetail_RowCommand" Width="98%">
                                <Columns>
                                    <asp:TemplateField HeaderText="L/B.Number">
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lblLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NUMBER") %>'></asp:Label>
                                           
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PA No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <%-- <asp:TemplateField HeaderText="Dyed Lot No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDyedLotNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DYED_LOT_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Grey&nbsp;Yarn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblARTICLE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ARTICLE_DESC") %>' ToolTip='<%# Bind("ARTICLE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order&nbsp;Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDetail" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_DESC") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Artical">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatternNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PATTERN_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cops">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadNoOfUnit2" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_NO_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_UOM_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avg Wt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_WEIGHT_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.L.N.O.U." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnLoadNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_NO_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.L.UOM" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnLoadUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_UOM_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.L.W.O.U." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnLoadWeightOfUnit" runat="server" CssClass="LabelNo SmallFont"
                                                Text='<%# Bind("UNLOAD_WEIGHT_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.L.Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To&nbsp;Location" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToLocation" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TO_LOCATION") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To&nbsp;Batch&nbsp;No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToBatch" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TO_BATCH_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="L.D.T."  Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_DATE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.L.D.T."  Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_DATE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_TRN") %>'
                                                CommandName="EditLotDetail" Text="Edit" CausesValidation="false"></asp:LinkButton>/
                                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUE_TRN") %>'
                                                CommandName="DelLotDetail" Text="Delete" CausesValidation="false"></asp:LinkButton></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                <RowStyle CssClass="SmallFont" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
           <%-- </fieldset>--%>
         
        </td>
    </tr>
  
</table>
