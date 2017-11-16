<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="AchieversCPS.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <!--Setting the viewport-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>UHCL - SCE Candidate Plan of Study</title>
    <!-- Custom CSS -->
    <link href="css/main1.css" rel="stylesheet" />
    <link href="css/loginfooter2.css" rel="stylesheet" />
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
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse" style="color: white">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">University of Houston Clear Lake</a>
                </div>
                <div class="navbar-collapse collapse navbar-right">
                    <ul class="nav navbar-nav">
                        <li><a class="active" href="https://www.uhcl.edu/eservices/" target="_blank">E-Services</a></li>
                        <li><a class="active" href="https://saprd.my.uh.edu/psc/saprd/EMPLOYEE/HRMS/c/UHS_CL_CUSTOM.CLASS_SEARCH.GBL" target="_blank">Class Schedule</a></li>
                        <li><a class="active" href="https://webmail.uhcl.edu/owa/auth/logon.aspx?replaceCurrent=1&url=https%3a%2f%2fwebmail.uhcl.edu%2fowa%2f" target="_blank">Webmail</a></li>
                        <li><a class="active" href="https://blackboard.uhcl.edu/" target="_blank">Blackboard</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <p class="candidate">Forgot Password Page</p>
        <br />
        <asp:Panel ID="Panel1" runat="server" CssClass="panelforfor"></asp:Panel>
        <div class="forgot">
            <asp:Label ID="lblUserId" runat="server" Text="Please Enter your Id:"></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnGetUser" runat="server" Text="Get Password" CssClass="btn btn-md" OnClick="btnGetUser_Click"></asp:Button>
            <br />
            <br />
            UserName is:
            <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
            <br />
            <br />
            Password is:
            <asp:Label ID="lblPassword" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Login.aspx" runat="server">Go to Home Page</asp:HyperLink>
        </div>

        <footer>
            <!--Contact me-->
            <div class="page" id="contact me">
                <div class="content">
                    <h4 id="cnt">Contact</h4>
                    <h4 id="sce">College of Science and Engineering</h4>
                    <div>
                        <span id="add">Bayou Building 3611
                        2700 Bay Area Blvd, Box 415
                        Houston, TX 77058-1002
                        </span>
                    </div>
                    <div>
                        <span id="ph">Phone: 281-283-3700</span>
                        <span id="ph1">Advising: 281-283-3711</span>
                    </div>
                    <div>
                        <span id="mail">cseadvising@uhcl.edu</span>
                    </div>
                    <ul class="social">
                        <li><a href="https://www.facebook.com/UHClearLake" target="_blank"><i class="fa fa-facebook-square fa-lg"></i></a></li>
                        <li><a href="https://twitter.com/UHClearLake" target="_blank"><i class="fa fa-twitter-square fa-lg"></i></a></li>
                        <li><a href="https://www.instagram.com/UHClearLake/" target="_blank"><i class="fa fa-instagram fa-lg"></i></a></li>
                    </ul>
                    <span id="copy">&copy; 2017 University of Houston Clear Lake</span>
                </div>
                <!--content div-->
            </div>
            <!--Contact me div-->
        </footer>
    </form>
</body>
</html>
