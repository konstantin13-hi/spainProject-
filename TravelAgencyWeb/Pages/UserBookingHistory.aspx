<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserBookingHistory.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserBookingHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align:center; padding-top:25px">Booking History</h1>
    <asp:Table ID="HistoryTable" runat="server" style="border:1px solid black; width: 75%; margin:auto; border-collapse: collapse;">
    <asp:TableHeaderRow style="background-color: #f2f2f2; font-weight: bold;">
        <asp:TableHeaderCell>Accommodation</asp:TableHeaderCell>
        <asp:TableHeaderCell>Check-in Date</asp:TableHeaderCell>
        <asp:TableHeaderCell>Check-out Date</asp:TableHeaderCell>
        <asp:TableHeaderCell>Price</asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>
    
    <div class="success-content-container" id="success-container" onclick="removeSuccessContainer()" style="margin: 50px auto; text-align: center; background-color: darkseagreen; width: 50%; border-radius: 10px; display: block;">
        <asp:Label Text="" ID="label_success1" runat="server" />
        <asp:Label Text="" ID="label_success2" runat="server" />
    </div>
</asp:Content>
