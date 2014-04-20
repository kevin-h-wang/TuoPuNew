<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonGroup.aspx.cs" Inherits="Whf.TuoPu.Web.BasicData.PersonGroup" %>

<%@ Register Src="..\Controls\Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GroupManager</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script src="../Script/Function.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function AddGroup() {
            var returnValue = showdialog("EditGroup.aspx", 500, 330);
            if (returnValue = 'ok') {
                document.getElementById("<%=btnRefresh.ClientID %>").click();
            }
        }

        function EditGroup(groupID) {
            var returnValue = showdialog("EditGroup.aspx?GroupID=" + groupID, 500, 330);
            if (returnValue = 'ok') {
                document.getElementById("<%=btnRefresh.ClientID %>").click();
            }
        }

        function SelectPerson(groupID) {
            showdialog("GroupPerson.aspx?GroupID=" + groupID, 800, 600);
        }

        function SelectPermission(groupID) {
            showdialog("GroupFunction.aspx?GroupID=" + groupID, 600, 400);
        }
    </script>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="TableMain">
        <tr>
            <td class="TDTitle">
                <asp:Label ID="lblTitle" CssClass="LabelMain" runat="server" Text="群组管理"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="TableMain" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="TDLabel">
                            <asp:Label ID="lblGroupCode" CssClass="LabelTitle" Text="群组代码" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:TextBox ID="txtGroupCode" CssClass="TextBoxMain160" runat="server"></asp:TextBox>
                        </td>
                        <td width="4"></td>
                        <td class="TDLabel">
                            <asp:Label ID="lblGroupName" CssClass="LabelTitle" Text="群组名称" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="TextBoxMain160"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="ButtonMain" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TDMain">
                <div class="DivGridView">
                    <kevin:KevinGrid CssClass="gridview_list" ShowCheckBox="true" ID="gvPerson" AutoGenerateColumns="false"
                        runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px"
                        CellPadding="3" CellSpacing="1" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="groupcode" HeaderText="权限组代码" />
                            <asp:BoundField DataField="groupname" HeaderText="权限组名称" />
                            <asp:BoundField DataField="groupstatus" HeaderText="权限组状态" />
                            <asp:BoundField DataField="memo" HeaderText="权限组备注" />
                            <asp:TemplateField HeaderText="编辑">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" CssClass="GVButtonEdit" runat="server">
                                    </asp:Button>
                                    <asp:HiddenField ID="hdfOID" runat="server" Value='<%# Bind("oid") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="人员">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" CssClass="GVButtonEdit" runat="server">
                                    </asp:Button>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="权限">
                                <ItemTemplate>
                                    <asp:Button ID="btnPermission" CssClass="GVButtonEdit" runat="server">
                                    </asp:Button>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridview_header" />
                        <RowStyle CssClass="gridview_row" />
                        <AlternatingRowStyle CssClass="altrow" />
                        <SelectedRowStyle CssClass="gridviewRowSelected" />
                    </kevin:KevinGrid>
                </div>
                <div class="DivSelectAll">
                    <asp:CheckBox ID="checkAll" runat="server" Text="全选" onclick="SelectAllCheckbox(this,'gvPerson')" />
                </div>
                <div class="DivNavigatior">
                    <uc1:Navigator ID="Navigator" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="TDOperate">
                <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="AddGroup(); return false;" CssClass="ButtonMain" />
                <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="ButtonMain" />
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:Button ID="btnRefresh" runat="server" />
    </div>
    </form>
</body>
</html>
