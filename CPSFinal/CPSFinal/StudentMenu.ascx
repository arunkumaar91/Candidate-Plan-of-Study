<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentMenu.ascx.cs" Inherits="AchieversCPS.StudentMenu" %>
<!-- Custom CSS -->
    <link href="css/studentHomePage.css" rel="stylesheet" />
    <link href="css/foot.css" rel="stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <!--JavaScript-->
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <!--Google Material Design
    <script src="Content/mdl-v1.1.2/material.min.js"></script>
    <link href="Content/mdl-v1.1.2/material.min.css" rel="stylesheet" />-->
    <!-- Google fonts -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700,300,800'
        rel='stylesheet' type='text/css' />
    <link href="https://fonts.googleapis.com/css?family=Oswald:200,300,400,500,600,700" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Alegreya:400,400i,700,700i,900,900i&amp;subset=latin-ext" rel="stylesheet" type="text/css" />
    <!--Font Awesome-->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" />
<div class="menubuttons" style="float: left">
    <asp:Button ID="initialCPS" runat="server" Text="View Initial CPS" CssClass="btn btn-secondary btn-lg" OnClick="initialCPS_Click1" /><br />
    <br />
    <asp:Button ID="schApp" runat="server" Text="Schedule Appointment" CssClass="btn btn-secondary btn-lg" OnClick="schApp_Click" /><br />
    <br />
    <asp:Button ID="retCPS" runat="server" Text="Retrieve (or) Print CPS" CssClass="btn btn-secondary btn-lg" /><br />
    <br />
    <asp:Button ID="cpsChng" runat="server" Text="CPS Change Form" CssClass="btn btn-secondary btn-lg" />
</div>
<br />
<br />
