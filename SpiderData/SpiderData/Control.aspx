<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="SpiderData.Control" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link rel="stylesheet" href="Button.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" Text="江苏省医疗器械查询" class="button blue" Width="300px" Height="60px" OnClick="Button1_Click" style="z-index: 1; left: 528px; top: 104px; position: absolute"/>
        <asp:Button ID="Button2" runat="server" Text="湖南省医疗器械查询" class="button green" Width="300px" Height="60px" OnClick="Button2_Click" style="z-index: 1; left: 528px; top: 204px; position: absolute"/>
        <asp:Button ID="Button3" runat="server" Text="综合查询" class="button orange" Width="300px" Height="60px" OnClick="Button3_Click" style="z-index: 1; left: 528px; top: 304px; position: absolute"/>
        <asp:Button ID="Button4" runat="server" Text="异源合并" class="button white" Width="300px" Height="60px" OnClick="Button4_Click" style="z-index: 1; left: 528px; top: 404px; position: absolute"/>
        <asp:Button ID="Button5" runat="server" style="z-index: 1; left: 528px; top: 504px; position: absolute" Text="数据导出" class="button white" BackColor="Yellow"  Width="300px" Height="60px" OnClick="Button5_Click"/>
        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="1" style="z-index: 1; left: 543px; top: 601px; position: absolute; right: 340px" Text="EXCEL格式" />
        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="1" style="z-index: 1; left: 701px; top: 602px; position: absolute" Text="JSON格式" />
        <asp:RadioButton ID="RadioButton3" runat="server" Checked="True" GroupName="2" style="z-index: 1; left: 542px; top: 635px; position: absolute" Text="江苏省数据" />
        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="2" style="z-index: 1; left: 649px; top: 636px; position: absolute" Text="湖南省数据" />
        <asp:RadioButton ID="RadioButton5" runat="server" GroupName="2" style="z-index: 1; left: 753px; top: 636px; position: absolute" Text="合并数据" />
    </form>
</body>
</html>
