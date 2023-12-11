<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="AdminAccomodation.aspx.cs" Inherits="TravelAgencyWeb.Admin_Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Manage Accomodations</h1>

   <!-- Displays all accomodations from the database--> 
    <asp:GridView ID="AdminAccomodationsGridView" 
        AutoGenerateColumns="False" EmptyDataText="No data" AllowPaging="True"
        DataKeyNames="id" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="AdminAccomodationsGridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="false" ReadOnly="true" SortExpression="id"/> 
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name"/> 
            <asp:BoundField DataField="adress" HeaderText="adress" SortExpression="adress" />
            <asp:BoundField DataField="numberofrooms" HeaderText="numberofrooms" SortExpression="numberofrooms" />
            <asp:BoundField DataField="area" HeaderText="area" SortExpression="area" />
            <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
            <asp:CheckBoxField DataField="hasswimmingpool" HeaderText="hasswimmingpool" SortExpression="hasswimmingpool" />
            <asp:CheckBoxField DataField="hasgym" HeaderText="hasgym" SortExpression="hasgym" />
            <asp:CheckBoxField DataField="hasparking" HeaderText="hasparking" SortExpression="hasparking" />
            <asp:CheckBoxField DataField="ispetfriendly" HeaderText="ispetfriendly" SortExpression="ispetfriendly" />
            <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
            <asp:BoundField DataField="category_id" HeaderText="category_id" SortExpression="category_id" />
            <asp:BoundField DataField="location_id" HeaderText="location_id" SortExpression="location_id" />
            <asp:BoundField DataField="seasonal_price_id" HeaderText="seasonal_price_id" SortExpression="seasonal_price_id" />
        </Columns>
    </asp:GridView>


    <asp:Button ID="ButtonNewAccomodation" Text="New Accomodation" runat="server" OnClick="NewAccomodation" />

    <!-- This panel is hidden until the user chooses to modify an entry -->
    <asp:Panel ID="PanelAccomodationValues" Visible="false" runat="server">
        <asp:Label Text="Name*" runat="server" />
        <asp:TextBox ID="TextBoxAccomodationName" runat="server" />
        <asp:RequiredFieldValidator ErrorMessage="Name is required" ControlToValidate="TextBoxAccomodationName" runat="server" />
        <br />

        <asp:Label Text="Adress" runat="server" />
        <asp:TextBox ID="TextBoxAccomodationAdress" runat="server" />
        <br />

        <asp:Label Text="Rooms" runat="server" />
        <asp:TextBox ID="TextBoxAccomodationRooms" runat="server" />
        <br />

        <asp:Label Text="Area" runat="server" />
        <asp:TextBox ID="TextBoxAccomodationArea" runat="server" />
        <br />

        <asp:Label Text="Price*" runat="server" />
        <asp:TextBox ID="TextBoxAccomodationPrice" runat="server" />
        <asp:RequiredFieldValidator ErrorMessage="Price is required" ControlToValidate="TextBoxAccomodationPrice" runat="server" />
        <br />

        <asp:Label Text="Swimming Pool" runat="server" />
        <asp:DropDownList ID="DropDownListAccomodationSwimmingPool" runat="server">
            <asp:ListItem Text="True"/>
            <asp:ListItem Text="False"/>
        </asp:DropDownList>
        <br />

        <asp:Label Text="Gym" runat="server" />
        <asp:DropDownList ID="DropDownAccomodationListGym" runat="server">
            <asp:ListItem Text="True"/>
            <asp:ListItem Text="False"/>
        </asp:DropDownList>
        <br />

        <asp:Label Text="Parking" runat="server" />
        <asp:DropDownList ID="DropDownAccomodationListParking" runat="server">
            <asp:ListItem Text="True"/>
            <asp:ListItem Text="False"/>
        </asp:DropDownList>
        <br />

        <asp:Label Text="Pets" runat="server" />
        <asp:DropDownList ID="DropDownAccomodationListPets" runat="server">
            <asp:ListItem Text="True"/>
            <asp:ListItem Text="False"/>
        </asp:DropDownList>
        <br />

        <asp:Label Text="Description" runat="server" />
        <asp:TextBox ID="TextBoxAccomodationDescription" runat="server" />
        <br />

        <asp:Label Text="Category" runat="server" />
        <asp:DropDownList ID="DropDownListCategories" runat="server" AppandDataBoundItems="True">
            <asp:ListItem Value="--Choose Category--" Text="--Choose Category--" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        <br />

        <asp:Label Text="Location" runat="server" />
        <asp:DropDownList ID="DropDownListLocations" runat="server" AppandDataBoundItems="True">
            <asp:ListItem Value="--Choose Location--" Text="--Choose Location--" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        <br />

        <asp:Label Text="SeasonalPrice" runat="server" />
        <asp:DropDownList ID="DropDownListSeasonalPrices" runat="server" AppandDataBoundItems="True">
            <asp:ListItem Value="--Choose Seasonal Price--" Text="--Choose Seasonal Price--" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        <br />

        <!-- Buttons for modifying the table-->
        <asp:Button ID="ButtonCreateAccomodation" Text="Create Accomodation" Visible="false" runat="server" OnClick="CreateAccomodation" />
        <asp:Button ID="ButtonUpdateAccomodation" Text="Update Accomodation" Visible="false" runat="server" OnClick="UpdateAccomodation" />
        <asp:Button ID="ButtonDeleteAccomodation" Text="Delete Accomodation" Visible="false" runat="server" OnClick="DeleteAccomodation"/>
        <asp:Button ID="ButtonCancel" Text="Cancel" Visible="false" runat="server" OnClick="CancelChanges"/>

    </asp:Panel>
</asp:Content>
