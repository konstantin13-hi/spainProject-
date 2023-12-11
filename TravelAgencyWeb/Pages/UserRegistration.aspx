<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div _designerregion="0">
        Login: 
        <br />
        <br />
        E-Mail:
        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate ="Email" ErrorMessage="Email is required!"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="Email" ErrorMessage="Please insert a valid Email format!" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>

        <br />
        Username:
        <asp:TextBox ID="Username" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate ="Username" ErrorMessage="Username is required!"></asp:RequiredFieldValidator>

        <br />
        Password:
        <asp:TextBox ID="Password" runat="server" type="password"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="Password" ErrorMessage="The password must have at least 8 characters!" ValidationExpression="^.{8,}$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate ="Password" ErrorMessage="Password is required!"></asp:RequiredFieldValidator>

        <br />
        Confirm Password:
        <input id="PasswordConf" runat="server" type="password" />
        <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToValidate="PasswordConf" ControlToCompare="Password" ErrorMessage="Passwords do not match!"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPasswordConf" runat="server" ControlToValidate ="PasswordConf" ErrorMessage="Password is required!"></asp:RequiredFieldValidator>

        <br />
        <asp:Label ID="ErrorLabel" runat="server" Visible="false"></asp:Label>
        <br />

        <asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click"/>
       </div>
</asp:Content>
