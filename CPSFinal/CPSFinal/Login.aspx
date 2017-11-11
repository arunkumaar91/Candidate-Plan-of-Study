<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AchieversCPS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <!--Setting the viewport-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>UHCL - SCE Candidate Plan of Study</title>
    <!-- Custom CSS -->
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/loginFooter1.css" rel="stylesheet" />
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
        <div class="wrapper">
            <div class="mycont">
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                    </ol>

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        <div class="item active">
                            <img src="Images/405ac576-d9e9-4b6f-8d07-ef92cc02f25e.jpg" alt="Los Angeles" style="width: 100%;" />
                        </div>

                        <div class="item">
                            <img src="Images/a852a19a-b9b6-4b53-96f3-cbf5254af344.jpg" alt="Chicago" style="width: 100%;" />
                        </div>
                    </div>

                    <!-- Left and right controls -->
                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
            </div>
            <div class="candidate">
                <span>Candidate Plan of Study - Application</span>
            </div>
            <div class="description">
                <p>
                    We want to make your life simpler and more convenient at University of Houston-Clear Lake. CPS application
            helps you to generate your candidate plan of study easy and real quick. Students, Faculty advisors, Academic Advisors can login below.
           
                </p>
            </div>
            <div class="login">
                <div class="lgn">
                    <asp:Label ID="username_lbl1" runat="server" Text="Username:"></asp:Label>
                    <asp:TextBox runat="server" ID="UserName" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName"
                        runat="server"
                        ErrorMessage="Please enter username." ControlToValidate="UserName"
                        Display="Dynamic" SetFocusOnError="True" CssClass="alert-danger" ValidationGroup="alertGrp" />
                    <br />
                    <br />
                    <asp:Label ID="password_lbl2" runat="server" Text="Password:"></asp:Label>
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword"
                        runat="server"
                        ErrorMessage="Please enter password." ControlToValidate="Password"
                        Display="Dynamic" SetFocusOnError="True" CssClass="alert-danger" ValidationGroup="alertGrp" />
                    <br />
                    <asp:HyperLink ID="hplPassword" NavigateUrl="~/ForgotPassword.aspx" runat="server">Forgot Password Click here!!</asp:HyperLink>
                    <br />
                </div>
                <div class="signIn">
                    <asp:Button ID="login_btn1" runat="server" Text="Sign In" CssClass="btn btn-secondary btn-lg" ValidationGroup="alertGrp" OnClick="login_btn1_Click" />
                    <asp:Button ID="login_btn2" runat="server" Text="Reset" CssClass="btn btn-secondary btn-lg" OnClick="login_btn2_Click" UseSubmitBehavior="False" />
                </div>
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
        </div>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
