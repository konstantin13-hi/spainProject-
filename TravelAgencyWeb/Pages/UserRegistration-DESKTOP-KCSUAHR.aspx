<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div _designerregion="0">
        Login: 
        <br />
        <br />
        E-Mail:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        Label:
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        Password:
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        Confirm Password:
        <input id="Password1" type="password" />
        <br />
        <asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click"/>
       </div>
</asp:Content>
