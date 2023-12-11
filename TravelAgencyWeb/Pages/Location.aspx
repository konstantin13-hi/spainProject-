<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="TravelAgencyWeb.Pages.Locations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px;
            width: 300px;
            float: left;
            margin-left: 10px;
            margin-bottom: 10px;
        }
        .clear {
            clear: both;
        }
    </style>
    <br />
    <h1><%# Location.Name %></h1>
    <asp:Label ID="ErrorMessageLabel" runat="server" Text=""></asp:Label>
    <asp:Panel ID="accommodations" runat="server" Visible="true">
        <h2>Accommodations in <%# Location.Name %></h2>
        <asp:Label ID="AccomodationLabel" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="AccomodationRepeater" runat="server">
            <ItemTemplate>
                <div class="card">
                    <h3><%# Eval("name") %></h3>
                    <p>Normal price for one night: <%# Eval("price") %></p>
                    <p>Number of rooms: <%# Eval("numberofrooms") %></p>
                    <asp:HiddenField ID="AccomRepeaterIdHiddenField" runat="server" Value='<%# Eval("id") %>'/>
                    <asp:Button ID="AccomodationButton" runat="server" Text="See more" OnClick="seeAccom"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:HyperLink ID="HyperLinkAccomodations" NavigateUrl="~/Pages/AccomodationBrowser .aspx" runat="server">See more accomodations</asp:HyperLink>
    <br class="clear" />

    <asp:Panel ID="landmarks" runat="server" Visible="true">
        <h2>Landmarks in <%# Location.Name %></h2>
        <asp:Label ID="LandmarkLabel" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="LandmarkRepeater" runat="server">
            <ItemTemplate>
                <div class="card">
                    <h3><%# Eval("name") %></h3>
                    <p>Type of landmark: <%# Eval("type") %></p>
                    <p>Price for visiting: <%# Eval("pricerange") %></p>
                    <asp:HiddenField ID="LandmarkRepeaterIdHiddenField" runat="server" Value='<%# Eval("id") %>'/>
                    <asp:Button ID="LandmarkButton" runat="server" Text="See more" OnClick="seeLandmark"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
    
    <br />
    <asp:HyperLink ID="HyperLinkLandmarks" NavigateUrl="~/Pages/Landmark.aspx" runat="server">See more landmarks</asp:HyperLink>
    
    <div class="clear"></div>
    <div>
        <h2>Weather in <%# Location.Name %> Today</h2>
        <asp:GridView ID="WeatherGridView" AutoGenerateColumns="False"
            EmptyDataText="No data" AllowPaging="True" DataKeyNames="id"
            runat="server">
                <Columns>
                    <asp:BoundField DataField="temperatureAvg" HeaderText="Average Temperature" />
                    <asp:BoundField DataField="temperatureMax" HeaderText="Max Temperature" />
                    <asp:BoundField DataField="temperatureMin" HeaderText="Min Temperature" />
                    <asp:BoundField DataField="rain" HeaderText="Rain" />
                    <asp:BoundField DataField="wind" HeaderText="Wind" />
                </Columns>
         </asp:GridView>
    </div>
</asp:Content>
