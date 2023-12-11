<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminLandmark.aspx.cs" Inherits="TravelAgencyWeb.Admin_Pages.AdminLandmark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Manage Landmarks</h1>

   <!-- Displays all landmarks from the database--> 
    <asp:GridView ID="AdminLandmarksGridView"
        AutoGenerateColumns="False" EmptyDataText="No data" AllowPaging="True"
        DataKeyNames="id" runat="server" 
        OnSelectedIndexChanged="AdminLandmarksGridView_SelectedIndexChanged" AutoGenerateSelectButton="true">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" InsertVisible="false" ReadOnly="true" SortExpression="id"/> 
            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name"/> 
            <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type"/> 
            <asp:BoundField DataField="pricerange" HeaderText="Price" SortExpression="pricerange"/> 
            <asp:BoundField DataField="location_id" HeaderText="Location_id" SortExpression="location_id"/> 
            <asp:BoundField DataField="adress" HeaderText="Address" SortExpression="adress"/> 
            <asp:BoundField DataField="websiteLink" HeaderText="WebsiteLink" SortExpression="websiteLink" />
        </Columns>
    </asp:GridView>

    <asp:Button ID="ButtonNewLandmark" Text="New Landmark" runat="server" OnClick="NewLandmark" />

    <!-- This panel is hidden until the user chooses to modify an entry -->
    <asp:Panel ID="PanelLandmarkValues" Visible="false" runat="server">
        <asp:Label Text="Name" runat="server" />
        <asp:TextBox ID="TextBoxLandmarkName" runat="server" />
        <br />

        <asp:Label Text="Type" runat="server" />
        <asp:TextBox ID="TextBoxLandmarkType" runat="server" />
        <br />

        <asp:Label Text="Location" runat="server" />
        <asp:DropDownList ID="DropDownListLocations" runat="server" AppandDataBoundItems="True">
            <asp:ListItem Value="--Choose Location--" Text="--Choose Location--" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        <br />

        <asp:Label Text="Adress" runat="server" />
        <asp:TextBox ID="TextBoxLandmarkAdress" runat="server" />
        <br />

        <asp:Label Text="Price" runat="server" />
        <asp:TextBox ID="TextBoxLandmarkPrice" runat="server" />
        <br />

        <asp:Label Text="Link" runat="server" />
        <asp:TextBox ID="TextBoxLandmarkLink" runat="server" />
        <br />

        <!-- Buttons for modifying the table-->
        <asp:Button ID="ButtonCreateLandmark" Text="Create Landmark" Visible="false" runat="server" OnClick="CreateLandmark" />
        <asp:Button ID="ButtonUpdateLandmark" Text="Update Landmark" Visible="false" runat="server" OnClick="UpdateLandmark" />
        <asp:Button ID="ButtonDeleteLandmark" Text="Delete Landmark" Visible="false" runat="server" OnClick="DeleteLandmark"/>
        <asp:Button ID="ButtonCancel" Text="Cancel" Visible="false" runat="server" OnClick="CancelChanges"/>
    </asp:Panel>

</asp:Content>
