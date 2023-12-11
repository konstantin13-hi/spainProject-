<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="UserReview.aspx.cs" Inherits="TravelAgencyWeb.Pages.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>My reviews</h2>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>  
                <asp:BoundField DataField="id" HeaderText="Id" Visible="true" />
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="accomodation_id" HeaderText="ID" Visible="false"/> 
                <asp:BoundField DataField="pointsAvg" HeaderText="Average" DataFormatString="{0:f1}" />  
                <asp:BoundField DataField="pointsOverall" HeaderText="Overall" />  
                <asp:BoundField DataField="pointsArea" HeaderText="Area" />  
                <asp:BoundField DataField="pointsTidiness" HeaderText="Cleanliness" />  
                <asp:BoundField DataField="reviewText" HeaderText="Review text" />  
            </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="MessageLabel" runat="server" Text="Label" Visible="false" ForeColor="Red"></asp:Label>
    <asp:Panel ID="PanelReviews" Visible="false" runat="server" Width="400px">
        Accommodation name: <asp:Label ID="name" runat="server" Height="22px" />
        <br />


        Average points: <asp:Label ID="avgPoints" runat="server" DataFormatString="{0:f1}" Height="22px" />
        <br />

        <asp:Label Text="Overall points " runat="server" />
        <asp:TextBox ID="Overall_points" runat="server" />
        <br />

        <asp:Label Text="Area points " runat="server" />
        <asp:TextBox ID="Area_points" runat="server" />
        <br />

        <asp:Label Text="Cleanliness points " runat="server" />
        <asp:TextBox ID="Tidiness_points" runat="server" />
        <br />

        <asp:Label Text="Written review " runat="server" />
        <br />
        <asp:TextBox ID="WrittenReview" runat="server" Height="100px" Width="390px" />
        <br />

        <asp:Button ID="update_button" runat="server" Text="Update" OnClick="update" />
        <asp:Button ID="delete_button" runat="server" Text="Delete" OnClick="delete" />
        
    </asp:Panel>
    <br />
    <asp:Button runat="server" Text="Give review" OnClick="give_review" />
    
</asp:Content>
