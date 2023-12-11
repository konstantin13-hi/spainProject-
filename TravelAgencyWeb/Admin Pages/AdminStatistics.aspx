<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminStatistics.aspx.cs" Inherits="TravelAgencyWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Website Statistics</h1>
    <h4>Choose the amount of rankings you want to see</h4>
    <asp:RadioButtonList id="statsettings" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="statsettings_SelectedIndexChanged">
            <asp:ListItem Value="1">See top 1</asp:ListItem>
            <asp:ListItem Value="3">See top 3</asp:ListItem>
            <asp:ListItem Value="5">See top 5</asp:ListItem>
            <asp:ListItem Value="10">See top 10</asp:ListItem>
    </asp:RadioButtonList>
    <div>
        <h2>Most reviewed Accomodation(s)</h2>
        <asp:GridView ID="MostReviewed" AutoGenerateColumns="False"
            EmptyDataText="No data" AllowPaging="True" DataKeyNames="id"
            runat="server" OnSelectedIndexChanged="statsettings_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="reviewCount" HeaderText="Number of Reviews" />
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <h2>Highest ranked Accomodation(s)</h2>
        <asp:GridView ID="HighestRankedAccomodations" AutoGenerateColumns="False"
            EmptyDataText="No data" AllowPaging="True" DataKeyNames="id"
            runat="server" OnSelectedIndexChanged="statsettings_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="averagePointsOverall" HeaderText="Points" />
                </Columns>
            </asp:GridView>
    </div>
    <div>
    <h2>Most favorited Accomodation(s)</h2>
        <asp:GridView ID="MostFavoritedAccomodations" AutoGenerateColumns="False"
            EmptyDataText="No data" AllowPaging="True" DataKeyNames="id"
            runat="server" OnSelectedIndexChanged="statsettings_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="FavoriteCount" HeaderText="Number of times favorites" />
                </Columns>
         </asp:GridView>
    </div>
    <h2>Another statistics</h2>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10">
            <Columns>
                <asp:BoundField DataField="AccommodationName" HeaderText="Accommodation Name" />
                <asp:BoundField DataField="NumberOfReviews" HeaderText="Number of Reviews" />
            </Columns>
        </asp:GridView>
</asp:Content>
