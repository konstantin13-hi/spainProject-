<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminUser.aspx.cs" Inherits="TravelAgencyWeb.Admin_Pages.AdminUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Manage Users</h1>

   <!-- Displays all users from the database--> 
    <asp:GridView ID="AdminUsersGridView" AutoGenerateColumns="False" EmptyDataText="No data"
        AllowPagin="true" DataKeyNames="id" runat="server" 
        OnSelectedIndexChanged="AdminUsersGridView_SelectedIndexChanged" AutoGenerateSelectButton="true">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:CheckBoxField DataField="isAdmin" HeaderText="isAdmin" SortExpression="isAdmin" />
            <asp:BoundField DataField="created_at" HeaderText="created_at" SortExpression="created_at" />
            <asp:BoundField DataField="updated_at" HeaderText="updated_at" SortExpression="updated_at" />
        </Columns>

    </asp:GridView>

    <asp:Button ID="ButtonNewUser" Text="New User" runat="server" OnClick="NewUser" />

    <!-- This panel is hidden until the user chooses to modify an entry -->
    <asp:Panel ID="PanelUserValues" Visible="false" runat="server">   
        <br />
        <asp:Label Text="Email: " runat="server" />
        <asp:TextBox ID="TextBoxEmail" runat="server" />
        <asp:RequiredFieldValidator ErrorMessage="Email cannot be empty" ControlToValidate="TextBoxEmail" runat="server" />
        <asp:RegularExpressionValidator ErrorMessage="Not a valid email" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ControlToValidate="TextBoxEmail" runat="server" />
        
        <br />
        <asp:Label Text="Password: " runat="server" />
        <asp:TextBox ID="TextBoxPassword" runat="server" />
        <asp:RequiredFieldValidator ErrorMessage="Password is required" ControlToValidate="TextBoxPassword" runat="server" />

        <br />
        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="TextBoxName" runat="server" />

        <br />
        <asp:Label Text="IsAdmin: " runat="server" />
        <asp:DropDownList ID="DropDownIsAdmin" runat="server">
            <asp:ListItem Text="True" />
            <asp:ListItem Text="False" />
        </asp:DropDownList>
        <br />

        <!-- Buttons for modifying the table-->
        <asp:Button ID="ButtonCreateUser" Text="Create User" Visible="false" runat="server" OnClick="CreateUser" />
        <asp:Button ID="ButtonUpdateUser" Text="Update User" Visible="false" runat="server" OnClick="UpdateUser" />
        <asp:Button ID="ButtonDeleteUser" Text="Delete User" Visible="false" runat="server" OnClick="DeleteUser"/>
        <asp:Button ID="ButtonCancel" Text="Cancel" Visible="false" runat="server" OnClick="CancelChanges"/>

    </asp:Panel>


</asp:Content>
