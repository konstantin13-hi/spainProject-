<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Normal.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="TravelAgencyWeb.Pages.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Give new review</h2>

    <br />
    <asp:Label ID="Cue_text" runat="server" Text="Choose accommodation you want to review: "></asp:Label>
    <asp:RadioButtonList ID="AccommodationList" runat="server"></asp:RadioButtonList>
    
    <br />
    <asp:Label ID="Label_name" runat="server" ForeColor="#000099" Visible="False"></asp:Label>
    <p>How was your stay overall? (1-10)</p> 
    <asp:TextBox ID="TextBox_overall" runat="server" CausesValidation="True" Width="40px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="PointsReguired" runat="server" ControlToValidate="TextBox_overall" 
        ErrorMessage="Points are reguired" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" ControlToValidate="TextBox_overall" runat="server" Type="Integer"
        MinimumValue="1" MaximumValue="10" ErrorMessage="Points must be between 1-10" Display="Dynamic" ForeColor="Red"></asp:RangeValidator>
    
    <p>How was the area around accommodation? (1-10) </p>
    <asp:TextBox ID="TextBox_area" runat="server" Width="40px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_area" 
        ErrorMessage="Points are reguired" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="TextBox_area" runat="server" Type="Integer"
        MinimumValue="1" MaximumValue="10" ErrorMessage="Points must be between 1-10" Display="Dynamic" ForeColor="Red"></asp:RangeValidator>
    
    <p>How was the cleanliness of the accommodation? (1-10) </p>
    <asp:TextBox ID="TextBox_tidiness" runat="server" Width="40px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_tidiness" 
        ErrorMessage="Points are reguired" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator3" ControlToValidate="TextBox_tidiness" runat="server" Type="Integer"
        MinimumValue="1" MaximumValue="10" ErrorMessage="Points must be between 1-10" Display="Dynamic" ForeColor="Red"></asp:RangeValidator>

    <p>Write your review. What was the best thing in the accommodation? Was there something that could have been better? </p>
    <asp:TextBox ID="TextBox_writtenReview" runat="server" Height="100px" Width="90%" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Give review" OnClick="Button1_Click" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Green"></asp:Label>
    <br />
    <br />
    <asp:Button ID="SeeRevs" runat="server" Text="See reviews" Visible="false" OnClick="SeeRevs_Click"/>
</asp:Content>
