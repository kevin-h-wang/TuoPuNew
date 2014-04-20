<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditGroup.aspx.cs" Inherits="Whf.TuoPu.Web.BasicData.EditGroup" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>群组编辑</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
    <script language="javascript" type="text/javascript">
        function CloseWindow() {
            var flag = document.getElementById("<%=hdfFlag.ClientID %>").value;
            if (flag == '1') {
                window.returnValue = 'ok';
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="TableMain">
        <tr>
            <td colspan="2" class="TDTitle">
                <asp:Label ID="lblTitleNew" Text="新增群组" CssClass="LabelMain" runat="server"></asp:Label>
                <asp:Label ID="lblTitleEdit" Text="编辑群组" CssClass="LabelMain" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="TDLabel">
                <asp:Label ID="lblGroupCode" Text="群组编码" CssClass="LabelTitle" runat="server"></asp:Label>
            </td>
            <td class="TDSperate"></td>
            <td class="TDValue">
                <asp:TextBox ID="txtGroupCode" CssClass="TextBoxMain160" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="4"></td>
        </tr>
        <tr>
            <td class="TDLabel">
                <asp:Label ID="lblGroupName" Text="群组名称" CssClass="LabelTitle" runat="server"></asp:Label>
            </td>
            <td class="TDSperate"></td>
            <td class="TDValue">
                <asp:TextBox ID="txtGroupName" CssClass="TextBoxMain160" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="4"></td>
        </tr>
        <tr>
            <td class="TDLabel">
                <asp:Label ID="lblGroupStatus" Text="群组状态" CssClass="LabelTitle" runat="server"></asp:Label>
            </td>
            <td class="TDSperate"></td>
            <td class="TDValue">
                <asp:DropDownList ID="drpGroupStatus" CssClass="DropDownListMain160" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="4"></td>
        </tr>
        <tr>
            <td class="TDLabel">
                <asp:Label ID="lblGroupMemo" Text="备注信息" CssClass="LabelTitle" runat="server"></asp:Label>
            </td>
            <td class="TDSperate"></td>
            <td class="TDValue">
                <asp:TextBox ID="txtGroupMemo" CssClass="TextBoxMain160" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="4">
                <asp:HiddenField ID="hdfGroupID" runat="server" />
                <asp:HiddenField ID="hdfFlag" Value="0" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" width="100%">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonMain" />
                <asp:Button ID="btnReturn" runat="server" OnClientClick="CloseWindow();" Text="返回"
                    CssClass="ButtonMain" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
