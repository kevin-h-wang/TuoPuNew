<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPerson.aspx.cs" Inherits="Whf.TuoPu.Web.Commons.SelectPerson" %>

<%@ Register Src="..\Controls\Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择人员</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
    <script src="../Script/Function.js" type="text/javascript"></script>
    <base target="_self" />
    <script language="javascript" type="text/javascript">
        function CloseWithValue() {
            var flag = document.getElementById("<%=hdfFlag.ClientID %>").value;
            if (flag == 1) {
                winclose('ok');
            }
            else {
                CloseWindow();
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
                    <asp:GridView CssClass="gridview_list" ID="gvPerson" AutoGenerateColumns="false"
                        runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px"
                        CellPadding="3" CellSpacing="1" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <HeaderStyle HorizontalAlign="Center" Height="25px" Width="45px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckb" runat="server" />
                                    <asp:HiddenField ID="hdfOID" runat="server" Value='<%# Bind("oid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="personmobilephone" HeaderText="移动电话" />
                            <asp:TemplateField HeaderText="员工状态">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlStatus" runat="server" Text='<%#Bind("personstatus") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="personmemo" HeaderText="备注信息" />
                        </Columns>
                        <HeaderStyle CssClass="gridview_header" />
                        <RowStyle CssClass="gridview_row" />
                        <AlternatingRowStyle CssClass="altrow" />
                        <SelectedRowStyle CssClass="gridviewRowSelected" />
                    </asp:GridView>
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
                <asp:Button ID="btnConfirm" runat="server" Text="确定" CssClass="ButtonMain" />
                <asp:Button ID="btnReturn" OnClientClick="CloseWithValue();" runat="server" Text="关闭" CssClass="ButtonMain" />
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:HiddenField ID="hdfFlag" Value="0" runat="server" />
    </div>
    </form>
</body>
</html>
