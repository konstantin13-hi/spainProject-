<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TravelAgencyWeb.Pages.Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card {
            border: 1px solid #ccc;
            border-radius: 7.5px;
            padding: 10px;
            width: 300px;
            float: left;
            margin-right: 10px; /* Add margin between cards */
            margin-bottom: 10px; /* Add margin below cards */
        }

        .card img {
            width: 100%;
            height: auto;
            object-fit: cover;
        }

        .item-container {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 20px;
        }
    </style>
    <h1>HADA Travel Agency</h1>

    <!-- Using repeaters to display database entries as cards -->
    <asp:HyperLink ID="HyperLinkLocations" NavigateUrl="~/Pages/LocationBrowser.aspx" runat="server">Browse Locations</asp:HyperLink>
    <br />
    <div class="item-container">
        <asp:Repeater ID="HomeLocationsRepeater" runat="server" OnItemCommand="GoToLocation" >
            <ItemTemplate>
                <div class="card">
                    <asp:Image  ImageUrl='<%# Eval("image_path") %>' runat="server" CssClass="card-image"/>
                    <h3><%# Eval("name") %></h3>
                    <p><%# Eval("country") %></p>
                    <asp:HiddenField ID="FieldHiddenId" runat="server" Value='<%# Eval("id") %>' />
                    <asp:Button Text="See More" runat="server"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:HyperLink ID="HyperLinkAccomodations" NavigateUrl="~/Pages/AccomodationBrowser.aspx" runat="server">Browse Accomodations</asp:HyperLink>
    <br />
    <div class="item-container">
        <asp:Repeater ID="HomeAccomodationsRepeater" runat="server" OnItemCommand="GoToAccomodation" >
            <ItemTemplate>
                <div class="card">
                    <asp:Image  ImageUrl='<%# Eval("image_path") %>' runat="server" CssClass="card-image"/>
                    <h3><%# Eval("name") %></h3>
                    <p><%# Eval("adress") %></p>
                    <asp:HiddenField ID="FieldHiddenId" runat="server" Value='<%# Eval("id") %>' />
                    <asp:Button Text="See More" runat="server"/>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:HyperLink ID="HyperLinkLandmarks" NavigateUrl="~/Pages/Landmark.aspx" runat="server">Browse Landmarks</asp:HyperLink>
    <br />
    <div class="item-container">
        <asp:Repeater ID="HomeLandmarksRepeater" runat="server" OnItemCommand="GoToLandmark" >
            <ItemTemplate>
                <div class="card">
                    <asp:Image  ImageUrl='<%# Eval("image_path") %>' runat="server" CssClass="card-image"/>
                    <h3><%# Eval("name") %></h3>
                    <p><%# Eval("type") %></p>
                    <asp:HiddenField ID="FieldHiddenId" runat="server" Value='<%# Eval("id") %>' />
                    <asp:Button Text="See More" runat="server"/>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
