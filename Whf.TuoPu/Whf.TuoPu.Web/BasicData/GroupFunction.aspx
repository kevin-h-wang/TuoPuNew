<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupFunction.aspx.cs"
    Inherits="Whf.TuoPu.Web.BasicData.GroupFunction" %>

<%@ Register Src="..\Controls\Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>function</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script src="../Script/Function.js" type="text/javascript"></script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        function OnTreeNodeChecked() {
            var ele = event.srcElement;
            if (ele.type == 'checkbox') {
                var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
                var div = document.getElementById(childrenDivID);
                if (div != null) {
                    var checkBoxs = div.getElementsByTagName('INPUT');
                    for (var i = 0; i < checkBoxs.length; i++) {
                        if (checkBoxs[i].type == 'checkbox')
                            checkBoxs[i].checked = ele.checked;
                    }
                }
            }
        }
    </script>
    <table cellpadding="0" cellspacing="0" class="TableMain">
        <tr>
            <td class="TDTitle">
                <asp:Label ID="lblTitle" CssClass="LabelMain" runat="server" Text="功能管理"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="TDMain" align="center">
                <table width="90%" height="90%">
                    <tr>
                        <td width="20%" valign="top" style="border: #ababab 1px solid;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TreeView ID="tvMenu" runat="server" onclick="OnTreeNodeChecked()">
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                            NodeSpacing="0px" VerticalPadding="2px" />
                                        <ParentNodeStyle Font-Bold="true" />
                                        <SelectedNodeStyle BackColor="blue" ForeColor="white" Font-Underline="False" HorizontalPadding="0px"
                                            VerticalPadding="0px" />
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TDOperate">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonMain" />
                <asp:Button ID="btnReturn" runat="server" OnClientClick="CloseWindow();" Text="返回" CssClass="ButtonMain" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    </form>
</body>
</html>
