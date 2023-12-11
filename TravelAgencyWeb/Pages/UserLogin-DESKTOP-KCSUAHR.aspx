<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="TravelAgencyWeb.Pages.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div _designerregion="0">
            Login
            <br />
            <br />
            E-Mail:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            Password:
            <input id="Password1" type="password" />
            <br />
            <asp:LinkButton NavigateUrl="UserRegistration.aspx" ID="ToRegistrationLink" runat="server">No account yet? Register!</asp:LinkButton>
            <br />
            <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />
        </div>
</asp:Content>
