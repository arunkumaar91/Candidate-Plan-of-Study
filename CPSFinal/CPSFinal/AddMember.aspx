<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="AchieversCPS.AddMember" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="ddlMemberSelection" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMemberSelection_SelectedIndexChanged">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Student</asp:ListItem>
            <asp:ListItem>Faculty Advisor</asp:ListItem>
        </asp:DropDownList>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div>
            <asp:Panel ID="pnlAddStudent" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblStudentId" runat="server" Text="Student Id:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStudentId" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStudentName1" runat="server" Text="Student First Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStudentFirstName" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblStudentName2" runat="server" Text="Student Last Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStudentLastName" runat="server" AutoPostBack="true" OnTextChanged="txtStudentLastName_TextChanged"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblUhclEmail" runat="server" Text="Student Email:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStudentEmail" runat="server"></asp:TextBox>
                            @uhcl.edu</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProgramName" runat="server" Text="Program Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDept" runat="server">
                                <asp:ListItem>Computer Science -CSCI</asp:ListItem>
                                <asp:ListItem>Software Engineering -SWEN</asp:ListItem>
                                <asp:ListItem>Computer Engineering -CENG</asp:ListItem>
                                <asp:ListItem>Systems Engineering -SENG</asp:ListItem>
                                <asp:ListItem>Engineering Management -EMGT</asp:ListItem>
                                <asp:ListItem>Statistics -STAT</asp:ListItem>
                                <asp:ListItem>Mathematics -MATH</asp:ListItem>
                                <asp:ListItem>Computer Information System -CINF</asp:ListItem>
                                <asp:ListItem>Biotechnology -BIOT</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStudentdateOfBirth" runat="server" Text="Date Of Birth:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCalender" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtCalender" runat="server" Enabled="true" ClearTime="True"></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGender" runat="server">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFacultyAdvisorName" runat="server" Text="Faculty Advisor:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAdvisor" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStudentPass" runat="server" Text="Faculty Advisor:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStudentPass" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAddStudent" runat="server" Text="Add Student" OnClick="btnAddStudent_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlAddFac" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblFacId" runat="server" Text="Id:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFacId" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFirstName" runat="server" Text="FirstName:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLastName" runat="server" Text="LastName:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFacEmail" runat="server" Text="Email Id:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFacultyEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbldateOfBirth" runat="server" Text="Date Of Birth:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateOfBirth" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDateOfBirth" runat="server" Enabled="true" ClearTime="True"></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAddFaculty" runat="server" Text="Add Faculty" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
