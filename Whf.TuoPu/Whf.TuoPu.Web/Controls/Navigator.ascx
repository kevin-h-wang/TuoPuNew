<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigator.ascx.cs" Inherits="Whf.TuoPu.Web.Controls.Navigator" %>
<div id="divPaging" runat="server">
    <asp:Label ID="lblCountPerPage" CssClass="LabelPaging" runat="server" Text="每页10条"></asp:Label>
    <span>/</span>
    <asp:Label ID="lblTotalCount" CssClass="LabelPaging" runat="server" Text="共10条"></asp:Label>
    &nbsp;
    <asp:Label ID="lblCurrentPage" CssClass="LabelPaging" runat="server" Text="第1页"></asp:Label>
    <span>/</span>
    <asp:Label ID="lblTotalPage" CssClass="LabelPaging" runat="server" Text="共1页"></asp:Label>
    &nbsp;
    <asp:TextBox ID="txtPage" Visible="false" runat="server" Width="30"></asp:TextBox>
    <asp:Button ID="btnGO" Visible="false" Text="GO" runat="server" />
    <asp:LinkButton ID="lbtnFirstPage" CssClass="LinkButtonPaging" runat="server" Text="首页"></asp:LinkButton>
    <asp:LinkButton ID="lbtnPrePage" CssClass="LinkButtonPaging" runat="server" Text="上一页"></asp:LinkButton>
    <asp:LinkButton ID="lbtnNextPage" CssClass="LinkButtonPaging" runat="server" Text="下一页"></asp:LinkButton>
    <asp:LinkButton ID="lbtnLastPage" CssClass="LinkButtonPaging" runat="server" Text="尾页"></asp:LinkButton>
    <asp:Label ID="lblRedirect" CssClass="LabelPaging" runat="server" Text="转到"></asp:Label>
    <asp:DropDownList ID="drpPageIndex" Width="40" AutoPostBack="true" runat="server">
    </asp:DropDownList>
    &nbsp;&nbsp;
</div>
