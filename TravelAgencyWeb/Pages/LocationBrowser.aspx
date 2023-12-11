<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="LocationBrowser.aspx.cs" Inherits="TravelAgencyWeb.Pages.LocationBrowser" %>
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

        .clear {
            clear: both;
        }
    </style>

    <br class="clear" />
    <h1>Browse Locations</h1>
    <h4>Destination/location name:</h4>
    <p> <asp:TextBox id="searchLocationsBox" runat="server" Placeholder="Search locations"/></p>    
    <asp:Button ID="searchLocationsButton" runat="server" Text="Search" OnClick="searchLocationsButton_click" />
    <asp:Label ID="searchLocationsLabel" runat="server" Text=""></asp:Label>
    <asp:Panel ID="results" runat="server" Visible="true">
        <div runat="server" id="searchResultsContainer">
            <asp:Repeater ID="ResultRepeater" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <asp:Image ImageUrl='<%# Eval("image_path") %>' runat="server" />
                        <h3><%# Eval("name") %></h3>
                        <p><%# Eval("country") %></p>
                        <asp:HiddenField ID="RepeaterIdHiddenField" runat="server" Value='<%# Eval("id") %>'/>
                        <asp:Button ID="Location1" runat="server" Text="See more" OnClick="seeLocation"/>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>


    <br class="clear" />

    <asp:Panel ID="popular" runat="server" Visible="true">
        <h4>Popular locations</h4>
        <asp:Repeater ID="CardRepeater" runat="server">
            <ItemTemplate>
                <div class="card">
                    <asp:Image ImageUrl='<%# Eval("image_path") %>' runat="server" />
                    <h3><%# Eval("name") %></h3>
                    <p><%# Eval("country") %></p>
                    <asp:HiddenField ID="RepeaterIdHiddenField" runat="server" Value='<%# Eval("id") %>'/>
                    <asp:Button ID="Location1" runat="server" Text="See more" OnClick="seeLocation"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
