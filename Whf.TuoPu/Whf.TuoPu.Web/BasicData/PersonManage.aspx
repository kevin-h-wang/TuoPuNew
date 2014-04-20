<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonManage.aspx.cs" Inherits="Whf.TuoPu.Web.BasicData.PersonManage" %>

<%@ Register Src="..\Controls\Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PersonManage</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script src="../Script/Function.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function AddPerson() {
            var returnValue = showdialog("Editperson.aspx", 500, 430);
            if (returnValue = 'ok') {
                document.getElementById("<%=btnRefresh.ClientID %>").click();
            }
        }

        function EditPerson(personid) {
            var returnValue = showdialog("Editperson.aspx?PersonID=" + personid, 500, 430);
            if (returnValue = 'ok') {
                document.getElementById("<%=btnRefresh.ClientID %>").click();
            }
        }
    </script>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="TableMain">
        <tr>
            <td class="TDTitle">
                <asp:Label ID="lblTitle" CssClass="LabelMain" runat="server" Text="人员管理"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="TableMain" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="TDLabel">
                            <asp:Label ID="lblEmpNO" CssClass="LabelTitle" Text="员工账号" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:TextBox ID="txtEmpNO" CssClass="TextBoxMain160" runat="server"></asp:TextBox>
                        </td>
                        <td width="4"></td>
                        <td class="TDLabel">
                            <asp:Label ID="lblEmpName" CssClass="LabelTitle" Text="员工姓名" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="TextBoxMain160"></asp:TextBox>
                        </td>
                        <td class="TDLabel">
                            <asp:Label ID="lblEmpType" CssClass="LabelTitle" Text="员工类别" runat="server"></asp:Label>
                        </td>
                        <td class="TDValue">
                            <asp:DropDownList ID="drpPersonType" CssClass="DropDownListMain160" runat="server">
                            </asp:DropDownList>
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
                            <asp:BoundField DataField="personaccount" HeaderText="员工账号" />
                            <asp:BoundField DataField="personname" HeaderText="员工姓名" />
                            <asp:TemplateField HeaderText="员工性别">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlSex" runat="server" Text='<%#Bind("personsex") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="员工类别">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlType" runat="server" Text='<%#Bind("persontype") %>'></asp:Literal>
                                    <asp:HiddenField ID="hdfOID" runat="server" Value='<%# Bind("oid") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="personofficephone" HeaderText="办公电话" />
                            <asp:BoundField DataField="personmobilephone" HeaderText="移动电话" />
                            <asp:BoundField DataField="personemail" HeaderText="电子邮件" />
                            <asp:TemplateField HeaderText="员工状态">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlStatus" runat="server" Text='<%#Bind("personstatus") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="personmemo" HeaderText="备注信息" />
                            <asp:TemplateField HeaderText="编辑">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" CssClass="GVButtonEdit" runat="server"></asp:Button>
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
                <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="AddPerson(); return false;"
                    CssClass="ButtonMain" />
                <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="ButtonMain" />
                <asp:Button ID="btnImport" runat="server" Text="导入Excel" CssClass="ButtonMain" />
                <asp:Button ID="btnExport" runat="server" Text="导出Excel" CssClass="ButtonMain" />
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:Button ID="btnRefresh" runat="server" />
    </div>
    </form>
</body>
</html>
