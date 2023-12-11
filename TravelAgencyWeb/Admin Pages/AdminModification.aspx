<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminModification.aspx.cs" Inherits="TravelAgencyWeb.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Modify Website</h1>
    <!-- Menu of all admin pages -->
    <asp:Menu ID="ModificationMenu" runat="server">
        <Items>
            <asp:MenuItem NavigateUrl="~/Admin Pages/AdminAccomodation.aspx" Text="Manage Accomodations" ToolTip="Manage Accomodations"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Admin Pages/AdminCategory.aspx" Text="Manage Categories" ToolTip="Manage Categories"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Admin Pages/AdminLandmark.aspx" Text="Manage Landmarks" ToolTip="Manage Landmarks"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Admin Pages/AdminLocation.aspx" Text="Manage Locations" ToolTip="Manage Locations"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Admin Pages/AdminUser.aspx" Text="Manage Users" ToolTip="Manage Users"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Admin Pages/AdminSeasonalPricing.aspx" Text="Manage Seasonal Pricing" Tooltip="Manage Seasonal Pricing"></asp:MenuItem>
        </Items>
    </asp:Menu>
</asp:Content>
