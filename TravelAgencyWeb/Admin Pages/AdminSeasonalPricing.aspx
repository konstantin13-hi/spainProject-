<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminSeasonalPricing.aspx.cs" Inherits="TravelAgencyWeb.Admin_Pages.AdminSeasonalPricing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <%@ Import Namespace="library" %>
    <h1>Seasonal Pricing</h1>
    Only one Seasonalpricing per Accomodation possible.
    Startdate must be in future and Enddate must be later than StartDate.<br />
    <br />
    ID:<asp:TextBox ID="IDTextBox" runat="server" Enabled="true" ReadOnly="true"></asp:TextBox>
    <br />
    Name:<asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNoteTitle" runat="server" ControlToValidate ="NameTextBox" ErrorMessage="Title for this travelnote is required!"></asp:RequiredFieldValidator>

    <br />
    Multiplier for price:
    <asp:TextBox ID="MultiplierTextBox" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="MultiplierValidator" runat="server" ControlToValidate="MultiplierTextBox" ErrorMessage="Invalid input. Please enter a valid double value." ValidationExpression="^\d+(\.\d+)?$"
        Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
    <br />
    <br />

    StartDate:<asp:Calendar ID="StartDateCalendar" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
        <DayStyle Width="14%" />
        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
        <TodayDayStyle BackColor="#CCCC99" />
    </asp:Calendar>
    <br />
    EndDate:<asp:Calendar ID="EndDateCalendar" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
        <DayStyle Width="14%" />
        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
        <TodayDayStyle BackColor="#CCCC99" />
    </asp:Calendar>
    <br />

    <asp:Button ID="Create" runat="server" Text="Create" OnClick="Create_Click" />
    <asp:Button ID="Update" runat="server" Text="Update" OnClick="Update_Click" />
    <asp:Button ID="Delete" runat="server" Text="Delete" OnClick="Delete_Click" />
    <asp:Button ID="Clear" runat="server" Text="Clear" OnClick="Clear_Click"  />
    <br />
    <asp:Label ID="feedback" runat="server"></asp:Label>
    <br />
    <br />
    All SeasonalPricings: <br />
    <asp:GridView ID="AllSeasonalPricing" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="AllSeasonalPricing_SelectedIndexChanged" CellPadding="4" ForeColor="Black" GridLines="Horizontal" SelectionMode="Single" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
             <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                <asp:BoundField DataField="NamePricing" HeaderText="Name" SortExpression="Title" />
                <asp:BoundField DataField="MultiplierPricing" HeaderText="Multiplier" SortExpression="Multiplier" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            </Columns>
             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
             <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F7F7F7" />
             <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
             <SortedDescendingCellStyle BackColor="#E5E5E5" />
             <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:Label ID="NoSeasonalPricesLoaded" runat="server"></asp:Label>

    <br />
    <br />
</asp:Content>
