<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminLocation.aspx.cs" Inherits="TravelAgencyWeb.Admin_Pages.AdminLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Manage Locations</h1>

   <!-- Displays all locations from the database--> 
    <asp:GridView ID="AdminLocationsGridView" 
        AutoGenerateColumns="False" EmptyDataText="No data" AllowPaging="True"
        DataKeyNames="id" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="AdminLocationsGridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="false" ReadOnly="true" SortExpression="id"/> 
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name"/> 
            <asp:BoundField DataField="country" HeaderText="country" SortExpression="country"/> 
        </Columns>
    </asp:GridView>
    
    <asp:Button ID="ButtonNewLocation" Text="New Location" runat="server" OnClick="NewLocation" />

    <!-- This panel is hidden until the user chooses to modify an entry -->
    <asp:Panel ID="PanelLocationValues" Visible="false" runat="server">
        <asp:Label Text="Name" runat="server" />
        <asp:TextBox ID="TextBoxLocationName" runat="server" />
        <br />

        <asp:Label Text="Country" runat="server" />
        <asp:TextBox ID="TextBoxLocationCountry" runat="server" />
        <br />
        
        <!-- Buttons for modifying the table-->
        <asp:Button ID="ButtonCreateLocation" Text="Create Location" Visible="false" runat="server" OnClick="CreateLocation" />
        <asp:Button ID="ButtonUpdateLocation" Text="Update Location" Visible="false" runat="server" OnClick="UpdateLocation" />
        <asp:Button ID="ButtonDeleteLocation" Text="Delete Location" Visible="false" runat="server" OnClick="DeleteLocation"/>
        <asp:Button ID="ButtonCancel" Text="Cancel" Visible="false" runat="server" OnClick="CancelChanges"/>
    </asp:Panel>
</asp:Content>
