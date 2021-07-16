<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserAccessRightNew.ascx.cs"
    Inherits="Module_Admin_Controls_UserAccessRightNew" %>
<%@ Register Src="../../../CommonControls/LOV/UserLOV.ascx" TagName="UserLOV" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
    function OnTreeClick(evt) {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
    }

    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }

    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) {
            var checkUncheckSwitch;

            if (check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else
                    return; //do not need to check parent if any(one or more) child not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }
        }
    }

    function AreAllSiblingsChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (!prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }

</script>
<table>
    <tr>
        <td class="td">
            <table align="left">
                <tr>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server" ValidationGroup="FA"
                            ToolTip="Update" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click"
                            OnClientClick="if (!confirm('Are you want to save ?')) { return false; }"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClientClick="if (!confirm('Are you want to print?')) { return false; }" OnClick="imgbtnPrint_Click1">
                        </asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClientClick="if (!confirm('Are you want to clear?')) { return false; }" OnClick="imgbtnClear_Click1">
                        </asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you want to Exit From This Form ?')) { return false; }">
                        </asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click1"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader ">
            <b class="titleheading">User Access Rights</b>
        </td>
    </tr>
    <tr>
        <td class="td">
            <asp:Label ID="lblMessage" runat="server" Style="font-size: x-small; color: #FF0000"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="td">
            <table class="tContentArial">
                <tr>
                    <td align="right" valign="top">
                        User Code&nbsp; :
                    </td>
                    <td align="left" valign="top">
                        <uc1:UserLOV ID="ddlUser" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlNav" runat="server" Height="350px" HorizontalAlign="Left" ScrollBars="Auto"
                                    Width="500px">
                                    <asp:Label ID="Label1" runat="server" Text="Select Navigation" Font-Size="X-Small"
                                        Font-Italic="true"></asp:Label>
                                    <br />
                                    <asp:TreeView ID="trvNav" runat="server" ShowCheckBoxes="All"  
                                        AutoGenerateDataBindings="False" 
                                        ontreenodecheckchanged="trvNav_TreeNodeCheckChanged">
                                    </asp:TreeView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </td> </tr>
</table>
