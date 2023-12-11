<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserBooking.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserBooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .center-content {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            height: 100%;
            font-size:large;
        }
        .content-container {
            margin-top: 0px;
        }
        h1 {
            padding-top: 5px;
            text-align: center;
        }

        p, button, label {
            text-align: center;
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px;
        }
        th {
            background-color: lightblue;
        }
        .calendar-container {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 10px;
            margin-top: 20px;
            text-align:center;
            
        }
        .pay-button {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-top: 20px;
        }
        
        .pay-button button {
            font-size: large;
            padding: 10px 20px;
        }
    </style>
    <div class="center-content">
         <div class="content-container">
        <h1 style="padding-top: 0%; text-align: center;">Booking Page</h1>
        <p style="text-align: center;">Location: <asp:Label ID="label_Location" Text="Loaded from Accommodation" runat="server" /></p>
        <p style="text-align: center;">Accommodation: <asp:Label ID="label_Accommodation" Text="Loaded from Accommodation" runat="server" /></p>
    <!--
    Location onChange (when selected country/city): Get all available accommodation
    Accommodation & Location can use to dropdown menu
    -->
        <!--SelectionMode
            Day:        	To select a single day.
            DayWeek:	    To select a single day or an entire week.
            DayWeekMonth:	To select a single day, a week, or an entire month.
            None:       	Nothing can be selected.
        -->
    <div class="calendar-container">
        <span>Checkin Date<asp:Calendar ID="calendar_CheckIn" runat="server" SelectionMode="Day" OnSelectionChanged="OnClick_IsValid"></asp:Calendar></span>    
        <span>Checkout Date<asp:Calendar ID="calendar_CheckOut" runat="server" SelectionMode="Day" OnSelectionChanged="OnClick_IsValid"></asp:Calendar></span>
    </div>
    <asp:Label Text="" ID="label_DateError" runat="server" />
    <p>Number of days: <asp:Label ID="label_NumberOfDays" Text="0" runat="server" /></p>
    <p>Price: <asp:Label ID="label_Price" Text="0" runat="server" /></p>
    <asp:Label ID="label_pay_button_error" Text="" runat="server" />
    <div class="pay-button">
        <asp:Button ID="Pay" runat="server" Text="Reserve A Room Now!" OnClick="Pay_Click" />
    </div>
        
         </div>
    </div>
</asp:Content>