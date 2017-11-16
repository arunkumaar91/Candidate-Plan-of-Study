<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FacultyAdvisor.aspx.cs" Inherits="AchieversCPS.FacultyAdvisor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <!--Setting the viewport-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>UHCL - SCE Candidate Plan of Study</title>
    <!-- Custom CSS -->
    <link href="css/facultyadvisorpage.css" rel="stylesheet" />
    <link href="css/acadadvisorfooter2.css" rel="stylesheet" />
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
        .auto-style3 {
            width: 193px;
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
                <asp:Button ID="getStudentDetails" runat="server" Text="Get Student Details" CssClass="btn btn-secondary btn-lg" OnClick="getStudentDetails_Click" />
                <br />
                <br />
                <asp:Button ID="viewStudentCPS" runat="server" Text="View or Print Student CPS" CssClass="btn btn-secondary btn-lg" OnClick="viewStudentCPS_Click" />
                <br />
                <br />
                <asp:Button ID="modifyDraftCPS" runat="server" Text="Modify Draft CPS" CssClass="btn btn-secondary btn-lg" OnClick="modifyDraftCPS_Click" />
            </div>
        </div>

        <div id="ClassPanel">

            <!-- GET STUDENT DETAILS PANEL -->

            <asp:Panel ID="getstudentdetailspanel" Visible="false" runat="server" CssClass="panel gen">
                <div class="accordian panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Get Student Details
                            </a>
                        </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse">
                        <div class="panel-body">
                            You can retrieve the list of students below for whom you are the advisor     
                        </div>
                    </div>
                </div>
                <br />
                <table>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbl_Std_Id" runat="server" Text="Student ID:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txt_Std_Id" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_view_std_list" runat="server" CssClass="viewbtn" Text="View Student List" OnClick="btn_view_std_list_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!-- VIEW OR PRINT STUDENT CPS-->

            <asp:Panel ID="vieworprintcpsfac" Visible="false" runat="server" CssClass="panel gen">
                <div class="accordian panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">View or Print CPS
                            </a>
                        </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse">
                        <div class="panel-body">
                            You can view the candidate plan of individual students here     
                        </div>
                    </div>
                </div>
                <br />
                <table>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbl_viewStdID" runat="server" Text="Student ID:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txt_view_std_id" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_viewStdCPS" runat="server" CssClass="viewbtn" Text="View Student CPS" OnClick="btn_viewStdCPS_Click" />
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="pnlPrintCPS" runat="server">
                    <table style="align-content: center; text-align: center; align-self: auto">
                        <tr style="text-align: left">
                            <td>Name:<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="auto-style3">Dept:<asp:Label ID="lblDept" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr style="text-align: left">
                            <td>Semester Admitted:<asp:Label ID="lblSemester" runat="server" Text="" />
                            </td>
                            <td class="auto-style3">Year Admitted:
                                <asp:Label ID="lblAdmitted" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr style="text-align: left">
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lblFoundation" runat="server" Text="" />
                                <br />
                                <asp:GridView ID="grdfdnCourses" runat="server">
                                    <EditRowStyle BorderStyle="None" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lblCore" runat="server" Text="" />
                                <br />
                                <asp:GridView ID="grdCoreCourses" runat="server"></asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lblElective" runat="server" Text="" />
                                <br />
                                <asp:GridView ID="grdElectiveCourses" runat="server"></asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPrint" runat="server" Text="Print PDF" OnClick="btnPrint_Click" CssClass="btn btn-secondary" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
            <!-- MODIFY DRAFT CPS-->

            <asp:Panel ID="modifydraftcpsfac" Visible="false" runat="server" CssClass="panel gen">
                <div class="accordian panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Modify Draft CPS
                            </a>
                        </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse">
                        <div class="panel-body">
                            You can modify or edit the candidate plan of individual students here     
                        </div>
                    </div>
                </div>
                <br />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
