<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunctionManage.aspx.cs"
    Inherits="Whf.TuoPu.Web.BasicData.FunctionManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>function</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script src="../Script/Function.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="TableMain">
        <tr>
            <td class="TDTitle">
                <asp:Label ID="lblTitle" CssClass="LabelMain" runat="server" Text="功能管理"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="TableMain" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="TDLabel">
                            <asp:Label ID="lblQueryFunCode" CssClass="LabelTitle" Text="菜单代码" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:TextBox ID="txtQueryFunCode" CssClass="TextBoxMain160" runat="server">
                            </asp:TextBox>
                        </td>
                        <td class="TDLabel">
                            <asp:Label ID="lblQueryFunName" CssClass="LabelTitle" Text="菜单名称" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:TextBox ID="txtQueryFunName" runat="server" CssClass="TextBoxMain160">
                            </asp:TextBox>
                        </td>
                        <td width="100"></td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="ButtonMain" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TDMain" align="center">
                <table width="90%" height="90%">
                    <tr>
                        <td width="20%" valign="top" style="border: #ababab 1px solid;">
                            <asp:TreeView ID="tvMenu" runat="server">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="true" />
                                <SelectedNodeStyle BackColor="blue" ForeColor="white" Font-Underline="False" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                            </asp:TreeView>
                        </td>
                        <td width="10px"></td>
                        <td width="80%">
                            <table class="TableMain">
                                <tr>
                                    <td class="TDLabel">
                                        <asp:Label ID="lblFuncCode" Text="菜单代码" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue">
                                        <asp:TextBox ID="txtFuncCode" CssClass="TextBoxMain300" runat="server">
                                        </asp:TextBox></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="4"></td>
                                </tr>
                                <tr>
                                    <td class="TDLabel">
                                        <asp:Label ID="lblFuncName" Text="菜单名称" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue">
                                        <asp:TextBox ID="txtFuncName" CssClass="TextBoxMain300" runat="server">
                                        </asp:TextBox></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="4"></td>
                                </tr>
                                <tr>
                                    <td class="TDLabel">
                                        <asp:Label ID="lblFuncLevel" Text="菜单层级" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue">
                                        <asp:TextBox ID="txtFuncLevel" Enabled="false" CssClass="TextBoxMain300" runat="server">
                                        </asp:TextBox></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="4"></td>
                                </tr>
                                <tr>
                                    <td class="TDLabel">
                                        <asp:Label ID="lblFuncStatus" Text="菜单状态" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue">
                                        <asp:DropDownList ID="drpFuncStatus" CssClass="DropDownListMain160" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="4"></td>
                                </tr>
                                <tr>
                                    <td class="TDLabel" align="right">
                                        <asp:Label ID="lblFuncOrder" Text="菜单顺序" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue" align="left">
                                        <asp:TextBox ID="txtFuncOrder" CssClass="TextBoxMain300" runat="server">
                                        </asp:TextBox></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="4"></td>
                                </tr>
                                <tr>
                                    <td class="TDLabel">
                                        <asp:Label ID="lblFuncMemo" Text="菜单说明" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue">
                                        <asp:TextBox ID="txtFuncMemo" CssClass="TextBoxMain300" runat="server">
                                        </asp:TextBox></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td class="TDLabel">
                                        <asp:Label ID="lblFuncUrl" Text="菜单地址" CssClass="LabelTitle" runat="server"></asp:Label>
                                    </td>
                                    <td class="TDSperate"></td>
                                    <td class="TDValue">
                                        <asp:TextBox ID="txtFuncUrl" CssClass="TextBoxMain300" runat="server">
                                        </asp:TextBox></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="4"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="30%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TDOperate">
                <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonMain" />
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonMain" />
                <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="ButtonMain" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdfOID" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfParentOID" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
