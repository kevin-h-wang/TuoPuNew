<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="Whf.TuoPu.Web.Portal.Left" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" href="../css/ssy_left.css" type="text/css" />
</head>
<body style="margin: 0 auto; padding: 0; height: 100%;">
    <form id="form1" runat="server">
    <table style="height: 100%; width: 100%;" cellspacing="0" cellpadding="0">
        <tr>
            <td width="100%" height="19" valign="top">
                <image src="../images/HRMleftmenu.jpg" width="161" height="33" />
            </td>
        </tr>
        <tr height="100%">
            <td width="100%" height="100%" align="left" valign="top" background="../images/HRMleftbg.jpg">
                <asp:TreeView ID="tvMenu" runat="server" HoverStyle="color:blue;background:#00ffCC;"
                    DefaultStyle="background:red;color:yellow;" SelectedStyle="color:red;background:#00ff00;filter:none;">
                </asp:TreeView>
            </td>
        </tr>
        <tr>
            <td width="100%" valign="top">
                <image border="0" src="../images/HRMleftbottom.jpg" width="161" height="12" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
