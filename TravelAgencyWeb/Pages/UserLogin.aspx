<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="TravelAgencyWeb.Pages.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
            <br />
            Login
            <br />
            <br />
            E-Mail:
            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate ="Email" ErrorMessage="Email is required!"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="Email" ErrorMessage="Please insert a valid Email of form !" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>

            <br />
            Password:
            <input id="Password" type="password" runat="server"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate ="Password" ErrorMessage="Password is required!"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="ErrorLabel" runat="server" Visible="false"></asp:Label>
            <br />
            <asp:HyperLink NavigateUrl="UserRegistration.aspx" ID="ToRegistrationLink" runat="server">No account yet? Register!</asp:HyperLink>
            <br />
            <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />
        </asp:Content>
