<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="ImageSave.aspx.cs" Inherits="Admin_ImageSave" Title="Image Save" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

    <script language="javascript" type="text/javascript">
			function func1()
			{
				document.getElementById("imgPhoto").style.display="";
				document.getElementById("imgPhoto").src=document.getElementById("ctl00_cphBody_tPhoto").value;
			}
    </script>

    <br />
    <br />
    <table cellpadding="0" cellspacing="0" border="1" width="90%" align="center" class="tContentArial">
        <tr>
            <td align="center" class="TableHeader" colspan="3">
                <b>Image Save</b>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" colspan="3">
                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right" valign="top">
                Images</td>
            <td width="2%" align="center" valign="top">
                <b>:</b></td>
            <td width="73%" align="left" valign="top">
                &nbsp;<input class="gCtrHindi" id="tPhoto" tabindex="5" type="file" onchange="func1();"
                    name="tPhoto" runat="server" />
                &nbsp;<img class="gCtrHindi" id="imgPhoto" style="display: none" height="30" width="30"
                    border="0" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" valign="top">
                <asp:Button ID="btnSave" runat="server" Width="75px" Text="Save" CssClass="AButton"
                    ValidationGroup="Manipulation" OnClick="btnSave_Click" TabIndex="7" />&nbsp;
                
                <%--<asp:Button ID="btnUpdate" runat="server" Width="75px" Text="Update" CssClass="AButton"
                    TabIndex="8" OnClick="btnUpdate_Click" ValidationGroup="Manipulation" />
                --%>
                &nbsp;&nbsp; 
                <asp:Button ID="btnClear" runat="server" Width="75px" Text="Refresh" CssClass="AButton"
                    TabIndex="9" OnClick="btnClear_Click" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" valign="top">
                <br />
                <br />
                <table cellpadding="0" cellspacing="0" border="1" width="100%" align="center" class="tContentArial">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:GridView ID="gvImage" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true"
                                AutoGenerateColumns="false" HeaderStyle-Font-Bold="false" Width="100%" OnRowCommand="gvImage_RowCommand"
                                OnPageIndexChanging="gvImage_PageIndexChanging" BorderWidth="1px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="25px" ItemStyle-VerticalAlign="top" HeaderStyle-BorderColor="#005BAB">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="in_ImageId" HeaderText="Image Id" ItemStyle-Width="25px" />
                                   
                                    <asp:TemplateField HeaderText="Image" ItemStyle-Width="100px" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="center"
                                        HeaderStyle-BorderColor="#005BAB">
                                        <ItemTemplate>
                                            <img src="./ShowImage.aspx?ImageId=<%# Eval("in_ImageId") %> &ilen=<%# Eval("in_PostedLength") %>"
                                                width="80px" height="80px" alt="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="40px" ItemStyle-VerticalAlign="top" HeaderStyle-BorderColor="#005BAB">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval("in_ImageId") %>' CommandName="ImageEdit"
                                                ImageUrl="~/CommonImages/edit1.jpg" runat="server" CssClass="ContolStyle"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="40px" ItemStyle-VerticalAlign="top" HeaderStyle-BorderColor="#005BAB">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgDelete" CommandArgument='<%# Eval("in_ImageId") %>' CommandName="ImageDelete"
                                                ImageUrl="~/CommonImages/del6.png" runat="server" CssClass="ContolStyle" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this image?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
