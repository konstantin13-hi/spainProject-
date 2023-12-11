<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="Landmark.aspx.cs" Inherits="TravelAgencyWeb.Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Landmarks</h1>

    <p>Choose city: </p>
    <asp:DropDownList ID="DropDownList_Landmark" runat="server" Width="200px"></asp:DropDownList> 
    <asp:Button ID="Button_seeLandmarks" runat="server" Text="See landmarks" OnClick="Button1_Click" BorderWidth="1px" />
    <asp:Button ID="Button_addLandmarks" runat="server" Text="Add landmark" OnClick="Button2_Click" BorderWidth="1px" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" CellPadding="2" GridLines="Horizontal" HorizontalAlign="Center" Width="90%" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
            <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
            <asp:BoundField DataField="pricerange" HeaderText="Price (€)" SortExpression="pricerange" />
            <asp:BoundField DataField="adress" HeaderText="Address" SortExpression="adress" />
            <asp:Hyperlinkfield HeaderText="Website"
                      dataTextField="websiteLink"
                      datanavigateurlfields="websiteLink" 
                      dataNavigateUrlFormatString="{0}"
                      Target="_blank"/>
        </Columns>
        <RowStyle HorizontalAlign="Center" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbconnection %>" SelectCommand="SELECT [name], [type], [pricerange], [adress], [websiteLink] FROM [landmarks] WHERE ([location_id] = @location_id)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList_Landmark" Name="location_id" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Table ID="LandmarkTable" runat="server"></asp:Table>


</asp:Content>
