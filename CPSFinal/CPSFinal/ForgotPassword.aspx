<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="AchieversCPS.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Label ID="lblUserId" runat="server" Text="Please Enter your Id:"></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnGetUser" runat="server" Text="Get Password" OnClick="btnGetUser_Click"></asp:Button>
            <br />
            <br />
            UserName is: <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
            <br />
            Password is: <asp:Label ID="lblPassword" runat="server" Text=""></asp:Label>
        </center>
    </div>
    </form>
</body>
</html>
