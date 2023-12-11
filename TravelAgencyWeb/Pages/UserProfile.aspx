<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>User Profile</h1>
    <!-- User navigation -->
    <asp:Menu ID="UserProfileNavigation" runat="server">
        <items>
            <asp:MenuItem NavigateUrl="~/Pages/UserFavorites.aspx" Text="Favorites" ToolTip="Favorites"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Pages/UserTravelNotes.aspx" Text="Travel Notes" ToolTip="Travel Notes"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Pages/UserBookingHistory.aspx" Text="Booking History" ToolTip="Booking History"></asp:MenuItem>
        </items>
    </asp:Menu>

    <br />
    <!-- User information -->
    <asp:Panel ID="PanelUserInformation" runat="server">
        <h2>User Information</h2>
        <asp:Label Text="Name" runat="server" />
        <asp:TextBox ID="TextBoxInfoName" placeholder="Displays name" Enabled="false" runat="server" />
        <br />
        <asp:Label Text="Email" runat="server" />
        <asp:TextBox ID="TextBoxInfoEmail" placeholder="Displays email" Enabled="false" runat="server" />
        <br />
    </asp:Panel>
    <asp:Button ID="ButtonEditProfile" Text="Edit Profile" runat="server" OnClick="ButtonEditProfile_Click"/>

    <!-- Edit Profile -->
    <asp:Panel ID="ProfileSettings" runat="server" Visible="false">
        <asp:Label Text="Name" runat="server" />
        <asp:TextBox ID="TextBoxName" placeholder="Displays name" runat="server" />
        <br />
        <asp:Label Text="Email" runat="server" />
        <asp:TextBox ID="TextBoxEmail" placeholder="Displays email" runat="server" />
        <asp:RequiredFieldValidator ErrorMessage="Email cannot be empty" ControlToValidate="TextBoxEmail" runat="server" />
        <asp:RegularExpressionValidator ErrorMessage="Not a valid email" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ControlToValidate="TextBoxEmail" runat="server" />
        <br />
        <asp:Label Text="Password" runat="server" />
        <asp:TextBox ID="TextBoxPassword" placeholder="Displays password" runat="server" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="The password must have at least 8 characters!" ValidationExpression="^.{8,}$"></asp:RegularExpressionValidator>
        <br />
        <asp:Button ID="ButtonSave" Text="Save" runat="server" OnClick="ButtonSaveChanges" />
        <asp:Button ID="ButtonCancel" Text="Cancel" runat="server" OnClick="ButtonCancelChanges" />
    </asp:Panel>
</asp:Content>
