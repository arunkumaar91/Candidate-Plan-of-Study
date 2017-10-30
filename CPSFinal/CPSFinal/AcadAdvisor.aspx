﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcadAdvisor.aspx.cs" Inherits="AchieversCPS.AcadAdvisor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <!--Setting the viewport-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>UHCL - SCE Candidate Plan of Study</title>
    <!-- Custom CSS -->
    <link href="css/acadadvisorpage.css" rel="stylesheet" />
    <link href="css/loginfooter.css" rel="stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" />
    <!--<link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />-->
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
        .auto-style2 {
            width: 189px;
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
        <div class="heading">
            Welcome
            <asp:Label ID="sgnName" runat="server" Text="Name"></asp:Label>
            <asp:Button ID="sgnButton" runat="server" Text="SignOut" CssClass="sgn" OnClick="sgnButton_Click" />
        </div>
        <br />
        <br />
        <br />
        <div>
            <div class="menubuttons" style="float: left">
                <asp:Button ID="generateForm" runat="server" Text="Generate Form" CssClass="btn btn-secondary btn-lg" OnClick="generateForm_Click" /><br />
                <br />
                <asp:Button ID="auditCPS" runat="server" Text="Audit CPS" CssClass="btn btn-secondary btn-lg" /><br />
                <br />
                <asp:Button ID="modifyCPS" runat="server" Text="Modify CPS" CssClass="btn btn-secondary btn-lg" /><br />
                <br />
                <asp:Button ID="addCatalog" runat="server" Text="Add Catalog" CssClass="btn btn-secondary btn-lg" OnClick="addCatalog_Click" />
            </div>
        </div>

        <div id="ClassPanel">
            <asp:Panel ID="generateformpanel" Visible="false" runat="server" CssClass="panel gen">
                <table>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_deptName" runat="server" Text="Department Name" CssClass="lbl_1"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbl_semester" runat="server" Text="Semester"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbl_year" runat="server" Text="Year"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlDepts" runat="server"></asp:DropDownList>
                        </td>

                        <td>&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txt_sem" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txt_year" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;<asp:Button ID="btn_viewStudentList" runat="server" CssClass="viewbtn" Text="View Student List" OnClick="btn_viewStudentList_Click" />
                        </td>


                    </tr>
                </table>
                <table style="align-content: center; text-align: center; align-self: auto">
                    <tr>
                        <td>
                            <asp:GridView ID="grdAllStudents" runat="server">
                                <Columns>
                                    <asp:HyperLinkField />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="AddCatalogPanel" Visible="false" runat="server">

                <asp:Label ID="lblCatalog" runat="server" Text="You can download Catalog by clicking button below!!"></asp:Label>
    <asp:Button ID="btnDownload" runat="server" Text="Download Catalog" OnClick="btnDownload_Click" />
    <asp:FileUpload ID="FileUpload1" runat="server"  />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click1" />
            </asp:Panel>
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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
        <script src="js/bootstrap.min.js">
        </script>
    </form>
</body>
</html>
