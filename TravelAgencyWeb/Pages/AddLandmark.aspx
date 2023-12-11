<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AddLandmark.aspx.cs" Inherits="TravelAgencyWeb.Pages.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Landmarks</h1>
    <p>Add landmark information below. Name and city are reguired. Other fields are optional.</p>
    <p>*Name: </p>
    <asp:TextBox ID="TextBox_name" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Landmark has to have a name" ControlToValidate="TextBox_name"></asp:RequiredFieldValidator>
    <p>*City: </p>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server"></asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Landmark has to have a city" ControlToValidate="RadioButtonList1"></asp:RequiredFieldValidator>
    
    <p>Address: </p>
    <asp:TextBox ID="TextBox2_address" runat="server" Width="500px"></asp:TextBox>
    <p>Type:</p>
    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem>Museum</asp:ListItem>
        <asp:ListItem>Adventure park</asp:ListItem>
        <asp:ListItem>Mountain</asp:ListItem>
        <asp:ListItem>Attraction</asp:ListItem>
        <asp:ListItem>Historical place</asp:ListItem>
        <asp:ListItem>Beach</asp:ListItem>
        <asp:ListItem>Park</asp:ListItem>
        <asp:ListItem Selected="True">Other</asp:ListItem>
    </asp:RadioButtonList>
    <p>Price: </p>
    <asp:TextBox ID="TextBox4_price" runat="server" Width="500px"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="The price must be an integer" 
        ControlToValidate="TextBox4_price" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
    <p>Websitelink: </p>
    <asp:TextBox ID="TextBox5_website" runat="server" Width="500px"></asp:TextBox>
    <br />
    
    <br />

    <asp:Button ID="Button1" runat="server" Text="Add landmark" OnClick="Button1_Click" BorderWidth="1px" />
    <br />

</asp:Content>
