<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Whf.TuoPu.Web._Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>托普机电综合管理系统</title>
    <meta http-equiv='Content-Type' content='text/html; charset=gb2312' />
    <link rel="stylesheet" type="text/css" href='CSS/adminCss.css' />
    <script type="text/javascript">
        function RefreshImage() {
            var el = document.getElementById("Image1");
            el.src = el.src + '?';
        }
    </script>
</head>
<body style="background-image: url(images/login_bg_1.jpg); background-repeat: repeat-x;">
    <form id="form1" runat="server">
    <table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0"
        id="__01">
        <tr>
            <td align="center" valign="top" bgcolor="#0C77D9">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <table width="1003" height="451" border="0" cellpadding="0" cellspacing="0">
                                <tr height="143">
                                    <td background="images/Login_topbar.jpg" height="143">
                                    </td>
                                </tr>
                                <tr height="308">
                                    <td background="images/Login_middlebar.jpg">
                                        <table width="1003" height="278" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td height="127" colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td width="430px" height="34">
                                                    &nbsp;
                                                </td>
                                                <td align="right" style="font-size: 12px; font-family: verdana; width: 80;">
                                                    用户名：
                                                </td>
                                                <td width="460px" height="34" style="font-size: 12">
                                                    <asp:TextBox ID="txtUserName" Text="SysAdmin" Style="width: 140px;" size="19" MaxLength="16" runat="server"
                                                        CssClass="bdlogin"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td height="26">
                                                    &nbsp;
                                                </td>
                                                <td align="right" style="font-size: 12px; font-family: verdana; width: 80;">
                                                    密码：
                                                </td>
                                                <td height="26" style="font-size: 12">
                                                    <asp:TextBox ID="txtUserPwd" Text="111111" TextMode="Password" runat="server" size="19" MaxLength="16"
                                                        CssClass="bdlogin" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td height="25">
                                                    &nbsp;
                                                </td>
                                                <td style="font-size: 12px; font-family: verdana; width: 80;" align="right">
                                                    验证码：
                                                </td>
                                                <td height="25" style="font-size: 12">
                                                    <asp:TextBox ID="txtValidCode" CssClass="bdlogin" Width="55px" runat="server" MaxLength="16"></asp:TextBox>&nbsp;
                                                    <asp:Image ID="Image1" onclick="RefreshImage()" runat="server" ImageUrl="Portal/ValidationCode.aspx"
                                                        ImageAlign="AbsBottom"></asp:Image>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td height="44">
                                                    &nbsp;
                                                </td>
                                                <td colspan="2" style="padding-left: 53px;">
                                                    <asp:ImageButton ID="ImageLogin" runat="server" EnableTheming="True" ImageUrl="~/images/login.gif"
                                                        OnClick="ImageLogin_Click" />
                                                    &nbsp;
                                                    <asp:ImageButton ID="ImageButton2" runat="server" EnableTheming="True" ImageUrl="~/images/cancel.gif" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="44">
                                                    &nbsp;
                                                </td>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="lblErrorInfo" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20" background="images/Login_03_02.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="26" style="background-image: url(IMAGES/LoginCopyright.jpg); text-align: center;
                            color: #fff; font-size: 12;">
                            &nbsp;&nbsp; CopyRight&copy;kevin.h.wang Ver3.0 All Rights Reserved &nbsp;&nbsp;服务电话：15190080304
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
