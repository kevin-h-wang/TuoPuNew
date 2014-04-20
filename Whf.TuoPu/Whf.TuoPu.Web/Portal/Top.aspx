<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="Whf.TuoPu.Web.Portal.Top" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>top</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" href="../css/adminCss.css" type="text/css">
    <script src="../Script/MdiWin.js"></script>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" id="HrmTop">
        <tr>
            <td width="880" valign="top" background="../images/HRMtopbg.jpg">
                <img src="../images/HRMtitle.jpg" width="988" height="88">
            </td>
            <td width="384" background="../images/HRMtopbg.jpg" height="88" style="background-repeat: repeat-x;
                background-position: left bottom">
                &nbsp;
            </td>
        </tr>
    </table>
    <table border="0" width="100%" cellspacing="0" cellpadding="0" height="23" background="../images/HRMtopmenubg.jpg">
        <tr>
            <td width="580" style="font-size:12px;">
                当前用户：<asp:Label ID="lblCurrentUser" runat="server"></asp:Label>
            </td>
            <td width="10">
            </td>
            <td width="208" align="right" style="font-size:12px;">
                当前时间：<asp:Label ID="lblDateTime" runat="server"></asp:Label>
            </td>
            <td>
            </td>
            <td align="right" valign="top">
                <a href="../Default.aspx?" target="_parent">
                    <img src="../images/Exit.jpg" width="44" height="21" border="0"
                        alt="" /></a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
