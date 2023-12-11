<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserTravelNotes.aspx.cs" Inherits="TravelAgencyWeb.Pages.UserTravelNotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <%@ Import Namespace="library" %>
        <br />
        <br />
        <h1>Your personell travel notes </h1>
        <br />
        ID:
        <asp:TextBox ID="TravelNoteID" runat="server" Enabled="true" ReadOnly="true"></asp:TextBox>
        <br />
        Creation Date:
        <asp:TextBox ID="CreationDate" runat="server" Enabled="true" ReadOnly="true"></asp:TextBox>
        <br />
        Modification Date:
        <asp:TextBox ID="ModificationDate" runat="server" Enabled="true" ReadOnly="true"></asp:TextBox>
        <br />
        Title:
        <asp:TextBox ID="NoteTitle" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNoteTitle" runat="server" ControlToValidate ="NoteTitle" ErrorMessage="Title for this travelnote is required!"></asp:RequiredFieldValidator>
        <br />
        Booking:
        <asp:DropDownList ID="BookingList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BookingList_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        Accomodation:
        <asp:TextBox ID="Accomodation" runat="server" Enabled="true" ReadOnly="true"></asp:TextBox>
        <br />
        Notes about the accomodation:
        <br />
        <asp:TextBox ID="AccomodationNote" runat="server" TextMode="MultiLine" Rows="5" Columns="50"></asp:TextBox>
        <br />
        Location:
        <asp:TextBox ID="Location" runat="server" Enabled="true" ReadOnly="true"></asp:TextBox>
        <br />
        Notes about the location:
        <br />
        <asp:TextBox ID="LocationNote" runat="server" TextMode="MultiLine" Rows="5" Columns="50"></asp:TextBox>
        <br />
        <br />
        Notes about memories and landmarks:
        <br />
        <asp:TextBox ID="GeneralNotes" runat="server" TextMode="MultiLine" Rows="5" Columns="50"></asp:TextBox>

        <br />
        <asp:Button ID="CreateNote" runat="server" Text="Create" OnClick="CreateNote_Click" />
        <asp:Button ID="UpdateNote" runat="server" Text="Update" OnClick="UpdateNote_Click" />
        <asp:Button ID="DeleteNote" runat="server" Text="Delete" OnClick="DeleteNote_Click" />
        <asp:Button ID="Clear" runat="server" Text="Clear" OnClick="Clear_Click"/>
        <br />
        <asp:Label ID="feedback" runat="server"></asp:Label>
        <br />
        <br />
        <asp:GridView SelectionMode="Single" AutoGenerateSelectButton="True" ID="TravelNotesGridView" runat="server" Width="170px" OnSelectedIndexChanged="TravelNotesGridView_SelectedIndexChanged" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:TemplateField HeaderText="Booking">
                    <ItemTemplate>
                        <%# ((TravelNoteEN)Container.DataItem).Booking.BookingID.ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Check-in-Date">
                    <ItemTemplate>
                        <%# ((TravelNoteEN)Container.DataItem).Booking.CheckInDate.ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Check-out-Date">
                    <ItemTemplate>
                        <%# ((TravelNoteEN)Container.DataItem).Booking.CheckOutDate.ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Accomodation">
                    <ItemTemplate>
                        <%# ((TravelNoteEN)Container.DataItem).Accomodation.Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location">
                    <ItemTemplate>
                        <%# ((TravelNoteEN)Container.DataItem).TravelLocation.Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" SortExpression="CreationDate" />
                <asp:BoundField DataField="ModificationDate" HeaderText="Modification Date" SortExpression="ModificationDate" />
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
        <asp:Label ID="NoTravelNotesLoaded" runat="server"></asp:Label>

        <br />
        <br />
    </asp:Content>
